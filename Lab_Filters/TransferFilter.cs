using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class TransferFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int dx = 50; // Смещение по X
            int dy = 30; // Смещение по Y

            int newX = x - dx;
            int newY = y - dy;

            if (newX < 0 || newX >= sourceImage.Width || newY < 0 || newY >= sourceImage.Height)
                return Color.Transparent;

            return sourceImage.GetPixel(newX, newY);
        }
    }
}
