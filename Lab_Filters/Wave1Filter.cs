using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    internal class Wave1Filter
    {
        public static Bitmap DoWaweX(Bitmap sourceImage)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            Bitmap newImage = new Bitmap(width, height);
            for (int k = 0; k < width; k++)
            {
                for (int l = 0; l < height; l++)
                {
                    int newX = (int)(k + 20 * Math.Sin(2 * Math.PI * l / 60));
                    int newY = l;

                    // Проверяем, чтобы новые координаты находились в пределах изображения
                    if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                    {
                        Color pixelColor = sourceImage.GetPixel(newX, newY);
                        newImage.SetPixel(k, l, pixelColor);
                    }
                }
            }
            return newImage;
        }
    }
}
