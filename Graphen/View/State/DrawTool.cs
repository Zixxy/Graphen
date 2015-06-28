using System;
using System.Text;
using Graphen.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Graphen.View.State
{
    abstract class DrawTool
    {
        protected DrawingState state;
        public DrawTool(DrawingState state)
        {
            this.state = state;
        }
        public abstract void DrawElement(object sender, MouseButtonEventArgs e, Point mousePosition);
    }
}
