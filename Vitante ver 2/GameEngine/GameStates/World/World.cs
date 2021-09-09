using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Vitante.Generator;
using Microsoft.Xna.Framework.Graphics;

namespace Vitante.GameEngine
{
    class World : GameState
    {
        List<Region> chunks = new List<Region>();
        WorldFile world = new WorldFile("Test");
        public World()

        {

            player = new Player(new Vector2(5100, 5100), GameMain.GetContent().Load<Texture2D>("Character"));

            for (int a = -2; a <= 2; a++)
            {
                for (int b = -2; b <= 2; b++)
                {
                    chunks.Add(world.GetRegion(a + (int)Math.Round(player.GetReference().X / 20.0 / 16.0), b + (int)Math.Round(player.GetReference().Y / 20.0 / 16.0), false));
                }
            }

            foreach (Region c in chunks)
            {
                bot.AddRange(c.GetObjects());
            }
        }

        public override void Update()
        {

            base.Update();

            int x = (int)Math.Round(player.GetReference().X / 20.0 / 16.0);
            int y = (int)Math.Round(player.GetReference().Y / 20.0 / 16.0);
            bool change = false;
            
            List<Region> cs = new List<Region>();
            cs.AddRange(chunks);
            foreach (Region c in chunks)
            {
                if (Math.Abs(x - c.x) > 1 || Math.Abs(y - c.y) > 1)
                {
                    cs.Remove(c);
                    foreach (Sprite s in bot.ToArray())
                    {
                        if (c.GetObjects().Contains(s))
                        {
                            bot.Remove(s);
                        }
                    }
                    change = true;
                }
            }
            if (change)
            {
                for (int a = -2; a <= 2; a++)
                {
                    for (int b = -2; b <= 2; b++)
                    {
                        bool create = true;
                        foreach (Region c in cs)
                        {
                            if (c.x == x + b && c.x == y + b)
                            {
                                create = false;
                                break;
                            }
                        }

                        if (create)
                        {
                            Region newc = world.GetRegion(a + (int)Math.Round(player.GetReference().X / 20.0 / 16.0), b + (int)Math.Round(player.GetReference().Y / 20.0 / 16.0), false);
                            chunks.Add(newc);
                            bot.AddRange(newc.GetObjects());
                            

                        }
                    }
                }
            }
            chunks = cs;
        }
    }
}
