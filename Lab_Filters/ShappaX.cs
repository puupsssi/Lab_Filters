using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Filters
{
    class ShappaX : MatrixFilter
    {
        public ShappaX()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];

            kernel[0, 0] = 3; kernel[0, 1] = 10; kernel[0, 2] = 3;
            kernel[1, 0] = 0; kernel[1, 1] = 0; kernel[1, 2] = 0;
            kernel[2, 0] = -3; kernel[2, 1] = -10; kernel[2, 2] = -3;
        }
    }
}