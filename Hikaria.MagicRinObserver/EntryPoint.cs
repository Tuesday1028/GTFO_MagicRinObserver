using BepInEx;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;
using Hikaria.MagicRinObserver.Config;
using Hikaria.MagicRinObserver.Managers;
using Hikaria.MagicRinObserver.Patches;
using Hikaria.MagicRinObserver.Utils;

namespace Hikaria.MagicRinObserver
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class EntryPoint : BasePlugin
    {
        public override void Load()
        {
            Instance = this;
            Init();
            RegisterTypesInIl2Cpp();
            RegisterPatchesInHarmony();
            Logs.LogMessage(Settings.Language.LOADED);
        }

        private void RegisterTypesInIl2Cpp()
        {
            ClassInjector.RegisterTypeInIl2Cpp<ChatManager>();
            ClassInjector.RegisterTypeInIl2Cpp<GameEventLogManager>();
        }

        private void RegisterPatchesInHarmony()
        {
            Patch.RegisterPatch<ChatCommand>();
            Patch.RegisterPatch<PlayerJoinLobby>();
            Patch.RegisterPatch<EnemyWakeup>();
            Patch.RegisterPatch<FriendlyFire>();
            Patch.RegisterPatch<PlayerSlip>();
            Patch.RegisterPatch<SecurityDoorState>();
            Patch.RegisterPatch<GiveResource>();
            Patch.RegisterPatch<PlayerUnderAttack>();
            Patch.RegisterPatch<EnemyUnderAttack>();
        }

        private void Init()
        {
            if (Instance._settings == null)
            {
                Instance._settings = new Settings();
            }
            Instance._settings.Setup();
            TranslateManager.Init();
        }

        public static EntryPoint Instance { get; private set; }

        private Settings _settings;

        public static Settings Settings
        {
            get
            {
                if (Instance._settings == null)
                {
                    Instance._settings = new Settings();
                    Instance._settings.Setup();
                }
                return Instance._settings;
            }
        }
    }
}
