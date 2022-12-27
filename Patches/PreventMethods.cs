using System.Reflection;
using Il2CppAssets.Scripts.Unity.Player;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Prevent these methods for non real boss events
/// </summary>
[HarmonyPatch]
internal static class PreventMethods
{
    private static System.Collections.Generic.IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(Btd6Player), nameof(Btd6Player.BossEventBestRound));
        yield return AccessTools.Method(typeof(Btd6Player), nameof(Btd6Player.CompleteBossEvent));
        yield return AccessTools.Method(typeof(InGame), nameof(InGame.RetrieveTopScoreAndPostAnalytics));
    }

    [HarmonyPrefix]
    private static bool Prefix() => InGameData.CurrentGame.gameEventId != BossRoundsMod.EventId;
}