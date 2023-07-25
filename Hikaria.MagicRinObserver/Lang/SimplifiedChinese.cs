using System;

namespace Hikaria.MagicRinObserver.Lang
{
    internal class SimplifiedChinese : LanguageBase
    {
        public override string AVAILABLE_COMMANDS
        {
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 可用指令如下:";
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

        public override string COMMAND_HELP
        { 
            get
            {
                return "<color=orange>[MagicRinObserver]</color> 指令 {0} 帮助信息:";
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

        public override string ENABLED
        {
            get
            {
                return "启用";
            }
        }

        public override string DISABLED
        {
            get
            {
                return "禁用";
            }
        }

        public override string DETECTION_ENEMYWAKEUP
        {
            get
            {
                return "敌人惊醒检测";
            }
        }

        public override string DETECTION_PLAYERSLIP
        {
            get
            {
                return "玩家跌落伤害检测";
            }
        }

        public override string DETECTION_FRIENDLYFIRE
        {
            get
            {
                return "队友伤害检测";
            }
        }

        public override string DETECTION_GIVERESOURCE
        {
            get
            {
                return "资源使用检测";
            }
        }

        public override string DETECTION_POUNCERCONSUME
        {
            get
            {
                return "狗吞玩家检测";
            }
        }

        public override string DETECTION_KILLTIMER
        {
            get
            {
                return "敌人击杀计时";
            }
        }

        public override string DETECTION_DOORSTATE
        {
            get
            {
                return "安全门状态检测";
            }
        }
    }
}
