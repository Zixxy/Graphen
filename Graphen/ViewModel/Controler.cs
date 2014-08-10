using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphen.Graph;

namespace Graphen.ViewModel
{
    // this class probobly should be singleton. Idk if this should be named controler? xD
    public class Controler : INotifyPropertyChanged
    {
        private Dictionary<Circle, Vertex> vertices; // we may do it using hashmap, previously implementing good hash function to circle.
        private Dictionary<Edge, System.Windows.Shapes.Line> edges;
        public static readonly Controler controler = new Controler();
        private Controler()
        {
            vertices = new Dictionary<Circle,Vertex>();
            edges = new Dictionary<Edge, System.Windows.Shapes.Line>();
        }
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add {  }
            remove { }
        }

        /*{Binding PickCircleToolProp}*/

        public void AddVertex(Circle circle)
        {
            vertices.Add(circle, new Vertex());
        }

        public void AddEdge(System.Windows.Shapes.Line line, Circle o1, Circle o2)
        {
            Vertex v;
            Vertex w;
            vertices.TryGetValue(o1, out v);
            vertices.TryGetValue(o1, out w);
            Edge e = new Edge(v, w);
            if(!edges.ContainsKey(e))
                edges.Add(e, line);
            v.AddEdge(e);
            w.AddEdge(e);
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

                    Physics.ActualizeForceVector(i, vertices.Keys);
                    Physics.UpdateCurrentCirclePosition(now.Second, now.Millisecond, i);

                    vertices.TryGetValue(i, out v);
                    foreach (Edge e in v.AdjacentEdges)
                    {
                        edges.TryGetValue(e, out line);
                        Action act = delegate()
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
                        line.Dispatcher.Invoke(act);
                    }
                }
            } while (Circle.movingCircles != 0);
        }
    }
}
