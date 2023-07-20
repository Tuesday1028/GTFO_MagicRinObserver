using Agents;
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
            PatchMethod<Dam_EnemyDamageBase>(nameof(Dam_EnemyDamageBase.ProcessReceivedDamage), PatchType.Prefix);
            ChatCommand.RegisterCommand("kt", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.EnemyUnderAttackKillTimerEnable = enable;
                GameEventLogManager.AddLog(string.Format(string.Concat("<color=orange>[MagicRinObserver]</color> <color={0}>", EntryPoint.Settings.Language.DETECTION_KILLTIMER, " {1}</color>"), EntryPoint.Settings.EnemyUnderAttackKillTimerEnable ? "green" : "red", EntryPoint.Settings.EnemyUnderAttackKillTimerEnable ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            }), string.Concat("[on|off], ", EntryPoint.Settings.Language.ENABLED, "|", EntryPoint.Settings.Language.DISABLED, " ",EntryPoint.Settings.Language.DETECTION_KILLTIMER));
        }

        private static void Dam_EnemyDamageBase__ProcessReceivedDamage__Prefix(Dam_EnemyDamageBase __instance, Agent damageSource, float damage)
        {
            if (SNet.IsMaster && EntryPoint.Settings.EnemyUnderAttackKillTimerEnable)
            {
                StartKillTimer(__instance, damageSource, damage);
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

        private static void StartKillTimer(Dam_EnemyDamageBase __instance, Agent damageSource, float damage)
        {
            EnemyAgent enemy = __instance.Owner;
            damage = damage <= enemy.Damage.Health ? damage : enemy.Damage.Health;
            int slot = damageSource.m_replicator.OwningPlayer.PlayerSlotIndex();
            if (!registeredKillTimerDamageStat.ContainsKey(enemy.GetInstanceID()))
            {
                registeredKillTimerDamageStat.Add(enemy.GetInstanceID(), new Dictionary<int, float>());
            }
            if (!registeredKillTimerDamageStat[enemy.GetInstanceID()].ContainsKey(slot))
            {
                registeredKillTimerDamageStat[enemy.GetInstanceID()].Add(slot, damage);
            }
            else
            {
                registeredKillTimerDamageStat[enemy.GetInstanceID()][slot] += damage;
            }
            if (killTimerEnemyNameIDs.ContainsKey(enemy.EnemyDataID))
            {
                if (!registeredKillTimer.ContainsKey(enemy.GetInstanceID()))
                {
                    Stopwatch timer = new();
                    int instanceID = enemy.GetInstanceID();
                    registeredKillTimer.Add(instanceID, timer);
                    timer.Start();
                    enemy.add_OnDeadCallback(new Action(delegate ()
                    {
                        if (registeredKillTimer.ContainsKey(instanceID))
                        {
                            timer.Stop();

                            float red = registeredKillTimerDamageStat[instanceID].ContainsKey(0) ? registeredKillTimerDamageStat[instanceID][0] : 0f;
                            float green = registeredKillTimerDamageStat[instanceID].ContainsKey(1) ? registeredKillTimerDamageStat[instanceID][1] : 0f;
                            float blue = registeredKillTimerDamageStat[instanceID].ContainsKey(2) ? registeredKillTimerDamageStat[instanceID][2] : 0f;
                            float purple = registeredKillTimerDamageStat[instanceID].ContainsKey(3) ? registeredKillTimerDamageStat[instanceID][3] : 0f;
                            float healthMax = enemy.Damage.HealthMax;

                            ChatManager.AddQueue(string.Format(EntryPoint.Settings.EnemyUnderAttackKillTimerText, killTimerEnemyNameIDs[enemy.EnemyDataID], string.Format("{0:0.00}", timer.Elapsed.TotalSeconds)));
                            ChatManager.AddQueue(string.Format("red: {0}({1}%), green: {2}({3}%)", new object[] { (int)Math.Round(red), (int)Math.Round(red / healthMax * 100f), (int)Math.Round(green), (int)Math.Round(green / healthMax * 100f) }));
                            ChatManager.AddQueue(string.Format("blue: {0}({1}%), purple: {2}({3}%)", new object[] { (int)Math.Round(blue), (int)Math.Round(blue / healthMax * 100f), (int)Math.Round(purple), (int)Math.Round(purple / healthMax * 100f) }));
                            
                            registeredKillTimer.Remove(instanceID);
                            registeredKillTimerDamageStat.Remove(instanceID);
                        }
                    }));
                }
            }
        }

        private static readonly Dictionary<int, Stopwatch> registeredKillTimer = new();

        private static readonly Dictionary<int, Dictionary<int, float>> registeredKillTimerDamageStat = new();

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