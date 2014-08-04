using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Graphen.ViewModel
{
    public static class ControlExtensions
    {
        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (System.Threading.Thread.CurrentThread != control.Dispatcher.Thread)
                control.Dispatcher.Invoke(action);
            else
                action();
        }
        public static void InvokeIfRequired<T>(this Control control, Action<T> action, T parameter)
        {
            if (System.Threading.Thread.CurrentThread != control.Dispatcher.Thread)
                control.Dispatcher.Invoke(action, parameter);
            else
                action(parameter);
        }
    }
}
