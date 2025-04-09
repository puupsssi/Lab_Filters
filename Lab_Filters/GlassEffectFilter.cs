using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class GlassEffectFilter : Filters
    {
        private static Random rand = new Random();

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int maxOffset = 5; // Максимальный сдвиг пикселя
            int newX = x + rand.Next(-maxOffset, maxOffset + 1);
            int newY = y + rand.Next(-maxOffset, maxOffset + 1);

            if (newX < 0 || newX >= sourceImage.Width || newY < 0 || newY >= sourceImage.Height)
                return sourceImage.GetPixel(x, y);

            return sourceImage.GetPixel(newX, newY);
        }
    }
}
