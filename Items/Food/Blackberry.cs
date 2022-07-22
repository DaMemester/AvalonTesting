using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Food;

public class Blackberry : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Blackberry");
        Tooltip.SetDefault("{$CommonItemTooltip.MediumStats}");
        SacrificeTotal = 5;
        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
        ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
            new Color(165, 160, 188),
            new Color(88, 81, 104),
            new Color(40, 38, 51)
        };
        ItemID.Sets.IsFood[Type] = true;
    }

    public override void SetDefaults()
    {
        // DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
        Item.DefaultToFood(22, 18, BuffID.WellFed2, 60 * 60 * 7); // 57600 is 16 minutes: 16 * 60 * 60
        Item.value = Item.buyPrice(0, 2);
        Item.rare = ItemRarityID.Blue;
    }
}
