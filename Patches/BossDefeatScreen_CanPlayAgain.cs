using HarmonyLib;
using Il2CppAssets.Scripts.Unity.UI_New.GameOver;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

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