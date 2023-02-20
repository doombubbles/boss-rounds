using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Go back to the main menu instead of to the boss menu, which may not even exist atm  
/// </summary>
[HarmonyPatch(typeof(InGame._ReturnToMainMenu_d__172), nameof(InGame._ReturnToMainMenu_d__172.MoveNext))]
internal static class InGame_ReturnToMainMenu
{
    [HarmonyPrefix]
    private static void Prefix()
    {
        var inGameData = InGameData.CurrentGame;
        if (inGameData?.gameEventId == BossRoundsMod.EventId)
        {
            inGameData.gameType = GameType.Standard;
        }
    }
}