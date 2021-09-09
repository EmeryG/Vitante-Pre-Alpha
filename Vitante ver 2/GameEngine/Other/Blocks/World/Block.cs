using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vitante.GameEngine
{
    class Block : Solid
    {
        int biome;
        int id;
        public int info;
        int x;
        int y;
        bool solid;

        public Block(int b, int iden, int i, int xc, int yc, Texture2D texture, bool s) : base(new Vector2(xc * 16, yc * 16), texture)
        {
            solid = s;
            biome = b;
            id = iden;
            info = i;
            x = xc;
            y = yc;
            scale = 0.25f;
        }

        public String Save()
        {
            x = (int) Math.Round(position.X/16.0);
            y = (int)Math.Round(position.Y / 16.0);
            return ";" + id + ":" + biome + ":" + info + ":" + x + "," + y;
        }

        public override Rectangle GetRectangle()
        {
            if (solid)
            {
                Vector2 center = GetCenter();
                return new Rectangle((int)position.X, (int)position.Y, 16, 16);
            }
            else
            {
                return new Rectangle(-1000, -1000, 1, 1);
            }
            
        }

    }
}
