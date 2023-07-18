using MagicRinObserver.Lang;
using MagicRinObserver.Utils;

namespace MagicRinObserver.Config
{
    internal class Settings
    {
        public bool EnemyWakeupEnable = true;

        public string EnemyWakeupText = "{0} 惊醒了 {1} 只 {2}";

        public bool FriendlyFireEnable = true;

        public bool SlipEnable = true;

        public string SlipText = "{0} 摔了一跤，HP-{1}%";

        public LanguageBase Language;

        public ConfigManager.Language LanguageName = ConfigManager.Language.zh_CN;

        public bool GiveResourceEnable = true;

        public string FriendlyFireText = "{0} 黑了 {1} 共 {2} 枪，HP-{3}%";

        public string GiveResourceHealthText = "{0} 对 {1} 使用了 {2} 次 医疗包";

        public string GiveResourceAmmoText = "{0} 对 {1} 使用了 {2} 次 {3}弹药包";

        public string GiveResourceDisinfectionText = "{0} 对 {1} 使用了 {2} 次 消毒剂";

        public string FriendlyFireExplosionText = "{0} 受到爆炸伤害，HP-{1}%";

        public string PouncerConsumeText = "{0} 被狗吞了";

        public bool PouncerConsumeEnable = true;

        public bool DoorStateEnable = true;

        public string EnemyUnderAttackInstantKillSuccessfullyText = "{0} 使用 {1} 命中 {2} 秒杀了 {3}";

        public string EnemyUnderAttackKillInSecSuccessfullyText = "{0} 使用 {1} 命中 {2} 次 秒杀了 {3}";

        public string EnemyUnderAttackKillInSecFailedText = "{0} 秒杀 {1} 失败";

        public bool EnemyUnderAttackInstantKillEnable = true;

        public bool EnemyUnderAttackKillTimerEnable = true;

        public string EnemyUnderAttackKillTimerText = "击杀 {0} 总计耗时 {1}s";

        public string DoorStateSecurityDoorOpenedText = "ZONE_{0} 安全门已开启";

        public string DoorStateSecurityDoorActivatedText = "ZONE_{0} 安全门已激活";

        public string DoorStateSecurityDoorClosedText = "ZONE_{0} 安全门已关闭";

        public string DoorStateSecurityDoorOpenedByPlayerText = "{0} 开启了 ZONE_{1} 安全门";

        public string DoorStateSecurityDoorActivatedByPlayerText = "{0} 激活了 ZONE_{1} 安全门";

        public string DoorStateSecurityDoorClosedByPlayerText = "{0} 关闭了 ZONE_{1} 安全门";

        public void Setup()
        {
            ConfigManager configManager = new();

            EnemyWakeupEnable = configManager.GetEnemyWakeup();
            EnemyWakeupText = configManager.GetEnemyWakeupText();

            SlipEnable = configManager.GetPlayerSlip();
            SlipText = configManager.GetSlipText();

            FriendlyFireEnable = configManager.GetFriendlyFire();
            FriendlyFireText = configManager.GetFriendlyFireText();
            FriendlyFireExplosionText = configManager.GetFirendFireExplosionText();

            GiveResourceEnable = configManager.GetGiveResource();
            GiveResourceHealthText = configManager.GetGiveResourceHealthText();
            GiveResourceDisinfectionText = configManager.GetGiveResourceDisinfectionText();
            GiveResourceAmmoText = configManager.GetGiveResourceAmmoText();

            PouncerConsumeEnable = configManager.GetPouncerConsume();
            PouncerConsumeText = configManager.GetPouncerConsumeText();

            /*
            PlayerUnderAttackEnable = configManager.GetPlayerUnderAttack();
            PlayerUnderAttackTentacleText = configManager.GetPlayerUnderAttackTentacleText();
            PlayerUnderAttackShooterProjectileText = configManager.GetPlayerUnderAttackShooterProjectileText();
            PlayerUnderAttackMeleeText = configManager.GetPlayerUnderAttackMeleeText();
            PlayerUnderAttackParasiteText = configManager.GetPlayerUnderAttackParasiteText();
            */

            DoorStateEnable = configManager.GetDoorStateSecurityDoorOpen();
            DoorStateSecurityDoorOpenedText = configManager.GetDoorStateSecurityDoorOpenedText();
            DoorStateSecurityDoorClosedText = configManager.GetDoorStateSecurityDoorClosedText();
            DoorStateSecurityDoorActivatedText = configManager.GetDoorStateSecurityDoorActivatedText();
            DoorStateSecurityDoorOpenedByPlayerText = configManager.GetDoorStateSecurityDoorOpenedByPlayerText();
            DoorStateSecurityDoorClosedByPlayerText = configManager.GetDoorStateSecurityDoorClosedByPlayerText();
            DoorStateSecurityDoorActivatedByPlayerText = configManager.GetDoorStateSecurityDoorActivatedByPlayerText();

            /*
            EnemyUnderAttackInstantKillEnable = configManager.GetEnemyUnderAttackInstantKill();
            EnemyUnderAttackInstantKillSuccessfullyText = configManager.GetEnemyUnderAttackInstantKillSuccessfullyTest();
            EnemyUnderAttackKillInSecSuccessfullyText = configManager.GetEnemyUnderAttackKillInSecSuccessfullyText();
            EnemyUnderAttackKillInSecFailedText = configManager.GetEnemyUnderAttackKillInSecFailedText();
            */

            EnemyUnderAttackKillTimerEnable = configManager.GetEnemyUnderAttackKillTimer();
            EnemyUnderAttackKillTimerText = configManager.GetEnemyUnderAttackKillTimerText();

            Language = configManager.GetLanguage();
            LanguageName = configManager.GetLanguageName();
        }
    }
}
