using Assets.Scripts.Unity.Menu;
using Assets.Scripts.Unity.UI_New.Main.DifficultySelect;
using Assets.Scripts.Unity.UI_New.Main.ModeSelect;
using MelonLoader;
using BTD_Mod_Helper;
using BossRounds;
using BTD_Mod_Helper.Extensions;
using BTD_Mod_Helper.UI.Modded;

[assembly: MelonInfo(typeof(BossRoundsMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BossRounds;

public class BossRoundsMod : BloonsTD6Mod
{
    public const string EventId = nameof(BossRoundsMod);
    public const string BossTypeKey = "BossRounds-BossType";
    public const string IsEliteKey = "BossRounds-IsElite";

    public BossRoundSet? SelectedSet { get; private set; }

    // Not really much point in making these settings lol, people would just abuse it even more
    public const int BaseMonkeyMoneyBonus = 400;
    public const float MapModeMonkeyMoneyMult = .5f;
    public const float EliteMonkeyMoneyMult = 2;

    public override void OnUpdate()
    {
        if (MenuManager.instance == null) return;

        var updateScreens = false;
        if (BossRoundSet.Cache.TryGetValue(RoundSetChanger.RoundSetOverride ?? "", out var roundSet))
        {
            if (roundSet != SelectedSet)
            {
                SelectedSet = roundSet;
                updateScreens = true;
            }
        }
        else if (SelectedSet != null)
        {
            SelectedSet = null;
            updateScreens = true;
        }

        // Refresh the labels on the screens if the boss mode changed
        if (updateScreens)
        {
            var menu = MenuManager.instance.GetCurrentMenu();
            if (menu.Is<DifficultySelectScreen>(out var difficultyScreen))
            {
                foreach (var item in difficultyScreen.transform.GetComponentsInChildren<DifficultySelectMmItems>())
                {
                    item.Initialise();
                }
            }
            else if (menu.Is<ModeScreen>(out var modeScreen))
            {
                foreach (var label in modeScreen.transform.GetComponentsInChildren<ModeSelectMonkeyMoneyLabel>())
                {
                    label.Initialise();
                }
            }
        }
    }
}