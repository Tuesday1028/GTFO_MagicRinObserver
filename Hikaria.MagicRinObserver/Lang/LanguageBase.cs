using System;

namespace Hikaria.MagicRinObserver.Lang
{
    public abstract class LanguageBase
    {
        public abstract string AVAILABLE_COMMANDS { get; }

        public abstract string COMMAND_LIST { get; }

        public abstract string COMMAND_HELP { get; }

        public abstract string COMMAND_WRONG_INPUT { get; }

        public abstract string SETTINGS_CHECK { get; }

        public abstract string CURRENT_VERSION { get; }

        public abstract string CHECKING_UPDATE { get; }

        public abstract string IS_LATEST_VERSION { get; }

        public abstract string CHECK_UPDATE_FAILD { get; }

        public abstract string DETECT_LATER_VERSION { get; }

        public abstract string LOADED { get; }

        public abstract string IGNORE_REPEAT_PATCH { get; }

        public abstract string NOT_HOST { get; }

        public abstract string IS_HOST { get; }

        public abstract string PATCHING { get; }

        public abstract string SELF { get; }

        public abstract string TOOL { get; }

        public abstract string WEAPON { get; }

        public abstract string ENABLED { get; }

        public abstract string DISABLED { get; }

        #region command check

        public abstract string DETECTION_ENEMYWAKEUP { get; }

        public abstract string DETECTION_PLAYERSLIP { get; }

        public abstract string DETECTION_FRIENDLYFIRE { get; }

        public abstract string DETECTION_GIVERESOURCE { get; }

        public abstract string DETECTION_POUNCERCONSUME { get; }

        public abstract string DETECTION_KILLTIMER { get; }

        public abstract string DETECTION_DOORSTATE { get; }

        #endregion
    }
}
