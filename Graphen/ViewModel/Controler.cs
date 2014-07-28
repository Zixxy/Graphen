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
        }
    }
}
