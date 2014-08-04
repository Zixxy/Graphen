﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace Graphen.ViewModel
{
    public class Circle
    {
        public readonly Ellipse ellipse;
        private System.Windows.Point position;
        private Vector strengthVector;
        private static readonly Vector zeroVector;
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
                {
                    Console.WriteLine("mszsz");
                    Console.WriteLine(strengthVector);
                    ++movingCircles;
                }
                else if (value == zeroVector && strengthVector != zeroVector)
                {
                    Console.WriteLine("DAFUUQ!?!?!??!");
                    Console.WriteLine(strengthVector);
                    --movingCircles;
                }
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
                Action updateAction = new Action(() => ellipse.Margin = new Thickness(value.X - 10, value.Y - 10, 0, 0));
                MainWindow.mainWindow.InvokeIfRequired(updateAction);
                position = value;
            }
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
