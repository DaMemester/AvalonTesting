using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee;

class VertexofExcalibur : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vertex of Excalibur");
        Tooltip.SetDefault("Deals more damage to enemies affected by a debuff\n'The unification of dark and light'");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 90;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.2f;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 18;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 9, 63, 0);
        Item.useAnimation = 18;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(3))
        {
            int num313 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Pink);
            Main.dust[num313].noGravity = true;
            Main.dust[num313].fadeIn = 1.25f;
            Main.dust[num313].velocity *= 0.25f;
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.NightsEdge)
            .AddIngredient(ItemID.Excalibur)
            .AddIngredient(ItemID.BrokenHeroSword)
            .AddIngredient(ItemID.DarkShard)
            .AddIngredient(ItemID.LightShard)
            .AddIngredient(ItemID.LunarBar, 4)
            .AddTile(TileID.AdamantiteForge).Register();
    }
    public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
    {
        bool hasDebuff = false;
        for (int i = 0; i < target.buffType.Length; i++)
        {
            if (Main.debuff[target.buffType[i]])
            {
                hasDebuff = true;
                break;
            }
        }
        if (hasDebuff)
        {
            if (target.boss) damage = (int)(damage * 1.3);
            else damage = (int)(damage * 1.6);
        }
    }
}
