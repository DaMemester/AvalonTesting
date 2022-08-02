using Avalon.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace Avalon.DropConditions;
public class SuperhardmodePreArmaDrop : IItemDropRuleCondition, IProvideItemConditionDescription
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return ModContent.GetInstance<AvalonWorld>().SuperHardmode &&
            !ModContent.GetInstance<DownedBossSystem>().DownedArmageddon &&
            !info.IsInSimulation && info.npc.value > 0;
    }
    public bool CanShowItemDropInUI()
    {
        return true;
    }
    public string GetConditionDescription()
    {
        return "Drops in early Superhardmode";
    }
}

