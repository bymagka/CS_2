using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Тинин А
 * 
 * 
 * 2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
    а) для целых чисел;
    б) *для обобщенной коллекции;
    в) *используя Linq.
 * 
 * 
 */
namespace Lesson_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //а) для целых чисел;
            int[] intArray = new int[] { 1, 10, 4, 5, 6, 2, 4, 5 , 5, 6, 6, 7,8};

            Dictionary<int, int> count = new Dictionary<int, int>();

            foreach(int item in intArray)
            {
                if (count.ContainsKey(item)) count[item]++;
                else count.Add(item, 1);
            }

            foreach(int key in count.Keys)
            {
                Console.WriteLine($"{count[key]} {key}");
            }

            Console.WriteLine();

            List<int> li = new List<int> { 1, 10, 4, 5, 6, 2, 4, 5, 5, 6, 6, 7, 8 };
            /*
             *     б) *для обобщенной коллекции;
             */
            CountInt<int,int>(li);
            /*
            *     
                  в) *используя Linq.
            */

            Console.WriteLine();

           

            var results = li.Select(x => x).GroupBy(x => x);

            foreach(var result in results)
            {
                Console.WriteLine($"{result.Count()} {result.Key}");
            }


            Task3A();

            Console.ReadLine();
        }

        //б) *для обобщенной коллекции;
        static Dictionary<T,int> CountInt<T,value>(List<T> li)
        {
            Dictionary<T, int> count = new Dictionary<T, int>();

            foreach (T item in li)
            {
                if (count.ContainsKey(item)) count[item]++;
                else count.Add(item, 1);
            }

            return count;
        }


        /*
         * 
         * 3. *Дан фрагмент программы:
            Dictionary<string, int> dict = new Dictionary<string, int>()
              {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
              };
                 var d = dict.OrderBy(delegate(KeyValuePair<string,int> pair) { return pair.Value; });
                 foreach (var pair in d)
                {
                  Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
                }
                    а) Свернуть обращение к OrderBy с использованием лямбда-выражения $.
                    б) *Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
         * 
         */
        public static void Task3A()
        {
            
            Dictionary<string, int> dict = new Dictionary<string, int>()
              {
                {"four",4 },
                {"two",2 },
                { "one",1 },
                {"three",3 },
              };

            //а) Свернуть обращение к OrderBy с использованием лямбда - выражения
            var d = dict.OrderBy(x => x.Value);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }

            Console.WriteLine();

            //б) *Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
            Func <KeyValuePair<string, int>, int> FuncForOrdering = new Func<KeyValuePair<string, int>, int>(Ordering);
            var d2 = dict.OrderBy(Ordering);
            foreach (var pair in d2)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        public static int Ordering(KeyValuePair<string, int> vocabulary)
        {
            return vocabulary.Value;
        }
    }
}
