using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Lab_Filters
{
    internal class TopHat : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null) return null;

            // Применяем opening фильтр
            Opening opening = new Opening();
            Bitmap openedImage = opening.processImage(sourceImage, worker);

            if (worker.CancellationPending || openedImage == null)
            {
                openedImage?.Dispose();
                return null;
            }

            // Вычисляем разницу между исходным изображением и opened изображением
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                {
                    openedImage.Dispose();
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color sourceColor = sourceImage.GetPixel(i, j);
                    Color openedColor = openedImage.GetPixel(i, j);

                    // Вычисляем разницу между исходным и opened изображением
                    int r = Clamp(sourceColor.R - openedColor.R, 0, 255);
                    int g = Clamp(sourceColor.G - openedColor.G, 0, 255);
                    int b = Clamp(sourceColor.B - openedColor.B, 0, 255);

                    resultImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            openedImage.Dispose();
            return resultImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Для составных фильтров этот метод не используется
            return sourceImage.GetPixel(x, y);
        }
    }
}
