using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace AvalonTesting.Projectiles;

public class HellstoneSeed : ModProjectile
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Hellstone Seed");
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = dims.Width * 8 / 28;
        Projectile.height = dims.Height * 8 / 28 / Main.projFrames[Projectile.type];
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, (int)Projectile.position.X, (int)Projectile.position.Y, 1);
        return true;
    }
    public override void AI()
    {
        Projectile.ai[0] += 1f;
        if (Projectile.ai[0] >= 15f)
        {
            Projectile.ai[0] = 15f;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
        }
        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
}
