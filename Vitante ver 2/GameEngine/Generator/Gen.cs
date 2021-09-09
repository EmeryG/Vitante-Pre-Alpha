using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitante.GameEngine;
using Microsoft.Xna.Framework;
using Vitante.Lib;

namespace Vitante.Generator
{
    class Gen
    {
        WorldFile file;
        Biome[] biomes = new Biome[4];

        public Gen(WorldFile f)
        {
            file = f;
            biomes[0] = new Snow();
            biomes[1] = new Forest();
            biomes[2] = new Plains();
            biomes[3] = new Desert();
            GenOverWorld(.3, 3);
        }

        public void GenInstance(double p, int rep, int size, Random r, int biome, int xc, int yc)
        {
            double[] white = PerlinNoise1D.WhiteNoise(new double[size]);
            double[] pn = PerlinNoise1D.GetPerlinNoise(white, 3, .4);
            for (int i = 0; i < rep; i++)
            {
                for (int x = 0; x < pn.Length; x++)
                {
                    pn[x] *= 5;
                }
                pn = PerlinNoise1D.GetPerlinNoise(pn, 3, p);
            }


            int[] region = new int[size];
            for (int x = 0; x < size; x++)
            {
                region[x] = (int)Math.Floor(4 * pn[x]);
            }

            List<Sprite> bs = new List<Sprite>();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < region[x] + 15; y++)
                {
                    bs.Add(Blocks.GetIBlock(biome, 4, 0, x, y));

                }

                for (int y = region[x] + 15; y < region[x] + 20; y++)
                {
                    bs.Add(Blocks.GetIBlock(biome,  biomes[biome].GetBlock(r), 0, x, y));
                }
            }
            file.SaveInstance(new Region(bs, xc, yc));
        }

        private void GenOverWorld(double p, int rep)
        {
            double[][] pn = PerlinNoise2D.WhiteNoise(2000, 2000);
            pn = PerlinNoise2D.GetPerlinNoise(pn, 8, .3);
            for (int i = 0; i < rep; i++)
            {
                for (int x = 0; x < 2000; x++)
                {
                    for (int y = 0; y < 2000; y++)
                    {
                        pn[x][y] *= 5;
                    }
                }
                pn = PerlinNoise2D.GetPerlinNoise(pn, 3, p);
            }

            Random r = new Random();

            for (int a = 0; a < 2000; a += 20)
            {
                for (int b = 0; b < 2000; b += 20)
                {
                    List<Sprite> bs = new List<Sprite>();
                    for (int x = a; x < a + 20; x++)
                    {
                        for (int y = b; y < b + 20; y++)
                        {
                            int biome = (int)Math.Floor(3.4 * pn[x][y]);
                            int id = 4;

                            if (r.Next(10000) == 0)
                            {
                                GenInstance(.3, 2, 100, r, biome, x, y);
                            }
                            else
                            {
                                id = biomes[biome].GetBlock(r);
                            }

                            bs.Add(Blocks.GetWBlock(biome, id, 0, x, y));
                        }
                    }
                    file.SaveRegion(new Region(bs, a/20, b/20));
                }
            }
            
        }
    }
}
