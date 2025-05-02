using System;
using System.ComponentModel;
using System.Drawing;

namespace Lab_Filters
{
    internal class DopZad : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null)
                return null;

            // Создаем новый объект изображения для результата
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            // Находим середину по ширине
            int midWidth = sourceImage.Width / 2;

            for (int y = 0; y < sourceImage.Height; y++)
            {
                worker.ReportProgress((int)((float)y / sourceImage.Height * 100));
                if (worker.CancellationPending)
                {
                    resultImage.Dispose();
                    return null;
                }

                // Обрабатываем левую и правую части
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    if (x < midWidth)
                    {
                        // Для левой части зеркалим по горизонтали
                        int mirrorX = midWidth - (x - 0);
                        resultImage.SetPixel(x, y, sourceImage.GetPixel(mirrorX, y));
                    }
                    else
                    {
                        // Для правой части оставляем оригинал
                        resultImage.SetPixel(x, y, sourceImage.GetPixel(x, y));
                    }
                }
            }

            return resultImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            return sourceImage.GetPixel(x, y);
        }
    }
}
