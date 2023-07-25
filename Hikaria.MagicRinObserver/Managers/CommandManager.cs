using Hikaria.MagicRinObserver.Patches;
using Hikaria.MagicRinObserver.Utils;

namespace Hikaria.MagicRinObserver.Managers
{
    internal sealed class CommandManager
    {
        public CommandManager(string prefix)
        {
            _prefix = prefix.ToLower();
            AddCommand("commands", new Action<string[]>(delegate (string[] parameters)
            {
                PrintAllCommandHelps();
            }), "列出所有可用指令");
            AddCommand("help", new Action<string[]>(delegate (string[] parameters)
            {
                PrintCommandHelp(parameters[0]);
            }), "查看指定指令帮助信息");
        }

        private void PrintAllCommandHelps()
        {
            GameEventLogManager.AddLog(EntryPoint.Settings.Language.AVAILABLE_COMMANDS);
            List<string> helps = new();
            foreach (var commandHelp in _commandHelps)
            {
                helps.Add(string.Format("{0} {1} {2}", _prefix, commandHelp.Key, commandHelp.Value));
            }
            GameEventLogManager.AddLog(helps.ToArray());
        }

        private void PrintCommandHelp(string key)
        {
            if (_commandHelps.TryGetValue(key, out string commandHelp))
            {
                GameEventLogManager.AddLog(string.Format(EntryPoint.Settings.Language.COMMAND_HELP, key));
                GameEventLogManager.AddLog(string.Format("{0} {1} {2}", _prefix, key, commandHelp));
            }
            else if (key != null)
            {
                GameEventLogManager.AddLog(EntryPoint.Settings.Language.COMMAND_WRONG_INPUT);
            }
        }

        public bool TryExcuteCommand(string key, string[] parameters)
        {
            try
            {
                key = key.ToLower();
                for (int i = 0; i< parameters.Length; i++)
                {
                    parameters[i] = parameters[i].ToLower();
                }
                if (_commands.TryGetValue(key, out Action<string[]> commandCallback))
                {
                    ExcuteCommand(commandCallback, parameters);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logs.LogError(ex.Message);
            }
            return false;
        }

        private void ExcuteCommand(Action<string[]> callback, string[] parameters)
        {
            callback(parameters);
        }

        public void AddCommand(string key, Action<string[]> callback, string commandHelp)
        {
            key = key.ToLower();
            _commands.Add(key, callback);
            _commandHelps.Add(key, commandHelp);
        }

        private readonly Dictionary<string, Action<string[]>> _commands = new();

        private readonly Dictionary<string, string> _commandHelps = new();

        private string _prefix = string.Empty;
    }
}
