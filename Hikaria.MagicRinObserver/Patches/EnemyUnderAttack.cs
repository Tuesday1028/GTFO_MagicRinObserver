using Enemies;
using MagicRinObserver.Managers;
using MagicRinObserver.Utils;
using SNetwork;
using System.Diagnostics;
using static MagicRinObserver.Managers.TranslateManager;

namespace MagicRinObserver.Patches
{
    internal class EnemyUnderAttack : Patch
    {
        public override string Name { get; } = "EnemyUnderAttack";

        public static EnemyUnderAttack Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
            Init();
        }

        public override void Execute()
        {
            PatchMethod<Dam_EnemyDamageBase>(nameof(Dam_EnemyDamageBase.ProcessReceivedDamage), PatchType.Postfix);
            ChatCommand.RegisterCommand("kt", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.EnemyUnderAttackKillTimerEnable = enable;
                GameEventLogManager.AddLog(string.Format("<color=orange>[MagicRinObserver]</color> <color={0}>击杀敌人计时已{1}</color>", EntryPoint.Settings.EnemyUnderAttackKillTimerEnable ? "green" : "red", EntryPoint.Settings.EnemyUnderAttackKillTimerEnable ? "启用" : "禁用"));
            }), "[on|off], 启用|禁用 击杀敌人计时");
        }

        private static void Dam_EnemyDamageBase__ProcessReceivedDamage__Postfix(Dam_EnemyDamageBase __instance)
        {
            if (SNet.IsMaster && EntryPoint.Settings.EnemyUnderAttackKillTimerEnable)
            {
                StartKillTimer(__instance);
            }
        }

        private static void Init()
        {
            killTimerEnemyNameIDs.Clear();
            JsonConverter converter = new();
            converter.TryRead(CONFIG_KILLTIMER_ENEMYNAMEID, out List<EnemyNameID> enemyNameIDs);
            foreach (var item in enemyNameIDs)
            {
                foreach (var id in item.IDs)
                {
                    killTimerEnemyNameIDs.Add(id, item.Name);
                }
            }
        }

        private static void StartKillTimer(Dam_EnemyDamageBase __instance)
        {
            EnemyAgent enemy = __instance.Owner;
            Stopwatch timer = new Stopwatch();
            if (killTimerEnemyNameIDs.ContainsKey(enemy.EnemyDataID))
            {
                if (!registeredKillTimer.ContainsKey(enemy.GetInstanceID()))
                {
                    registeredKillTimer.Add(enemy.GetInstanceID(), timer);
                    enemy.add_OnDeadCallback(new Action(delegate ()
                    {
                        if (registeredKillTimer.ContainsKey(enemy.GetInstanceID()))
                        {
                            timer.Stop();
                            registeredKillTimer.Remove(enemy.GetInstanceID());
                            ChatManager.AddQueue(string.Format(EntryPoint.Settings.EnemyUnderAttackKillTimerText, killTimerEnemyNameIDs[enemy.EnemyDataID], string.Format("{0:0.00}", timer.Elapsed.TotalSeconds)));
                        }
                    }));
                    timer.Start();
                }
            }
        }

        private static readonly Dictionary<int, Stopwatch> registeredKillTimer = new();

        private static readonly Dictionary<string, Thread> alerts = new();

        private static readonly Dictionary<string, Timer> alertsInstance = new();

        private static readonly Dictionary<uint, string> killTimerEnemyNameIDs = new();

        private static readonly string CONFIG_KILLTIMER_ENEMYNAMEID = string.Concat(BepInEx.Paths.ConfigPath, "\\MagicRinObserver\\KillTimerEnemyNameIDs.json");

        public class Timer
        {
            public void add()
            {
                
            }

            public void Start()
            {
                while (_sec > 0)
                {
                    Thread.Sleep(250);
                    _sec--;
                }
            }

            private int _sec = 10;

            private int _num = 1;
        }
    }
}