using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class Sepia : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int k = 50;
            Color sourceColor = sourceImage.GetPixel(x, y);
            float intensity = (float)(0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.114 * sourceColor.B);
            int R = Clamp((int)intensity + 2 * k, 0, 255);
            int G = Clamp((int)intensity + k / 2, 0, 255);
            int B = Clamp((int)intensity - 1 * k, 0, 255);

            Color resultColor = Color.FromArgb(R, G, B);
            return resultColor;
        }
        private int Clamp(float value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return (int)value;
        }
    }
}
