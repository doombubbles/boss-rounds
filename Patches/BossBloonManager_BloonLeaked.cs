using HarmonyLib;
using Il2CppAssets.Scripts.Simulation.Track;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;

namespace BossRounds.Patches;

[HarmonyPatch(typeof(BossBloonManager), nameof(BossBloonManager.BloonLeaked))]
internal static class BossBloonManager_BloonLeaked
{
    [HarmonyPrefix]
    private static bool Prefix() => !InGame.instance.bridge.IsSandboxMode();
}