using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Lab_Filters
{
    internal class Grad : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null) return null;

            // Применяем дилатацию
            Dilation dilation = new Dilation();
            Bitmap dilatedImage = dilation.processImage(sourceImage, worker);

            if (worker.CancellationPending || dilatedImage == null)
            {
                dilatedImage?.Dispose();
                return null;
            }

            // Применяем эрозию
            Erosion erosion = new Erosion();
            Bitmap erodedImage = erosion.processImage(sourceImage, worker);

            if (worker.CancellationPending || erodedImage == null)
            {
                dilatedImage.Dispose();
                erodedImage?.Dispose();
                return null;
            }

            // Вычисляем разницу между дилатацией и эрозией
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                {
                    dilatedImage.Dispose();
                    erodedImage.Dispose();
                    return null;
                }

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color dilatedColor = dilatedImage.GetPixel(i, j);
                    Color erodedColor = erodedImage.GetPixel(i, j);

                    // Вычисляем разницу между дилатацией и эрозией
                    int r = Clamp(dilatedColor.R - erodedColor.R, 0, 255);
                    int g = Clamp(dilatedColor.G - erodedColor.G, 0, 255);
                    int b = Clamp(dilatedColor.B - erodedColor.B, 0, 255);

                    resultImage.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            dilatedImage.Dispose();
            erodedImage.Dispose();
            return resultImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Для составных фильтров этот метод не используется
            return sourceImage.GetPixel(x, y);
        }
    }
}
