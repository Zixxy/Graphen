using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph
{
    public class Vertex
    {
        //Helps to give default names for edges
        private static ulong defaultVertexID = 0;

        private readonly ulong vertexID;
        private ICollection<Edge> adjacentEdges;

        public String Label { get; private set; }

        public Vertex()
        {
            vertexID = ++defaultVertexID;
            Label = vertexID.ToString();
            adjacentEdges = new List<Edge>();
        }

        public Vertex(String label)
        {
            vertexID = ++defaultVertexID;
            Label = label;
            adjacentEdges = new List<Edge>();
        }

        public Vertex(List<Edge> adjacentEdges)
        {
            vertexID = ++defaultVertexID;
            Label = vertexID.ToString();
            this.adjacentEdges = adjacentEdges;
        }

        public Vertex(String label, List<Edge> adjacentEdges)
        {
            vertexID = ++defaultVertexID;
            Label = label;
            this.adjacentEdges = adjacentEdges;
        }

        public IEnumerable<Edge> AdjacentEdges
        {
            get
            {
                foreach (Edge e in adjacentEdges)
                {
                    yield return e;
                }
            }
        }

        public IEnumerable<Vertex> AdjacentVertices
        {
            get
            {
                foreach (Edge e in adjacentEdges)
                {
                    yield return e.GetDestination(this);
                }
            }
        }

        public Vertex this[int index]
        {
            get
            {
                return adjacentEdges.ElementAt(index).GetDestination(this);
            }
        }

        internal bool AddEdge(Edge e)
        {
            if (e.First != this && e.Second != this)
            {
                throw new ArgumentException(e.ToString() + " is not adjacent to " + ToString());
            }

            if (!adjacentEdges.Contains(e))
            {
                adjacentEdges.Add(e);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool AddEdges(ICollection<Edge> edges)
        {
            bool result = true;
            foreach (Edge e in edges)
            {
                if (!AddEdge(e))
                {
                    result = false;
                }
            }
            return result;
        }

        internal bool RemoveEdge(Edge e)
        {
            if (e.First != this && e.Second != this)
            {
                throw new ArgumentException(e.ToString() + " is not adjacent to " + ToString());
            }

            return adjacentEdges.Remove(e);
        }

        internal void RemoveAllEdges()
        {
            adjacentEdges.Clear();
        }

        public int GetDegree()
        {
            return adjacentEdges.Count;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vertex))
            {
                return false;
            }

            Vertex ver = (Vertex)obj;
            return vertexID == ver.vertexID;
        }

        public static bool operator ==(Vertex v, Vertex g)
        {
            if (Object.ReferenceEquals(v, null) && Object.ReferenceEquals(g, null))
                return true;
            else if (Object.ReferenceEquals(v, null))
                return false;
            else
                return v.Equals(g);
        }

        public static bool operator !=(Vertex v, Vertex g)
        {
            return !(v == g);
        }

        public override int GetHashCode()
        {
            return (int)vertexID;
        }

        public override string ToString()
        {
            return "Vertex: " + Label;
        }
    }
}
