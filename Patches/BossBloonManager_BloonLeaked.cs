using Assets.Scripts.Simulation.Track;
using Assets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

[HarmonyPatch(typeof(BossBloonManager), nameof(BossBloonManager.BloonLeaked))]
internal static class BossBloonManager_BloonLeaked
{
    [HarmonyPrefix]
    private static bool Prefix() => !InGame.instance.bridge.IsSandboxMode();
}