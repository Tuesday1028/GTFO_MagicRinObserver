using System;

namespace MagicRinObserver.Lang
{
    internal class SimplifiedChinese : LanguageBase
    {
        public override string AVAILABLE_COMMANDS
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 可用命令如下:";
            }
        }

        public override string SETTINGS_CHECK
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 当前设置:";
            }
        }

        public override string COMMAND_LIST
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 输入 '/magicrin commonds' 可查看所有指令";
            }
        }

        public override string COMMAND_WRONG_INPUT
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=red>输入有误!</color>";
            }
        }

        public override string CURRENT_VERSION
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 当前版本: v{0}";
            }
        }

        public override string CHECKING_UPDATE
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 正在检查更新...";
            }
        }

        public override string IS_LATEST_VERSION
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=green>当前已是最新版本</color>";
            }
        }

        public override string CHECK_UPDATE_FAILD
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=red>检查更新失败!</color>";
            }
        }

        public override string DETECT_LATER_VERSION
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=green>发现新版本: </color><color=orange>v{0}</color>\\<color=orange>[MagicRinObserver]</color> 更新日志: \\{1}";
            }
        }

        public override string LOADED
        {
            get
            {
                return "MagicRinObserver 已加载";
            }
        }

        public override string IGNORE_REPEAT_PATCH
        {
            get
            {
                return "忽略重复的补丁: {0}";
            }
        }

        public override string PATCHING
        {
            get
            {
                return "正在应用补丁: {0}";
            }
        }

        public override string NOT_HOST
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=red>你当前不是房主，将无法使用本mod功能</color>";
            }
        }

        public override string IS_HOST
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> <color=green>你当前是房主，可以使用本mod功能</color>";
            }
        }

        public override string NOISE_NONE
        {
            get
            {
                return "间接";
            }
        }

        public override string NOISE_SILENT
        {
            get
            {
                return "手电光照";
            }
        }

        public override string NOISE_SNEAK
        {
            get
            {
                return "潜行";
            }
        }

        public override string NOISE_WALK
        {
            get
            {
                return "行走";
            }
        }

        public override string NOISE_RUN
        {
            get
            {
                return "跑动";
            }
        }

        public override string NOISE_JUMP
        {
            get
            {
                return "跳跃";
            }
        }

        public override string NOISE_MELEEHIT
        {
            get
            {
                return "近战";
            }
        }

        public override string NOISE_SHOOT
        {
            get
            {
                return "射击";
            }
        }

        public override string NOISE_DECOY
        {
            get
            {
                return "诱饵";
            }
        }

        public override string UNKNOWN
        {
            get
            {
                return "未知";
            }
        }

        public override string SELF
        {
            get
            {
                return "自己";
            }
        }

        public override string TOOL
        {
            get
            {
                return "工具";
            }
        }

        public override string WEAPON
        {
            get
            {
                return "武器";
            }
        }

        public override string NOISE_LOUDLANDING
        {
            get
            {
                return "落地";
            }
        }
    }
}
