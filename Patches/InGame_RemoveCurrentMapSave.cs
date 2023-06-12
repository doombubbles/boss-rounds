using HarmonyLib;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

namespace BossRounds.Patches;

/// <summary>
/// Allow continuing from check point on defeat
/// </summary>
[HarmonyPatch(typeof(InGame), nameof(InGame.RemoveCurrentMapSave))]
internal static class InGame_RemoveCurrentMapSave
{
    [HarmonyPrefix]
    private static bool Prefix(bool canClearCheckpoints) =>
        canClearCheckpoints || InGameData.CurrentGame.gameEventId != BossRoundsMod.EventId;
}