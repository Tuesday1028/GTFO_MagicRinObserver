using MagicRinObserver.Utils;
using UnityEngine.Networking;

namespace MagicRinObserver.Managers
{
    internal static class UpdateChecker
    {
        private static void DoCheckUpdate(UnityWebRequest request)
        {
            if (!isLogged)
            {
                GameEventLogManager.AddLog(string.Format(EntryPoint.Settings.Language.CURRENT_VERSION, PluginInfo.PLUGIN_VERSION));
                isLogged = true;
            }

            GameEventLogManager.AddLog(EntryPoint.Settings.Language.CHECKING_UPDATE);
            JsonConverter converter = new();
            Version latestVersion = converter.Deserialize<Version>(request.downloadHandler.GetText());
            if (latestVersion.internalVersion > DetectedLatestVersion)
            {
                GameEventLogManager.AddLog(string.Format(EntryPoint.Settings.Language.DETECT_LATER_VERSION, latestVersion.version, latestVersion.changeLog).Split('\\'));
                DetectedLatestVersion = latestVersion.internalVersion;
            }
            else
            {
                GameEventLogManager.AddLog(EntryPoint.Settings.Language.IS_LATEST_VERSION);
            }

            GameEventLogManager.AddLog(EntryPoint.Settings.Language.COMMAND_LIST);
        }

        public static void CheckUpdate()
        {
            HttpHelper.Get(CheckUpdateURL, new Action<UnityWebRequest>(delegate (UnityWebRequest request)
            {
                DoCheckUpdate(request);
            }), new Action<UnityWebRequest>(delegate (UnityWebRequest request)
            {
                Logs.LogError(string.Format("检查更新失败, 原因: {0}", request.error));
                GameEventLogManager.AddLog(EntryPoint.Settings.Language.CHECK_UPDATE_FAILD);
            }));
        }

        [Serializable]
        public struct Version
        {
            public int internalVersion;

            public string version;

            public string changeLog;
        }

        private static bool isLogged;

        private static int DetectedLatestVersion = PluginInfo.INTERNAL_VERSION;

        private const string CheckUpdateURL = "https://raw.githubusercontent.com/Hikaria0108/GTFO_MagicRinObserver/main/latestVersion.json";
    }
}
