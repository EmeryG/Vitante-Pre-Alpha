using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Vitante.GameEngine
{
    class GameState
    {
        protected List<Sprite> top = new List<Sprite>();
        protected List<Sprite> bot = new List<Sprite>();
        protected Player player;

        public virtual void Update()
        {
            player.Update();

            foreach(Sprite e in top) {
                if (e is Entity)
                {
                    ((Entity) e).Update();
                }
            }

            foreach (Sprite e in bot)
            {
                if (e is Entity)
                {
                    ((Entity) e).Update();
                }
            }
        }

        public Player DrawPlayer()
        {
            return player;
        }

        public List<Sprite> DrawTop()
        {
            List<Sprite> a = new List<Sprite>();
            a.AddRange(top);
            a.Add(player);
            return a;
        }

        public List<Sprite> DrawBot()
        {
            return bot;
        }

        public List<Solid> GetSolids()
        {
            List<Solid> solids = new List<Solid>();

            foreach (Sprite s in top)
            {
                if (s is Solid)
                {
                    solids.Add((Solid) s);
                }
            }

            foreach (Sprite s in bot)
            {
                if (s is Solid)
                {
                    solids.Add((Solid) s);
                }
            }

            return solids;
        }
    }
}
