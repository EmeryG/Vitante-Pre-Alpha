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
            List<Movement> moves = new List<Movement>();
            if (keyboard.IsKeyDown(Keys.W)) { moves.Add(Movement.Up); }
            if (keyboard.IsKeyDown(Keys.S)) { moves.Add(Movement.Down); }
            if (keyboard.IsKeyDown(Keys.A)) { moves.Add(Movement.Left); }
            if (keyboard.IsKeyDown(Keys.D)) { moves.Add(Movement.Right); }
            Move(moves, map);
        }
    }
}
