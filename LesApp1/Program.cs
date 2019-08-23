using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Примітка. Оскільки тема базується на ознайомленні із вбудованими
// колекціями, і в умові сказано "создать колекцию" але невказано чи свою
// то можна допустити, що іде мова про створення колекції як екземпляра
// користуючись наявними бібліотеками

namespace LesApp1
{
    class Program
    {
        #region БД
        private static string[]
            fNames = new[]
            {
                "Андрій",
                "Антоній",
                "Артем",
                "Богдан",
                "Борис",
                "Вадим",
                "Валерій",
                "Варфоломій",
                "Василь",
                "Віктор",
                "Владислав",
                "Володимир",
                "Георгій",
                "Дмитро",
            },
            sNames = new[]
            {
                "Мельник",
                "Ковальчук",
                "Шевченко",
                "Пінчук",
                "Попович",
                "Іванов",
                "Бойко",
                "Коваленко",
                "Ткачук",
                "Волошин",
                "Козак",
                "Поліщук",
                "Бондаренко",
                "Павлюк",
            },
            products = new[]    // https://studopedia.org/4-170560.html
            {
                "Обладнання",
                "Перероблювані матеріали",
                "Послуги",
                "Інтелектуальна продукція",
            };

        #endregion

        // рандом
        private static Random rnd = new Random();

        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // set size of console
            Console.SetWindowSize(80, 60);
            Console.SetBufferSize(80, 60);

            #region Hashtable
#if false
            Hashtable ht = new Hashtable();
            {
                List<string> keys = new List<string>();
                for (int i = 0; i < rnd.Next(10, 28); i++)
                {
                    // на випадок повторень ключа
                    try
                    {
                        keys.Add(GetFullName());
                        ht.Add(keys.Last(), GetProduct());
                    }
                    catch (ArgumentException)
                    {
                        keys.RemoveAt(i--);
                    }
                }

                // виведення
                foreach (var i in keys)
                {
                    GetRes(i, (string)ht[i]);
                }
            }
#endif
            #endregion

            #region ListDictionary
#if false
            ListDictionary ld = new ListDictionary();
            {
                List<string> keys = new List<string>();
                for (int i = 0; i < rnd.Next(10, 28); i++)
                {
                    // на випадок повторень ключа
                    try
                    {
                        keys.Add(GetFullName());
                        ld.Add(keys.Last(), GetProduct());
                    }
                    catch (ArgumentException)
                    {
                        keys.RemoveAt(i--);
                    }
                }

                // виведення
                foreach (var i in keys)
                {
                    GetRes(i, (string)ld[i]);
                }
            } 
#endif
            #endregion

            #region HybridDictionary
#if false
            HybridDictionary hd = new HybridDictionary();
            {
                List<string> keys = new List<string>();
                for (int i = 0; i < rnd.Next(10, 28); i++)
                {
                    // на випадок повторень ключа
                    try
                    {
                        keys.Add(GetFullName());
                        hd.Add(keys.Last(), GetProduct());
                    }
                    catch (ArgumentException)
                    {
                        keys.RemoveAt(i--);
                    }
                }

                // виведення
                foreach (var i in keys)
                {
                    GetRes(i, (string)hd[i]);
                }
            } 
#endif
            #endregion

            #region SortedList
#if false
            SortedList<string, string> sl = new SortedList<string, string>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                try
                {
                    sl.Add(GetFullName(), GetProduct());
                }
                catch (ArgumentException)
                {
                    i--;
                }
            }

            // виведення
            foreach (var i in sl)
            {
                GetRes(i.Key, i.Value);
            }
#endif
            #endregion

            #region OrderedDictionary
#if false
            OrderedDictionary od = new OrderedDictionary();
            {
                List<string> keys = new List<string>();
                for (int i = 0; i < rnd.Next(10, 28); i++)
                {
                    try
                    {
                        keys.Add(GetFullName());
                        od.Add(keys.Last(), GetProduct());
                    }
                    catch (ArgumentException)
                    {
                        keys.RemoveAt(i--);
                    }
                }

                // виведення
                foreach (var i in keys)
                {
                    GetRes(i, (string)od[i]);
                }
            }
#endif
            #endregion

            #region StringDictionary
#if false
            StringDictionary sd = new StringDictionary();
            {
                List<string> keys = new List<string>();
                for (int i = 0; i < rnd.Next(10, 28); i++)
                {
                    try
                    {
                        keys.Add(GetFullName());
                        sd.Add(keys.Last(), GetProduct());
                    }
                    catch (ArgumentException)
                    {
                        keys.RemoveAt(i--);
                    }
                }

                // виведення
                foreach (var i in keys)
                {
                    GetRes(i, sd[i]);
                }
            } 
#endif
            #endregion

            #region NameValueCollection
#if false
            NameValueCollection nvc = new NameValueCollection();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                nvc.Add(GetFullName(), GetProduct());
            }

            // виведення
            foreach (var i in nvc.AllKeys)
            {
                GetRes(i, nvc[i]);
            } 
#endif
            #endregion

            #region List
#if false
            List<Customer> list = new List<Customer>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                list.Add(GetCustomer());
            }

            // виведення
            foreach (var i in list)
            {
                GetRes(i.FullName, i.CategoryProdact);
            }
#endif
            #endregion

            #region Dictionary
#if false
            Dictionary<string, string> d = new Dictionary<string, string>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                try
                {
                    d.Add(GetFullName(), GetProduct());
                }
                catch (ArgumentException)
                {
                    i--;
                }
            }

            // виведення
            foreach (var i in d)
            {
                GetRes(i.Key, i.Value);
            }
#endif
            #endregion

            #region SortedDictionary
#if false
            SortedDictionary<string, string> sd = new SortedDictionary<string, string>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                try
                {
                    sd.Add(GetFullName(), GetProduct());
                }
                catch (ArgumentException)
                {
                    i--;
                }
            }

            // виведення
            foreach (var i in sd)
            {
                GetRes(i.Key, i.Value);
            }
#endif
            #endregion

            #region LinkedList
#if false
            LinkedList<Customer> ll = new LinkedList<Customer>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                ll.AddLast(GetCustomer());
            }

            // виведення
            foreach (var i in ll)
            {
                GetRes(i.FullName, i.CategoryProdact);
            }
#endif
            #endregion

            #region ArrayList
#if false
            ArrayList al = new ArrayList();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                al.Add(GetCustomer());
            }

            // виведення
            foreach (var i in al)
            {
                GetRes(((Customer)i).FullName, ((Customer)i).CategoryProdact);
            }
#endif
            #endregion

            #region Queue
#if false
            Queue<Customer> q = new Queue<Customer>();
            List<Customer> l = new List<Customer>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                l.Add(GetCustomer());
                q.Enqueue(l.Last());
            }

            // виведення
            foreach (var i in l)
            {
                GetRes(i.FullName, i.CategoryProdact);
            }
            Console.WriteLine();
#if false
            foreach (var i in q)
            {
                GetRes(i.FullName, i.CategoryProdact);
            }
#if false    // можна отримати доступ і після проходження всього перебору
            {
                Customer i = q.Dequeue();
                GetRes(i.FullName, i.CategoryProdact);
            } 
#endif
#endif
#if true
            while (q.Count > 0)
            {
                Customer i = q.Dequeue();
                GetRes(i.FullName, i.CategoryProdact);
            }
#endif

#endif
            #endregion

            #region Stack
#if true
            Stack<Customer> s = new Stack<Customer>();
            List<Customer> l = new List<Customer>();
            for (int i = 0; i < rnd.Next(10, 28); i++)
            {
                l.Add(GetCustomer());
                s.Push(l.Last());
            }

            // виведення
            foreach (var i in l)
            {
                GetRes(i.FullName, i.CategoryProdact);
            }
            Console.WriteLine();
#if false
            foreach (var i in s)
            {
                GetRes(i.FullName, i.CategoryProdact);
            }
#if true    // можна отримати доступ і після проходження всього перебору
            {
                Customer i = s.Pop();
                GetRes(i.FullName, i.CategoryProdact);
            } 
#endif
#endif
#if true
            while (s.Count > 0)
            {
                Customer i = s.Pop();
                GetRes(i.FullName, i.CategoryProdact);
            }
#endif

#endif
            #endregion

            // repeat
            DoExitOrRepeat();
        }

        /// <summary>
        /// Генерація рядка для виведення
        /// </summary>
        /// <param name="customer">клієнт</param>
        /// <param name="product">продукт</param>
        /// <returns></returns>
        // https://docs.microsoft.com/ru-ru/dotnet/standard/base-types/composite-formatting
        private static void GetRes(string customer, string product)
            => Console.WriteLine($"  Customer: {customer,-25} |  Product: {product,-20}");


        /// <summary>
        /// Отримати ПІБ
        /// </summary>
        /// <returns></returns>
        private static string GetFullName()
            => sNames[rnd.Next(0, sNames.Length)] +
            " " + fNames[rnd.Next(0, fNames.Length)];

        private static string GetProduct()
            => products[rnd.Next(0, products.Length)];

        /// <summary>
        /// Отримати клієнта
        /// </summary>
        /// <returns></returns>
        private static Customer GetCustomer()
            => new Customer(GetFullName(), GetProduct());

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
