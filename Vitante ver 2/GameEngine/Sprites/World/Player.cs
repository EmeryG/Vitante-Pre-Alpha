using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Vitante.GameEngine 
{
    class Player : Solid, Entity
    {
        private Vector2 reference;

        public Player(Vector2 pos, Texture2D tex)
            : base(Vector2.Zero, tex)
        {
            reference = pos;
            scale = 0.375f;
        }

        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                 reference.Y -= 2;
                if (CheckCollision())
                {
                    reference.Y += 2;
                }
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                reference.Y += 2;
                if (CheckCollision())
                {
                    reference.Y -= 2;
                }
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                reference.X += 2;
                if (CheckCollision())
                {
                    reference.X -= 2;
                }
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                reference.X -= 2;
                if (CheckCollision())
                {
                    reference.X += 2;
                }
            }

            rotation = (float) Math.PI * 0 / 180.0f;
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle((int)reference.X, (int)reference.Y, 12, 12);
        }

        public Vector2 GetReference()
        {
            return reference;
        }
    }
}
