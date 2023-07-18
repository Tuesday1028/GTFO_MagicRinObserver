using Agents;
using Gear;
using LevelGeneration;
using MagicRinObserver.Managers;
using MagicRinObserver.Utils;
using Player;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MagicRinObserver.Patches
{
    internal class GiveResource : Patch
    {
        public override string Name { get; } = "GiveResource";

        public static GiveResource Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<Dam_PlayerDamageBase>(nameof(Dam_PlayerDamageBase.ReceiveAddHealth), PatchType.Prefix);
            PatchMethod<PlayerBackpackManager>(nameof(PlayerBackpackManager.ReceiveAmmoGive), PatchType.Prefix);
            PatchMethod<Dam_PlayerDamageBase>(nameof(Dam_PlayerDamageBase.ModifyInfection), PatchType.Prefix);
            PatchMethod<PlayerAgent>(nameof(PlayerAgent.GiveAmmoRel), PatchType.Prefix);
            PatchMethod<PlayerAgent>(nameof(PlayerAgent.GiveDisinfection), PatchType.Prefix);
            ChatCommand.RegisterCommand("gr", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.GiveResourceEnable = enable;
                GameEventLogManager.AddLog(string.Format("<color=orange>[MagicRinObserver]</color> <color={0}>资源使用检测已{1}</color>", EntryPoint.Settings.GiveResourceEnable ? "green" : "red", EntryPoint.Settings.GiveResourceEnable ? "启用" : "禁用"));
            }), "[on|off], 启用|禁用 资源使用检测");
        }

        private static void PlayerAgent__GiveAmmoRel__Prefix()
        {
            if (SNet.IsMaster && EntryPoint.Settings.GiveResourceEnable)
            {
                localGive = true;
            }
        }

        private static void PlayerAgent__GiveDisinfection__Prefix()
        {
            if (SNet.IsMaster && EntryPoint.Settings.GiveResourceEnable)
            {
                localGive = true;
            }
        }

        private static void Dam_PlayerDamageBase__ReceiveAddHealth__Prefix(Dam_PlayerDamageBase __instance, pAddHealthData data)
        {
            if (SNet.IsMaster && EntryPoint.Settings.GiveResourceEnable)
            {
                OnPreReceiveAddHealth(__instance, data);
            }
        }

        private static void Dam_PlayerDamageBase__ModifyInfection__Prefix(Dam_PlayerDamageBase __instance, pInfection data)
        {
            if (SNet.IsMaster && EntryPoint.Settings.GiveResourceEnable)
            {
                if (data.effect == pInfectionEffect.DisinfectionPack)
                {
                    OnPreReceiveModifyInfection(__instance, data);
                }
            }
        }

        private static void PlayerBackpackManager__ReceiveAmmoGive__Prefix(pAmmoGive data)
        {
            if (SNet.IsMaster && EntryPoint.Settings.GiveResourceEnable)
            {
                OnPreReceiveAmmoGive(data);
            }
        }

        private static void OnPreReceiveAddHealth(Dam_PlayerDamageBase __instance, pAddHealthData data)
        {
            SNet_Player fromPlayer = null;
            if (data.source.pRep.TryGetID(out IReplicator replicator))
            {
                fromPlayer = replicator.OwningPlayer;
            }
            SNet_Player toPlayer = __instance.Owner.Owner;
            float percent = data.health.Get(100f);
            if (percent < 20f)
            {
                return;
            }
            string threadName = fromPlayer.name + toPlayer.name + ResourcePackType.Medipack;
            if (!alerts.ContainsKey(threadName))
            {
                CreateThread(fromPlayer, toPlayer, ResourcePackType.Medipack, threadName, true);
                return;
            }
            if (alerts[threadName].IsAlive)
            {
                alertsInstance[threadName].add();
                return;
            }
            CreateThread(fromPlayer, toPlayer, ResourcePackType.Medipack, threadName, false);
        }

        private static void OnPreReceiveAmmoGive(pAmmoGive data)
        {
            SNet_Player fromPlayer;
            if (localGive)
            {
                fromPlayer = SNet.LocalPlayer;
            }
            else
            {
                SNet.Replication.TryGetLastSender(out fromPlayer);
            }
            localGive = false;
            data.targetPlayer.GetPlayer(out SNet_Player toPlayer);
            ResourcePackType type = data.ammoClassRel == 0f ? ResourcePackType.AmmoPack : ResourcePackType.ToolPack;
            string threadName = fromPlayer.name + toPlayer.name + type;
            if (!alerts.ContainsKey(threadName))
            {
                CreateThread(fromPlayer, toPlayer, type, threadName, true);
                return;
            }
            if (alerts[threadName].IsAlive)
            {
                alertsInstance[threadName].add();
                return;
            }
            CreateThread(fromPlayer, toPlayer, type, threadName, false);
        }

        private static void OnPreReceiveModifyInfection(Dam_PlayerDamageBase __instance, pInfection data)
        {
            SNet_Player fromPlayer;
            if (localGive)
            {
                fromPlayer = SNet.LocalPlayer;
            }
            else
            {
                SNet.Replication.TryGetLastSender(out fromPlayer);
            }
            localGive = false;
            SNet_Player toPlayer = __instance.Owner.Owner;
            string threadName = fromPlayer.name + toPlayer.name + ResourcePackType.DisinfectionPack;
            if (!alerts.ContainsKey(threadName))
            {
                CreateThread(fromPlayer, toPlayer, ResourcePackType.DisinfectionPack, threadName, true);
                return;
            }
            if (alerts[threadName].IsAlive)
            {
                alertsInstance[threadName].add();
                return;
            }
            CreateThread(fromPlayer, toPlayer, ResourcePackType.DisinfectionPack, threadName, false);
        }

        private static void CreateThread(SNet_Player fromPlayer, SNet_Player toPlayer, ResourcePackType resourcePackType, string threadName, bool add)
        {
            Timer timer = new();
            Thread thread = new(new ThreadStart(delegate
            {
                timer.Start(fromPlayer, toPlayer, resourcePackType);
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

        private static bool localGive = false;

        private static readonly Dictionary<string, Thread> alerts = new();

        private static readonly Dictionary<string, Timer> alertsInstance = new();

        public class Timer
        {
            public void add()
            {
                _num++;
                _sec = 5;
            }

            public void Start(SNet_Player fromPlayer, SNet_Player toPlayer, ResourcePackType resourcePackType)
            {
                while (_sec > 0)
                {
                    Thread.Sleep(1000);
                    _sec--;
                }
                string toPlayerName = toPlayer.NickName;
                if (fromPlayer.Lookup == toPlayer.Lookup)
                {
                    toPlayerName = EntryPoint.Settings.Language.SELF;
                }
                switch (resourcePackType)
                {
                    case ResourcePackType.Medipack:
                        ChatManager.AddQueue(string.Format(EntryPoint.Settings.GiveResourceHealthText, fromPlayer.NickName, toPlayerName, _num, toPlayer.NickName));
                        return;
                    case ResourcePackType.DisinfectionPack:
                        ChatManager.AddQueue(string.Format(EntryPoint.Settings.GiveResourceDisinfectionText, fromPlayer.NickName, toPlayerName, _num, toPlayer.NickName));
                        return;
                    case ResourcePackType.AmmoPack:
                        ChatManager.AddQueue(string.Format(EntryPoint.Settings.GiveResourceAmmoText, fromPlayer.NickName, toPlayerName, _num, EntryPoint.Settings.Language.WEAPON, toPlayer.NickName));
                        return;
                    case ResourcePackType.ToolPack:
                        ChatManager.AddQueue(string.Format(EntryPoint.Settings.GiveResourceAmmoText, fromPlayer.NickName, toPlayerName, _num, EntryPoint.Settings.Language.TOOL, toPlayer.NickName));
                        return;
                }
            }

            private int _sec = 5;

            private int _num = 1;
        }

        [Flags]
        public enum ResourcePackType
        {
            Medipack,
            AmmoPack,
            ToolPack,
            DisinfectionPack
        }
    }
}