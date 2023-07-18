using Agents;
using Enemies;
using SNetwork;
using MagicRinObserver.Utils;
using Player;
using MagicRinObserver.Managers;

namespace MagicRinObserver.Patches
{
    internal class PlayerUnderAttack : Patch
    {
        public override string Name { get; } = "PlayerUnderAttack";

        public static PlayerUnderAttack Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<PouncerBehaviour>(nameof(PouncerBehaviour.RequestConsume), PatchType.Postfix);
            ChatCommand.RegisterCommand("pc", new Action<string[]>(delegate (string[] parameters)
            {
                bool enable = ChatCommand.StringToBool(parameters[0]);
                EntryPoint.Settings.PouncerConsumeEnable = enable;
                GameEventLogManager.AddLog(string.Format("<color=orange>[MagicRinObserver]</color> <color={0}>玩家被狗吞检测已{1}</color>", EntryPoint.Settings.PouncerConsumeEnable ? "green" : "red", EntryPoint.Settings.PouncerConsumeEnable ? "启用" : "禁用"));
            }), "[on|off], 启用|禁用 玩家被狗吞检测");
        }

        private static void PouncerBehaviour__RequestConsume__Postfix(int playerSlotIndex)
        {
            if (SNet.IsMaster && EntryPoint.Settings.PouncerConsumeEnable)
            {
                PlayerManager.TryGetPlayerAgent(ref playerSlotIndex, out PlayerAgent player);
                ChatManager.AddQueueInSeparate(string.Format(EntryPoint.Settings.PouncerConsumeText, player.Owner.NickName));
            }
        }

        private static readonly Dictionary<string, Thread> alerts = new();

        private static readonly Dictionary<string, Timer> alertsInstance = new();
    }
}