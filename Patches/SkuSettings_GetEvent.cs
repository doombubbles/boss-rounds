using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.ServerEvents;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

namespace BossRounds.Patches;

[HarmonyPatch]
internal static class SkuSettings_GetEvent
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(SkuSettings), nameof(SkuSettings.GetEvent),
            new[] { typeof(string), typeof(bool) }, new[] { typeof(BossEvent) });
    }

    [HarmonyPrefix]
    private static bool Prefix(ref object __result)
    {
        if (InGameData.CurrentGame.gameEventId != BossRoundsMod.EventId) return true;

        var bossData = InGameData.CurrentGame.bossData;

        __result = new BossEvent
        {
            id = BossRoundsMod.EventId,
            eventData = new BossDataModel
            {
                bossType = bossData.bossBloon,
                leaderboardScoringType = LeaderboardScoringType.GameTime
            }
        };

        return false;
    }
}