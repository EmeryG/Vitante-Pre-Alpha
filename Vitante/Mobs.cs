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
    enum Movement
    {
        Up,
        Down,
        Left,
        Right
    };

    class Mob
    {
        protected Boolean flying = false;
        protected int attk = 0;
        protected Texture2D texture;
        protected Vector2 relevance = Vector2.Zero;
        protected Vector2 coord = Vector2.Zero;
        protected float rotation = 0f;
        protected int health = 100;
        protected Stats stats = new Stats(1, 5, 0);
        protected int range = 96;

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

        public void SetHealth(int h)
        { health = Math.Abs(h); }

        public int GetHealth()
        { return health; }

        public void Move(List<Movement> m, Map map)
        {
            if (m.Contains(Movement.Up)) { relevance.Y += 5; rotation = (float)Math.PI * 180f / 180f; }
            if (m.Contains(Movement.Down)) { relevance.Y -= 5; rotation = (float)Math.PI * 0f / 180f; }
            if (m.Contains(Movement.Left)) { relevance.X += 5; rotation = (float)Math.PI * 90f / 180f; }
            if (m.Contains(Movement.Right)) { relevance.X -= 5; rotation = (float)Math.PI * 270f / 180f; }

            // Advanced Rotation
            if (m.Contains(Movement.Up) && m.Contains(Movement.Down))
            {
                rotation = (float)Math.PI * 0f / 180f;
            }
            else if (m.Contains(Movement.Left) && m.Contains(Movement.Up))
            {
                rotation = (float)Math.PI * 135f / 180f;
            }
            else if (m.Contains(Movement.Right) && m.Contains(Movement.Up))
            {
                rotation = (float)Math.PI * 225 / 180f;
            }
            else if (m.Contains(Movement.Down) && m.Contains(Movement.Left))
            {
                rotation = (float)Math.PI * 45f / 180f;
            }
            else if (m.Contains(Movement.Down) && m.Contains(Movement.Right))
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

        public Boolean Damage(Stats s)
        { health -= s.GetStr() - stats.GetDef(); return health > 0; }
    }

    class Monster : Mob
    {
        Mob target = null;
        Mob lastDamager = null;

        public Monster(Texture2D t)
            : base(t)
        { }

        public Monster(Texture2D t, Vector2 coords)
            : base(t, coords)
        { }

        public void Update(Map m)
        {
            if(attk < 0)
                attk--;

            List<Mob> pts = new List<Mob>();
            foreach (Mob mob in pts) // fix this line in the future
            {
                if (!(mob is Monster))
                {
                    if (Math.Sqrt(Math.Pow(getPos().X - mob.getPos().X, 2) + Math.Pow(getPos().Y - mob.getPos().Y, 2)) < 512)
                    {
                        pts.Add(mob);
                    }
                }
            }

            if (!pts.Contains(lastDamager))
            {
                foreach (Mob pt in pts)
                {
                    if (!(pt.GetHealth() > health * 2))
                    {
                        target = pt;
                        break;
                    }
                }
            }
            else
            {
                target = lastDamager;
            }

            if (attk == 0 && Math.Sqrt(Math.Pow(getPos().X - target.getPos().X, 2) + Math.Pow(getPos().Y - target.getPos().Y, 2)) < range)
            {
                target.Damage(stats);
            }
            List<Movement> move = new List<Movement>();

            if (getPos().X - target.getPos().X <= 0)
            {
                move.Add(Movement.Left);
            }
            else
            {
                move.Add(Movement.Right);
            }
            if (getPos().Y - target.getPos().Y <= 0)
            {
                move.Add(Movement.Up);
            }
            else
            {
                move.Add(Movement.Down);
            }
            Move(move, m);
        }

    }
}
