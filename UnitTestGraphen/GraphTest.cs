using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphen.Graph;

namespace UnitTestGraphen
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void VerticesTest()
        {
            Graph graph = new Graph();
            graph.AddVertex(new Vertex());
            graph.AddVertex(new Vertex());
            graph.AddVertex(new Vertex());
            Vertex v = new Vertex();
            graph.AddVertex(v);
            Assert.AreEqual(4, graph.GetVerticesAmount());
            graph.RemoveVertex(v);
            Assert.AreEqual(3, graph.GetVerticesAmount());
        }

        [TestMethod]
        public void VertexIndexTest()
        {
            Graph graph = new Graph();
            graph.AddVertex(new Vertex());
            Vertex v = new Vertex();
            graph.AddVertex(v);
            graph.AddVertex(new Vertex());

            Assert.AreEqual(v, graph[1]);
        }

        [TestMethod]
        public void ClearTest()
        {
            Graph graph = new Graph();
            graph.AddVertex(new Vertex());
            graph.AddVertex(new Vertex());
            graph.AddVertex(new Vertex());
            graph.Clear();
            Assert.AreEqual(0, graph.GetVerticesAmount());
            Assert.AreEqual(0, graph.GetEdgesAmount());
        }

        [TestMethod]
        public void VerticesEnumeratioTest()
        {
            Vertex[] vArr = new Vertex[]{ new Vertex(), new Vertex(), new Vertex() };
            Graph graph = new Graph();
            foreach (Vertex v in vArr)
            {
                graph.AddVertex(v);
            }
            int i = 0;
            foreach (Vertex v in graph.Vertices)
            {
                Assert.AreEqual(vArr[i], v);
            }
        }
    }
}
