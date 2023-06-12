using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.UI.Modded;
using HarmonyLib;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Difficulty;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Menu;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.Main.DifficultySelect;

namespace BossRounds.Patches;

/// <summary>
/// Increase monkey money rewards for playing on boss mode
/// </summary>
[HarmonyPatch(typeof(Game), nameof(Game.GetMonkeyMoneyReward))]
internal static class Game_GetMonkeyMoneyReward
{
    [HarmonyPostfix]
    private static void Postfix(string map, string difficulty, string mode, GameModel useModel, ref int __result)
    {
        bool elite;

        if (InGameData.CurrentGame is { gameEventId: BossRoundsMod.EventId } data && InGame.instance != null)
        {
            elite = data.bossData.bossEliteMode;
        }
        else if (InGame.instance == null &&
                 RoundSetChanger.RoundSetOverride != null &&
                 BossRoundSet.Cache.TryGetValue(RoundSetChanger.RoundSetOverride, out var bossRoundset))
        {
            elite = bossRoundset.elite;
        }
        else return;

        var totalBonus = (float) CostHelper.CostForDifficulty(BossRoundsMod.BaseMonkeyMoneyBonus, difficulty);

        var mapMoney = useModel.monkeyMoneyReward;
        if (mode == ModeType.Impoppable)
        {
            mapMoney *= 1.5f;
        }
        else if (mode == ModeType.CHIMPS && !MenuManager.instance.GetCurrentMenu().Is<DifficultySelectScreen>())
        {
            // Has anyone even beaten a Boss on CHIMPS before?
            mapMoney *= 5f;
        }

        totalBonus += mapMoney * BossRoundsMod.MapModeMonkeyMoneyMult;

        if (elite)
        {
            totalBonus *= BossRoundsMod.EliteMonkeyMoneyMult;
        }

        __result += (int) totalBonus;
    }
}