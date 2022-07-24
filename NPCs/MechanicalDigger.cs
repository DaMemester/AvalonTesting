using System;
using Avalon.Items.Banners;
using Avalon.Items.Other;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class MechanicalDiggerHead : MechanicalDiggerWorm
{
    public override string Texture => "Avalon/NPCs/MechanicalDiggerHead";

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("MechanicalDiggerHead").Type);
        }
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot) =>
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MechanicalWhoopieCushion>(), 25));

    public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
        spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.Player.ZoneDungeon && Main.hardMode &&
        ModContent.GetInstance<AvalonTestingWorld>().SuperHardmode
            ? 0.073f * AvalonTestingGlobalNPC.EndoSpawnRate
            : 0f;

    public override void SetDefaults()
    {
        NPC.damage = 110;
        NPC.netAlways = true;
        NPC.scale = 1f;
        NPC.noTileCollide = true;
        NPC.lifeMax = 5000;
        NPC.defense = 90;
        NPC.noGravity = true;
        NPC.width = 24;
        NPC.aiStyle = -1;
        NPC.behindTiles = true;
        NPC.value = 20000f;
        NPC.height = 32;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<MechanicalDiggerBanner>();
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.55f);
    }

    public override void Init()
    {
        base.Init();
        head = true;
    }
}

public class MechanicalDiggerBody : MechanicalDiggerWorm
{
    public override string Texture => "Avalon/NPCs/MechanicalDiggerBody";

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("MechanicalDiggerBody").Type);
        }
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

    public override void SetDefaults()
    {
        NPC.damage = 90;
        NPC.netAlways = true;
        NPC.scale = 1f;
        NPC.noTileCollide = true;
        NPC.lifeMax = 5000;
        NPC.defense = 80;
        NPC.noGravity = true;
        NPC.width = 22;
        NPC.aiStyle = -1;
        NPC.behindTiles = true;
        NPC.value = 20000f;
        NPC.height = 22;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<MechanicalDiggerBanner>();
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.55f);
    }
}

public class MechanicalDiggerTail : MechanicalDiggerWorm
{
    public override string Texture => "Avalon/NPCs/MechanicalDiggerTail";

    public override void HitEffect(int hitDirection, double damage)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
                Mod.Find<ModGore>("MechanicalDiggerTail").Type);
        }
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

    public override void SetDefaults()
    {
        NPC.damage = 120;
        NPC.netAlways = true;
        NPC.scale = 1f;
        NPC.noTileCollide = true;
        NPC.lifeMax = 5000;
        NPC.defense = 70;
        NPC.noGravity = true;
        NPC.width = 24;
        NPC.aiStyle = -1;
        NPC.behindTiles = true;
        NPC.value = 20000f;
        NPC.height = 20;
        NPC.knockBackResist = 0f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<MechanicalDiggerBanner>();
    }

    public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
    {
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.55f);
    }

    public override void Init()
    {
        base.Init();
        tail = true;
    }
}

// I made this 2nd base class to limit code repetition.
public abstract class MechanicalDiggerWorm : Worm
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Mechanical Digger");
        var debuffData = new NPCDebuffImmunityData
        {
            SpecificallyImmuneTo = new[] { BuffID.Confused, BuffID.Frostburn },
        };
        NPCID.Sets.DebuffImmunitySets[Type] = debuffData;
    }

    public override void Init()
    {
        minLength = 12;
        maxLength = 18;
        tailType = ModContent.NPCType<MechanicalDiggerTail>();
        bodyType = ModContent.NPCType<MechanicalDiggerBody>();
        headType = ModContent.NPCType<MechanicalDiggerHead>();
        speed = 8.5f;
        turnSpeed = 0.07f;
    }
}

//ported from my tAPI mod because I'm lazy
// This abstract class can be used for non splitting worm type NPC.
public abstract class Worm : ModNPC
{
    public int bodyType;
    public bool directional = false;

    public bool flies = false;

    /* ai[0] = follower
     * ai[1] = following
     * ai[2] = distanceFromTail
     * ai[3] = head
     */
    public bool head;
    public int headType;
    public int maxLength;
    public int minLength;
    public float speed;
    public bool tail;
    public int tailType;
    public float turnSpeed;

    public override void AI()
    {
        if (NPC.localAI[1] == 0f)
        {
            NPC.localAI[1] = 1f;
            Init();
        }

        if (NPC.ai[3] > 0f)
        {
            NPC.realLife = (int)NPC.ai[3];
        }

        if (!head && NPC.timeLeft < 300)
        {
            NPC.timeLeft = 300;
        }

        if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
        {
            NPC.TargetClosest();
        }

        if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
        {
            NPC.timeLeft = 300;
        }

        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            if (!tail && NPC.ai[0] == 0f)
            {
                if (head)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    NPC.ai[2] = Main.rand.Next(minLength, maxLength + 1);
                    NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2)),
                        (int)(NPC.position.Y + NPC.height), bodyType, NPC.whoAmI);
                }
                else if (NPC.ai[2] > 0f)
                {
                    NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2)),
                        (int)(NPC.position.Y + NPC.height), NPC.type, NPC.whoAmI);
                }
                else
                {
                    NPC.ai[0] = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + (NPC.width / 2)),
                        (int)(NPC.position.Y + NPC.height), tailType, NPC.whoAmI);
                }

                Main.npc[(int)NPC.ai[0]].ai[3] = NPC.ai[3];
                Main.npc[(int)NPC.ai[0]].realLife = NPC.realLife;
                Main.npc[(int)NPC.ai[0]].ai[1] = NPC.whoAmI;
                Main.npc[(int)NPC.ai[0]].ai[2] = NPC.ai[2] - 1f;
                NPC.netUpdate = true;
            }

            if (!head && (!Main.npc[(int)NPC.ai[1]].active ||
                          (Main.npc[(int)NPC.ai[1]].type != headType && Main.npc[(int)NPC.ai[1]].type != bodyType)))
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
            }

            if (!tail && (!Main.npc[(int)NPC.ai[0]].active ||
                          (Main.npc[(int)NPC.ai[0]].type != bodyType && Main.npc[(int)NPC.ai[0]].type != tailType)))
            {
                NPC.life = 0;
                NPC.HitEffect();
                NPC.active = false;
            }

            if (!NPC.active && Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f);
            }
        }

        int num180 = (int)(NPC.position.X / 16f) - 1;
        int num181 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
        int num182 = (int)(NPC.position.Y / 16f) - 1;
        int num183 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
        if (num180 < 0)
        {
            num180 = 0;
        }

        if (num181 > Main.maxTilesX)
        {
            num181 = Main.maxTilesX;
        }

        if (num182 < 0)
        {
            num182 = 0;
        }

        if (num183 > Main.maxTilesY)
        {
            num183 = Main.maxTilesY;
        }

        bool flag18 = flies;
        if (!flag18)
        {
            for (int num184 = num180; num184 < num181; num184++)
            {
                for (int num185 = num182; num185 < num183; num185++)
                {
                    if (Main.tile[num184, num185] != null && ((Main.tile[num184, num185].HasUnactuatedTile &&
                                                               (Main.tileSolid[Main.tile[num184, num185].TileType] ||
                                                                (Main.tileSolidTop
                                                                     [Main.tile[num184, num185].TileType] &&
                                                                 Main.tile[num184, num185].TileFrameY == 0))) ||
                                                              Main.tile[num184, num185].LiquidAmount > 64))
                    {
                        Vector2 vector17;
                        vector17.X = num184 * 16;
                        vector17.Y = num185 * 16;
                        if (NPC.position.X + NPC.width > vector17.X && NPC.position.X < vector17.X + 16f &&
                            NPC.position.Y + NPC.height > vector17.Y && NPC.position.Y < vector17.Y + 16f)
                        {
                            flag18 = true;
                            if (Main.rand.NextBool(100) && NPC.behindTiles &&
                                Main.tile[num184, num185].HasUnactuatedTile)
                            {
                                WorldGen.KillTile(num184, num185, true, true);
                            }

                            if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].TileType == 2)
                            {
                                ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].TileType;
                            }
                        }
                    }
                }
            }
        }

        if (!flag18 && head)
        {
            var rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
            int num186 = 1000;
            bool flag19 = true;
            for (int num187 = 0; num187 < 255; num187++)
            {
                if (Main.player[num187].active)
                {
                    var rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186,
                        (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                    if (rectangle.Intersects(rectangle2))
                    {
                        flag19 = false;
                        break;
                    }
                }
            }

            if (flag19)
            {
                flag18 = true;
            }
        }

        if (directional)
        {
            if (NPC.velocity.X < 0f)
            {
                NPC.spriteDirection = 1;
            }
            else if (NPC.velocity.X > 0f)
            {
                NPC.spriteDirection = -1;
            }
        }

        float num188 = speed;
        float num189 = turnSpeed;
        var vector18 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
        float num191 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2);
        float num192 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2);
        num191 = (int)(num191 / 16f) * 16;
        num192 = (int)(num192 / 16f) * 16;
        vector18.X = (int)(vector18.X / 16f) * 16;
        vector18.Y = (int)(vector18.Y / 16f) * 16;
        num191 -= vector18.X;
        num192 -= vector18.Y;
        float num193 = (float)Math.Sqrt((num191 * num191) + (num192 * num192));
        if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
        {
            try
            {
                vector18 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
                num191 = Main.npc[(int)NPC.ai[1]].position.X + (Main.npc[(int)NPC.ai[1]].width / 2) - vector18.X;
                num192 = Main.npc[(int)NPC.ai[1]].position.Y + (Main.npc[(int)NPC.ai[1]].height / 2) - vector18.Y;
            }
            catch
            {
            }

            NPC.rotation = (float)Math.Atan2(num192, num191) + 1.57f;
            num193 = (float)Math.Sqrt((num191 * num191) + (num192 * num192));
            int num194 = NPC.width;
            num193 = (num193 - num194) / num193;
            num191 *= num193;
            num192 *= num193;
            NPC.velocity = Vector2.Zero;
            NPC.position.X = NPC.position.X + num191;
            NPC.position.Y = NPC.position.Y + num192;
            if (directional)
            {
                if (num191 < 0f)
                {
                    NPC.spriteDirection = 1;
                }

                if (num191 > 0f)
                {
                    NPC.spriteDirection = -1;
                }
            }
        }
        else
        {
            if (!flag18)
            {
                NPC.TargetClosest();
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.velocity.Y > num188)
                {
                    NPC.velocity.Y = num188;
                }

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num188 * 0.4)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - (num189 * 1.1f);
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X + (num189 * 1.1f);
                    }
                }
                else if (NPC.velocity.Y == num188)
                {
                    if (NPC.velocity.X < num191)
                    {
                        NPC.velocity.X = NPC.velocity.X + num189;
                    }
                    else if (NPC.velocity.X > num191)
                    {
                        NPC.velocity.X = NPC.velocity.X - num189;
                    }
                }
                else if (NPC.velocity.Y > 4f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + (num189 * 0.9f);
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - (num189 * 0.9f);
                    }
                }
            }
            else
            {
                if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
                {
                    float num195 = num193 / 40f;
                    if (num195 < 10f)
                    {
                        num195 = 10f;
                    }

                    if (num195 > 20f)
                    {
                        num195 = 20f;
                    }

                    NPC.soundDelay = (int)num195;
                    SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Roar_1"), NPC.position);
                }

                num193 = (float)Math.Sqrt((num191 * num191) + (num192 * num192));
                float num196 = Math.Abs(num191);
                float num197 = Math.Abs(num192);
                float num198 = num188 / num193;
                num191 *= num198;
                num192 *= num198;
                if (ShouldRun())
                {
                    bool flag20 = true;
                    for (int num199 = 0; num199 < 255; num199++)
                    {
                        if (Main.player[num199].active && !Main.player[num199].dead && Main.player[num199].ZoneCorrupt)
                        {
                            flag20 = false;
                        }
                    }

                    if (flag20)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient &&
                            NPC.position.Y / 16f > (Main.rockLayer + Main.maxTilesY) / 2.0)
                        {
                            NPC.active = false;
                            int num200 = (int)NPC.ai[0];
                            while (num200 > 0 && num200 < 200 && Main.npc[num200].active &&
                                   Main.npc[num200].aiStyle == NPC.aiStyle)
                            {
                                int num201 = (int)Main.npc[num200].ai[0];
                                Main.npc[num200].active = false;
                                NPC.life = 0;
                                if (Main.netMode == NetmodeID.Server)
                                {
                                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num200);
                                }

                                num200 = num201;
                            }

                            if (Main.netMode == NetmodeID.Server)
                            {
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI);
                            }
                        }

                        num191 = 0f;
                        num192 = num188;
                    }
                }

                bool flag21 = false;
                if (NPC.type == NPCID.WyvernHead)
                {
                    if (((NPC.velocity.X > 0f && num191 < 0f) || (NPC.velocity.X < 0f && num191 > 0f) ||
                         (NPC.velocity.Y > 0f && num192 < 0f) || (NPC.velocity.Y < 0f && num192 > 0f)) &&
                        Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) > num189 / 2f && num193 < 300f)
                    {
                        flag21 = true;
                        if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num188)
                        {
                            NPC.velocity *= 1.1f;
                        }
                    }

                    if (NPC.position.Y > Main.player[NPC.target].position.Y ||
                        Main.player[NPC.target].position.Y / 16f > Main.worldSurface || Main.player[NPC.target].dead)
                    {
                        flag21 = true;
                        if (Math.Abs(NPC.velocity.X) < num188 / 2f)
                        {
                            if (NPC.velocity.X == 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X - NPC.direction;
                            }

                            NPC.velocity.X = NPC.velocity.X * 1.1f;
                        }
                        else
                        {
                            if (NPC.velocity.Y > -num188)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189;
                            }
                        }
                    }
                }

                if (!flag21)
                {
                    if ((NPC.velocity.X > 0f && num191 > 0f) || (NPC.velocity.X < 0f && num191 < 0f) ||
                        (NPC.velocity.Y > 0f && num192 > 0f) || (NPC.velocity.Y < 0f && num192 < 0f))
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189;
                        }
                        else
                        {
                            if (NPC.velocity.X > num191)
                            {
                                NPC.velocity.X = NPC.velocity.X - num189;
                            }
                        }

                        if (NPC.velocity.Y < num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189;
                        }
                        else
                        {
                            if (NPC.velocity.Y > num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189;
                            }
                        }

                        if (Math.Abs(num192) < num188 * 0.2 && ((NPC.velocity.X > 0f && num191 < 0f) ||
                                                                (NPC.velocity.X < 0f && num191 > 0f)))
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + (num189 * 2f);
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - (num189 * 2f);
                            }
                        }

                        if (Math.Abs(num191) < num188 * 0.2 && ((NPC.velocity.Y > 0f && num192 < 0f) ||
                                                                (NPC.velocity.Y < 0f && num192 > 0f)))
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + (num189 * 2f);
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - (num189 * 2f);
                            }
                        }
                    }
                    else
                    {
                        if (num196 > num197)
                        {
                            if (NPC.velocity.X < num191)
                            {
                                NPC.velocity.X = NPC.velocity.X + (num189 * 1.1f);
                            }
                            else if (NPC.velocity.X > num191)
                            {
                                NPC.velocity.X = NPC.velocity.X - (num189 * 1.1f);
                            }

                            if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num188 * 0.5)
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num189;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189;
                                }
                            }
                        }
                        else
                        {
                            if (NPC.velocity.Y < num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + (num189 * 1.1f);
                            }
                            else if (NPC.velocity.Y > num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - (num189 * 1.1f);
                            }

                            if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num188 * 0.5)
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num189;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - num189;
                                }
                            }
                        }
                    }
                }
            }

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            if (head)
            {
                if (flag18)
                {
                    if (NPC.localAI[0] != 1f)
                    {
                        NPC.netUpdate = true;
                    }

                    NPC.localAI[0] = 1f;
                }
                else
                {
                    if (NPC.localAI[0] != 0f)
                    {
                        NPC.netUpdate = true;
                    }

                    NPC.localAI[0] = 0f;
                }

                if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) ||
                     (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) ||
                     (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) ||
                     (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
                {
                    NPC.netUpdate = true;
                    return;
                }
            }
        }

        CustomBehavior();
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => head ? null : false;

    public virtual void Init()
    {
    }

    public virtual bool ShouldRun() => false;

    public virtual void CustomBehavior()
    {
    }
}
