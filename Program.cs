using System.Xml.Linq;

namespace Lab12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph(6);
            var weights = new Dictionary<Edge, double>();
            weights[graph.Connect(0, 1)] = 18;
            weights[graph.Connect(0, 2)] = 25;
            weights[graph.Connect(2, 3)] = 5;
            weights[graph.Connect(2, 4)] = 23;
            weights[graph.Connect(2, 5)] = 30;
            weights[graph.Connect(4, 5)] = 10;

            var path = Dijkstra(graph, weights, graph[0], graph[5]).Select(n => n.NodeNumber+1);
            foreach (var node in path)
            {
                Console.WriteLine(node);
            }
        }
        public static List<Node> Dijkstra(Graph graph, Dictionary<Edge, double> weights, Node start, Node end)
        {
            var notVisited = graph.Nodes.ToList();
            var track = new Dictionary<Node, DijkstraData>();
            track[start] = new DijkstraData { Price = 0, Previous = null };

            while (true)
            {
                Node toOpen = null;
                var bestPrice = double.PositiveInfinity;
                foreach (var e in notVisited)
                {
                    if (track.ContainsKey(e) && track[e].Price < bestPrice)
                    {
                        bestPrice = track[e].Price;
                        toOpen = e;
                    }
                }

                if (toOpen == null) return null;
                if (toOpen == end) break;

                foreach (var e in toOpen.IncidentEdges.Where(z => z.From == toOpen))
                {
                    var currentPrice = track[toOpen].Price + weights[e];
                    var nextNode = e.OtherNode(toOpen);
                    if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
                    {
                        track[nextNode] = new DijkstraData { Previous = toOpen, Price = currentPrice };
                    }
                }

                notVisited.Remove(toOpen);
            }

            var result = new List<Node>();
            while (end != null)
            {
                result.Add(end);
                end = track[end].Previous;
            }
            result.Reverse();
            return result;
        }
    }

    class DijkstraData
    {
        public Node Previous { get; set; }
        public double Price { get; set; }
    }
}