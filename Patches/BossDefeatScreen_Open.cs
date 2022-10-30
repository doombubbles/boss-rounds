using Assets.Scripts.Models.Profile;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.GameOver;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using MelonLoader;

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
                { bestRound = 69, hasCompleted = true, seenCompletion = true, tierBeaten = 5, newBestRound = false },
            normal = new BossModeData
                { bestRound = 69, hasCompleted = true, seenCompletion = true, tierBeaten = 5, newBestRound = false },
            eventId = BossRoundsMod.EventId,
            leaderboardStandings = new List<BossLeaderboardStanding>(),
            hasClaimedRewards = true
        };
        
        if (Game.Player.Data.GetSavedMap(InGameData.CurrentGame.selectedMap, out var map))
        {
            
        }
        else
        {
            ModHelper.Msg<BossRoundsMod>("No saved map :thinking:");
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