using MagicRinObserver.Managers;
using MagicRinObserver.Utils;
using SNetwork;
using System;

namespace MagicRinObserver.Patches
{
    internal class ChatCommand : Patch
    {
        public override string Name { get; } = "ChatCommand";

        public static ChatCommand Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
            _commandManager = new CommandManager(_prefix);
        }

        public override void Execute()
        {
            PatchMethod<PlayerChatManager>(nameof(PlayerChatManager.PostMessage), PatchType.Prefix);
            RegisterCommand("check", new Action<string[]>(delegate (string[] parameters)
            {
                CheckSettings();
            }), "检查功能启用状态");
        }

        private static void PlayerChatManager__PostMessage__Prefix(PlayerChatManager __instance)
        {
            string text = __instance.m_currentValue;
            if (text.StartsWith(_prefix))
            {
                try
                {
                    List<string> parameters = text.Split(' ', StringSplitOptions.None).ToList();
                    parameters.RemoveAt(0);
                    string key = parameters[0];
                    parameters.RemoveAt(0);
                    if (!Instance._commandManager.TryExcuteCommand(key, parameters.ToArray()))
                    {
                        throw new Exception("输入有误");
                    }
                }
                catch (Exception ex)
                {
                    Logs.LogError(ex.Message);
                    Logs.LogError(string.Format("输入信息:" , text));
                    GameEventLogManager.AddLog(EntryPoint.Settings.Language.COMMAND_WRONG_INPUT);
                    GameEventLogManager.AddLog(EntryPoint.Settings.Language.COMMAND_LIST);
                }
                finally
                {
                    __instance.m_currentValue = "";
                }
            }
        }

        public static void RegisterCommand(string key, Action<string[]> callback, string commandHelp)
        {
            Instance._commandManager.AddCommand(key, callback, commandHelp);
        }

        private static void CheckSettings()
        {
            GameEventLogManager.AddLog(EntryPoint.Settings.Language.SETTINGS_CHECK);
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_ENEMYWAKEUP, " <color={0}>[{1}]</color>"), (EntryPoint.Settings.EnemyWakeupEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.EnemyWakeupEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_FRIENDLYFIRE, " <color={0}>[{1}]</color>"), (EntryPoint.Settings.FriendlyFireEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.FriendlyFireEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_PLAYERSLIP, " <color={0}>[{1}]</color>"), (EntryPoint.Settings.SlipEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.SlipEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_DOORSTATE, " <color={0}>[{1}]</color>"), (EntryPoint.Settings.DoorStateEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.DoorStateEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_GIVERESOURCE, "  <color={0}>[{1}]</color>"), (EntryPoint.Settings.GiveResourceEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.GiveResourceEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_POUNCERCONSUME, " <color={0}>[{1}]</color>"), (EntryPoint.Settings.PouncerConsumeEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.PouncerConsumeEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
            GameEventLogManager.AddLog(string.Format(string.Concat(EntryPoint.Settings.Language.DETECTION_KILLTIMER, " <color={0}>[{1}]</color>"), (EntryPoint.Settings.EnemyUnderAttackKillTimerEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.EnemyUnderAttackKillTimerEnable && SNet.IsMaster) ? EntryPoint.Settings.Language.ENABLED : EntryPoint.Settings.Language.DISABLED));
        }

        public static bool StringToBool(string param)
        {
            param = param.ToLower();
            bool flag;
            if (param == "on")
            {
                flag = true;
            }
            else
            {
                if (!(param == "off"))
                {
                    throw new Exception("非法输入");
                }
                flag = false;
            }
            return flag;
        }

        private const string _prefix = "/magicrin";

        private CommandManager _commandManager;
    }
}
