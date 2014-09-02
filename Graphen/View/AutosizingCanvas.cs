using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Graphen.View
{
    class AutosizingCanvas : Canvas
    {
        private double NominalWidth;
        private double NominalHeight;
        internal void SetNominalSize(double width, double height)
        {
            NominalWidth = width;
            NominalHeight = height;
        }

        internal void EnsureSize()
        {
            Transform transform = RenderTransform;
            ScaleTransform scale = transform as ScaleTransform;
            if (scale == null)
            {
                Height = NominalHeight;
                Width = NominalWidth;
            }
            else
            {
                Height = NominalHeight / scale.ScaleY;
                Width = NominalWidth / scale.ScaleX;
            }
            Console.WriteLine(Height);
            Console.WriteLine(Margin);
        }
    }
}
