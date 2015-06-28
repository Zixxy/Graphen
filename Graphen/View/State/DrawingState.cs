using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphen.ViewModel;

namespace Graphen.View.State
{
    public class DrawingState {
        public Circle firstCircle;
        public Circle secondCircle;
        public Controller controller;
        public AutosizingCanvas canvas;
        public MainWindow mainWindow;

        public DrawingState(Controller controller, AutosizingCanvas canvas, MainWindow mainWindow) {
            this.controller = controller;
            this.canvas = canvas;
            this.mainWindow = mainWindow;
        }

        public enum Tool {
            DRAW_VERTEX, REMOVE_VERTEX, DRAW_EDGE, REMOVE_EDGE, SET_COLOR, VALIDATE;
        }

        public Tool currentTool {
            private set {
                this.currentTool = value;
            }
            get {
                return this.currentTool;
            }
        }

        public DrawTool reset(Tool newState) {
            switch(newState) {
                case Tool.DRAW_VERTEX:
                    {
                        currentTool = newState;
                        return new VertexDrawer(this);
                        break;
                    }
                
                case Tool.DRAW_EDGE:
                    {
                        currentTool = newState;
                        return new LineDrawer(this);
                        break;
                    }
                    
                case Tool.REMOVE_EDGE:
                    {
                        currentTool = newState;
                        return new VertexRemover(this);
                    }
                case Tool.VALIDATE:
                    {
                        throw new NotImplementedException();
                       // break;
                    }
                case Tool.SET_COLOR:
                    {
                        throw new NotImplementedException();
                       // break;
                    }
                default:
                    throw new ArgumentException("Invalid DrawTool:" + newState + "picked"); 
            }
            return null;
        }
    }
}
