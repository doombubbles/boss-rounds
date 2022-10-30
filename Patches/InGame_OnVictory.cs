using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Allow rewards for boss rounds games
/// </summary>
[HarmonyPatch(typeof(InGame), nameof(InGame.OnVictory))]
internal static class InGame_OnVictory
{
    [HarmonyPrefix]
    private static void Prefix()
    {
        if (InGameData.CurrentGame.gameEventId == BossRoundsMod.EventId)
        {
            InGameData.CurrentGame.gameType = GameType.Standard;
        }
    }

    [HarmonyPostfix]
    private static void Postfix()
    {
        if (InGameData.CurrentGame.gameEventId == BossRoundsMod.EventId)
        {
            InGameData.CurrentGame.gameType = GameType.BossBloon;
        }
    }
}