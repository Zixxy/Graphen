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

namespace Graphen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Circle firstCircle;
        private Circle secondCircle;
        private Controler controler;
        public enum DrawingTool { DrawVertex, DrawEdge, SetColor, Validate }
        public DrawingTool ActualTool { get; set; }
        public MainWindow()
        {
            controler = Controler.controler;
            InitializeComponent();
            this.Hide();
            MenuWindow menu = new MenuWindow();
            menu.Show();
           // DataContext = new Controler();
        }

        private void PickCircleTool(object sender, RoutedEventArgs e)
        {
            ActualTool = DrawingTool.DrawVertex;
        }
        private void PickEdgeTool(object sender, RoutedEventArgs e)
        {
            ActualTool = DrawingTool.DrawEdge;
        }
        private void DrawElement(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = Mouse.GetPosition(paintSurface);// System.Windows.Forms.Control.MousePosition;
            switch (ActualTool)
            {
                case DrawingTool.DrawVertex:
                    {
                        Circle circle = new Circle(position);
                        controler.AddVertex(circle);
                        Ellipse ellipse = circle.ellipse;
                        ellipse.MouseDown += (object a, MouseButtonEventArgs b) =>  
                        { 
                            /*
                             * i have to find better solution of mouse clicking ellipse.
                             * For now it is not well.
                             * */
                            if (firstCircle == null)
                                firstCircle = circle;
                            else if(secondCircle != null)
                                firstCircle = circle;
                            else
                                secondCircle = circle;
                        };
                        paintSurface.Children.Add(ellipse);
                        break;
                    }
                case DrawingTool.DrawEdge:
                    {
                        if (firstCircle == null || secondCircle == null)
                            return;
                        Line line = new Line()
                        {
                            X1 = firstCircle.Position.X,
                            Y1 = firstCircle.Position.Y,
                            X2 = secondCircle.Position.X,
                            Y2 = secondCircle.Position.Y
                        };
                        controler.AddEdge(line, firstCircle, secondCircle);
                        line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                        line.StrokeThickness = 2;
                        paintSurface.Children.Add(line);
                        firstCircle = secondCircle;
                        secondCircle = null;
                        break;
                    }
                case DrawingTool.Validate:
                    {
                        throw new NotImplementedException();
                       // break;
                    }
                case DrawingTool.SetColor:
                    {
                        throw new NotImplementedException();
                       // break;
                    }
                default:
                    throw new ArgumentException("Invalid DrawTool:" + ActualTool + "picked"); 

            }
        }
    }
}
