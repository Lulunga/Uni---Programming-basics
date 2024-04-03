using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        private static void ChangeImage(List<double> median, double[,] image, int i, int j)
        {
            int count = median.Count;
            if (count % 2 == 0)
                image[i, j] = (median[count / 2] + median[(count / 2) - 1]) / 2;
            else image[i, j] = median[count / 2];
        }

        public static double[,] MedianFilter(double[,] original)
        {
            int width = original.GetLength(0);
            int height = original.GetLength(1);
            var image = new double[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var currentMedian = new List<double>();
                    LoopIn3x3Matrix(original, width, height, i, j, currentMedian);
                    currentMedian.Sort();
                    ChangeImage(currentMedian, image, i, j);
                }
            }
            return image;
        }

        private static void LoopIn3x3Matrix(double[,] original, int width, int height, int i, int j,
                                            List<double> currentMedian)
        {
            for (var ii = -1; ii < 2; ii++)
            {
                for (var jj = -1; jj < 2; jj++)
                {
                    bool xCondition = i + ii >= 0 && i + ii < width;
                    bool yCondition = j + jj >= 0 && j + jj < height;
                    if (xCondition && yCondition) currentMedian.Add(original[i + ii, j + jj]);
                }
            }
        }
    }
}