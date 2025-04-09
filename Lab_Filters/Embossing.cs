using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class Embossing : MatrixFilter
    {
        private int[,] kernel = { { 0, 1, 0,},
                                  { 1, 0, -1,},
                                  { 0, -1, 0,}
                                }; // ядро
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int kernelSize = 3;
            int radius = kernelSize / 2; // вычисление половины ядра(радиус)

            float intensity = 0;
            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    int pixelX = Clamp(x + i, 0, sourceImage.Width - 1);
                    int pixelY = Clamp(y + i, 0, sourceImage.Height - 1);

                    Color pixelColor = sourceImage.GetPixel(pixelX, pixelY);

                    int grayValue = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B); // оттенки серого
                    intensity = intensity + grayValue * kernel[i + radius, j + radius];
                }
            }
            intensity = Math.Max(0, Math.Min(255, intensity + 255));  // Переводим в полутоновое изображение 
            intensity = (float)(intensity / 2.0);

            return Color.FromArgb((int)intensity, (int)intensity, (int)intensity);

        }
        private int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}
