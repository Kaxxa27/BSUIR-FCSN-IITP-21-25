using System;
using System.Collections.Generic;
using System.Text;

namespace _153503_KakhnouskiEugene_Lab1.Interfaces
{
    interface ICustomCollection<T>
    {
        //Индекстатор коллекции
        T this[int index] { get; set; }
        void Reset();
        void Next();
        T Current();
        int Count { get;}
        void Add(T item);
        void Remove(T item);
        T RemoveCurrent();
    }
}
