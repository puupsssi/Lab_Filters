using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab_Filters
{
    class Dilation : MatrixFilter
    {
        public Dilation()
        {
            // Создаем структурный элемент (можно изменить на нужный)
            this.kernel = new float[,] {
                { 0, 1, 0 },
                { 1, 1, 1 },
                { 0, 1, 0 }
            };
        }

        /*protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float maxR = 0;
            float maxG = 0;
            float maxB = 0;

            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    // Для dilation берем максимальное значение в окрестности
                    if (kernel[k + radiusX, l + radiusY] > 0)
                    {
                        maxR = Math.Max(maxR, neighborColor.R);
                        maxG = Math.Max(maxG, neighborColor.G);
                        maxB = Math.Max(maxB, neighborColor.B);
                    }
                }
            }

            return Color.FromArgb(
                Clamp((int)maxR, 0, 255),
                Clamp((int)maxG, 0, 255),
                Clamp((int)maxB, 0, 255)
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
                    if (neighbor.R == 255) return Color.White;
                }
            }
            return Color.Black;
        }


    }
}
