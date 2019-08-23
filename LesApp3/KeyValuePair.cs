using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Примітка. Можна було реалізувати, щоб властивість часу була 
// схована від користувача, але якщо в подальшому розширенні пригодиться

namespace LesApp3
{
    /// <summary>
    /// Структура "ключ-значення" + час створення
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    struct KeyValuePair<TKey, TValue> : 
        IKeyValuePair<TKey, TValue>
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; }
        /// <summary>
        /// Значення
        /// </summary>
        public TValue Value { get; }
        /// <summary>
        /// Час створення
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Конструктор користувача
        /// </summary>
        /// <param name="key">ключ</param>
        /// <param name="value">значення</param>
        public KeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
            this.Time = DateTime.Now;
        }

        /// <summary>
        /// Виведення пари "ключ-значення"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"Key: {Key}, Value: {Value};";

        /// <summary>
        /// Хеш-код екземпляру
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => Key.GetHashCode() ^
            Value.GetHashCode() ^
            Time.GetHashCode();

        /// <summary>
        /// Порівняння елементів
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            KeyValuePair<TKey, TValue> temp = (KeyValuePair<TKey, TValue>)obj;
            // структури не можуть бути null, 
            // тому перевірки на це робити не потрібно

            // в даному випадку, ідея погягає в перевірці практично значень
            // елементів, а не перевірка ідентичності і відповідності, 
            // і що це точна копія відповідного елементу

            return this.Key.Equals(temp.Key) &&
                this.Value.Equals(temp.Value);
        }
    }
}
