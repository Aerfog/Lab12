namespace Lab12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] table =
            {   //0  1   2    3   4   5
           /*0*/{ 0, 18, 25, -1, -1, -1},
           /*1*/{ -1, 0, -1, -1, -1, -1},
           /*2*/{ -1, -1, 0, 5, 23, 30},
           /*3*/{ -1, -1, -1, 0, -1, -1},
           /*4*/{ -1, -1, -1, -1, 0, 10},
           /*5*/{ -1, -1, -1, -1, -1, 0}
            };

            Graph graph = new Graph(table);
            int start = 0;
            var dist = graph.DijkstraAlgoritme(start);
            int i = 1;
            foreach (var item in dist)
            {
                Console.WriteLine("From {0} to {1} -> {2}", start+1,i,item);
                i++;
            }
        }
    }
}