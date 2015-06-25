using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph.Traverse
{
    class RandomWalkStrategy : ITraversableStrategy
    {
        private ICollection<Vertex> vertices;

        public RandomWalkStrategy(ICollection<Vertex> vertices)
        {
            this.vertices = vertices;
        }

        public bool hasNextVertex()
        {
            return true;
        }

        public Vertex getNextVertex()
        {
            return vertices.ElementAt(new Random().Next(vertices.Count));
        }

        public void visitVertex(Vertex v) { }
    }
}
