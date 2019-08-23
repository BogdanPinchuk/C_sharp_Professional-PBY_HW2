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

                throw new Exception(outOfRange);
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

                throw new Exception(outOfRange);
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
            => array.Select(t => t.Key).ToArray();

        /// <summary>
        /// Повертає масив значень даної колекції
        /// </summary>
        public ICollection<TValue> Values
            => array.Select(t => t.Value).ToArray();

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
            // перша перевірка чи взагалі такий ключ 
            if (ContainsKey(item.Key))
            {
                // а так як може бути декілька значень на один ключ, то
                return 0 > Values
                    .Where(t => t.Equals(item.Value))
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







        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
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
