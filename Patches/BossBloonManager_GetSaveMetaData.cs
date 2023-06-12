using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Track;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppSystem.Collections.Generic;

namespace BossRounds.Patches;

/// <summary>
/// Save the boss type / eliteness to the map save metadata
/// </summary>
[HarmonyPatch(typeof(BossBloonManager), nameof(BossBloonManager.GetSaveMetaData))]
internal static class BossBloonManager_GetSaveMetaData
{
    [HarmonyPostfix]
    private static void Postfix(Dictionary<string, string> metaData)
    {
        var inGameData = InGameData.CurrentGame;
        if (inGameData.gameEventId == BossRoundsMod.EventId)
        {
            metaData[BossRoundsMod.BossTypeKey] = inGameData.bossData.bossBloon.ToString();
            metaData[BossRoundsMod.IsEliteKey] = inGameData.bossData.bossEliteMode.ToString();
        }
    }
}