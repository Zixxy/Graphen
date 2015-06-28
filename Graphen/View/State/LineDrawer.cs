using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Graphen.View.State
{
    class LineDrawer : DrawTool
    {
        public LineDrawer(DrawingState state) : base(state) { }
        public static void CreateEdge(DrawingState state)
        {
            if (state.firstCircle == null || state.secondCircle == null)
                return;
            if (!state.controller.ContainsEdge(state.firstCircle, state.secondCircle) &&
                state.firstCircle != state.secondCircle)
            {
                Line line = new Line()
                {
                    X1 = state.firstCircle.Position.X,
                    Y1 = state.firstCircle.Position.Y,
                    X2 = state.secondCircle.Position.X,
                    Y2 = state.secondCircle.Position.Y
                };
                state.controller.AddEdge(line, state.firstCircle, state.secondCircle);
                line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                line.StrokeThickness = 2;
                state.canvas.Children.Add(line);

                line.MouseDown += (object a, MouseButtonEventArgs b) =>
                {
                    if (state.currentTool == DrawingState.Tool.REMOVE_EDGE)
                    {
                        state.controller.RemoveEdge(a as Line, state.mainWindow);
                    }
                };
            }
            state.firstCircle = null;
            state.secondCircle = null;
        }

        public override void DrawElement(object sender, MouseButtonEventArgs e, System.Windows.Point mousePosition)
        {
            CreateEdge(state);
        }

    }
}
