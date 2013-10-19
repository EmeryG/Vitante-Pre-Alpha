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
        // Good old placeholder variables.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player main;
        Map map;
        Texture2D cursor;

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
            cursor = Content.Load<Texture2D>("cursor");

            // Tile Loading
            Texture2D[] tiles = new Texture2D[7];
            tiles[0] = new Texture2D(graphics.GraphicsDevice, 64, 64);
            tiles[1] = Content.Load<Texture2D>("grass");
            tiles[2] = Content.Load<Texture2D>("icebrick");
            Blocks.SetTextures(tiles);

            // Loading character
            main = new Player(Content.Load<Texture2D>("character"), new Vector2(64, 64));

            // Makes a fake file for map processing.
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

            main.Update(Keyboard.GetState(), graphics, map);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Vector2 pos = Vector2.Zero;

            // Draws Map, according to relavance coord.
            int a = 0;
            foreach (List<Tile> list in map.GetMain())
            {
                int b = 0;
                foreach (Tile t in list)
                {
                    spriteBatch.Draw(Blocks.GetTexture(t), new Vector2(0 + main.GetRelevance().X + b * 64, 0 + main.GetRelevance().Y + a * 64), Color.White);
                    b += 1;
                }
                a += 1;
            }

            // Draws Character (for now)
            spriteBatch.Draw(
                main.GetTexture(), 
                new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2), 
                null,
                Color.White,
                main.GetRotation(),
                new Vector2(main.GetTexture().Width / 2, main.GetTexture().Height / 2),
                1.0f,
                SpriteEffects.None,
                1.0f);

            spriteBatch.Draw(cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            
            // Ends spriteBatch, draws a game.
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
