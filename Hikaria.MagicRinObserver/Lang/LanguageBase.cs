using System;

namespace MagicRinObserver.Lang
{
    public abstract class LanguageBase
    {
        public abstract string AVAILABLE_COMMANDS { get; }

        public abstract string COMMAND_LIST { get; }

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

        public abstract string NOISE_NONE { get; }

        public abstract string NOISE_SILENT { get; }

        public abstract string NOISE_SNEAK { get; }

        public abstract string NOISE_WALK { get; }

        public abstract string NOISE_RUN { get; }

        public abstract string NOISE_JUMP { get; }

        public abstract string NOISE_MELEEHIT { get; }

        public abstract string NOISE_SHOOT { get; }

        public abstract string NOISE_DECOY { get; }

        public abstract string UNKNOWN { get; }

        public abstract string SELF { get; }

        public abstract string TOOL { get; }

        public abstract string WEAPON { get; }

        public abstract string NOISE_LOUDLANDING { get; }
    }
}
