using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.Graph.Builders
{
    public interface IGraphBuilder
    {
        IGraphBuilder addVertex();
        IGraphBuilder addEdge(int firstId, int secondId);
        Graph build();
    }
}
