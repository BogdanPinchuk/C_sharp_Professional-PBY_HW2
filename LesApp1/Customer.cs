using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp1
{
    /// <summary>
    /// Клієнт
    /// </summary>
    class Customer
    {
        /// <summary>
        /// ПІБ клієнта
        /// </summary>
        public string FullName { get; set; }
        public string CategoryProdact { get; set; }

        public Customer() { }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="name">ПІБ клієнта</param>
        /// <param name="product">категорія продукту</param>
        public Customer(string name, string product)
        {
            this.FullName = name;
            this.CategoryProdact = product;
        }

    }
}
