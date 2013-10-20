using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Vitante
{
    class Player : Mob
    {
        public Player(Texture2D t)
            : base(t)
        { }

        public Player(Texture2D t, Vector2 coords)
            : base(t, coords)
        { }

        public Vector2 GetRelevance()
        {
            return relevance;
        }

        public void Update(KeyboardState keyboard, GraphicsDeviceManager graphics, Map map)
        {
            coord.X = graphics.GraphicsDevice.Viewport.Width / 2 - relevance.X;
            coord.Y = graphics.GraphicsDevice.Viewport.Height / 2 - relevance.Y;
            if (keyboard.IsKeyDown(Keys.W)) { relevance.Y += 5; rotation = (float)Math.PI * 180f / 180f; }
            if (keyboard.IsKeyDown(Keys.S)) { relevance.Y -= 5; rotation = (float)Math.PI * 0f / 180f; }
            if (keyboard.IsKeyDown(Keys.A)) { relevance.X += 5; rotation = (float)Math.PI * 90f / 180f; }
            if (keyboard.IsKeyDown(Keys.D)) { relevance.X -= 5; rotation = (float)Math.PI * 270f / 180f; }

            // Advanced Rotation
            if (keyboard.IsKeyDown(Keys.W) && keyboard.IsKeyDown(Keys.S))
            {
                rotation = (float)Math.PI * 0f / 180f;
            }
            else if ((keyboard.IsKeyDown(Keys.A) && keyboard.IsKeyDown(Keys.W)))
            {
                rotation = (float)Math.PI * 135f / 180f;
            }
            else if ((keyboard.IsKeyDown(Keys.D) && keyboard.IsKeyDown(Keys.W)))
            {
                rotation = (float)Math.PI * 225 / 180f;
            }
            else if ((keyboard.IsKeyDown(Keys.S) && keyboard.IsKeyDown(Keys.A)))
            {
                rotation = (float)Math.PI * 45f / 180f;
            }
            else if ((keyboard.IsKeyDown(Keys.S) && keyboard.IsKeyDown(Keys.D)))
            {
                rotation = (float)Math.PI * 315f / 180f;
            }

            // Collision Detection. Checks the tile at a location, then it checks if the tile is solid, and undos the movement if it is.
            if (Blocks.Solid(map.GetTile(new Vector2(coord.X - texture.Width / 2,
                    coord.Y - texture.Height / 2))) &&
                Blocks.Solid(map.GetTile(new Vector2(coord.X + texture.Width / 2,
                    coord.Y - texture.Height / 2))))
            { relevance.Y += -5; }

            if (Blocks.Solid(map.GetTile(new Vector2(coord.X - texture.Width / 2,
                    coord.Y + texture.Height / 2))) &&
                Blocks.Solid(map.GetTile(new Vector2(coord.X + texture.Width / 2,
                    coord.Y + texture.Height / 2))))
            {
                relevance.Y -= -5;
            }
            if (Blocks.Solid(map.GetTile(new Vector2(coord.X - texture.Width / 2,
                                coord.Y - texture.Height / 2))) &&
                            Blocks.Solid(map.GetTile(new Vector2(coord.X - texture.Width / 2,
                                coord.Y + texture.Height / 2))))
            { relevance.X += -5; }

            if (Blocks.Solid(map.GetTile(new Vector2(coord.X + texture.Width / 2,
                      coord.Y - texture.Height / 2))) &&
                  Blocks.Solid(map.GetTile(new Vector2(coord.X + texture.Width / 2,
                      coord.Y + texture.Height / 2))))
            { relevance.X -= -5; }
        }
    }
}
