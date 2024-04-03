using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] matrixSobelX)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var matrixSobelY = TransposeMatrix(matrixSobelX);
            var filteredPixels = new double[width, height];
            var offsetX = matrixSobelX.GetLength(0) / 2;
            var offsetY = matrixSobelX.GetLength(1) / 2;

            for (var x = offsetX; x < width - offsetX; x++)
                for (var y = offsetY; y < height - offsetY; y++)
                {
                    var gx = GetConvolution(g, matrixSobelX, x, y, offsetX);
                    var gy = GetConvolution(g, matrixSobelY, x, y, offsetY);
                    /*as per sober filter, the resulting gradient approximations can be combined
                     to give the gradient magnitude, using: g = Math.sqrt (gx^2 + gy^2)
                     the absolute magnitude is the output edges */
                    filteredPixels[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }

            return filteredPixels;
        }

        private static double GetConvolution(double[,] original, double[,] s, int x, int y, int offset)
        {
            var width = s.GetLength(0);
            var height = s.GetLength(1);
            var result = 0.0;
            for (var sx = 0; sx < width; sx++)
                for (var sy = 0; sy < height; sy++)
                    result += s[sx, sy] * original[x + sx - offset, y + sy - offset];
            return result;
        }

        // this method is just transposing the given matrix as per linear algebra rules 
        public static double[,] TransposeMatrix(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);
            var transposedMatrix = new double[columns, rows];
            for (var c = 0; c < columns; c++)
                for (var r = 0; r < rows; r++)
                    transposedMatrix[c, r] = matrix[r, c];
            return transposedMatrix;
        }
    }
}