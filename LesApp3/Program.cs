using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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

            var collection = Enumerable.Range(5, 5).ToArray();

            var res = collection.Where(t => t == 4).Select(t => t).ToArray();

            //Dictionary<int, int>


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
