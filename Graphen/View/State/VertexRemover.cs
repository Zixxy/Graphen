using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphen.View.State
{
    class VertexRemover : DrawTool
    {
        public VertexRemover(DrawingState state) : base(state) {}

        public override void DrawElement(object sender, System.Windows.Input.MouseButtonEventArgs e, System.Windows.Point mousePosition)
        {
            state.controller.RemoveVertex(mousePosition, state.mainWindow);
        }
    }
}
