﻿using Assets.Scripts.Models.ServerEvents;
using Assets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Save matches into the map save slot, not into the special save slot for bosses
/// </summary>
[HarmonyPatch(typeof(DailyChallengeExtensions), nameof(DailyChallengeExtensions.GetContinueMapSaveId))]
internal static class DailyChallengeExtensions_GetContinueMapSaveId
{
    [HarmonyPrefix]
    private static bool Prefix(ChallengeType chalType, ref string __result)
    {
        if (chalType == ChallengeType.BossBloon && InGameData.CurrentGame.gameEventId == BossRoundsMod.EventId)
        {
            __result = InGameData.CurrentGame.selectedMap;
            return false;
        }

        return true;
    }
}