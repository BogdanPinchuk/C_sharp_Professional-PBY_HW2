using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp3
{
    /// <summary>
    /// Колекція "ключ-значення" зі збереженням хронологічного порядку
    /// </summary>
    class OrderedCollection<TKey, TValue> :
        //IDictionary<TKey, TValue>,    // не дає можливості реалізувати свій KeyValuePair
        ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>,
        IEnumerator<KeyValuePair<TKey, TValue>>,
        IEqualityComparer<KeyValuePair<TKey, TValue>>,
        IComparer<KeyValuePair<TKey, TValue>>
    {
        // Масив даних
        private KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[4];

        /// <summary>
        /// Кількість елементів внесених користувачем
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Ємність масиву
        /// </summary>
        public int Capacity
            => array.Length;

        /// <summary>
        /// Логіка порівняння користувача
        /// </summary>
        // не відомо, що користувач захоче в логіці порівнювати, 
        // тому надамо йому можливість реалізувати свою
        public IComparer<KeyValuePair<TKey, TValue>> Comparer { get; set; }

        #region Exception
        /// <summary>
        /// Вихід за межі масиву
        /// </summary>
        string outOfRange = "\n\tСпроба вийти за межі масиву.";
        #endregion

        /// <summary>
        /// Перевірка чи дана колекція тільки для читання
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Доступ до масиву по індексу
        /// </summary>
        /// <param name="index">індекс</param>
        /// <returns></returns>
        public KeyValuePair<TKey, TValue> this[int index]
        {
            get
            {
                if (0 <= index && index < Count)
                {
                    return array[index];
                }
                else
                {
                    throw new Exception(outOfRange);
                }
            }
            set
            {
                if (0 <= index && index < Count)
                {
                    array[index] = value;
                }
                else if (index == Count)
                {
                    Add(value);
                }
                else
                {
                    throw new Exception(outOfRange);
                }
            }
        }

        /// <summary>
        /// Доступ до масиву по ключу
        /// </summary>
        /// <param name="index">індекс</param>
        /// <returns></returns>
        // установка значення тільки по першому найденому ключу
        public TValue[] this[TKey key]
        {
            get
            {
                return array
                    .Where(t => t.Key.Equals(key))
                    .Select(t => t.Value)
                    .ToArray();
            }
        }

        /// <summary>
        /// Ітератор/енумератор
        /// </summary>
        int position = -1;

        /// <summary>
        /// Повернення поточного значення - generic
        /// </summary>
        public KeyValuePair<TKey, TValue> Current
            => array[position];

        /// <summary>
        /// Повернення поточного значення
        /// </summary>
        object IEnumerator.Current
            => Current;

        /// <summary>
        /// Повернення нумератора - generic
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            => this as IEnumerator<KeyValuePair<TKey, TValue>>;

        /// <summary>
        /// Повернення нумератора
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Скидання (лічильника) ітератора
        /// </summary>
        public void Reset() => position = -1;

        /// <summary>
        /// Видалення всіх елементів
        /// </summary>
        public void Clear()
        {
            array = new KeyValuePair<TKey, TValue>[4];
            Count = 0;
        }

        /// <summary>
        /// Звільнення пам'яті
        /// </summary>
        public void Dispose() => Reset();

        /// <summary>
        /// Повертає масив ключів даної колекції
        /// </summary>
        public ICollection<TKey> Keys
            => array.ExtractArray(Count).Select(t => t.Key).ToArray();

        /// <summary>
        /// Повертає масив значень даної колекції
        /// </summary>
        public ICollection<TValue> Values
            => array.ExtractArray(Count).Select(t => t.Value).ToArray();

        /// <summary>
        /// Крокування по масиву
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (position++ < Count - 1)
            {
                return true;
            }
            else
            {
                Reset();
                return false;
            }
        }

        /// <summary>
        /// Пошук першого індекса по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int IndexOf(TKey key)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (key.Equals(array[i].Key))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Пошук першого індекса по елементу
        /// </summary>
        /// <param name="item">Елемент</param>
        /// <returns></returns>
        private int IndexOf(KeyValuePair<TKey, TValue> item)
            => IndexOf(item);

        /// <summary>
        /// Превірка наявності ключа
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
            => IndexOf(key) != -1;

        /// <summary>
        /// Перевірка наявності елемента в колекції
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            // перша перевірка чи взагалі є такий ключ 
            if (ContainsKey(item.Key))
            {
                // а так як може бути декілька значень на один ключ, то
                return 0 > Values
                    .Where(t => t.Equals(item.Value) && t.Equals(item.Value))
                    .Select(t => t)
                    .ToArray()
                    .Length;
            }

            return false;
        }

        /// <summary>
        /// Перевірка наявності елемента в колекції по ключу і повернення значення
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (ContainsKey(key))
            {
                value = array[IndexOf(key)].Value;
            }

            value = default(TValue);

            return false;
        }

        /// <summary>
        /// Хеш-код екземпляру
        /// </summary>
        /// <returns></returns>
        public int GetHashCode(KeyValuePair<TKey, TValue> obj)
            => obj.GetHashCode();

        /// <summary>
        /// Порівняння елементів по значенням
        /// </summary>
        /// <param name="x">1-й елемент</param>
        /// <param name="y">2-й елемент</param>
        /// <returns></returns>
        // не відомо, що користувач захоче в логіці порівнювати, 
        // тому надамо йому можливість реалізувати свою
        public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            => (Comparer ?? new NestedCompare()).Compare(x, y);

        /// <summary>
        /// Перевірка рівності 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            => x.Equals(y);     // більш логічніше якщо б я користувався цим
                                //=> 0 == Compare(x, y);    // якщо опиратися на значення ключів

        /// <summary>
        /// Копіює ICollection в Array, починаючи з певного індексу
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (0 <= arrayIndex && arrayIndex < Count)
            {
                Array.Copy(this.array, arrayIndex, array, 0, Count - arrayIndex);
            }
            else
            {
                throw new Exception(outOfRange);
            }
        }

        /// <summary>
        /// Додавання одного елемента
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">значення</param>
        public void Add(TKey key, TValue value)
            => Add(new KeyValuePair<TKey, TValue>(key, value));

        /// <summary>
        /// Додавання одного елемента
        /// </summary>
        /// <param name="item">Структура</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            // аналіз розмірів і зміна ємності при необхідності
            AnaliseSize(ref this.array, Count, 1);

            // додавання нового елемента
            array[Count++] = item;
        }

        /// <summary>
        /// Аналіз і зміна розмірів масива за необхідністю
        /// </summary>
        /// <typeparam name="T">Тип елементів</typeparam>
        /// <param name="array">вхідний масив</param>
        /// <param name="ownCount">Кількість власних елементів</param>
        /// <param name="newCount">Кількість вхідних нових елементів</param>
        private void AnaliseSize<T>(ref T[] array, int ownCount, int newCount)
        {
            #region вибір розміру масиву
            // в даному випадку для керування об'ємом масиву необхідно
            // розв'язати рівняння: capacity = 2^n > count
            // 2^n > count
            // log_2(2^n) > log_2(count)
            // n > log_2(count)
            // n = ln(count) / ln(2)
            // а так як передається певна кількість елементів length,
            // які необхідно доадти в масив, то рівняння прийме вигляд
            // n = ln(count + length) / ln(2)
            // якщо count + length >= capacity то міняємо розмір
            #endregion

            // розрахунок степіня двійки, який визначатиме ємність
            int power = (int)Math.Ceiling(
                Math.Log(ownCount + newCount) / Math.Log(2));

            if (ownCount + newCount >= array.Length ||      // при додаванні елементів
                (ownCount + newCount < array.Length / 2 &&  // при видаленні елементів
                array.Length / 2 >= 4))                     // щоб не було падіння нижче ємності в 4 елементи
            {
                Resize<T>((int)Math.Pow(2, power), ref array);
            }
        }

        /// <summary>
        /// Зміна розміру масиву
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="capacity">Нова ємність масиву</param>
        /// <param name="coutn">Кількість чоловік певного типу</param>
        /// <param name="array">Масив певного типу</param>
        private void Resize<T>(int capacity, ref T[] array)
        {
            // створення нового масиву
            T[] temp = new T[capacity];
            // копіювання елементів
            Array.Copy(array, 0, temp, 0, array.Length);
            // змінна ссилки на новий масив
            array = temp;
        }

        /// <summary>
        /// Видалення елемента по індексу
        /// </summary>
        /// <param name="index">індекс</param>
        public void RemoveAt(int index)
        {
            if ((0 <= index && index < Count) == false)
            {
                // вихід за межі
                throw new Exception(outOfRange);
            }

            // зміщення елементів
            Array.Copy(this.array, index + 1, this.array, index, Count-- - index - 1);
            // зміна розмірів за потребою
            AnaliseSize(ref this.array, Count, 0);
        }

        /// <summary>
        /// Видалення тільки першого елемента по вказаному значенню і повернення результату успішності
        /// </summary>
        /// <param name="item">значення</param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
            {
                RemoveAt(IndexOf(item));
            }

            return false;
        }

        /// <summary>
        /// Видалення тільки першого елемента по ключу
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (ContainsKey(key))
            {
                RemoveAt(IndexOf(key));
            }

            return false;
        }

        /// <summary>
        /// Сортування даних в хронологічному порядку
        /// </summary>
        public void SortOrder()
        {
            // створюємо тимчасовий масив
            KeyValuePair<TKey, TValue>[] temp = new KeyValuePair<TKey, TValue>[Count];
            // копіюємо туди елементи
            Array.Copy(array, 0, temp, 0, Count);
            // сортуємо масив
            temp = temp.OrderBy(t => t.Time).Select(t => t).ToArray();
            // перезаписуємо дані в необхідному (хронологічному порядку)
            Array.Copy(temp, 0, this.array, 0, Count);
        }

        /// <summary>
        /// Сортування в зворотньому хронологічному порядку
        /// </summary>
        public void SortDescending()
        {
            // створюємо тимчасовий масив
            KeyValuePair<TKey, TValue>[] temp = new KeyValuePair<TKey, TValue>[Count];
            // копіюємо туди елементи
            Array.Copy(array, 0, temp, 0, Count);
            // сортуємо масив
            temp = temp.OrderByDescending(t => t.Time).Select(t => t).ToArray();
            // перезаписуємо дані в необхідному (хронологічному порядку)
            Array.Copy(temp, 0, this.array, 0, Count);
        }

        /// <summary>
        /// Вложений клас для логіки прівняння
        /// </summary>
        private class NestedCompare :
            IComparer<KeyValuePair<TKey, TValue>>
        {
            /// <summary>
            /// Невірний введениий тип
            /// </summary>
            string errorType = "\n\tСпроба ввести недопустимий тип.";

            /// <summary>
            /// Перевірка типу, чи можна їх порівнювати
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="type">змінна певного типу</param>
            private void ExamType<T>(T type)
            {
                // знаходження коду базового типу
                //int code = (int)Type.GetTypeCode(typeof(T));
                int code = (int)Type.GetTypeCode(type.GetType());

                if ((4 <= code && code <= 16) == false)
                {
                    throw new Exception(errorType);
                }
            }

            // порівняння по ключу

            /// <summary>
            /// Внутрішня логіка порівняння
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                // перевірка типу
                ExamType(x.Key);

                dynamic a = x.Key,
                    b = y.Key;

                if (a > b)
                {
                    return 1;
                }
                else if (a < b)
                {
                    return -1;
                }
                else   // a == b
                {
                    return 0;
                }
            }
        }

    }
}
