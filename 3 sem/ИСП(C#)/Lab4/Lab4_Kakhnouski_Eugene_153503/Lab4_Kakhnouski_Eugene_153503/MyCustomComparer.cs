using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Lab4_Kakhnouski_Eugene_153503
{
    class MyCustomComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            if (x is null || y is null)
                throw new ArgumentException("Некорректное значение параметра");
            return x.Name.Length - y.Name.Length;
        }
    }
}
