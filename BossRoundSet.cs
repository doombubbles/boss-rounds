using System;
using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Data.Boss;
using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Models.ServerEvents;
using Il2CppAssets.Scripts.Unity;

namespace BossRounds;

/// <summary>
/// Round set for enabling a specific boss. Automatically loads from the different values of <see cref="BossType" />
/// </summary>
public class BossRoundSet : ModRoundSet
{
    public static readonly Dictionary<string, BossRoundSet> Cache = new();
    public static readonly Dictionary<BossType, BossRoundSet> NormalCache = new();
    public static readonly Dictionary<BossType, BossRoundSet> EliteCache = new();

    public readonly BossType bossType;
    public readonly bool elite;

    // ReSharper disable once UnusedMember.Global gotta have empty constructor for any ModContent
    public BossRoundSet()
    {
    }

    public BossRoundSet(BossType bossType, bool elite)
    {
        this.bossType = bossType;
        this.elite = elite;
    }

    public override string BaseRoundSet => RoundSetType.Default;
    public override int DefinedRounds => BaseRounds.Count;

    public override string Name => (elite ? "Elite" : "") + bossType;

    public override string Icon =>
        VanillaSprites.ByName.TryGetValue(bossType + "Btn" + (elite ? "Elite" : ""), out var icon)
            ? icon
            : VanillaSprites.WoodenRoundButton;

    /// <summary>
    /// Load a BossRoundSet for each boss type / eliteness
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<ModContent> Load()
    {
        foreach (var boss in Enum.GetValues(typeof(BossType)).Cast<BossType>())
        {
            yield return new BossRoundSet(boss, false);
            yield return new BossRoundSet(boss, true);
        }
    }

    private readonly Dictionary<int, RoundInfo> roundInfos = new();

    public override void Register()
    {
        foreach (var round in SkuSettings.instance.gameEvents.roundSets[bossType.ToString().ToLower()].rounds)
        {
            roundInfos[round.roundNumber] = round;
        }
        base.Register();
        Cache[Id] = this;
        var cache = elite ? EliteCache : NormalCache;
        cache[bossType] = this;
    }

    public override void ModifyRoundModels(RoundModel roundModel, int round)
    {
        if (!roundInfos.TryGetValue(round + 1, out var roundInfo)) return;

        var groups = roundInfo.bloonGroups.ToArray()
            .Select(group => new BloonGroupModel("", group.bloon, group.start, group.End, group.count))
            .ToArray();

        if (roundInfo.addToRound)
        {
            roundModel.groups = roundModel.groups.Concat(groups).ToArray();
            roundModel.AddChildDependants(groups);
        }
        else
        {
            roundModel.RemoveChildDependants(roundModel.groups);
            roundModel.groups = groups;
            roundModel.AddChildDependants(groups);
        }

        roundModel.emissions_ = null;
    }
}