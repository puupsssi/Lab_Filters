using System;
using System.Drawing;
using System.ComponentModel;

namespace Lab_Filters
{
    internal class BlackHat : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null) return null;

            // Шаг 0: Бинаризация
            Bitmap binarized = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int x = 0; x < sourceImage.Width; x++)
                for (int y = 0; y < sourceImage.Height; y++)
                    binarized.SetPixel(x, y, Binarize(sourceImage.GetPixel(x, y)));

            // 1. Closing = Dilation → Erosion
            Bitmap closedImage = ApplyClosing(binarized, worker);
            if (closedImage == null || worker.CancellationPending)
            {
                closedImage?.Dispose();
                binarized.Dispose();
                return null;
            }

            // 2. Black Hat = Closed - Original (логически)
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            for (int x = 0; x < sourceImage.Width; x++)
            {
                worker.ReportProgress((int)((float)x / sourceImage.Width * 100));
                if (worker.CancellationPending)
                {
                    closedImage.Dispose();
                    binarized.Dispose();
                    resultImage.Dispose();
                    return null;
                }

                for (int y = 0; y < sourceImage.Height; y++)
                {
                    Color original = binarized.GetPixel(x, y);
                    Color closed = closedImage.GetPixel(x, y);

                    bool isOriginalBlack = original.R < 128;
                    bool isClosedWhite = closed.R >= 128;

                    Color result = (isOriginalBlack && isClosedWhite) ? Color.White : Color.Black;

                    resultImage.SetPixel(x, y, result);
                }
            }

            closedImage.Dispose();
            binarized.Dispose();
            return resultImage;
        }

        private Color Binarize(Color c)
        {
            int avg = (c.R + c.G + c.B) / 3;
            return avg > 127 ? Color.White : Color.Black;
        }


        private Bitmap ApplyClosing(Bitmap source, BackgroundWorker worker)
        {
            // Применяем Dilation
            Dilation dilation = new Dilation();
            Bitmap dilated = dilation.processImage(source, worker);
            if (dilated == null || worker.CancellationPending)
                return null;

            // Применяем Erosion к результату дилатации
            Erosion erosion = new Erosion();
            Bitmap result = erosion.processImage(dilated, worker);
            dilated.Dispose();
            return result;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Этот метод используется только в других фильтрах, здесь он не нужен
            return sourceImage.GetPixel(x, y);
        }

        private int Clamp(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}
