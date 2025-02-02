using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class Elektriwave : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Elektriwave");
        Tooltip.SetDefault("Has a chance to inflict Electrified");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 40;
        Item.damage = 106;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ModContent.RarityType<TealRarity>();
        Item.useTime = 15;
        Item.knockBack = 6f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 616000;
        Item.useAnimation = 15;
        Item.UseSound = SoundID.Item15;
    }
}
