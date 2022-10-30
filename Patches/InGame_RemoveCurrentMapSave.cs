using Assets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Allow continuing from check point on defeat
/// </summary>
[HarmonyPatch(typeof(InGame), nameof(InGame.RemoveCurrentMapSave))]
internal static class InGame_RemoveCurrentMapSave
{
    [HarmonyPrefix]
    private static bool Prefix(bool fromDefeat)
    {
        return !(fromDefeat && InGameData.CurrentGame.gameEventId == BossRoundsMod.EventId);
    }
}