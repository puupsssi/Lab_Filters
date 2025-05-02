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
        private int[,] kernel = { { 0, 1, 0 },
                                  { 1, 0, -1 },
                                  { 0, -1, 0 }
                                }; // ядро
        private const int brightnessShift = 128; // Сдвиг по яркости

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int newR = 0;
            int newG = 0;
            int newB = 0;

            // Применяем ядро тиснения
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int neighborX = x + j;
                    int neighborY = y + i;

                    // Проверка границ
                    if (neighborX < 0 || neighborX >= sourceImage.Width || neighborY < 0 || neighborY >= sourceImage.Height)
                    {
                        continue; // Пропускаем пиксели за пределами изображения
                    }

                    Color neighborColor = sourceImage.GetPixel(neighborX, neighborY);
                    int intensity = (int)(0.36 * neighborColor.R + 0.53 * neighborColor.G + 0.11 * neighborColor.B);

                    // Применяем ядро
                    int kernelValue = kernel[i + 1, j + 1];
                    newR += kernelValue * intensity;
                    newG += kernelValue * intensity;
                    newB += kernelValue * intensity;
                }
            }

            // Добавляем сдвиг по яркости
            newR += brightnessShift;
            newG += brightnessShift;
            newB += brightnessShift;

            // Нормализация значений
            newR = Clamp(newR, 0, 255);
            newG = Clamp(newG, 0, 255);
            newB = Clamp(newB, 0, 255);

            // Создаем новый цвет
            return Color.FromArgb(newR, newG, newB);
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(value, max));
        }
    }
}