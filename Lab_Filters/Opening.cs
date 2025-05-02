using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace Lab_Filters
{
    class Opening : Filters
    {
        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if (sourceImage == null) return null;

            // Применяем эрозию
            Erosion erosion = new Erosion();
            Bitmap eroded = erosion.processImage(sourceImage, worker);

            if (worker.CancellationPending || eroded == null)
            {
                eroded?.Dispose();
                return null;
            }

            // Применяем дилатацию
            Dilation dilation = new Dilation();
            Bitmap result = dilation.processImage(eroded, worker);

            eroded.Dispose();
            return result;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            // Для составных фильтров этот метод не используется
            return sourceImage.GetPixel(x, y);
        }
    }
}
