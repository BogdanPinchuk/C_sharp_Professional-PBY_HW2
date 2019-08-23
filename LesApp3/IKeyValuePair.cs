using System;

namespace LesApp3
{
    /// <summary>
    /// Інтерфейст на структуру "ключ-значення"
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    interface IKeyValuePair<TKey, TValue>
    {
        TKey Key { get; }
        DateTime Time { get; }
        TValue Value { get; }
    }
}