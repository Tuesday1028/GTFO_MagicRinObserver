using Agents;
using Enemies;
using Hikaria.MagicRinObserver.Managers;
using Hikaria.MagicRinObserver.Utils;
using Player;
using SNetwork;

namespace Hikaria.MagicRinObserver.Patches
{
    internal class EnemyWakeup : Patch
    {
        public override string PatchName => "EnemyWakeup";

        public static EnemyWakeup Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<EnemyDetection>(nameof(EnemyDetection.UpdateHibernationDetection), PatchType.Postfix);
            PatchMethod<ES_ScoutScream>(nameof(ES_ScoutScream.ActivateState), PatchType.Postfix);
            ChatCommand.RegisterCommand("wakeup", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.EnemyWakeupEnable = enable;
                GameEventLogManager.AddLog(string.Format(string.Concat("<color=orange>[MagicRinObserver]</color> <color={0}>", EntryPoint.Settings.Language.DETECTION_ENEMYWAKEUP, " {1}</color>"), EntryPoint.Settings.EnemyWakeupEnable ? "green" : "red", EntryPoint.Settings.EnemyWakeupEnable ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            }), string.Concat("[on|off], ", EntryPoint.Settings.Language.ENABLED, "|", EntryPoint.Settings.Language.DISABLED, " ", EntryPoint.Settings.Language.DETECTION_ENEMYWAKEUP));
        }

        private static void EnemyDetection__UpdateHibernationDetection__Postfix(EnemyDetection __instance, AgentTarget target, bool __result)
        {
            if (SNet.IsMaster && EntryPoint.Settings.EnemyWakeupEnable)
            {
                if (__result)
                {
                    OnEnemyWakeup(__instance, target);
                }
            }
        }

        private static void ES_ScoutScream__ActivateState__Postfix(ES_ScoutScream __instance, AgentTarget agentTarget)
        {
            if (SNet.IsMaster && EntryPoint.Settings.EnemyWakeupEnable)
            {
                OnEnemyWakeup(__instance, agentTarget);
            }
        }

        private static void OnEnemyWakeup(EnemyDetection __instance, AgentTarget target)
        {
            EnemyAgent enemy = __instance.m_ai.m_enemyAgent;
            if (enemy.IsScout)
            {
                return;
            }
            SNet_Player player = target.m_agent.m_replicator.OwningPlayer;
            string enemyName = TranslateManager.EnemyName(enemy.EnemyDataID);
            string threadName = player.name + enemyName;
            if (!alerts.ContainsKey(threadName))
            {
                CreateThread(player, enemy, threadName, true);
                return;
            }
            if (alerts[threadName].IsAlive)
            {
                alertsInstance[threadName].add();
                return;
            }
            CreateThread(player, enemy, threadName, false);
        }

        private static void OnEnemyWakeup(ES_ScoutScream __instance, AgentTarget target)
        {
            EnemyAgent enemy = __instance.m_ai.m_enemyAgent;
            SNet_Player player = target.m_agent.m_replicator.OwningPlayer;
            string enemyName = TranslateManager.EnemyName(enemy.EnemyDataID);
            string threadName = player.name + enemyName;
            if (!alerts.ContainsKey(threadName))
            {
                CreateThread(player, enemy, threadName, true);
                return;
            }
            if (alerts[threadName].IsAlive)
            {
                alertsInstance[threadName].add();
                return;
            }
            CreateThread(player, enemy, threadName, false);
        }

        private static void CreateThread(SNet_Player player, EnemyAgent enemy, string threadName, bool add)
        {
            Timer timer = new();
            Thread thread = new(new ThreadStart(delegate
            {
                timer.Start(player, enemy);
            }));
            thread.Start();
            if (add)
            {
                alerts.Add(threadName, thread);
                alertsInstance.Add(threadName, timer);
                return;
            }
            alerts[threadName] = thread;
            alertsInstance[threadName] = timer;
        }

        private static readonly Dictionary<string, Thread> alerts = new();

        private static readonly Dictionary<string, Timer> alertsInstance = new();

        public class Timer
        {
            public void Start(SNet_Player player, EnemyAgent enemy)
            {
                while (_sec > 0)
                {
                    Thread.Sleep(1000);
                    _sec--;
                }
                ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.EnemyWakeupText, new object[] { player.NickName, _num, TranslateManager.EnemyName(enemy.EnemyDataID) }));
            }

            public void add()
            {
                _num++;
                _sec = 3;
            }

            private int _sec = 3;

            private int _num = 1;
        }
    }
}
