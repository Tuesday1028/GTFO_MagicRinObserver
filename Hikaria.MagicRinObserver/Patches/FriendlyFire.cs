using Hikaria.MagicRinObserver.Managers;
using Hikaria.MagicRinObserver.Utils;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Hikaria.MagicRinObserver.Patches
{
    internal class FriendlyFire : Patch
    {
        public override string Name { get; } = "FriendlyFire";

        public static FriendlyFire Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<Dam_PlayerDamageBase>(nameof(Dam_PlayerDamageBase.ReceiveBulletDamage), PatchType.Postfix, null, null, null);
            PatchMethod<Dam_PlayerDamageBase>(nameof(Dam_PlayerDamageBase.ReceiveExplosionDamage), PatchType.Postfix, null, null, null);
            ChatCommand.RegisterCommand("ff", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.FriendlyFireEnable = enable;
                GameEventLogManager.AddLog(string.Format(string.Concat("<color=orange>[MagicRinObserver]</color> <color={0}>", EntryPoint.Settings.Language.DETECTION_FRIENDLYFIRE, " {1}</color>"), EntryPoint.Settings.FriendlyFireEnable ? "green" : "red", EntryPoint.Settings.FriendlyFireEnable ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            }), string.Concat("[on|off], ", EntryPoint.Settings.Language.ENABLED, "|", EntryPoint.Settings.Language.DISABLED, " ", EntryPoint.Settings.Language.DETECTION_FRIENDLYFIRE));
        }

        private static void CreateThread(SNet_Player fromPlayer, SNet_Player toPlayer, float damage, string threadName, bool add)
        {
            Timer timer = new(damage);
            Thread thread = new(new ThreadStart(delegate
            {
                timer.Start(fromPlayer, toPlayer);
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

        private static void OnBulletDamage(pBulletDamageData data, Dam_PlayerDamageBase __instance)
        {
            SNet_Player toPlayer = __instance.Owner.Owner;
            SNet_Player fromPlayer;
            data.source.pRep.TryGetID(out IReplicator replicator);
            fromPlayer = replicator.OwningPlayer;
            float damage = data.damage.Get(100f);
            string threadName = fromPlayer.name + toPlayer.name;
            if (!alerts.ContainsKey(threadName))
            {
                CreateThread(fromPlayer, toPlayer, damage, threadName, true);
                return;
            }
            if (alerts[threadName].IsAlive)
            {
                alertsInstance[threadName].add(damage);
                return;
            }
            CreateThread(fromPlayer, toPlayer, damage, threadName, false);
        }

        private static void Dam_PlayerDamageBase__ReceiveExplosionDamage__Postfix(pExplosionDamageData data, Dam_PlayerDamageBase __instance)
        {
            if (EntryPoint.Settings.FriendlyFireEnable && SNet.IsMaster)
            {
                ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.FriendlyFireExplosionText, __instance.Owner.Owner.NickName, (int)data.damage.Get(100f)));
            }
        }

        private static void Dam_PlayerDamageBase__ReceiveBulletDamage__Postfix(pBulletDamageData data, Dam_PlayerDamageBase __instance)
        {
            if (EntryPoint.Settings.FriendlyFireEnable && SNet.IsMaster)
            {
                OnBulletDamage(data, __instance);
            }
        }

        private static Dictionary<string, Thread> alerts = new();

        private static Dictionary<string, Timer> alertsInstance = new();

        public class Timer
        {
            public Timer(float damage)
            {
                _damage = damage;
            }

            public void Start(SNet_Player fromPlayer, SNet_Player toPlayer)
            {
                while (_sec > 0)
                {
                    Thread.Sleep(1000);
                    _sec--;
                }
                ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.FriendlyFireText, new object[] { fromPlayer.NickName, toPlayer.NickName, _num, (int)_damage }));
            }

            public void add(float damage)
            {
                _num++;
                _sec = 3;
                _damage += damage;
            }

            private int _sec = 3;

            private int _num = 1;

            private float _damage;
        }
    }
}
