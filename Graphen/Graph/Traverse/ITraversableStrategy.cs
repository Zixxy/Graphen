using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph.Traverse
{
    interface ITraversableStrategy
    {
        public bool hasNextVertex();

        public Vertex getNextVertex();

        public void visitVertex(Vertex v);
    }
}
