using Graphen.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Point;

namespace Graphen.View.State
{
    class VertexDrawer : DrawTool
    {
        public VertexDrawer(DrawingState state) : base(state) {}
        public static Circle CreateVertex(Point position, DrawingState state)
        {
            Circle circle = new Circle(position);
            state.controller.AddVertex(circle);
            Ellipse ellipse = circle.ellipse;

            ellipse.MouseDown += (object a, MouseButtonEventArgs b) =>
            {
                if (state.currentTool == DrawingState.Tool.DRAW_EDGE)
                {
                    if (state.firstCircle == null)
                        state.firstCircle = circle;
                    else if (state.secondCircle != null)
                        state.firstCircle = circle;
                    else
                        state.secondCircle = circle;
                }
                else
                {
                    state.firstCircle = state.secondCircle = null;
                }
            };
            //TODO - choose hover effect, do when styling
            ellipse.MouseEnter += (object o, MouseEventArgs e) =>
            {
                if (state.currentTool == DrawingState.Tool.DRAW_EDGE)
                {
                    ellipse.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                }
            };
            ellipse.MouseLeave += (object o, MouseEventArgs e) =>
            {
                ellipse.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 150, 0));
            };

            state.canvas.Children.Add(ellipse);
            return circle;
        }

        public override void DrawElement(object sender, MouseButtonEventArgs e, Point mousePosition)
        {
            CreateVertex(mousePosition, state);
        }
    }
}
