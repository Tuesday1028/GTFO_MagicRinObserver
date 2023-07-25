using LevelGeneration;
using Hikaria.MagicRinObserver.Managers;
using Hikaria.MagicRinObserver.Utils;
using SNetwork;
using System;

namespace Hikaria.MagicRinObserver.Patches
{
    internal class SecurityDoorState : Patch
    {
        public override string Name { get; } = "SecurityDoorState";

        public static SecurityDoorState Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<LG_Door_Sync>(nameof(LG_Door_Sync.AttemptDoorInteraction), PatchType.Prefix);
            PatchMethod<LG_Door_Sync>(nameof(LG_Door_Sync.AttemptInteract), PatchType.Prefix);
            ChatCommand.RegisterCommand("door", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.DoorStateEnable = enable;
                GameEventLogManager.AddLog(string.Format(string.Concat("<color=orange>[MagicRinObserver]</color> <color={0}>", EntryPoint.Settings.Language.DETECTION_DOORSTATE, " {1}</color>"), EntryPoint.Settings.DoorStateEnable ? "green" : "red", EntryPoint.Settings.DoorStateEnable ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            }), string.Concat("[on|off], ", EntryPoint.Settings.Language.ENABLED, "|", EntryPoint.Settings.Language.DISABLED, " ", EntryPoint.Settings.Language.DETECTION_DOORSTATE));
        }

        private static void OnDoorInteract(pDoorInteraction interaction, LG_SecurityDoor door)
        {
            SNet_Player fromPlayer;
            if (localInteract)
            {
                fromPlayer = SNet.LocalPlayer;
            }
            else
            {
                SNet.Replication.TryGetLastSender(out fromPlayer);
            }
            string zone = door.LinkedToZoneData.Alias.ToString();
            if (interaction.type == eDoorInteractionType.ActivateChainedPuzzle)
            {
                if (interaction.isPlayerIntertaction || localInteract || !string.IsNullOrEmpty(fromPlayer.NickName))
                {
                    ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.DoorStateSecurityDoorActivatedByPlayerText, fromPlayer.NickName, zone));
                }
                else
                {
                    ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.DoorStateSecurityDoorActivatedText, zone));
                }
            }
            else if (interaction.type == eDoorInteractionType.Open)
            {
                if (interaction.isPlayerIntertaction || localInteract || !string.IsNullOrEmpty(fromPlayer.NickName))
                {
                    ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.DoorStateSecurityDoorOpenedByPlayerText, fromPlayer.NickName, zone));
                }
                else
                {
                    ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.DoorStateSecurityDoorOpenedText, zone));
                }
            }
            else if (interaction.type == eDoorInteractionType.Close)
            {
                if (interaction.isPlayerIntertaction || localInteract || !string.IsNullOrEmpty(fromPlayer.NickName))
                {
                    ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.DoorStateSecurityDoorClosedByPlayerText, fromPlayer.NickName, zone));
                }
                else
                {
                    ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.DoorStateSecurityDoorClosedText, zone));
                }
            }
            localInteract = false;
        }

        private static void LG_Door_Sync__AttemptDoorInteraction__Prefix(LG_Door_Sync __instance)
        {
            if (SNet.IsMaster && EntryPoint.Settings.DoorStateEnable)
            {
                if (__instance.m_core.DoorType == eLG_DoorType.Security)
                {
                    localInteract = true;
                }
            }
        }

        private static void LG_Door_Sync__AttemptInteract__Prefix(LG_Door_Sync __instance, pDoorInteraction interaction)
        {
            if (SNet.IsMaster && EntryPoint.Settings.DoorStateEnable)
            {
                if (__instance.m_core.DoorType == eLG_DoorType.Security)
                {
                    OnDoorInteract(interaction, __instance.m_core.Cast<LG_SecurityDoor>());
                }
            }
        }

        private static bool localInteract;
    }
}
