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
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D main;
        Vector2 past;
        Map map;

        public Game()
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

            Texture2D[] tiles = new Texture2D[7];
            tiles[0] = new Texture2D(graphics.GraphicsDevice, 64, 64);
            tiles[1] = Content.Load<Texture2D>("grass");
            tiles[2] = Content.Load<Texture2D>("icebrick");
            Blocks.SetTextures(tiles);

            main = Content.Load<Texture2D>("character");
            past = Vector2.Zero;
            past.X =+ 64;
            past.Y =+ 64;
            string[] list = new string[6];
            list[0] += "Blocks:iiiiiii";
            list[1] += "Blocks:iggiggi";
            list[2] += "Blocks:iggiggi";
            list[3] += "Blocks:iggiigi";
            list[4] += "Blocks:igggggi";
            list[5] += "Blocks:iggiiii";

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
            
            if (keyboard.IsKeyDown(Keys.W) && Blocks.Solid(map.GetTile(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - past.X, graphics.GraphicsDevice.Viewport.Height / 2 - past.Y)))) { past.Y += -5; }
            if (keyboard.IsKeyDown(Keys.S) && Blocks.Solid(map.GetTile(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - past.X, graphics.GraphicsDevice.Viewport.Height / 2 - past.Y)))) { past.Y -= -5; }
            if (keyboard.IsKeyDown(Keys.A) && Blocks.Solid(map.GetTile(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - past.X, graphics.GraphicsDevice.Viewport.Height / 2 - past.Y)))) { past.X += -5; }
            if (keyboard.IsKeyDown(Keys.D) && Blocks.Solid(map.GetTile(new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - past.X, graphics.GraphicsDevice.Viewport.Height / 2 - past.Y)))) { past.X -= -5; }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Vector2 pos = Vector2.Zero;

            int a = 0;
            foreach (List<Tile> list in map.GetMain())
            {
                int b = 0;
                foreach (Tile t in list)
                {
                    spriteBatch.Draw(Blocks.GetTexture(t), new Vector2(0 + past.X + b * 64, 0 + past.Y + a * 64), Color.White);
                    if (b == 0 && a == 0)
                    {
                        
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
