using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.Serialization;

namespace Graphen.ViewModel
{
    public class Circle
    {
        public readonly Ellipse ellipse;
        private int radius = 10;
        private System.Windows.Point position;
        private Vector strengthVector;
        private static readonly Vector zeroVector;
        public static readonly MainWindow madaads;
        public static int movingCircles = 0;
        public Vector StrengthVector
        {
            get
            {
                return strengthVector;
            }
            set
            {
                if (strengthVector == zeroVector && value != zeroVector)
                    ++movingCircles;
                else if (value == zeroVector && strengthVector != zeroVector)
                    --movingCircles;
                strengthVector = value;
            }
        }

        public Circle(System.Windows.Point position)
        {
            ellipse = new Ellipse(){
                 Width = 20,
                 Height = 20,
                 Fill = new SolidColorBrush() 
                 { 
                     Color = Color.FromArgb(255, 0, 150, 0) 
                 },
                 Margin = new Thickness(position.X - 10, position.Y - 10, 0, 0),
            };
            this.position = position;
        }

        public System.Windows.Point Position
        {
            get
            {
                return position;
            }
            set
            {
                Action updateAction = new Action(() => 
                    ellipse.Margin = new Thickness(value.X - 10, value.Y - 10, 0, 0));
                ellipse.Dispatcher.Invoke(updateAction);
                position = value;
            }
        }

        public bool ContainsPoint(System.Windows.Point point) {
            return Math.Pow(point.X - position.X, 2) + Math.Pow(point.Y - position.Y, 2) <= Math.Pow(radius, 2);
        }

        public override int GetHashCode()
        {
            return ellipse.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Circle))
                return false;
            Circle o = (Circle)obj;
            return o.ellipse.Equals(ellipse)  && Position == o.Position;
        }

        public override string ToString()
        {
            return base.ToString() + position.ToString();
        }
        public static bool operator ==(Circle a, Circle b)
        {
            if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null))
                return true;
            else if (Object.ReferenceEquals(a, null))
                return false;
            else
                return a.Equals(b);
        }

        public static bool operator !=(Circle a, Circle b)
        {
            return !(a == b);
        }
        public static bool DetectColision(Circle a, Circle b)
        {
            var r1 = a.ellipse.Width/2;
            var r2 = b.ellipse.Width/2;
            return CountDistance(a, b) < r1 + r2;
        }
        public static double CountDistance(Circle a, Circle b)
        {
            return Math.Sqrt((a.Position.X - b.Position.X) * (a.Position.X - b.Position.X) +
                (a.Position.Y - b.Position.Y) * (a.Position.Y - b.Position.Y));
        }
    }
}
