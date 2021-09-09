using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Vitante.Lib;
using Vitante.GameEngine;

namespace Vitante.Generator
{
    class WorldFile
    {
        string filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Vitante/Worlds";

        public WorldFile(string name)
        {
            filepath += "/" + name;

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                new Gen(this);
            }
        }

        public Region GetRegion(int x, int y, bool instance)
        {
            List<Sprite> sprites = new List<Sprite>();

            string[] lines;
            if (instance)
            {
               lines = File.ReadAllLines(filepath + "/i-" + x + "," + y + ".rgi");
            }
            else
            {
                lines = File.ReadAllLines(filepath + "/" + x + "," + y + ".rgi");
            }
           

            foreach(string s in lines) {
                sprites.Add(ProcessInfo(s, instance));
            }

            return new Region(sprites, x, y);
        }

        public Sprite ProcessInfo(string desc, Boolean instance)
        {
            char[] ia = desc.ToCharArray();

            int id = 0;
            int biome = 0;
            int info = 0;
            int x = 0;
            int y = 0;


            int stage = 0;
            for (int i = 1; i < ia.Length; i++)
            {
                if (ia[i] != ':' || ia[i] != ')' || ia[i] != ',')
                {
                    stage++;
                    string recieve = "";
                    while (ia[i] != ':' && ia[i] != ')' && ia[i] != ',')
                    {
                        recieve += ia[i];
                        if (i == ia.Length - 1)
                        {
                            break;
                        }
                        i++;
                    }

                    switch(stage) {
                        case 1:
                            id = Int32.Parse(recieve);
                            break;
                        case 2:
                            biome = Int32.Parse(recieve);
                            break;
                        case 3:
                            info = Int32.Parse(recieve);
                            break;
                        case 4:
                            x = Int32.Parse(recieve);
                            break;
                        case 5:
                            y = Int32.Parse(recieve);
                            break;
                    }
                }
            }
            if(instance) {
                return Blocks.GetIBlock(biome, id, info, x, y);
            } else {
                return Blocks.GetWBlock(biome, id, info, x, y);
            }
        }

        public void SaveRegion(Region r) {
            List<String> lines = new List<String>();

            foreach (Sprite s in r.GetObjects())
            {
                if (s is Block)
                {
                    Block b = (Block) s;
                    lines.Add(((Block)s).Save());
                }
            }

            File.WriteAllLines(filepath + "/" + r.x + "," + r.y + ".rgi", lines.ToArray());
        }

        public void SaveInstance(Region r) {
            List<String> lines = new List<String>();

            foreach (Sprite s in r.GetObjects())
            {
                if (s is InstanceBlock)
                {
                    InstanceBlock b = (InstanceBlock)s;
                    lines.Add(((InstanceBlock)s).Save());
                }
            }

            File.WriteAllLines(filepath + "/i-" + r.x + "," + r.y + ".rgi", lines.ToArray());
        }
    }
}
