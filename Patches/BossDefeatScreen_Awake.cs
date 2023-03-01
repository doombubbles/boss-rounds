using HarmonyLib;
using Il2CppAssets.Scripts.Unity.UI_New.GameOver;

namespace BossRounds.Patches;

/// <summary>
/// Patch to catch the mystery exception thrown within awake
/// </summary>
[HarmonyPatch(typeof(BossDefeatScreen), nameof(BossDefeatScreen.Awake))]
internal static class BossDefeatScreen_Awake
{
    [HarmonyPrefix]
    private static void Prefix()
    {
    }
}