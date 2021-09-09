using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Vitante.GameEngine 
{
    class Sprite
    {
        protected Vector2 position;
        protected Texture2D texture;
        protected float rotation;
        protected float scale;
        protected SpriteEffects flip;

        public Sprite(Vector2 pos, Texture2D tex)
        {
            position = pos;
            texture = tex;
            flip = SpriteEffects.None;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public float GetRotation()
        {
            return rotation;
        }

        public Texture2D GetTexture()
        {
            return texture;
        }

        public float GetScale()
        {
            return scale;
        }

        public Vector2 GetCenter()
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public SpriteEffects GetFlip()
        {
            return flip;
        }
    }
}
