﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphen.Graph;

namespace Graphen.ViewModel
{
    public class Controller : INotifyPropertyChanged
    {
        private Graph.Graph graph;

        private Dictionary<Circle, Vertex> vertices;
        private Dictionary<Edge, System.Windows.Shapes.Line> edges;

        internal Controller()
        {
            graph = new Graph.Graph();
            vertices = new Dictionary<Circle,Vertex>();
            edges = new Dictionary<Edge, System.Windows.Shapes.Line>();
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add {  }
            remove { }
        }

        public void AddVertex(Circle circle)
        {
            Vertex newVertex = new Vertex();
            graph.AddVertex(newVertex);

            vertices.Add(circle, newVertex);
        }

        public bool ContainsEdge(Circle c1, Circle c2)
        {
            Vertex v, w;
            vertices.TryGetValue(c1, out v);
            vertices.TryGetValue(c2, out w);

            Edge e = new Edge(v, w);
            return graph.ContainsEdge(e);
        }
        public void AddEdge(System.Windows.Shapes.Line line, Circle c1, Circle c2)
        {
            Vertex v, w;
            vertices.TryGetValue(c1, out v);
            vertices.TryGetValue(c2, out w);

            Edge e = new Edge(v, w);

            if (graph.AddEdge(e))
            {
                edges.Add(e, line);
            }
        }

        internal void ArrangeVertices()
        {
            do
            {
                DateTime now = DateTime.Now;
                ICollection<Circle> circles = vertices.Keys;
                
                foreach (Circle i in vertices.Keys)
                {
                    Vertex v;
                    System.Windows.Shapes.Line line;
                    System.Windows.Point oldPosition;

                    oldPosition = i.Position;

                    Physics.UpdateForceVector(i, vertices.Keys);
                    Physics.UpdateCurrentCirclePosition(now.Second, now.Millisecond, i);

                    vertices.TryGetValue(i, out v);
                    foreach (Edge e in v.AdjacentEdges)
                    {
                        edges.TryGetValue(e, out line);
                        Action moveLine = delegate()
                        {
                            if (line.X1 == oldPosition.X && line.Y1 == oldPosition.Y)
                            {
                                line.X1 = i.Position.X;
                                line.Y1 = i.Position.Y;
                            }
                            else
                            {
                                line.X2 = i.Position.X;
                                line.Y2 = i.Position.Y;
                            }
                        };
                        line.Dispatcher.Invoke(moveLine);
                    }
                }
            } while (Circle.movingCircles != 0);
        }
    }
}
