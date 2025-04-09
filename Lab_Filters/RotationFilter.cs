using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class RotationFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            double angle = -45.0 * Math.PI / 180.0; // Угол в радианах
            int centerX = sourceImage.Width / 2;
            int centerY = sourceImage.Height / 2;

            int newX = (int)((x - centerX) * Math.Cos(angle) - (y - centerY) * Math.Sin(angle) + centerX);
            int newY = (int)((x - centerX) * Math.Sin(angle) + (y - centerY) * Math.Cos(angle) + centerY);

            if (newX < 0 || newX >= sourceImage.Width || newY < 0 || newY >= sourceImage.Height)
                return Color.Transparent;

            return sourceImage.GetPixel(newX, newY);
        }
    }
}
