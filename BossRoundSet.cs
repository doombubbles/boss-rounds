using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Data.Boss;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Bloons;
using BTD_Mod_Helper.Api.Enums;

namespace BossRounds;

/// <summary>
/// Round set for enabling a specific boss. Automatically loads from the different values of <see cref="BossType"/>
/// </summary>
public class BossRoundSet : ModRoundSet
{
    public static readonly Dictionary<string, BossRoundSet> Cache = new();

    public readonly BossType bossType;
    public readonly bool elite;

    public override string BaseRoundSet => RoundSetType.Default;
    public override int DefinedRounds => BaseRounds.Count;

    public override string Name => (elite ? "Elite" : "") + bossType;

    public override string Icon =>
        VanillaSprites.ByName.TryGetValue(bossType + "Btn" + (elite ? "Elite" : ""), out var icon)
            ? icon
            : VanillaSprites.WoodenRoundButton;

    // ReSharper disable once UnusedMember.Global gotta have empty constructor for any ModContent
    public BossRoundSet()
    {
    }

    public BossRoundSet(BossType bossType, bool elite)
    {
        this.bossType = bossType;
        this.elite = elite;
    }

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

    public override void Register()
    {
        base.Register();
        Cache[Id] = this;
    }
}