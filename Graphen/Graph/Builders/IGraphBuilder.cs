using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph.Builders
{
    interface IGraphBuilder
    {
        public IGraphBuilder addVertex();
        public IGraphBuilder addEdge(int firstId, int secondId);
        public Graph build();
    }
}
