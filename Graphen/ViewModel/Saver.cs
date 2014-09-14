using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphen.Graph;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Graphen.ViewModel
{
    internal static class Saver
    {
        internal static bool SaveGraph(Graphen.Graph.Graph graph, Dictionary<Circle, Vertex> verticesMap, String fileName)
        {
            Tuple<int, int> verticesAndEgesAmount = Tuple.Create<int, int>(graph.GetVerticesAmount(), graph.GetEdgesAmount());
            List<Tuple<ulong, ulong>> edgesAsList = GetEdgesAsList(graph.Edges);
            List<Tuple<System.Windows.Point, ulong>> verticeToSerialize = GetVericesToCircleMapAsList(verticesMap);

            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, verticesAndEgesAmount);
                    bin.Serialize(stream, edgesAsList);
                    bin.Serialize(stream, verticeToSerialize);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        internal static void LoadGraph(String fileName, Controller controller, MainWindow view)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    Tuple<int, int> verticesAndEgesAmount = (Tuple<int, int>) bin.Deserialize(stream);
                    List<Tuple<ulong, ulong>> edgesAsList = (List<Tuple<ulong, ulong>>)bin.Deserialize(stream);
                    List<Tuple<System.Windows.Point, ulong>> verticesMap = (List<Tuple<System.Windows.Point, ulong>>) bin.Deserialize(stream);
                    controller.RecounstructGraphFromFile(verticesAndEgesAmount, edgesAsList, verticesMap, view);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private static List<Tuple<ulong, ulong>> GetEdgesAsList(IEnumerable<Edge> edges)
        {
            List<Tuple<ulong, ulong>> resultEdgesList = new List<Tuple<ulong,ulong>>();
            foreach (Edge e in edges) {
                resultEdgesList.Add(Tuple.Create<ulong, ulong>(e.First.vertexID, e.Second.vertexID));
            }
            return resultEdgesList;
        }

        private static List<Tuple<System.Windows.Point, ulong>> GetVericesToCircleMapAsList(Dictionary<Circle, Vertex> verticesMap)
        {
            List<Tuple<System.Windows.Point, ulong>> verticesToSerialize = new List<Tuple<System.Windows.Point, ulong>>();
            foreach (Circle circle in verticesMap.Keys)
            {
                verticesToSerialize.Add(Tuple.Create<System.Windows.Point, ulong>(circle.Position, verticesMap[circle].vertexID));
            }

            return verticesToSerialize;
        }
    }
}
