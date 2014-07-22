using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        private enum DrawingTool { DrawCircle, DrawEdge, SetColor, Validate }
        private DrawingTool actualTool;
        private Ellipse firstEllipse;
        private Ellipse secondEllipse;
        private Dictionary<Ellipse, System.Windows.Point> EllipseCoordinatesMap; // it'll be removed.
        public MainWindow()
        {
            InitializeComponent();
            this.Hide();
            MenuWindow menu = new MenuWindow();
            menu.Show();
            EllipseCoordinatesMap = new Dictionary<Ellipse, System.Windows.Point>();
        }

        private void PickCircleTool(object sender, RoutedEventArgs e)
        {
            actualTool = DrawingTool.DrawCircle;
        }
        private void PickEdgeTool(object sender, RoutedEventArgs e)
        {
            actualTool = DrawingTool.DrawEdge;
        }
        private void DrawElement(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point pos = Mouse.GetPosition(paintSurface);// System.Windows.Forms.Control.MousePosition;
            switch (actualTool)
            {
                case DrawingTool.DrawCircle:
                    {
                        /*TODO
                         * New vertex now.
                         * Need to merge it with model.  
                         */
                        Ellipse ellipse = new Ellipse()
                        {
                            Width = 20,
                            Height = 20,
                            Fill = new SolidColorBrush() 
                            { 
                                Color = Color.FromArgb(255, 0, 150, 0) 
                            },
                            Margin = new Thickness(pos.X - 10, pos.Y - 10, 0, 0),
                        };
                        ellipse.MouseDown += (object a, MouseButtonEventArgs b) =>  
                        {
                            if (firstEllipse == null)
                                firstEllipse = ellipse;
                            else if(secondEllipse != null)
                                firstEllipse = ellipse;
                            else
                                secondEllipse = ellipse;
                        };
                        paintSurface.Children.Add(ellipse);
                        EllipseCoordinatesMap.Add(ellipse, pos);
                        break;
                    }
                case DrawingTool.DrawEdge:
                    {
                        /*TODO
                         * New Edge now.
                         * Need to merge it with model.  
                         */
                        if (firstEllipse == null || secondEllipse == null)
                            return;
                        System.Windows.Point start;
                        System.Windows.Point end;
                        EllipseCoordinatesMap.TryGetValue(firstEllipse, out start);
                        EllipseCoordinatesMap.TryGetValue(secondEllipse, out end);
                        Line line = new Line()
                        {
                            X1 = start.X,
                            Y1 = start.Y,
                            X2 = end.X,
                            Y2 = end.Y
                        };
                        line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                        line.StrokeThickness = 2;
                        paintSurface.Children.Add(line);
                        firstEllipse = null;
                        secondEllipse = null;
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
                    throw new ArgumentException("Invalid DrawTool:" + actualTool + "picked"); 

            }
        }
    }
}
