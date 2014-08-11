﻿using Graphen.ViewModel;
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

        public static MainWindow mainWindow; //WHY

        public enum DrawingTool 
        {
            DRAW_VERTEX, DRAW_EDGE, SET_COLOR, VALIDATE 
        }
        public DrawingTool ActualTool { get; set; }

        public MainWindow()
        {
            controller = new Controller();
            mainWindow = this;
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

        private void ArrangeVertices(object sender, RoutedEventArgs e)
        {
            Thread execute = new Thread(controller.ArrangeVertices);
            execute.Start();
        }

        //For each tool separate method.
        private void DrawElement(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = Mouse.GetPosition(paintSurface);// System.Windows.Forms.Control.MousePosition;
            switch (ActualTool)
            {
                case DrawingTool.DRAW_VERTEX:
                    {
                        Circle circle = new Circle(position);
                        controller.AddVertex(circle);
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
                case DrawingTool.DRAW_EDGE:
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
                        controller.AddEdge(line, firstCircle, secondCircle);
                        line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                        line.StrokeThickness = 2;
                        paintSurface.Children.Add(line);
                        firstCircle = secondCircle;
                        secondCircle = null;
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
    }
}
