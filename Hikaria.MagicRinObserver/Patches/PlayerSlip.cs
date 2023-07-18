using MagicRinObserver.Managers;
using MagicRinObserver.Utils;
using SNetwork;
using System;
using UnityEngine;

namespace MagicRinObserver.Patches
{
    internal class PlayerSlip : Patch
    {
        public override string Name { get; } = "PlayerSlip";

        public static PlayerSlip Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<Dam_PlayerDamageBase>("ReceiveFallDamage", PatchType.Postfix);
            ChatCommand.RegisterCommand("slip", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.SlipEnable = enable;
                GameEventLogManager.AddLog(string.Format("<color=orange>[MagicRinObserver]</color> <color={0}>玩家摔倒检测已{1}</color>", EntryPoint.Settings.SlipEnable ? "green" : "red", EntryPoint.Settings.SlipEnable ? "启用" : "禁用"));
            }), "[on|off], 启用|禁用 玩家摔倒检测");
        }

        private static void Dam_PlayerDamageLocal__ReceiveFallDamage__Postfix(Dam_PlayerDamageBase __instance, pMiniDamageData data)
        {
            if (SNet.IsMaster && EntryPoint.Settings.SlipEnable)
            {
                OnFallDamage(__instance, data);
            }
        }

        private static void Dam_PlayerDamageBase__ReceiveFallDamage__Postfix(Dam_PlayerDamageBase __instance, pMiniDamageData data)
        {
            if (SNet.IsMaster && EntryPoint.Settings.SlipEnable)
            {
                OnFallDamage(__instance, data);
            }
        }

        private static void OnFallDamage(Dam_PlayerDamageBase __instance, pMiniDamageData data)
        {
            ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.SlipText, __instance.Owner.Owner.NickName, (int)data.damage.Get(100f)));
        }
    }
}
