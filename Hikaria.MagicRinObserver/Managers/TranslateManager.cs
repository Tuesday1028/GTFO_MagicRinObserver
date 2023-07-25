using Agents;
using GameData;
using Hikaria.MagicRinObserver.Utils;

namespace Hikaria.MagicRinObserver.Managers
{
    internal static class TranslateManager
    {
        public static string EnemyName(uint id)
        {
            if (EnemyID2Name.TryGetValue(id, out string name))
            {
                return name;
            }
            Logs.LogMessage(string.Format("Unknown Enemy ID: {0}, Name: {1}", id, EnemyDataBlock.GetBlock(id).name));
            return EnemyDataBlock.GetBlock(id).name;
        }

        private static readonly Dictionary<uint, string> EnemyID2Name = new();

        [Serializable]
        public struct EnemyNameID
        {
            public List<uint> IDs;

            public string Name;
        }

        public static void Init()
        {
            LoadFromDisk();
        }

        private static void LoadFromDisk()
        {
            EnemyID2Name.Clear();
            JsonConverter converter = new();
            converter.TryRead(CONFIG_ENEMYNAMEID, out List<EnemyNameID> enemyNameIDs);
            foreach (var item in enemyNameIDs)
            {
                foreach (var id in item.IDs)
                {
                    EnemyID2Name.Add(id, item.Name);
                }
            }
        }

        private static readonly string CONFIG_ENEMYNAMEID = string.Concat(BepInEx.Paths.ConfigPath, "\\MagicRinObserver\\EnemyNameIDs.json");
    }
}
