using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitante
{
    enum Tiles
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
        public static List<List<Tiles>> GetMap(string[] lines)
        {
            List<List<Tiles>> hi = new List<List<Tiles>>();
            foreach (string sline in lines)
            {
                if (sline.ToLower().StartsWith("blocks:"))
                {
                    string line = sline.Substring(7);
                    List<Tiles> mc = new List<Tiles>();
                    foreach (char c in line.ToCharArray())
                    {
                        switch (c)
                        {
                            case 'g':
                                mc.Add(Tiles.Grass);
                                break;
                            case 'i':
                                mc.Add(Tiles.StoneBrick);
                                break;
                            case 't':
                                mc.Add(Tiles.SandStone);
                                break;
                            case 'b':
                                mc.Add(Tiles.Brick);
                                break;
                            case 'w':
                                mc.Add(Tiles.WoodPlank);
                                break;
                            default:
                                mc.Add(Tiles.None);
                                break;
                        }
                    }
                    hi.Add(mc);
                }
            }
            if (hi.Count == 0)
            {
                List<Tiles> mc = new List<Tiles>();
                mc.Add(Tiles.None);
                hi.Add(mc);
            }
            return hi;
        }
    }
}
