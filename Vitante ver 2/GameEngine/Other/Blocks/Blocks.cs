using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vitante.Generator;
using Vitante.GameEngine;

namespace Vitante.Lib
{

    abstract class Blocks {
        public static Sprite GetIBlock(int biome, int id, int info, int xc, int yc)
        {
            switch (id)
            {
                case 0:
                    return new InstanceBlock(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Grass"));
                case 1:
                    return new InstanceBlock(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Sand"));
                case 2:
                    return new InstanceBlock(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Snow"));
                case 3:
                    return new InstanceBlock(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Tree"));
                case 4:
                    return new InstanceBlock(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Water"));
                default:
                    return null;
            }
        }

        public static Sprite GetWBlock(int biome, int id, int info, int xc, int yc)
        {
            switch (id)
            {
                case 0:
                    return new Block(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Grass"), false);
                case 1:
                    return new Block(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Sand"), false);
                case 2:
                    return new Block(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Snow"), false);
                case 3:
                    return new Block(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Tree"), true);
                case 4:
                    return new Block(biome, id, info, xc, yc, GameMain.GetContent().Load<Texture2D>("Water"), false);
                default:
                    return null;
            }
        }
    }
}
