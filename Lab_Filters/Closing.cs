using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Lab_Filters
{
    internal class Closing : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null) return null;

            // Применяем дилатацию
            Dilation dilation = new Dilation();
            Bitmap dilated = dilation.processImage(sourceImage, worker);

            if (worker.CancellationPending || dilated == null)
            {
                dilated?.Dispose();
                return null;
            }

            // Применяем эрозию
            Erosion erosion = new Erosion();
            Bitmap result = erosion.processImage(dilated, worker);

            dilated.Dispose();
            return result;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Для составных фильтров этот метод не используется
            return sourceImage.GetPixel(x, y);
        }
    }
}
