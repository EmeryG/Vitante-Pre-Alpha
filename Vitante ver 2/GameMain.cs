using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vitante.GameEngine;

namespace Vitante {
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static Vector2 center;
        static ContentManager content;

        public GameMain()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            content = Content;
            content.RootDirectory = "Content/Assets";
        }

        public static ContentManager GetContent()
        {
            return content;
        }

        protected override void Initialize() { 
            base.Initialize();
            center = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            GameStateManager.SetState(new World()); // Filler
        }

        protected override void LoadContent() { spriteBatch = new SpriteBatch(GraphicsDevice); }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            GameStateManager.GetState().Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Orange);

            spriteBatch.Begin();

            Player p = GameStateManager.GetState().DrawPlayer();

            foreach (Sprite sprite in GameStateManager.GetState().DrawBot())
            {
                spriteBatch.Draw(sprite.GetTexture(), center + (p.GetReference()*new Vector2(-1, -1)) + sprite.GetPosition(), null, Color.White, sprite.GetRotation(), sprite.GetCenter(), sprite.GetScale(), sprite.GetFlip(), 1.0f);
            }

            foreach (Sprite sprite in GameStateManager.GetState().DrawTop())
            {
                if (sprite == (Sprite)p)
                {
                    spriteBatch.Draw(sprite.GetTexture(), center, null, Color.White, sprite.GetRotation(), sprite.GetCenter(), sprite.GetScale(), sprite.GetFlip(), 1.0f);
                }
                else {
                    spriteBatch.Draw(sprite.GetTexture(), center + p.GetReference() * -1 + sprite.GetPosition(), null, Color.White, sprite.GetRotation(), sprite.GetCenter(), sprite.GetScale(), sprite.GetFlip(), 1.0f);
                }
                
            }
                
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
