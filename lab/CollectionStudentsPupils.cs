using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace lab
{
    public class CollectionStudentsPupils<T>
    {
        public Stack<Dictionary<int, T>>? City { get; set; }

        public int Size { get; set; }

        public CollectionStudentsPupils(int size)
        {
            City = new Stack<Dictionary<int, T>>(size);
            Size = size;
            for (int i = 0; i < Size; i++)
            {
                City.Push(new Dictionary<int, T>(5));
            }
        }

        public void PrintCollection()
        {
            if (Size < 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }
            int numberCity = 1;
            foreach(Dictionary<int, T> dict in City)
            {
                Console.WriteLine($"\nГруппа {numberCity}");
                numberCity++;
                foreach (KeyValuePair<int, T> pair in dict)
                    Console.WriteLine(pair.Key + ": " + pair.Value);
            }
        }
    }
}
