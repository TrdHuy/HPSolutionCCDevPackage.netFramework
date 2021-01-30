using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HPSolutionCCDevPackage.netFramework.Utils
{
    public static class HPSPExtension
    {
        public static bool IsZero(this Size size)
        {
            return size.Width == 0 || size.Height == 0;
        }

        public static Size MultipleSize(this Size size, double mul)
        {
            return new Size(size.Width * mul, size.Height * mul);
        }

        public static bool IsDifferent(this Size size, Size compareSize)
        {
            return size.Width != compareSize.Width ||
                size.Height != compareSize.Height;
        }

        public static bool IsNaN(this Size size)
        {
            return Double.IsNaN(size.Width) ||
                Double.IsNaN(size.Height);
        }

        public static bool IsInfinity(this Size size)
        {
            return Double.IsInfinity(size.Width) ||
                Double.IsInfinity(size.Height);
        }
    }
}
