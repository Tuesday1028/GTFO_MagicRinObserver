using System;

namespace Hikaria.MagicRinObserver.Config
{
    internal class ConfigDescription
    {
        internal static string FRIENDLY_FIRE_DESC
        {
            get
            {
                return "默认[开启|关闭]队友伤害提示功能，接受的值：[true|false]";
            }
        }

        internal static string LANGUAGE_DESC
        {
            get
            {
                return "切换mod显示语言，接受的值：[en_US|zh_CN]";
            }
        }

        internal static string SETTINGS_COMMON
        {
            get
            {
                return "通用设置";
            }
        }

        internal static string SETTINGS_DOOR
        {
            get
            {
                return "安全门设置";
            }
        }

        internal static string SETTINGS_PLAYERUNDERATTACK
        {
            get
            {
                return "玩家受到攻击设置";
            }
        }

        internal static string SETTINGS_PLAYERSLIP
        {
            get
            {
                return "玩家摔倒设置";
            }
        }

        internal static string SETTINGS_GIVERESOURCE
        {
            get
            {
                return "资源使用设置";
            }
        }

        internal static string SETTINGS_ENEMYWAKEUP
        {
            get
            {
                return "敌人惊醒设置";
            }
        }

        internal static string SETTINGS_FRIENDLYFIRE
        {
            get
            {
                return "队友伤害设置";
            }
        }

        internal static string SETTINGS_ENEMYUNDERATTACK
        {
            get
            {
                return "敌人受到伤害设置";
            }
        }

        internal static string FRIENDLY_FIRE_NAME
        {
            get
            {
                return "FriendlyFire";
            }
        }

        internal static string LANGUAGE_NAME
        {
            get
            {
                return "Language";
            }
        }

        internal static string ENEMYWAKEUP_DESC
        {
            get
            {
                return "默认[开启|关闭]敌人惊醒提示功能，接受的值：[true|false]";
            }
        }

        internal static string ENEMYWAKEUP_NAME
        {
            get
            {
                return "EnemyWakeup";
            }
        }

        internal static string ENEMYWAKEUP_TEXT_NAME
        {
            get
            {
                return "EnemyWakeupText";
            }
        }

        internal static string ENEMYWAKEUP_TEXT_DESC
        {
            get
            {
                return "自定义敌人惊醒检测的提示语句，{0}为玩家名字，{1}为敌人数量，{2}为敌人名称";
            }
        }

        internal static string SLIP_NAME
        {
            get
            {
                return "PlayerSlip";
            }
        }

        internal static string SLIP_DESC
        {
            get
            {
                return "默认[开启|关闭]玩家跌落伤害检测提示功能，接受的值：[true|false]";
            }
        }

        internal static string SLIP_TEXT_NAME
        {
            get
            {
                return "PlayerSlipText";
            }
        }

        internal static string SLIP_TEXT_DESC
        {
            get
            {
                return "自定义玩家跌落伤害检测的提示语句，{0}为玩家名字，{1}为损失的血量";
            }
        }

        internal static string GIVERESOURCE_NAME
        {
            get
            {
                return "GiveResource";
            }
        }

        internal static string GIVERESOURCE_DESC
        {
            get
            {
                return "默认[开启|关闭]资源使用检测提示功能，接受的值：[true|false]";
            }
        }

        internal static string GIVERESOURCE_HEALTH_TEXT_DESC
        {
            get
            {
                return "自定义医疗包使用检测的提示语句，{0}为提供者名字，{1}为接受者名字，{2}为使用医疗包次数";
            }
        }

        internal static string GIVERESOURCE_AMMO_TEXT_DESC
        {
            get
            {
                return "自定义弹药包使用检测的提示语句，{0}为提供者名字，{1}为接受者名字，{2}为使用弹药包次数，{3}为弹药包种类";
            }
        }

        internal static string GIVERESOURCE_DISINFECTION_TEXT_DESC
        {
            get
            {
                return "自定义消毒剂使用检测的提示语句，{0}为提供者名字，{1}为接受者名字，{2}为使用消毒剂次数";
            }
        }


        internal static string GIVERESOURCE_AMMO_NAME
        {
            get
            {
                return "GiveResourceAmmoText";
            }
        }

        internal static string GIVERESOURCE_HEALTH_NAME
        {
            get
            {
                return "GiveResourceHealthText";
            }
        }


        internal static string GIVERESOURCE_DISINFECTION_NAME
        {
            get
            {
                return "GiveResourceDisinfectionText";
            }
        }

        internal static string FRIENDLY_FIRE_BULLET_TEXT_DESC
        {
            get
            {
                return "自定义黑枪的提示语句，{0}为黑枪者名字，{1}为被黑者名字，{2}为黑枪次数，{3}为损失的血量";
            }
        }

        internal static string FRIENDLY_FIRE_BULLET_TEXT_NAME
        {
            get
            {
                return "FriendlyFireBulletText";
            }
        }

        internal static string FRIENDLY_FIRE_EXPLOSION_TEXT_NAME
        {
            get
            {
                return "FriendlyFireExplosionText";
            }
        }

        internal static string FRIEDNLU_FIRE_EXPLOSION_TEXT_DESC
        {
            get
            {
                return "自定义拌雷误伤的提示语句，{0}为被炸者名字，{1}为损失的血量";
            }
        }

        internal static string POUNCERCONSUME_NAME
        {
            get
            {
                return "PouncerConsume";
            }
        }

        internal static string POUNCERCONSUME_TEXT_NAME
        {
            get
            {
                return "PouncerConsumeText";
            }
        }

        internal static string POUNCERCONSUME_TEXT_DESC
        {
            get
            {
                return "自定义狗吞玩家检测的提示语句，{0}为玩家名字";
            }
        }

        internal static string POUNCERCONSUME_DESC
        {
            get
            {
                return "默认[开启|关闭]狗吞玩家检测提示功能，接受的值：[true|false]";
            }
        }

        internal static string ENEMYUNDERATTACKKILLTIMER_TEXT_NAME
        {
            get
            {
                return "EnemyUnderAttackKillTimerText";
            }
        }

        internal static string ENEMYUNDERATTACKKILLTIMER_TEXT_DESC
        {
            get
            {
                return "自定义击杀敌人计时的提示语句，{0}为敌人种类，{1}为击杀耗时";
            }
        }

        internal static string DOORSTATE_DESC
        {
            get
            {
                return "默认[开启|关闭]安全门状态检测提示功能，接受的值：[true|false]";
            }
        }

        internal static string DOORSTATE_NAME
        {
            get
            {
                return "DoorState";
            }
        }

        internal static string DOORSTATESECURITYDOOROPENED_TEXT_NAME
        {
            get
            {
                return "DoorStateSecurityDoorOpenedText";
            }
        }

        internal static string DOORSTATESECURITYDOOROPENED_TEXT_DESC
        {
            get
            {
                return "自定义安全门开启检测的提示语句";
            }
        }

        internal static string DOORSTATESECURITYDOORCLOSED_TEXT_DESC
        {
            get
            {
                return "自定义安全门关闭检测的提示语句";
            }
        }

        internal static string DOORSTATESECURITYDOORCLOSED_TEXT_NAME
        {
            get
            {
                return "DoorStateSecurityDoorClosedText";
            }
        }
        internal static string DOORSTATESECURITYDOORACTIVATED_TEXT_NAME
        {
            get
            {
                return "DoorStateSecurityDoorActivatedText";
            }
        }

        internal static string DOORSTATESECURITYDOORACTIVATED_TEXT_DESC
        {
            get
            {
                return "自定义安全门激活检测的提示语句";
            }
        }

        internal static string DOORSTATESECURITYDOOROPENEDBYPLAYER_TEXT_DESC
        {
            get
            {
                return "自定义安全门被玩家开启检测的提示语句";
            }
        }

        internal static string DOORSTATESECURITYDOOROPENEDBYPLAYER_TEXT_NAME
        {
            get
            {
                return "DoorStateSecurityDoorOpenedByPlayerText";
            }
        }


        internal static string DOORSTATESECURITYDOORCLOSEDBYPLAYER_TEXT_DESC
        {
            get
            {
                return "自定义安全门被玩家关闭检测的提示语句";
            }
        }

        internal static string DOORSTATESECURITYDOORCLOSEDBYPLAYER_TEXT_NAME
        {
            get
            {
                return "DoorStateSecurityDoorClosedByPlayerText";
            }
        }
        internal static string DOORSTATESECURITYDOORACTIVATEDBYPLAYER_TEXT_NAME
        {
            get
            {
                return "DoorStateSecurityDoorActivatedByPlayerText";
            }
        }

        internal static string DOORSTATESECURITYDOORACTIVATEDBYPLAYER_TEXT_DESC
        {
            get
            {
                return "自定义安全门被玩家激活检测的提示语句";
            }
        }

        internal static string ENEMYUNDERATTACKKILLTIMER_NAME
        {
            get
            {
                return "EnemyUnderAttackKillTimer";
            }
        }

        internal static string ENEMYUNDERATTACKKILLTIMER_DESC
        {
            get
            {
                return "默认[开启|关闭]击杀敌人计时提示功能，接受的值：[true|false]";
            }
        }
    }
}
