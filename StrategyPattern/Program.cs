using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = CreateLongList();

            SortingStrategy sortedStrategy = new SortingStrategy();

            sortedStrategy.SortingAlgorithm = new SelectionSort();
            sortedStrategy.List = new List<int>(list);
            sortedStrategy.Sort();

            sortedStrategy.SortingAlgorithm = new QuickSort();
            sortedStrategy.List = new List<int>(list);
            sortedStrategy.Sort();
        }

        private static List<int> CreateLongList()
        {
            List<int> list = new List<int>();

            Random random = new Random();
            for (int i = 0; i < 50000; i++)
            {
                list.Add(random.Next(1, 50000));
            }

            return list;
        }
    }

    internal interface ISortingAlgorithm
    {
        void Sort(List<int> list);
    }

    internal class SelectionSort : ISortingAlgorithm
    {
        public void Sort(List<int> list)
        {
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                    if (list[j] < list[minIdx])
                        minIdx = j;

                int temp = list[minIdx];
                list[minIdx] = list[i];
                list[i] = temp;
            }
        }

        public override string ToString()
        {
            return "Selection Sort Algorithm";
        }
    }

    internal class QuickSort : ISortingAlgorithm
    {
        public void Sort(List<int> list)
        {
            Sort(list, 0, list.Count - 1);
        }

        private void Sort(List<int> list, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);

                Sort(list, low, pi - 1);
                Sort(list, pi + 1, high);
            }
        }

        private int Partition(List<int> list, int low, int high)
        {
            int pivot = list[high];

            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (list[j] <= pivot)
                {
                    i++;

                    int temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }

            int temp1 = list[i + 1];
            list[i + 1] = list[high];
            list[high] = temp1;

            return i + 1;
        }

        public override string ToString()
        {
            return "Quick Sort Algorithm";
        }
    }

    internal class SortingStrategy
    {
        public List<int> List { get; set; }
        public ISortingAlgorithm SortingAlgorithm { get; set; }

        internal void Sort()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            this.SortingAlgorithm.Sort(List);

            watch.Stop();
            Console.WriteLine(SortingAlgorithm.ToString() + " Running time: " + watch.ElapsedMilliseconds + " Milliseconds");
        }
    }
}
