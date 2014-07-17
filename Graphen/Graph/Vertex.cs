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

        private ulong vertexID;
        private ICollection<Edge> adjacentEdges;

        public String Label { get; private set; }

        internal Vertex()
        {
            vertexID = ++defaultVertexID;
            Label = vertexID.ToString();
            adjacentEdges = new List<Edge>();
        }

        internal Vertex(String label)
        {
            vertexID = ++defaultVertexID;
            Label = label;
            adjacentEdges = new List<Edge>();
        }

        internal Vertex(List<Edge> adjacentEdges)
        {
            vertexID = ++defaultVertexID;
            Label = vertexID.ToString();
            this.adjacentEdges = adjacentEdges;
        }

        internal Vertex(String label, List<Edge> adjacentEdges)
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
                    if (e.Directed && e.Second == this)
                    {
                        continue;
                    }
                    yield return e.GetDestination(this);
                }
            }
        }

        //TODO Indexer at vertices or edges?
        //public Edge this[int index]
        //{
        //    get
        //    {
        //        if (index < 0 || adjacentEdges.Count <= index)
        //        {
        //            throw new ArgumentOutOfRangeException();
        //        }

        //        return adjacentEdges.ElementAt(index);
        //    }
        //}

        public Vertex this[int index]
        {
            get
            {
                if (index < 0 || adjacentEdges.Count <= index)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return adjacentEdges.ElementAt(index).GetDestination(this);
            }
        }

        //TODO multiedges
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
                throw new ArgumentException();
            }

            return adjacentEdges.Remove(e);
        }

        internal void RemoveAllEdges()
        {
            adjacentEdges.Clear();
        }

        //TODO Consider what to do with in and out degree
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
            return v.Equals(g);
        }

        public static bool operator !=(Vertex v, Vertex g)
        {
            return !v.Equals(g);
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
