using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Items.Weapons.Magic;

class Ancient : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Ancient");
        Tooltip.SetDefault("Creates a sandstorm");
        SacrificeTotal = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 46;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shootSpeed = 10f;
        Item.crit += 2;
        Item.mana = 19;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 25;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.AncientSandstorm>();
        Item.UseSound = SoundID.Item34;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 25, 0, 0);
        Item.useAnimation = 25;
        Item.height = dims.Height;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float num175 = 14f;
        float num176 = (float)Main.mouseX + Main.screenPosition.X - (player.position.X + (float)player.width * 0.5f) + (float)Main.rand.Next(-10, 11);
        float num177 = (float)Main.mouseY + Main.screenPosition.Y - (player.position.Y + (float)player.height * 0.5f) + (float)Main.rand.Next(-10, 11);
        float num178 = (float)Math.Sqrt((double)(num176 * num176 + num177 * num177));
        num178 = num175 / num178;
        num176 *= num178;
        num177 *= num178;
        for (int num179 = 0; num179 < 2; num179++)
        {
            float num180 = velocity.X;
            float num181 = velocity.Y;
            num180 += (float)Main.rand.Next(-15, 16) * 0.05f;
            num181 += (float)Main.rand.Next(-15, 16) * 0.05f;
            Projectile.NewProjectile(source, position.X, position.Y, num176, num177, type, damage, knockback, player.whoAmI, 0f, 0f);
        }

        return false;
    }
    public override void HoldItem(Player player)
    {
        Item ancient = Item;
        int baseCost = 19;
        if (player.GetModPlayer<Players.ExxoEquipEffectPlayer>().AncientLessCost)
        {
            foreach (Item item in player.inventory)
            {
                if (item.type == ancient.type)
                {
                    item.mana = 9;
                    break;
                }
            }
        }
        else
        {
            foreach (Item item in player.inventory)
            {
                if (item.type == ancient.type)
                {
                    item.mana = baseCost;
                    break;
                }
            }
        }
    }
}
