using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vitante.Generator
{
    class PerlinNoise1D
    {
        public static double[] WhiteNoise(double[] array)
        {
            Random r = new Random();
            for (int x = 0; x < array.Length; x++)
            {
                if (array[x] == 0)
                {
                    array[x] = r.NextDouble();
                }
            }
            return array;
        }

        private static double Interpolate(double a, double b, double x)
        {
            double ft = x * Math.PI;
            double f = (1 - Math.Cos(ft)) * 0.5;
            return a * (1 - f) + b * f;
        }

        private static double[] SmoothNoise(double[] array, int octave)
        {
            double[] smoothNoise = new double[array.Length];
            double frequency = Math.Pow(2, octave);

            
            for (int x = 1; x < array.Length - 1; x++)
            {
                int sample = (int)Math.Floor((double)x / frequency) * (int)frequency;
                double xblend = ((double)x * frequency) - Math.Floor((double)x * frequency);

                double left = Interpolate(array[x-1], array[x], xblend);
                double right = Interpolate(array[x], array[x+1], xblend);
                
                smoothNoise[x] = Interpolate(left, right, xblend);
            }

            smoothNoise[0] = (array[0] + array[1]) / 2;
            

            double xb = ((double)array.Length - 1 * frequency) - Math.Floor((double)array.Length - 1 * frequency);
            smoothNoise[array.Length - 1] = Interpolate(array[array.Length - 2], array[array.Length - 1], xb);


            return smoothNoise;
        }

        public static double[] GetPerlinNoise(double[] array, int octaves, double p)
        {
            double[] perlinNoise = new double[array.Length];
            double a = 1.0;
            double ta = 1.0;

            for (int octave = octaves - 1; octave >= 0; octave--)
            {
                a *= p;
                ta += a;

                double[] smoothNoise = SmoothNoise(array, octave);
                for (int x = 0; x < smoothNoise.Length; x++)
                {
                    perlinNoise[x] += smoothNoise[x] * a;

                }
            }

            for (int x = 0; x < perlinNoise.Length; x++)
            {
                perlinNoise[x] /= ta;
                    
            }

            array = new double[array.Length * 2 - 1];
            for (int x = 0; x < array.Length; x += 2)
            {
                array[x] = perlinNoise[x / 2];
            }

            for (int x = 1; x < array.Length-1; x += 2)
            {
                array[x] = (Interpolate(array[x - 1], array[x + 1], (array[x - 1] + array[x + 1])/2) + (array[x - 1] + array[x + 1])/2)/2;
            }

            return array;
        }
    }
}
