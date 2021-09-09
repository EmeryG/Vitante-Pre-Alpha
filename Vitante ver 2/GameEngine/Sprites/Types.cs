using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Vitante.GameEngine
{
    interface Entity 
    {
        void Update();
    }

    class Solid : Sprite
    {

        public Solid(Vector2 pos, Texture2D tex)
            : base(pos, tex)
        { }

        public virtual Rectangle GetRectangle() 
        {
            Vector2 center = GetCenter();
            return new Rectangle((int)position.X + (int)GetCenter().X, (int)position.Y + (int)GetCenter().Y, this.texture.Width, this.texture.Height); ;
        }

        protected Boolean CheckCollision()
        {
            foreach (Solid s in GameStateManager.GetState().GetSolids())
            {
                if (s == this)
                {
                    continue;
                }

                if (s.GetRectangle().Intersects(GetRectangle()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
