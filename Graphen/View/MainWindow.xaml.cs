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

namespace Graphen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller;
        
        private Circle firstCircle;
        private Circle secondCircle;

        public enum DrawingTool 
        {
            DRAW_VERTEX, REMOVE_VERTEX, DRAW_EDGE, SET_COLOR, VALIDATE
        }
        public DrawingTool ActualTool { get; set; }

        public MainWindow()
        {
            controller = new Controller();
            InitializeComponent();
        }

        private void PickCircleTool(object sender, RoutedEventArgs e)
        {
            ActualTool = DrawingTool.DRAW_VERTEX;
        }

        private void PickEdgeTool(object sender, RoutedEventArgs e)
        {
            ActualTool = DrawingTool.DRAW_EDGE;
        }

        private void PickRemoveVertexTool(object sender, RoutedEventArgs e)
        {
            ActualTool = DrawingTool.REMOVE_VERTEX;
        }

        private void ArrangeVertices(object sender, RoutedEventArgs e)
        {
            Thread execute = new Thread(controller.ArrangeVertices);
            execute.Start();
        }

        private void DrawElement(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = Mouse.GetPosition(paintSurface);// System.Windows.Forms.Control.MousePosition;
            switch (ActualTool)
            {
                case DrawingTool.DRAW_VERTEX:
                    {
                        CreateVertex(position);
                        break;
                    }
                case DrawingTool.REMOVE_VERTEX:
                    {
                        RemoveVertex(position);
                        break;
                    }
                case DrawingTool.DRAW_EDGE:
                    {
                        CreateEdge();
                        break;
                    }
                case DrawingTool.VALIDATE:
                    {
                        throw new NotImplementedException();
                       // break;
                    }
                case DrawingTool.SET_COLOR:
                    {
                        throw new NotImplementedException();
                       // break;
                    }
                default:
                    throw new ArgumentException("Invalid DrawTool:" + ActualTool + "picked"); 

            }
        }
        private void CreateVertex(System.Windows.Point position)
        {
            Circle circle = new Circle(position);
            controller.AddVertex(circle);
            Ellipse ellipse = circle.ellipse;
            ellipse.MouseDown += (object a, MouseButtonEventArgs b) =>
            {
                if (firstCircle == null)
                    firstCircle = circle;
                else if (secondCircle != null)
                    firstCircle = circle;
                else
                    secondCircle = circle;
            };
            paintSurface.Children.Add(ellipse);
        }

        private void CreateEdge()
        {
            if (firstCircle == null || secondCircle == null)
                return;
            if (!controller.ContainsEdge(firstCircle, secondCircle) && 
                firstCircle != secondCircle)
            {
                Line line = new Line()
                {
                    X1 = firstCircle.Position.X,
                    Y1 = firstCircle.Position.Y,
                    X2 = secondCircle.Position.X,
                    Y2 = secondCircle.Position.Y
                };
                controller.AddEdge(line, firstCircle, secondCircle);
                line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                line.StrokeThickness = 2;
                paintSurface.Children.Add(line);
            }
            firstCircle = null;
            secondCircle = null;
        }

        private void RemoveVertex(System.Windows.Point position)
        {
            controller.RemoveVertex(position, this);
            firstCircle = secondCircle = null;
        }

        public void RemoveElementFromSurface(System.Windows.UIElement element)
        {
            paintSurface.Children.Remove(element);
        }
    }
}
