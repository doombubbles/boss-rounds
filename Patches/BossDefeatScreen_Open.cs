﻿using HarmonyLib;
using Il2CppAssets.Scripts.Models.Profile;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.GameOver;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppSystem.Collections.Generic;

namespace BossRounds.Patches;

/// <summary>
/// Fix the boss defeat screen
/// </summary>
[HarmonyPatch(typeof(BossDefeatScreen), nameof(BossDefeatScreen.Open))]
internal static class BossDefeatScreen_Open
{
    [HarmonyPrefix]
    private static void Prefix(BossDefeatScreen __instance, ref BossEventData __state)
    {
        if (InGameData.CurrentGame.gameEventId != BossRoundsMod.EventId) return;

        __state = Game.Player.Data._CurrentBossEventData_k__BackingField;

        Game.Player.Data._CurrentBossEventData_k__BackingField = new BossEventData
        {
            elite = new BossModeData
            {
                highestCompletedRound = 69, hasCompleted = true, seenCompletion = true, tierBeaten = 5,
                newBestRound = false
            },
            normal = new BossModeData
            {
                highestCompletedRound = 69, hasCompleted = true, seenCompletion = true, tierBeaten = 5,
                newBestRound = false
            },
            eventId = BossRoundsMod.EventId,
            leaderboardStandings = new List<BossLeaderboardStanding>(),
            hasClaimedRewards = true
        };

        var lastRound = InGame.instance.GetLastRoundMapSaveData();
        if (lastRound != null)
        {
            __instance.retryMapSave = lastRound;
            __instance.canRetry = true;
        }
    }

    [HarmonyPostfix]
    private static void Postfix(BossDefeatScreen __instance, ref BossEventData __state)
    {
        if (InGameData.CurrentGame.gameEventId != BossRoundsMod.EventId) return;

        Game.Player.Data._CurrentBossEventData_k__BackingField = __state;
        __instance.bestRoundTxt.SetText("n/a");
    }
}