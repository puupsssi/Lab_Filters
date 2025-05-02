using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab_Filters
{
    class Erosion : MatrixFilter
    {
        public Erosion()
        {
            // Создаем структурный элемент (можно изменить на нужный)
            this.kernel = new float[,] {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 }
            };
        }

        /*protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float minR = 255;
            float minG = 255;
            float minB = 255;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    // Для erosion берем минимальное значение в окрестности
                    if (kernel[k + radiusX, l + radiusY] > 0)
                    {
                        minR = Math.Min(minR, neighborColor.R);
                        minG = Math.Min(minG, neighborColor.G);
                        minB = Math.Min(minB, neighborColor.B);
                    }
                }
            }

            return Color.FromArgb(
                Clamp((int)minR, 0, 255),
                Clamp((int)minG, 0, 255),
                Clamp((int)minB, 0, 255)
            );
        }*/
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = 1;
            int radiusY = 1;
            for (int dy = -radiusY; dy <= radiusY; dy++)
            {
                for (int dx = -radiusX; dx <= radiusX; dx++)
                {
                    int nx = Clamp(x + dx, 0, sourceImage.Width - 1);
                    int ny = Clamp(y + dy, 0, sourceImage.Height - 1);
                    Color neighbor = sourceImage.GetPixel(nx, ny);
                    if (neighbor.R == 0) return Color.Black;
                }
            }
            return Color.White;
        }


    }
}
