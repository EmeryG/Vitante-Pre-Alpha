using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Vitante.GameEngine 
{
    class InstancePlayer : Solid, Entity
    {
        private Vector2 reference;
        private float velocity;

        public InstancePlayer(Vector2 pos, Texture2D tex)
            : base(Vector2.Zero, tex)
        {
            reference = pos;
            scale = 0.375f;
        }

        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.W) && velocity == -1)
            {
                velocity = 0;
                velocity += 30;
            }

            velocity -= 2;
            reference.Y += velocity;
            if (CheckCollision())
            {
                reference.Y -= velocity;
                velocity = -1;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                flip = SpriteEffects.FlipHorizontally;
                reference.X += 5;
                if (CheckCollision())
                {
                    reference.X -= 5;
                }
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                flip = SpriteEffects.None;
                reference.X -= 5;
                if (CheckCollision())
                {
                    reference.X += 5;
                }
            }

            rotation = (float) Math.PI * 0 / 180.0f;
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle((int)reference.X + 6, (int)reference.Y + 6, 12, 12);
        }

        public Vector2 GetReference()
        {
            return reference;
        }
    }
}
