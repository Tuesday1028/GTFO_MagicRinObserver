using BepInEx;
using BepInEx.Configuration;
using Hikaria.MagicRinObserver.Lang;
using Hikaria.MagicRinObserver.Utils;
using System;
using System.IO;

namespace Hikaria.MagicRinObserver.Config
{
    internal class ConfigManager
    {
        static ConfigManager()
        {
            Logs.LogDebug("正在加载配置文件...");

            ConfigFile configFile = new(string.Concat(Paths.ConfigPath, "\\MagicRinObserver\\MagicRinObserver.cfg"), true);

            language = configFile.Bind(ConfigDescription.SETTINGS_COMMON, ConfigDescription.LANGUAGE_NAME, Language.zh_CN, ConfigDescription.LANGUAGE_DESC);

            enemyWakeup = configFile.Bind(ConfigDescription.SETTINGS_ENEMYWAKEUP, ConfigDescription.ENEMYWAKEUP_NAME, true, ConfigDescription.ENEMYWAKEUP_DESC);
            enemyWakeup_text = configFile.Bind(ConfigDescription.SETTINGS_ENEMYWAKEUP, ConfigDescription.ENEMYWAKEUP_TEXT_NAME, EntryPoint.Settings.EnemyWakeupText, ConfigDescription.ENEMYWAKEUP_TEXT_DESC);

            firendlyFire = configFile.Bind(ConfigDescription.SETTINGS_FRIENDLYFIRE, ConfigDescription.FRIENDLY_FIRE_NAME, true, ConfigDescription.FRIENDLY_FIRE_DESC);
            friendlyFire_text = configFile.Bind(ConfigDescription.SETTINGS_FRIENDLYFIRE, ConfigDescription.FRIENDLY_FIRE_BULLET_TEXT_NAME, EntryPoint.Settings.FriendlyFireText, ConfigDescription.FRIENDLY_FIRE_BULLET_TEXT_DESC);
            friendlyFire_Explosion_text = configFile.Bind(ConfigDescription.SETTINGS_FRIENDLYFIRE, ConfigDescription.FRIENDLY_FIRE_EXPLOSION_TEXT_NAME, EntryPoint.Settings.FriendlyFireExplosionText, ConfigDescription.FRIEDNLU_FIRE_EXPLOSION_TEXT_DESC);

            giveResource = configFile.Bind(ConfigDescription.SETTINGS_GIVERESOURCE, ConfigDescription.GIVERESOURCE_NAME, true, ConfigDescription.GIVERESOURCE_DESC);
            giveResourceHealth_text = configFile.Bind(ConfigDescription.SETTINGS_GIVERESOURCE, ConfigDescription.GIVERESOURCE_HEALTH_NAME, EntryPoint.Settings.GiveResourceHealthText, ConfigDescription.GIVERESOURCE_HEALTH_TEXT_DESC);
            giveResourceAmmo_text = configFile.Bind(ConfigDescription.SETTINGS_GIVERESOURCE, ConfigDescription.GIVERESOURCE_AMMO_NAME, EntryPoint.Settings.GiveResourceAmmoText, ConfigDescription.GIVERESOURCE_AMMO_TEXT_DESC);
            giveResourceDisinfection_text = configFile.Bind(ConfigDescription.SETTINGS_GIVERESOURCE, ConfigDescription.GIVERESOURCE_DISINFECTION_NAME, EntryPoint.Settings.GiveResourceDisinfectionText, ConfigDescription.GIVERESOURCE_DISINFECTION_TEXT_DESC);

            playerSlip = configFile.Bind(ConfigDescription.SETTINGS_PLAYERSLIP, ConfigDescription.SLIP_NAME, true, ConfigDescription.SLIP_DESC);
            playerSlip_text = configFile.Bind(ConfigDescription.SETTINGS_PLAYERSLIP, ConfigDescription.SLIP_TEXT_NAME, EntryPoint.Settings.SlipText, ConfigDescription.SLIP_TEXT_DESC);

            pouncerConsume = configFile.Bind(ConfigDescription.SETTINGS_PLAYERUNDERATTACK, ConfigDescription.POUNCERCONSUME_NAME, true, ConfigDescription.POUNCERCONSUME_DESC);
            pouncerConsume_text = configFile.Bind(ConfigDescription.SETTINGS_PLAYERUNDERATTACK, ConfigDescription.POUNCERCONSUME_TEXT_NAME, EntryPoint.Settings.PouncerConsumeText, ConfigDescription.POUNCERCONSUME_TEXT_DESC);

            enemyUnderAttackKillTimer = configFile.Bind(ConfigDescription.SETTINGS_ENEMYUNDERATTACK, ConfigDescription.ENEMYUNDERATTACKKILLTIMER_NAME, EntryPoint.Settings.EnemyUnderAttackKillTimerEnable, ConfigDescription.ENEMYUNDERATTACKKILLTIMER_DESC);
            enemyUnderAttackKillTimer_text = configFile.Bind(ConfigDescription.SETTINGS_ENEMYUNDERATTACK, ConfigDescription.ENEMYUNDERATTACKKILLTIMER_TEXT_NAME, EntryPoint.Settings.EnemyUnderAttackKillTimerText, ConfigDescription.ENEMYUNDERATTACKKILLTIMER_TEXT_DESC);

            doorState = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATE_NAME, EntryPoint.Settings.DoorStateEnable, ConfigDescription.DOORSTATE_DESC);
            doorStateSecurityDoorOpened_text = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATESECURITYDOOROPENED_TEXT_NAME, EntryPoint.Settings.DoorStateSecurityDoorOpenedText, ConfigDescription.DOORSTATESECURITYDOOROPENED_TEXT_DESC);
            doorStateSecurityDoorClosed_text = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATESECURITYDOORCLOSED_TEXT_NAME, EntryPoint.Settings.DoorStateSecurityDoorClosedText, ConfigDescription.DOORSTATESECURITYDOORCLOSED_TEXT_DESC);
            doorStateSecurityDoorActivated_text = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATESECURITYDOORACTIVATED_TEXT_NAME, EntryPoint.Settings.DoorStateSecurityDoorActivatedText, ConfigDescription.DOORSTATESECURITYDOORACTIVATED_TEXT_DESC);
            doorStateSecurityDoorOpenedByPlayer_text = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATESECURITYDOOROPENEDBYPLAYER_TEXT_NAME, EntryPoint.Settings.DoorStateSecurityDoorOpenedByPlayerText, ConfigDescription.DOORSTATESECURITYDOOROPENEDBYPLAYER_TEXT_DESC);
            doorStateSecurityDoorClosedByPlayer_text = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATESECURITYDOORCLOSEDBYPLAYER_TEXT_NAME, EntryPoint.Settings.DoorStateSecurityDoorClosedByPlayerText, ConfigDescription.DOORSTATESECURITYDOORCLOSEDBYPLAYER_TEXT_DESC);
            doorStateSecurityDoorActivatedByPlayer_text = configFile.Bind(ConfigDescription.SETTINGS_DOOR, ConfigDescription.DOORSTATESECURITYDOORACTIVATEDBYPLAYER_TEXT_NAME, EntryPoint.Settings.DoorStateSecurityDoorActivatedByPlayerText, ConfigDescription.DOORSTATESECURITYDOORACTIVATEDBYPLAYER_TEXT_DESC);

            Logs.LogDebug("配置文件加载完成");
        }

        public bool GetEnemyWakeup()
        {
            return enemyWakeup.Value;
        }

        public string GetEnemyWakeupText()
        {
            return enemyWakeup_text.Value;
        }

        public bool GetFriendlyFire()
        {
            return firendlyFire.Value;
        }

        public bool GetPlayerSlip()
        {
            return playerSlip.Value;
        }

        public string GetSlipText()
        {
            return playerSlip_text.Value;
        }

        public LanguageBase GetLanguage()
        {
            switch (language.Value)
            {
                case Language.en_US:
                    return new English();
                case Language.zh_CN:
                default:
                    return new SimplifiedChinese();
            }
        }

        public Language GetLanguageName()
        {
            return language.Value;
        }

        public bool GetGiveResource()
        {
            return giveResource.Value;
        }

        public string GetGiveResourceHealthText()
        {
            return giveResourceHealth_text.Value;
        }

        public string GetGiveResourceAmmoText()
        {
            return giveResourceAmmo_text.Value;
        }

        public string GetGiveResourceDisinfectionText()
        {
            return giveResourceDisinfection_text.Value;
        }

        public string GetFriendlyFireText()
        {
            return friendlyFire_text.Value;
        }

        public string GetFirendFireExplosionText()
        {
            return friendlyFire_Explosion_text.Value;
        }

        public string GetGiveResourceHealthSelfText()
        {
            return giveResourceHealthSelf_text.Value;
        }

        public string GetPouncerConsumeText()
        {
            return pouncerConsume_text.Value;
        }

        public bool GetPouncerConsume()
        {
            return pouncerConsume.Value;
        }

        public bool GetPlayerUnderAttack()
        {
            return playerUnderAttack.Value;
        }

        public string GetPlayerUnderAttackTentacleText()
        {
            return playerUnderAttackTentacle_text.Value;
        }

        public string GetPlayerUnderAttackShooterProjectileText()
        {
            return playerUnderAttackShooterProjectile_text.Value;
        }

        public string GetPlayerUnderAttackMeleeText()
        {
            return playerUnderAttackMelee_text.Value;
        }

        public string GetPlayerUnderAttackParasiteText()
        {
            return playerUnderAttackParasite_text.Value;
        }

        public bool GetEnemyUnderAttackInstantKill()
        {
            return enemyUnderAttackInstantKill.Value;
        }

        public bool GetEnemyUnderAttackKillTimer()
        {
            return enemyUnderAttackKillTimer.Value;
        }

        public string GetEnemyUnderAttackInstantKillSuccessfullyTest()
        {
            return enemyUnderAttackInstantKillSuccessfully_text.Value;
        }

        public string GetEnemyUnderAttackKillInSecFailedText()
        {
            return enemyUnderAttackKillInSecFailed_text.Value;
        }

        public string GetEnemyUnderAttackKillInSecSuccessfullyText()
        {
            return enemyUnderAttackKillInSecSuccessfully_text.Value;
        }

        public bool GetDoorStateSecurityDoorOpen()
        {
            return doorState.Value;
        }

        public string GetDoorStateSecurityDoorOpenedText()
        {
            return doorStateSecurityDoorOpened_text.Value;
        }

        public string GetDoorStateSecurityDoorActivatedText()
        {
            return doorStateSecurityDoorActivated_text.Value;
        }

        public string GetDoorStateSecurityDoorClosedText()
        {
            return doorStateSecurityDoorClosed_text.Value;
        }

        public string GetDoorStateSecurityDoorOpenedByPlayerText()
        {
            return doorStateSecurityDoorOpenedByPlayer_text.Value;
        }

        public string GetDoorStateSecurityDoorActivatedByPlayerText()
        {
            return doorStateSecurityDoorActivatedByPlayer_text.Value;
        }

        public string GetDoorStateSecurityDoorClosedByPlayerText()
        {
            return doorStateSecurityDoorClosedByPlayer_text.Value;
        }

        public string GetEnemyUnderAttackKillTimerText()
        {
            return enemyUnderAttackKillTimer_text.Value;
        }


        public static ConfigEntry<bool> enemyWakeup;

        public static ConfigEntry<string> enemyWakeup_text;

        public static ConfigEntry<bool> firendlyFire;

        public static ConfigEntry<Language> language;

        public static ConfigEntry<bool> playerSlip;

        public static ConfigEntry<string> playerSlip_text;

        public static ConfigEntry<bool> giveResource;

        public static ConfigEntry<string> giveResourceHealth_text;

        public static ConfigEntry<string> giveResourceAmmo_text;

        public static ConfigEntry<string> giveResourceDisinfection_text;

        public static ConfigEntry<string> friendlyFire_text;

        public static ConfigEntry<string> friendlyFire_Explosion_text;

        public static ConfigEntry<string> giveResourceHealthSelf_text;

        public static ConfigEntry<bool> pouncerConsume;

        public static ConfigEntry<string> pouncerConsume_text;

        public static ConfigEntry<bool> playerUnderAttack;

        public static ConfigEntry<string> playerUnderAttackTentacle_text;

        public static ConfigEntry<string> playerUnderAttackShooterProjectile_text;

        public static ConfigEntry<string> playerUnderAttackMelee_text;

        public static ConfigEntry<string> playerUnderAttackParasite_text;

        public static ConfigEntry<string> enemyUnderAttackInstantKillSuccessfully_text;

        public static ConfigEntry<string> enemyUnderAttackKillInSecFailed_text;

        public static ConfigEntry<string> enemyUnderAttackKillTimer_text;

        public static ConfigEntry<string> enemyUnderAttackKillInSecSuccessfully_text;

        public static ConfigEntry<string> doorStateSecurityDoorOpened_text;

        public static ConfigEntry<string> doorStateSecurityDoorOpenedByPlayer_text;

        public static ConfigEntry<string> doorStateSecurityDoorActivated_text;

        public static ConfigEntry<string> doorStateSecurityDoorActivatedByPlayer_text;

        public static ConfigEntry<string> doorStateSecurityDoorClosed_text;

        public static ConfigEntry<string> doorStateSecurityDoorClosedByPlayer_text;

        public static ConfigEntry<bool> doorState;

        public static ConfigEntry<bool> enemyUnderAttackInstantKill;

        public static ConfigEntry<bool> enemyUnderAttackKillTimer;

        public enum Language : byte
        {
            en_US,
            zh_CN
        }
    }
}
