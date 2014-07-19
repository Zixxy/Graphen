using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph
{
    public class Edge
    {
        public Vertex First { get; private set; }
        public Vertex Second { get; private set; }
        public bool Directed { get; private set; }
        
        public Edge(Vertex first, Vertex second)
        {
            First = first;
            Second = second;
        }

        public Vertex GetDestination(Vertex v)
        {
            return v == First ? Second : First;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Edge))
            {
                return false;
            }
            Edge edge = (Edge)obj;
            return First == edge.First && Second == edge.Second && Directed == edge.Directed;
        }

        public static bool operator ==(Edge e, Edge r)
        {
            return e.Equals(r);
        }

        public static bool operator !=(Edge e, Edge r)
        {
            return !e.Equals(r);
        }

        public override int GetHashCode()
        {
            return (First.GetHashCode() * 31 + Second.GetHashCode()) * 31;
        }

        public override string ToString()
        {
            String result = "Edge: ";
            result += First.Label;
            result += Second.Label;
            return result;
        }
    }
}
