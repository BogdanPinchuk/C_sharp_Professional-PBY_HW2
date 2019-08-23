using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp3
{
    /// <summary>
    /// Клас методів розширення
    /// </summary>
    public static class Extention
    {
        /// <summary>
        /// Витягування введених даних користувача
        /// </summary>
        /// <typeparam name="T">тип даних</typeparam>
        /// <param name="array">масив даних</param>
        /// <param name="count">кількість елементів які необхідно витягнути</param>
        /// <returns></returns>
        public static T[] ExtractArray<T>(this T[] array, int count)
        {
            T[] temp = new T[count];
            Array.Copy(array, 0, temp, 0, count);
            return temp;
        }

    }
}
