using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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

        public static T FindChild<T>(this DependencyObject dO, string childName)
        where T : DependencyObject
        {
            if (dO == null) return null;

            T foundChild = null;

            int childrentCount = VisualTreeHelper.GetChildrenCount(dO);

            for (int i = 0; i < childrentCount; i++)
            {
                var child = VisualTreeHelper.GetChild(dO, i);

                T childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;

                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }
}
