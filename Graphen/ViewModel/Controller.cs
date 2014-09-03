using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphen.Graph;

namespace Graphen.ViewModel
{
    public class Controller : INotifyPropertyChanged
    {
        private Graph.Graph graph;

        private Dictionary<Circle, Vertex> vertices;
        private Dictionary<Edge, System.Windows.Shapes.Line> edges;

        internal Controller()
        {
            graph = new Graph.Graph();
            vertices = new Dictionary<Circle,Vertex>();
            edges = new Dictionary<Edge, System.Windows.Shapes.Line>();
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add {  }
            remove { }
        }

        public void AddVertex(Circle circle)
        {
            Vertex newVertex = new Vertex();
            graph.AddVertex(newVertex);

            vertices.Add(circle, newVertex);
        }

        //TODO Consider ambiguous choice
        public void RemoveVertex(System.Windows.Point position, MainWindow view)
        {
            foreach (Circle c in vertices.Keys)
            {
                if (c.ContainsPoint(position))
                {
                    Vertex v;
                    vertices.TryGetValue(c, out v);

                    view.RemoveElementFromSurface(c.ellipse);
                    foreach (Edge e in v.AdjacentEdges)
                    {
                        System.Windows.Shapes.Line edgeLine;
                        edges.TryGetValue(e, out edgeLine);
                        view.RemoveElementFromSurface(edgeLine);
                    }

                    graph.RemoveVertex(v);
                }
            }
        }
        public void ApplyTranslateTransformToEachElement(System.Windows.Media.TranslateTransform transf)
        {
            foreach (Circle i in vertices.Keys)
            {
                i.ellipse.RenderTransform = transf;
            }
        }

        public void UpdateEveryElementPosition(double deltaX, double deltaY)
        {
            foreach (Circle i in vertices.Keys)
            {
                i.Position = new System.Windows.Point(i.Position.X + deltaX, i.Position.Y + deltaY);
            }
        }

        public bool ContainsEdge(Circle c1, Circle c2)
        {
            Vertex v, w;
            vertices.TryGetValue(c1, out v);
            vertices.TryGetValue(c2, out w);

            Edge e = new Edge(v, w);
            return graph.ContainsEdge(e);
        }

        public void AddEdge(System.Windows.Shapes.Line line, Circle c1, Circle c2)
        {
            Vertex v, w;
            vertices.TryGetValue(c1, out v);
            vertices.TryGetValue(c2, out w);

            Edge e = new Edge(v, w);

            if (graph.AddEdge(e))
            {
                edges.Add(e, line);
            }
        }

        //TODO Consider ambiguous choice
        //TODO do it in O(1)
        public void RemoveEdge(System.Windows.Shapes.Line lineCliked, MainWindow view)
        {
            Edge edgeToRemove = edges.First(e => e.Value == lineCliked).Key;
            graph.RemoveEdge(edgeToRemove);
            edges.Remove(edgeToRemove);

            view.RemoveElementFromSurface(lineCliked);
        }

        internal void ArrangeVertices()
        {
            do
            {
                DateTime now = DateTime.Now;
                ICollection<Circle> circles = vertices.Keys;
                
                foreach (Circle i in vertices.Keys)
                {
                    Vertex v;
                    System.Windows.Shapes.Line line;
                    System.Windows.Point oldPosition;

                    oldPosition = i.Position;

                    Physics.UpdateForceVector(i, vertices.Keys);
                    Physics.UpdateCurrentCirclePosition(now.Second, now.Millisecond, i);

                    vertices.TryGetValue(i, out v);
                    foreach (Edge e in v.AdjacentEdges)
                    {
                        edges.TryGetValue(e, out line);
                        Action moveLine = delegate()
                        {
                            if (line.X1 == oldPosition.X && line.Y1 == oldPosition.Y)
                            {
                                line.X1 = i.Position.X;
                                line.Y1 = i.Position.Y;
                            }
                            else
                            {
                                line.X2 = i.Position.X;
                                line.Y2 = i.Position.Y;
                            }
                        };
                        line.Dispatcher.Invoke(moveLine);
                    }
                }
            } while (Circle.movingCircles != 0);
        }

        public void CleanGraph(MainWindow view)
        {
            foreach (Circle c in vertices.Keys)
            {
                view.RemoveElementFromSurface(c.ellipse);
            }

            foreach (System.Windows.Shapes.Line l in edges.Values)
            {
                view.RemoveElementFromSurface(l);
            }

            vertices.Clear();
            edges.Clear();

            graph.Clear();
        }
    }
}
