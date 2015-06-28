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
using Graphen.View.State;

namespace Graphen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller;
        private DrawingState state;
        private DrawTool tool;

        const double scaleRate = 1.1;

        public DrawingState CurrentTool { get; set; }

        public MainWindow()
        {
            controller = new Controller();
            state = new DrawingState(controller, paintSurface, this);
            InitializeComponent();
        }
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            paintSurface.SetNominalSize(paintSurfaceSurround.ActualWidth, paintSurfaceSurround.ActualHeight);
            paintSurface.EnsureSize();
        }
        private void PaintSurfaceMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                scaleTransform.ScaleX *= scaleRate;
                scaleTransform.ScaleY *= scaleRate;
            }
            else
            {
                scaleTransform.ScaleX /= scaleRate;
                scaleTransform.ScaleY /= scaleRate;
            }
            paintSurface.EnsureSize();
        }
        private void PickCircleTool(object sender, RoutedEventArgs e)
        {
            tool = state.reset(DrawingState.Tool.DRAW_VERTEX);
        }

        private void PickEdgeTool(object sender, RoutedEventArgs e)
        {
            tool = state.reset(DrawingState.Tool.DRAW_EDGE);
        }

        private void PickRemoveVertexTool(object sender, RoutedEventArgs e)
        {
            tool = state.reset(DrawingState.Tool.REMOVE_VERTEX);
        }

        private void PickRemoveEdgeTool(object sender, RoutedEventArgs e)
        {
            tool = state.reset(DrawingState.Tool.REMOVE_EDGE);
        }

        private void ArrangeVertices(object sender, RoutedEventArgs e)
        {
            Thread execute = new Thread(controller.ArrangeVertices);
            execute.Start();
        }

        private void ClearWorkspace(object sender, RoutedEventArgs e)
        {
            controller.CleanGraph(this);
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SaveGraph(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Saving graph");
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.FileName = "Graph";
            saveFileDialog.DefaultExt = ".gph";
            saveFileDialog.Filter = "Graph documents (.gph)|*.gph";

            Nullable<bool> dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                controller.SaveGraph(saveFileDialog.FileName);
            }
        }

        private void OpenGraph(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Loading graph");
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.FileName = "Graph";
            openFileDialog.DefaultExt = ".gph";
            openFileDialog.Filter = "Graph documents (.gph)|*.gph";

            Nullable<bool> dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                controller.LoadGraph(openFileDialog.FileName, this);
            }
        }

        private void DrawElement(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point position = Mouse.GetPosition(paintSurface);// System.Windows.Forms.Control.MousePosition;
            tool.DrawElement(sender, e, Mouse.GetPosition(paintSurface));
        }
        public void RemoveElementFromSurface(System.Windows.UIElement element)
        {
            paintSurface.Children.Remove(element);
        }

        public Dictionary<ulong, Circle> RestoreVertices(List<Tuple<System.Windows.Point, ulong>> verticesMap)
        {
            Dictionary<ulong, Circle> verticesToCircle = new Dictionary<ulong, Circle>();
            foreach (Tuple<System.Windows.Point, ulong> circleToVertex in verticesMap)
            {
                Circle circle = VertexDrawer.CreateVertex(circleToVertex.Item1, state);
                verticesToCircle.Add(circleToVertex.Item2, circle);
            }
            return verticesToCircle;
        }

        public void RestoreEdge(Circle one, Circle two)
        {
            state.firstCircle = one;
            state.secondCircle = two;
            LineDrawer.CreateEdge(state);
        }
    }
}
