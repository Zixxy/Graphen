using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph
{
    class Graph
    {
        private ICollection<Vertex> vertices;
        private ICollection<Edge> edges;

        public Graph()
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
        }

        public Vertex this[int index]
        {
            get
            {
                return vertices.ElementAt(index);
            }
        }

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                foreach (Vertex v in vertices)
                {
                    yield return v;
                }
            }
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                foreach (Edge e in edges)
                {
                    yield return e;
                }
            }
        }

        public void AddVertex(Vertex v)
        {
            vertices.Add(v);
        }

        public bool ContainsEdge(Edge e)
        {
            return edges.Contains(e);
        }
        public bool AddEdge(Edge e)
        {
            if (!edges.Contains(e))
            {
                e.First.AddEdge(e);
                e.Second.AddEdge(e);
                edges.Add(e);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveEdge(Edge e)
        {
            if (!edges.Contains(e))
            {
                throw new ArgumentException("No such edge in graph: " + e.ToString());
            }

            edges.Remove(e);
            e.First.RemoveEdge(e);
            e.Second.RemoveEdge(e);
        }

        public void RemoveVertex(Vertex v)
        {
            if (!vertices.Contains(v))
            {
                throw new ArgumentException("No such vertex in graph: " + v.ToString());
            }

            foreach (Edge e in v.AdjacentEdges)
            {
                e.GetDestination(v).RemoveEdge(e);
            }
            v.RemoveAllEdges();

            vertices.Remove(v);
        }

        public void RemoveAllEdges()
        {
            edges.Clear();
            foreach (Vertex v in vertices)
            {
                v.RemoveAllEdges();
            }
        }

        public void Clear()
        {
            while (vertices.Count > 0)
            {
                Vertex v = vertices.First();
                RemoveVertex(v);
                vertices.Remove(v);
            }
        }

        public int GetVerticesAmount()
        {
            return vertices.Count;
        }

        public int GetEdgesAmount()
        {
            return edges.Count;
        }
    }
}
