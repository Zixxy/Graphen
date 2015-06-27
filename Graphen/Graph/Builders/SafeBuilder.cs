using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph.Builders
{
    public class SafeBuilder : IGraphBuilder
    {
        private ICollection<Vertex> vertices;
        private ICollection<Tuple<int, int>> edges;

        public SafeBuilder()
        {
            vertices = new List<Vertex>();
            edges = new  HashSet<Tuple<int, int>>();
        }

        public IGraphBuilder addVertex()
        {
            vertices.Add(new Vertex());
            return this;
        }

        public IGraphBuilder addEdge(int firstId, int secondId)
        {
            if (firstId != secondId)
            {
                edges.Add(Tuple.Create<int, int>(Math.Min(firstId, secondId), Math.Max(firstId, secondId)));
            }
            return this;
        }

        public Graph build()
        {
            Graph graph = new Graph();
            foreach (Vertex v in vertices) {
                graph.AddVertex(v);
            }
            foreach (Tuple<int, int> edge in edges)
            {
                if (edge.Item1 < vertices.Count && edge.Item2 < vertices.Count)
                {
                    graph.AddEdge(new Edge(vertices.ElementAt(edge.Item1), vertices.ElementAt(edge.Item2)));
                }
            }
            return graph;
        }
    }
}
