using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // колекція "ключ-значення" з автоматичним сортуванням
            SortedList<string, string> slist = new SortedList<string, string>();

            // максимальне значення ключа
            int maxValue = 18;
            // список ключів для рандому
            List<int> list = new List<int>();

            for (int i = 0; i < maxValue; i++)
            {
                // https://unicode-table.com/ru/
                slist.Add(((char)(ChangeValue.RandomValue(0, maxValue, ref list) + 65)).ToString(), $"Value number {i}");
            }

            Console.WriteLine("\n\tБез внесення змін в сортування:");
            // виведення
            foreach (var i in slist)
            {
                Console.WriteLine($"\tKey: {i.Key}, {i.Value}");
            }

            Console.WriteLine("\n\tЗадання оберненого порядку в сортуванні:");

            foreach (var i in slist.Reverse())
            {
                Console.WriteLine($"\tKey: {i.Key}, {i.Value}");
            }

            // repeat
            DoExitOrRepeat();
        }

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
