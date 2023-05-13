using System.Xml.Linq;

namespace Lab12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var g = new Graph();
            for (int i = 1; i <= 6; i++)
            {
                g.AddNode(i);
            }

            g.AddEdge(1, 2, 18);
            g.AddEdge(1, 3, 25);
            g.AddEdge(3, 4, 5);
            g.AddEdge(3, 5, 23);
            g.AddEdge(3, 6, 30);
            g.AddEdge(5, 6, 10);

            var dijkstra = new Dijkstra(g);
            var path = dijkstra.FindShortestPath(1, 6);
            Console.WriteLine(path);
        }
    }

    internal class GraphNodeInfo
    {
        public Node Node { get; set; }
        public bool IsUnvisited { get; set; }
        public int EdgesWeightSum { get; set; }
        public Node PreviousNode { get; set; }
        public GraphNodeInfo(Node node)
        {
            Node = node;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousNode = null;
        }
    }

    internal class Dijkstra
    {
        Graph graph;

        List<GraphNodeInfo> infos;

        public Dijkstra(Graph graph)
        {
            this.graph = graph;
        }

        void InitInfo()
        {
            infos = new List<GraphNodeInfo>();
            foreach (var n in graph.Nodes)
            {
                infos.Add(new GraphNodeInfo(n));
            }
        }

        GraphNodeInfo GetNodeInfo(Node n)
        {
            foreach (var i in infos)
            {
                if (i.Node.Equals(n))
                {
                    return i;
                }
            }
            return null;
        }

        public GraphNodeInfo FindUnvisitedNodeWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphNodeInfo minNodeInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minNodeInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }
            return minNodeInfo;
        }

        public string FindShortestPath(int startName, int finishName)
        {
            return FindShortestPath(graph.FindNode(startName), graph.FindNode(finishName));
        }

        private string FindShortestPath(Node startNode, Node finishNode)
        {
            InitInfo();
            var first = GetNodeInfo(startNode);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedNodeWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextNode(current);
            }

            return GetPath(startNode, finishNode);
        }

        void SetSumToNextNode(GraphNodeInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Node.Edges)
            {
                var nextInfo = GetNodeInfo(e.ConnectedNode);
                var sum = info.EdgesWeightSum + e.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousNode = info.Node;
                }
            }
        }

        string GetPath(Node startNode, Node endNode)
        {
            var path = endNode.ToString();
            while (startNode != endNode)
            {
                endNode = GetNodeInfo(endNode).PreviousNode;
                path = endNode.ToString() + "->" + path;
            }
            return path;
        }
    }
}