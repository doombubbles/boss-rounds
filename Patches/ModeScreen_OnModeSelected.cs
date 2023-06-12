using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.UI.Modded;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.ServerEvents;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Main.ModeSelect;

namespace BossRounds.Patches;

/// <summary>
/// Activate the appropriate boss mode if a boss round is selected
/// </summary>
[HarmonyPatch(typeof(ModeScreen), nameof(ModeScreen.OnModeSelected))]
internal static class ModeScreen_OnModeSelected
{
    [HarmonyPrefix]
    private static void Prefix(string modeType)
    {
        if (!BossRoundSet.Cache.TryGetValue(RoundSetChanger.RoundSetOverride, out var bossRoundset)) return;

        var inGameData = InGameData.Editable;
        inGameData.SetupBoss(BossRoundsMod.EventId, bossRoundset.bossType, bossRoundset.elite, false,
            BossGameData.DefaultSpawnRounds, new DailyChallengeModel
            {
                difficulty = inGameData.selectedDifficulty,
                map = inGameData.selectedMap,
                mode = modeType,
                towers = new TowerData[]
                    {
                        new() { isHero = true, tower = DailyChallengeModel.CHOSENPRIMARYHERO, max = 1 }
                    }
                    .ToIl2CppList()
            }, LeaderboardScoringType.GameTime);

        ModHelper.Msg<BossRoundsMod>("It's boss time!");
    }
}