using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph.Traverse
{
    interface ITraversableStrategy
    {
        bool hasNextVertex();

        Vertex getNextVertex();

        void visitVertex(Vertex v);
    }
}
