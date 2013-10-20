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
    class Mob
    {
        protected Boolean flying = false;
        protected Texture2D texture;
        protected Vector2 relevance = Vector2.Zero;
        protected Vector2 coord = Vector2.Zero;
        protected float rotation = 0f;

        public Mob(Texture2D t)
        { texture = t; }

        public Mob(Texture2D t, Vector2 coords)
        { texture = t; relevance = coords; coord = coords; }

        public Vector2 getPos()
        { return coord; }

        public Texture2D GetTexture()
        { return texture; }

        public float GetRotation()
        { return rotation; }
    }

    class Monster : Mob
    {
        Mob target = null;

        public Monster(Texture2D t) : base(t)
        { }

        public Monster(Texture2D t, Vector2 coords) : base(t, coords)
        { }

        public void Update(List<Mob> mobs)
        {
            List<Mob> targets = new List<Mob>();
            foreach (Mob mob in mobs)
            {
                if (!(mob is Monster))
                {
                    if (Math.Sqrt(Math.Pow(getPos().X - mob.getPos().X, 2) + Math.Pow(getPos().Y - mob.getPos().Y, 2)) < 256)
                    {
                        targets.Add(mob);
                    }
                }
            }
        }
        
    }
}
