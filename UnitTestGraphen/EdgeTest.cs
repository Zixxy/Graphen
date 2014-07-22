using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphen.Graph;

namespace UnitTestGraphen
{
    [TestClass]
    public class EdgeTest
    {
        [TestMethod]
        public void DestinationTest()
        {
            Vertex v = new Vertex();
            Vertex g = new Vertex();

            Edge eVG = new Edge(v, g);

            Assert.IsTrue(eVG.GetDestination(v).Equals(g));
            Assert.IsTrue(eVG.GetDestination(g).Equals(v));
        }

        [TestMethod]
        public void EqualsTest()
        {
            Vertex v = new Vertex();
            Vertex g = new Vertex();

            Edge eVV = new Edge(v, v);
            Edge eVG = new Edge(v, g);
            Edge eGV = new Edge(g, v);
            Edge eGG = new Edge(g, g);

            Assert.IsTrue(eGG.Equals(eGG));
            Assert.IsTrue(eGG.Equals(new Edge(g, g)));
            Assert.IsTrue(eVG.Equals(new Edge(v, g)));

            Assert.IsTrue(eGG == (new Edge(g, g)));
            Assert.IsTrue(eVG == (new Edge(v, g)));

            Assert.IsFalse(eGV.Equals(eVG));
            Assert.IsTrue(eVG != eGV);

        }

        [TestMethod]
        public void HashCodeTest()
        {
            Vertex v = new Vertex();
            Vertex g = new Vertex();

            Edge eVV = new Edge(v, v);
            Edge eVG = new Edge(v, g);

            Assert.IsTrue(eVV.GetHashCode() == eVV.GetHashCode());
            Assert.IsTrue(eVG.GetHashCode() == new Edge(v, g).GetHashCode());
        }
    }
}
