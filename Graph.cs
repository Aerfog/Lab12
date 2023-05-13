using System.Xml.Linq;

namespace Lab12
{
    public class Edge
    {
        public Node ConnectedNode { get; set; } 
        public int EdgeWeight { get; set; } 
        public Edge(Node connectedNode, int weight)
        {
            ConnectedNode = connectedNode;
            EdgeWeight = weight;
        }

    }

    public class Node
    {
        public readonly List<Edge> Edges = new List<Edge>();
        public readonly int NodeNumber;

        public Node(int number)
        {
            NodeNumber = number;
        }

        private void AddEdge(Edge newEdge)
        {
            Edges.Add(newEdge);
        }

        public void AddEdge(Node node, int edgeWeight)
        {
            AddEdge(new Edge(node, edgeWeight));
        }

        public override string ToString() => NodeNumber.ToString();
    }

    public class Graph
    {
        public List<Node> Nodes {  get; }

        public Graph()
        {
            Nodes = new List<Node>(); 
        }
        public void AddNode(int nodeName)
        {
            Nodes.Add(new Node(nodeName));
        }

        public Node FindNode(int nodeName)
        {
            foreach (var n in Nodes)
            {
                if (n.NodeNumber == nodeName)
                {
                    return n;
                }
            }

            return null;
        }

        public void AddEdge(int firstName, int secondName, int weight)
        {
            var n1 = FindNode(firstName);  
            var n2 = FindNode(secondName); 

            if (n2 != null && n1 != null)
            {
                n1.AddEdge(n2, weight);
                n2.AddEdge(n1, weight);
            }
        }
    }
}
