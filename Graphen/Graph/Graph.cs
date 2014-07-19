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

        IEnumerable<Vertex> Vertices
        {
            get
            {
                foreach (Vertex v in vertices)
                {
                    yield return v;
                }
            }
        }

        IEnumerable<Edge> Edges
        {
            get
            {
                foreach (Edge e in edges)
                {
                    yield return e;
                }
            }
        }

        public void AddVertex(String label)
        {
            vertices.Add(new Vertex(label));
        }

        //TODO WTF? :D
        public void AddVertex()
        {
            vertices.Add(new Vertex());
        }

        public void AddEdge(Vertex v, Vertex g)
        {
            Edge edge = new Edge(v, g);
            v.AddEdge(edge);
            g.AddEdge(edge);
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
                RemoveEdge(e);
            }
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
            foreach (Vertex v in vertices)
            {
                RemoveVertex(v);
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
