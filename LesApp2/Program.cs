using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2
{
    class Program
    {
        // рандом
        private static Random rnd = new Random();

        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // різні типи колекцій згідно умови
            Dictionary<int, double> dic = new Dictionary<int, double>();
            SortedList<int, double> sl = new SortedList<int, double>();
            SortedDictionary<int, double> sd = new SortedDictionary<int, double>();

            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                try
                {
                    int num = rnd.Next(0, short.MaxValue);
                    double sum = rnd.NextDouble() * 1000000;
                    dic.Add(num, sum);
                    sl.Add(num, sum);
                    sd.Add(num, sum);
                }
                catch (ArgumentException)
                {
                    i--;
                }
            }

            // виведення
            Console.WriteLine("\n\t Dictionary:\n");
            foreach (var i in dic)
            {
                GetRes(i.Key, i.Value);
            }

            Console.WriteLine("\n\t SortedList:\n");
            foreach (var i in sl)
            {
                GetRes(i.Key, i.Value);
            }

            Console.WriteLine("\n\t SortedDictionary:\n");
            foreach (var i in sd)
            {
                GetRes(i.Key, i.Value);
            }


            // repeat
            DoExitOrRepeat();
        }

        private static void GetRes(int num, double sum)
            => Console.WriteLine($"  ID company: {num,-10} |  Budget: {sum,-10:F2}");


        /// <summary>
        /// Метод виходу або повторення методу Main()
        /// </summary>
        static void DoExitOrRepeat()
        {
            Console.WriteLine("\n\nСпробувати ще раз: [т, н]");
            Console.Write("\t");
            var button = Console.ReadKey(true);

            if ((button.KeyChar.ToString().ToLower() == "т") ||
                (button.KeyChar.ToString().ToLower() == "n")) // можливо забули переключити розкладку клавіатури
            {
                Console.Clear();
                Main();
                // без використання рекурсії
                //Process.Start(Assembly.GetExecutingAssembly().Location);
                //Environment.Exit(0);
            }
            else
            {
                // закриває консоль
                Environment.Exit(0);
            }
        }
    }
}
