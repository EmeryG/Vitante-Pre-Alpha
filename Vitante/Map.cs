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
    // Tile enumeration
    enum Tile
    {
        None,
        Grass,
        IceBrick,
        StoneBrick,
        SandStone,
        Brick,
        WoodPlank
    };

    class MapLoader
    {
        public static Map GetMap(string[] lines)
        {
            List<List<Tile>> hi = new List<List<Tile>>();
            foreach (string sline in lines)
            {
                // Checks and removes the blocks starter.
                if (sline.ToLower().StartsWith("blocks:"))
                {
                    string line = sline.Substring(7);
                    // Starts processing the characters to tiles.
                    List<Tile> mc = new List<Tile>();
                    foreach (char c in line.ToCharArray())
                    {
                        switch (c)
                        {
                            case 'g':
                                mc.Add(Tile.Grass);
                                break;
                            case 'i':
                                mc.Add(Tile.IceBrick);
                                break;
                            case 's':
                                mc.Add(Tile.StoneBrick);
                                break;
                            case 't':
                                mc.Add(Tile.SandStone);
                                break;
                            case 'b':
                                mc.Add(Tile.Brick);
                                break;
                            case 'w':
                                mc.Add(Tile.WoodPlank);
                                break;
                            default:
                                mc.Add(Tile.None);
                                break;
                        }
                    }
                    hi.Add(mc);
                }
            }
            if (hi.Count == 0)
            {
                List<Tile> mc = new List<Tile>();
                mc.Add(Tile.None);
                hi.Add(mc);
            }
            return new Map(hi);
        }
    }

    class Blocks
    {
        static Texture2D none;
        static Texture2D grass;
        static Texture2D icebrick;
        static Texture2D stonebrick;
        static Texture2D sandstone;
        static Texture2D brick;
        static Texture2D woodplank;


        // Setting textures of tiles.
        static public void SetTextures(Texture2D[] tiles)
        {
            none = tiles[0];
            grass = tiles[1];
            icebrick = tiles[2];
            stonebrick = tiles[3];
            sandstone = tiles[4];
            brick = tiles[5];
            woodplank = tiles[6];
        }

        // Gets the texture of a tile.
        static public Texture2D GetTexture(Tile t)
        {
            switch (t)
            {
                case Tile.None:
                    return none;
                case Tile.Grass:
                    return grass;
                case Tile.IceBrick:
                    return icebrick;
                case Tile.SandStone:
                    return sandstone;
                case Tile.Brick:
                    return brick;
                case Tile.WoodPlank:
                    return woodplank;
                default:
                    return null;
            }
        }
            
        // Checks if a tile is solid.
        static public Boolean Solid(Tile t)
        {
            switch (t)
            {
                case Tile.IceBrick:
                    return true;
                case Tile.StoneBrick:
                    return true;
                case Tile.SandStone:
                    return true;
                case Tile.Brick:
                    return true;
                case Tile.WoodPlank:
                    return true;
                default:
                    return false;
            }
        }
    }

    class Map
    {
        List<List<Tile>> main = new List<List<Tile>>();

        public Map(List<List<Tile>> t)
        {
            main = t;
        }

        public Map() { }

        // Adds a row (x) of tiles
        public void AddLine(List<Tile> line)
        {
            main.Add(line);
        }

        // For ease of drawing
        public List<List<Tile>> GetMain()
        {
            return main;
        }

        // Get a tile at a specific coordnate
        public Tile GetTile(Vector2 point)
        {
            Tile t = Tile.None;
            try
            {
                List<Tile> a = main.ToArray()[(int)Math.Floor(point.Y / 64)];
                t = a.ToArray()[(int)Math.Floor(point.X / 64)];
            } catch(IndexOutOfRangeException) { }
            return t;
        }
    }
}
