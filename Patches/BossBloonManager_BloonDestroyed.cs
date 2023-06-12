using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Track;

namespace BossRounds.Patches;

/// <summary>
/// Allow matches to be won after defeating the final boss bloon tier
/// </summary>
[HarmonyPatch(typeof(BossBloonManager), nameof(BossBloonManager.BloonDestroyed))]
internal static class BossBloonManager_BloonDestroyed
{
    [HarmonyPrefix]
    private static void Prefix(BossBloonManager __instance)
    {
        __instance.BossDefeatedEvent = null;
    }
}