using System;
using Il2CppAssets.Scripts.Data.Boss;
using Il2CppAssets.Scripts.Models.ServerEvents;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper;
using HarmonyLib;
using Il2Cpp;

namespace BossRounds.Patches;

/// <summary>
/// Activate the appropriate boss mode if the save has the correct metadata
/// </summary>
[HarmonyPatch(typeof(ContinueGamePanel), nameof(ContinueGamePanel.ContinueClicked))]
internal static class ContinueGamePanel_ContinueClicked
{
    [HarmonyPrefix]
    private static void Prefix(ContinueGamePanel __instance)
    {
        var map = __instance.saveData;
        if (map != null &&
            map.metaData.ContainsKey(BossRoundsMod.BossTypeKey) &&
            map.metaData.ContainsKey(BossRoundsMod.IsEliteKey) &&
            Enum.TryParse(map.metaData[BossRoundsMod.BossTypeKey], out BossType bossType) &&
            bool.TryParse(map.metaData[BossRoundsMod.IsEliteKey], out var isElite))
        {
            var inGameData = InGameData.Editable;
            inGameData.SetupBoss(BossRoundsMod.EventId, bossType, isElite, false,
                BossGameData.DefaultSpawnRounds, new DailyChallengeModel
                {
                    difficulty = map.mapDifficulty,
                    map = map.mapName,
                    mode = map.modeName
                });

            ModHelper.Msg<BossRoundsMod>("It's boss time again!");
        }
    }
}