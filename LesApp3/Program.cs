using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// ключовий момент OrderedDictionary:
// - Розташування елементів лишається згідно порядку їх додавання
// оскільки основний напрям сформований на хронологічний порядок,
// добре було б реалізувати можливість зберігати декілька значень 
// на один ключ на основі NameValueCollection
// а також сортувати внутрішньо і можливісю відновлення хронологічного порядку
// так як клас можна розширити іншими внутрішніми методами сортування по ключу і по значеннню
// і для пришвидшення виконання реалізувати його generic
// додамо можливість доступу до наступного після оснанього елемента
// через індексатор, але лише при при присвоєнні

namespace LesApp3
{
    class Program
    {
        // рандом
        private static Random rnd = new Random();

        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // set size of console
            Console.SetWindowSize(80, 60);
            Console.SetBufferSize(80, 60);

            // create own colection
            OrderedCollection<char, int> collection = new OrderedCollection<char, int>();

            // add data to collection, 5 times
            for (int i = 0; i < 15; i++)
            {
                collection.Add((char)(65 + rnd.Next(0, 26)), rnd.Next(0, byte.MaxValue));
                Thread.Sleep(rnd.Next(10, 26));
            }

            // виведення
            Console.WriteLine("\n\tВнесені дані без змін:\n");
            foreach (var i in collection)
            {
                Console.WriteLine("\t" + i.ToString());
            }

            #region тестування CopyTo + сортування по ключу для перевірки чи є однакові елементи
#if false
            var mas = new KeyValuePair<char, int>[collection.Count];
            collection.CopyTo(mas, 0);

            mas = mas.OrderBy(t => t.Key).Select(t => t).ToArray();

            Console.WriteLine();

            Console.WriteLine("\n\tВідсотровані дані по ключу:\n");
            foreach (var i in mas)
            {
                Console.WriteLine("\t" + i.ToString());
            }  
#endif
            #endregion

            // сортуємо в зворотньому напрямку
            collection.SortDescending();
            // додаємо елемент через індексатор
            Thread.Sleep(rnd.Next(10, 28));
            collection[collection.Count] = new KeyValuePair<char, int>((char)(65 + rnd.Next(0, 26)), rnd.Next(0, byte.MaxValue));

            // виведення
            Console.WriteLine("\n\tРезультат після сортування в зворотньому напрямку і додавання елемента через індексатор:\n");
            foreach (var i in collection)
            {
                Console.WriteLine("\t" + i.ToString());
            }

            Console.WriteLine("\n\tПорівняння 1-го і останього елементів:\n");
            Console.WriteLine("\t" + collection[0].ToString());
            Console.WriteLine("\t" + collection[collection.Count - 1].ToString());
            Console.WriteLine("\t" + collection.Compare(collection[0], collection[collection.Count - 1]));

            // спроба вийти за діапазон
            try
            {
                Console.WriteLine(collection[collection.Count]);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\t" + e.Message);
            }

            // видалення перших 5 елементів
            for (int i = 0; i < 5; i++)
            {
                collection.RemoveAt(0);
            }

            Console.WriteLine("\n\tРезультат після видалення 5 перших елементів:\n");
            foreach (var i in collection)
            {
                Console.WriteLine("\t" + i.ToString());
            }

            collection.SortOrder();
            Console.WriteLine("\n\tПісля сортування:\n");
            foreach (var i in collection)
            {
                Console.WriteLine("\t" + i.ToString());
            }

            #region Отримання всіх ключів
            {
                Console.WriteLine("\n\tКлючі:\n");
                var a = collection.Keys;
                foreach (var i in a)
                {
                    Console.Write(" " + i);
                }
                Console.WriteLine("\n\n\tЗначення:\n");
                var b = collection.Values;
                foreach (var i in b)
                {
                    Console.Write(" " + i);
                }
            }
            #endregion

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
