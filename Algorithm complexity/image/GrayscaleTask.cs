namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            // luminance conversion formula  Y = 0.299R + 0.587G + 0.114B
            const double redLuma = 0.299;
            const double greenLuma = 0.587;
            const double blueLuma = 0.114;
            // The range for each colour is 0-255 (as 2^8 = 256 possibilities).
            const double range = 255;
            var imageWidth = original.GetLength(0);
            var imageHeight = original.GetLength(1);
            var grayscale = new double[imageWidth, imageHeight];
            for (int x = 0; x < imageWidth; x++)
            {
                for (int y = 0; y < imageHeight; y++)
                {
                    grayscale[x, y] = (redLuma * original[x, y].R
                        + greenLuma * original[x, y].G
                        + blueLuma * original[x, y].B) / range;
                }
            }
            return grayscale;
        }
    }
}