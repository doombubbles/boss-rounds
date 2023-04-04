using System.Collections.Generic;
using System.Reflection;
using BTD_Mod_Helper.Api;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using HarmonyLib;

namespace BossRounds.Patches;

/// <summary>
/// Go back to the main menu instead of to the boss menu, which may not even exist atm  
/// </summary>
[HarmonyPatch]
internal static class InGame_ReturnToMainMenu
{
    private static IEnumerable<MethodBase> TargetMethods()
    {
        yield return MoreAccessTools.SafeGetNestedClassMethod(typeof(InGame), 
            nameof(InGame.ReturnToMainMenu), "MoveNext");
    }

    [HarmonyPrefix]
    private static void Prefix()
    {
        var inGameData = InGameData.CurrentGame;
        if (inGameData?.gameEventId == BossRoundsMod.EventId)
        {
            inGameData.gameType = GameType.Standard;
        }
    }
}