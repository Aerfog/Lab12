namespace Lab12
{
    internal class Graph
    {
        private int[,] list;
        private int n;
        public Graph(int n)
        {
            list = new int[n, n];
            this.n = n;
        }

        public Graph(int[,] arr)
        {
            list = arr;
            this.n = arr.GetLength(0);
        }
        public void Add(int from, int to, int cost)
        {
            list[from, to] = cost;
        }

        public IList<int> DijkstraAlgoritme(int start)
        {
            List<int> distance = new List<int>();
            bool[] visited = new bool[n];
            for(int i =0;i<n;i++)
            {
                distance.Add(int.MaxValue);
                visited[i] = false;
            }

            distance[start] = 0;
            int index = -1;
            for(int i = 0; i< n;i++)
            {
                int min = int.MaxValue;
                for(int j = 0; j < n; j++)
                {
                    if (!visited[j] && distance[j] <= min)
                    {
                        min = distance[j];
                        index = j; 

                    }
                }
                visited[index] = true;
                for(int j =0;j<n; j++)
                {
                    if (!visited[j] &&
                        list[index, j] > -1 &&
                        distance[index] != int.MaxValue &&
                        distance[index] + list[index, j] < distance[j])
                            distance[j] = distance[index] + list[index, j];
                }
            }

            return distance;
        }
    }
}
