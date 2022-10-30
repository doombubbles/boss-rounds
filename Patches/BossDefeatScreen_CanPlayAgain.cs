using Assets.Scripts.Unity.UI_New.GameOver;
using Assets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Allow restarting boss rounds games
/// </summary>
[HarmonyPatch(typeof(BossDefeatScreen), nameof(BossDefeatScreen.CanPlayAgain), MethodType.Getter)]
internal static class BossDefeatScreen_CanPlayAgain
{
    [HarmonyPrefix]
    private static bool Prefix(ref bool __result)
    {
        if (InGameData.CurrentGame.gameEventId == BossRoundsMod.EventId)
        {
            __result = true;
            return false;
        }
        return true;
    }
}