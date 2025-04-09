using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class BrightnessFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float brightness = 20.0f;//отвечает за яркость, чем больше, тем светлее
            Color sourceColor = sourceImage.GetPixel(x, y);

            int R = Clamp(sourceColor.R + brightness, 0, 255);
            int G = Clamp(sourceColor.G + brightness, 0, 255);
            int B = Clamp(sourceColor.B + brightness, 0, 255);

            return Color.FromArgb(R, G, B);
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
