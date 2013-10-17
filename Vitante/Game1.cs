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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D grass;
        Texture2D main;

        Vector2 past;
        List<List<Tiles>> map;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grass = Content.Load<Texture2D>("grass");
            main = Content.Load<Texture2D>("character");
            past = Vector2.Zero;
            string[] list = new string[2];
            list[0] += "Blocks:g g g";
            list[1] += "Blocks:g g ";
            map = MapLoader.GetMap(list);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W)) { past.Y += 5; }
            if (keyboard.IsKeyDown(Keys.S)) { past.Y -= 5; }
            if (keyboard.IsKeyDown(Keys.A)) { past.X += 5; }
            if (keyboard.IsKeyDown(Keys.D)) { past.X -= 5; }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Vector2 pos = Vector2.Zero;

            int a = 0;
            foreach (List<Tiles> list in map)
            {
                int b = 0;
                foreach (Tiles t in list)
                {
                    if (t == Tiles.Grass)
                    {
                        spriteBatch.Draw(grass, new Vector2(0 + past.X + b * 64, 0 + past.Y + a * 64), Color.White);
                        Console.WriteLine(new Vector2(0 + past.X + b * 64, 0 + past.Y + a * 64).X + ", " + new Vector2(0 + past.X + b * 64, 0 + past.Y + a * 64).Y);
                        Console.WriteLine(b.ToString() + ", " + past.X.ToString() + ",,," + a.ToString() + ", " + past.Y.ToString());
                    }
                    b += 1;
                }
                a += 1;
            }
            spriteBatch.Draw(main, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
