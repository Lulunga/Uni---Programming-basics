using System;
using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var pixels = new List<double>();

            foreach (var p in original)
                pixels.Add(p);
            pixels.Sort();
            whitePixelsFraction = (int)(whitePixelsFraction * pixels.Count);

            if (whitePixelsFraction > 0 && whitePixelsFraction <= pixels.Count)
                whitePixelsFraction = pixels[(int)(pixels.Count - whitePixelsFraction)];
            else if (whitePixelsFraction > pixels.Count)
                whitePixelsFraction = int.MaxValue;
            else
                whitePixelsFraction = int.MaxValue;
            return ReplacePixels(original, whitePixelsFraction);
        }

        public static double[,] ReplacePixels(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    if (original[x, y] >= whitePixelsFraction)
                        original[x, y] = 1;
                    else
                        original[x, y] = 0;
            return original;
        }
    }
}