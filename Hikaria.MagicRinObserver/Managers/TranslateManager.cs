using Agents;
using GameData;
using MagicRinObserver.Utils;

namespace MagicRinObserver.Managers
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

        public static string NoiseName(Agent.NoiseType type)
        {
            switch (type)
            {
                case Agent.NoiseType.None:
                    return EntryPoint.Settings.Language.NOISE_NONE;
                case Agent.NoiseType.Silent:
                    return EntryPoint.Settings.Language.NOISE_SILENT;
                case Agent.NoiseType.Shoot:
                case Agent.NoiseType.ShootSilenced:
                    return EntryPoint.Settings.Language.NOISE_SHOOT;
                case Agent.NoiseType.Sneak:
                    return EntryPoint.Settings.Language.NOISE_SNEAK;
                case Agent.NoiseType.LoudLanding:
                    return EntryPoint.Settings.Language.NOISE_LOUDLANDING;
                case Agent.NoiseType.Walk:
                    return EntryPoint.Settings.Language.NOISE_WALK;
                case Agent.NoiseType.Run:
                    return EntryPoint.Settings.Language.NOISE_RUN;
                case Agent.NoiseType.Jump:
                    return EntryPoint.Settings.Language.NOISE_JUMP;
                case Agent.NoiseType.MeleeHit:
                    return EntryPoint.Settings.Language.NOISE_MELEEHIT;
                case Agent.NoiseType.Decoy:
                    return EntryPoint.Settings.Language.NOISE_DECOY;
            }
            return EntryPoint.Settings.Language.UNKNOWN;
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
