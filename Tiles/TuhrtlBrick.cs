﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AvalonTesting.Tiles;

public class TuhrtlBrick : ModTile
{
    public override void SetStaticDefaults()
    {
        AddMapEntry(new Color(78, 70, 67));
        Main.tileSolid[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMerge[Type][TileID.WoodBlock] = true;
        Main.tileMerge[TileID.WoodBlock][Type] = true;
        Main.tileBlockLight[Type] = true;
        ItemDrop = Mod.Find<ModItem>("TuhrtlBrick").Type;
        SoundType = SoundID.Tink;
        SoundStyle = 1;
        MinPick = 210;
        DustType = DustID.Silt;
        global::AvalonTesting.MergeWith(Type, ModContent.TileType<TropicalMud>());
        global::AvalonTesting.MergeWith(Type, ModContent.TileType<TropicalGrass>());
    }
    public override bool CanExplode(int i, int j)
    {
        return false;
    }
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        global::AvalonTesting.MergeWithFrame(i, j, Type, ModContent.TileType<TropicalMud>(), false, false, false, false, resetFrame);
        return false;
    }
}
