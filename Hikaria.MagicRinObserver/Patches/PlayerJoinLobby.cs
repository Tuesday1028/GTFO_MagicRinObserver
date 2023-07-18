using MagicRinObserver.Managers;
using MagicRinObserver.Utils;
using Player;
using SNetwork;
using System;
using UnityEngine;

namespace MagicRinObserver.Patches
{
    internal class PlayerJoinLobby : Patch
    {
        public override string Name { get; } = "PlayerJoinLobby";

        public static PlayerJoinLobby Instance { get; private set; }

        public override void Initialize()
        {
            Instance = this;
        }

        public override void Execute()
        {
            PatchMethod<GS_Lobby>(nameof(GS_Lobby.OnPlayerEvent), PatchType.Postfix);
            PatchMethod<LocalPlayerAgent>(nameof(LocalPlayerAgent.Setup), PatchType.Postfix);
        }

        private static void GS_Lobby__OnPlayerEvent__Postfix(SNet_Player player, SNet_PlayerEvent playerEvent)
        {
            if (!player.HasPlayerAgent)
            {
                return;
            }
            if (playerEvent == SNet_PlayerEvent.PlayerAgentSpawned)
            {
                if (player.IsLocal)
                {
                    UpdateChecker.CheckUpdate();
                    if (!SNet.IsMaster)
                    {
                        GameEventLogManager.AddLog(EntryPoint.Settings.Language.NOT_HOST);
                    }
                    else
                    {
                        GameEventLogManager.AddLog(EntryPoint.Settings.Language.IS_HOST);
                    }
                }
            }
            else if (playerEvent == SNet_PlayerEvent.PlayerLeftSessionHub)
            {
                if (player.IsMaster && !player.IsLocal)
                {
                    if (!SNet.IsMaster)
                    {
                        GameEventLogManager.AddLog(EntryPoint.Settings.Language.NOT_HOST);
                    }
                    else
                    {
                        GameEventLogManager.AddLog(EntryPoint.Settings.Language.IS_HOST);
                    }
                }
            }
        }

        private static void LocalPlayerAgent__Setup__Postfix()
        {
            InstallComponents();
        }

        private static void InstallComponents()
        {
            GameObject gameObject = GameObject.Find("MagicRinObserver");
            if (gameObject == null)
            {
                gameObject = new GameObject("MagicRinObserver");
                GameObject.DontDestroyOnLoad(gameObject);
            }
            if (gameObject.GetComponent<ChatManager>() == null)
            {
                gameObject.AddComponent<ChatManager>();
                Logs.LogDebug("已安装组件 ChatManager");
            }
            if (gameObject.GetComponent<GameEventLogManager>() == null)
            {
                gameObject.AddComponent<GameEventLogManager>();
                Logs.LogDebug("已安装组件 GameEventLogManager");
            }
        }
    }
}
