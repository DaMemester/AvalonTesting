using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class CorruptionSeed : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Corruption Seed");
        Tooltip.SetDefault("For use with Blowpipes");
        SacrificeTotal = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 2;
        Item.ammo = AmmoID.Dart;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.shoot = ModContent.ProjectileType<Projectiles.CorruptionSeed>();
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }
}
