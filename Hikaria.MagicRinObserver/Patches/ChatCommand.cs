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
            PatchMethod<PlayerChatManager>("PostMessage", PatchType.Prefix);
            RegisterCommand("check", new Action<string[]>(delegate (string[] parameters)
            {
                CheckSettings();
            }), "检查功能启用状态");
        }

        private static void PlayerChatManager__PostMessage__Prefix(PlayerChatManager __instance)
        {
            string text = __instance.m_currentValue.ToLower();
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
            GameEventLogManager.AddLog(string.Format("敌人惊醒检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.EnemyWakeupEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.EnemyWakeupEnable && SNet.IsMaster) ? "启用" : "禁用"));
            GameEventLogManager.AddLog(string.Format("队友伤害检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.FriendlyFireEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.FriendlyFireEnable && SNet.IsMaster) ? "启用" : "禁用"));
            GameEventLogManager.AddLog(string.Format("玩家摔倒检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.SlipEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.SlipEnable && SNet.IsMaster) ? "启用" : "禁用"));
            GameEventLogManager.AddLog(string.Format("安全门状态检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.DoorStateEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.DoorStateEnable && SNet.IsMaster) ? "启用" : "禁用"));
            GameEventLogManager.AddLog(string.Format("资源使用检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.GiveResourceEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.GiveResourceEnable && SNet.IsMaster) ? "启用" : "禁用"));
            GameEventLogManager.AddLog(string.Format("狗吞玩家检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.PouncerConsumeEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.PouncerConsumeEnable && SNet.IsMaster) ? "启用" : "禁用"));
            GameEventLogManager.AddLog(string.Format("击杀敌人计时 <color={0}>[{1}]</color>", (EntryPoint.Settings.EnemyUnderAttackKillTimerEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.EnemyUnderAttackKillTimerEnable && SNet.IsMaster) ? "启用" : "禁用"));
            //GameEventLogManager.AddLog(string.Format("秒杀敌人检测 <color={0}>[{1}]</color>", (EntryPoint.Settings.EnemyUnderAttackInstantKillEnable && SNet.IsMaster) ? "green" : "red", (EntryPoint.Settings.EnemyUnderAttackInstantKillEnable && SNet.IsMaster) ? "启用" : "禁用"));
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
