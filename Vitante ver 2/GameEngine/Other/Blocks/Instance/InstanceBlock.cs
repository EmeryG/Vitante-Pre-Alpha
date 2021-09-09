using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vitante.GameEngine
{
    class InstanceBlock : Solid
    {
        int biome;
        int id;
        public int info;
        int x;
        int y;

        public InstanceBlock(int b, int iden, int i, int xc, int yc, Texture2D texture)
            : base(new Vector2(xc * 16, yc * 16), texture)
        {
            biome = b;
            id = iden;
            info = i;
            x = xc;
            y = yc;
            scale = 0.25f;
        }

        public String Save()
        {
            x = (int)Math.Round(position.X / 16.0);
            y = (int)Math.Round(position.X / 16.0);
            return ";" + id + ":" + biome + ":" + info + ":" + x + "," + y;
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X + texture.Width - 8, (int)position.Y + texture.Height - 8, 16, 16);
        }
    }
}
