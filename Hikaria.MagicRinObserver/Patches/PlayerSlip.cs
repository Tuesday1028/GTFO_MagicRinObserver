using Hikaria.MagicRinObserver.Managers;
using Hikaria.MagicRinObserver.Utils;
using SNetwork;
using System;
using UnityEngine;

namespace Hikaria.MagicRinObserver.Patches
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
                GameEventLogManager.AddLog(string.Format(string.Concat("<color=orange>[MagicRinObserver]</color> <color={0}>", EntryPoint.Settings.Language.DETECTION_PLAYERSLIP, " {1}</color>"), EntryPoint.Settings.SlipEnable ? "green" : "red", EntryPoint.Settings.SlipEnable ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            }), string.Concat("[on|off], ", EntryPoint.Settings.Language.ENABLED, "|", EntryPoint.Settings.Language.DISABLED, " ", EntryPoint.Settings.Language.DETECTION_PLAYERSLIP));
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
