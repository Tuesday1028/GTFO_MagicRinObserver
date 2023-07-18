using System;

namespace MagicRinObserver.Lang
{
    internal class English : LanguageBase
    {
        public override string AVAILABLE_COMMANDS
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> Command list:";
            }
        }

        public override string SETTINGS_CHECK
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> Current settings:";
            }
        }

        public override string COMMAND_LIST
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> Enter '/magicrin commands' to list all commands";
            }
        }

        public override string COMMAND_WRONG_INPUT
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=red>Wrong input!</color>";
            }
        }

        public override string CURRENT_VERSION
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> Build: v{0}";
            }
        }

        public override string CHECKING_UPDATE
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> Checking for updates...";
            }
        }

        public override string IS_LATEST_VERSION
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=green>The current version is already the latest.</color>";
            }
        }

        public override string CHECK_UPDATE_FAILD
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=red>Check for updates failed.</color>";
            }
        }

        public override string DETECT_LATER_VERSION
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=green>New version: </color><color=orange>v{0}</color>\\<color=orange>[MagicRinObserver]</color> Changelog: \\{1}";
            }
        }

        public override string LOADED
        {
            get
            {
                return "MagicRinObserver Loaded";
            }
        }

        public override string IGNORE_REPEAT_PATCH
        {
            get
            {
                return "Ignoring duplicate patch: {0}";
            }
        }

        public override string PATCHING
        {
            get
            {
                return "Applying patch: {0}";
            }
        }

        public override string NOT_HOST
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=red>The mod will not function since you are not the host.</color>";
            }
        }

        public override string IS_HOST
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=green>The mod will function since you are the host.</color>";
            }
        }

        public override string NOISE_NONE
        {
            get
            {
                return "indirect";
            }
        }

        public override string NOISE_SILENT
        {
            get
            {
                return "silent";
            }
        }

        public override string NOISE_SNEAK
        {
            get
            {
                return "sneak";
            }
        }

        public override string NOISE_WALK
        {
            get
            {
                return "walk";
            }
        }

        public override string NOISE_RUN
        {
            get
            {
                return "run";
            }
        }

        public override string NOISE_JUMP
        {
            get
            {
                return "jump";
            }
        }

        public override string NOISE_MELEEHIT
        {
            get
            {
                return "melee hit";
            }
        }

        public override string NOISE_SHOOT
        {
            get
            {
                return "shoot";
            }
        }

        public override string NOISE_DECOY
        {
            get
            {
                return "decoy";
            }
        }

        public override string UNKNOWN
        {
            get
            {
                return "unknown";
            }
        }

        public override string SELF
        {
            get
            {
                return "self";
            }
        }

        public override string TOOL
        {
            get
            {
                return "ammo";
            }
        }

        public override string WEAPON
        {
            get
            {
                return "tool";
            }
        }

        public override string NOISE_LOUDLANDING
        {
            get
            {
                return "loud landing";
            }
        }
    }
}
