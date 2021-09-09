using System;
using System.Threading;

namespace Vitante.Generator {
    class PerlinNoise2D
    {
        public static double[][] WhiteNoise(int width, int height)
        {
            double[][] array = new double[width][];
            Random r = new Random();
            for (int x = 0; x < width; x++)
            {
                array[x] = new double[height];
                for (int y = 0; y < height; y++)
                {
                    array[x][y] = r.NextDouble();
                }
            }
            return array;
        }

        private static double[][] SmoothNoise(double[][] baseNoise, int octave, int width, int height)
        {
            double[][] smoothNoise = new double[width][];

            for (int x = 0; x < width; x++)
            {
                smoothNoise[x] = new double[height];
            }

            double samplePeriod = Math.Pow(2, octave);
            double sampleFrequency = 1 / samplePeriod;

            for (int x = 0; x < width; x++)
            {
                int sample_x0 = (int)Math.Floor((double)x / samplePeriod) * (int)samplePeriod;
                int sample_x1 = (int)Math.Floor(sample_x0 + samplePeriod) % width;
                double xblend = (x - sample_x0) * sampleFrequency;


                for (int y = 0; y < height; y++)
                {
                    int sample_y0 = (int)Math.Floor(y / samplePeriod) * (int)samplePeriod;
                    int sample_y1 = (int)Math.Floor(sample_y0 + samplePeriod) % height;
                    double yblend = (y - sample_y0) * sampleFrequency;

                    double top = Interpolate(baseNoise[sample_x0][sample_y0], baseNoise[sample_x1][sample_y0], xblend);
                    double bottom = Interpolate(baseNoise[sample_x0][sample_y1], baseNoise[sample_x1][sample_y1], xblend);

                    smoothNoise[x][y] = Interpolate(top, bottom, yblend);
                }
            }

            return smoothNoise;
        }

        private static double Interpolate(double a, double b, double x)
        {
            double ft = x * Math.PI;
            double f = (1 - Math.Cos(ft)) * 0.5;
            return a * (1 - f) + b * f;
        }

        public static double[][] GetPerlinNoise(double[][] baseNoise, int octaves,  double p)
        {
            int width = baseNoise.Length;
            int height = baseNoise[0].Length;

            double[][] perlinNoise = new double[width][];

            for (int x = 0; x < width; x++)
            {
                perlinNoise[x] = new double[height];
            }

            double a = 1.0;
            double ta = 1.0;

            for (int octave = octaves - 1; octave >= 0; octave--)
            {
                a *= p;
                ta += a;

                double[][] smoothNoise = SmoothNoise(baseNoise, octave, width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        perlinNoise[x][y] += smoothNoise[x][y] * a;
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    perlinNoise[x][y] /= ta;
                }
            }

            return perlinNoise;
        }
    }
}
