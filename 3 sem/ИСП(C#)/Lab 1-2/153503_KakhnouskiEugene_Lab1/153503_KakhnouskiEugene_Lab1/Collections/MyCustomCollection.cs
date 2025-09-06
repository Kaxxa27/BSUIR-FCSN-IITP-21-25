using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using _153503_KakhnouskiEugene_Lab1.Interfaces;


namespace _153503_KakhnouskiEugene_Lab1.Collections
{
    class MyCustomCollection<T> : ICustomCollection<T>, IEnumerable<T>
    {
        #region Class Node
        private class Node
        {
            public Node() { }
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node(T Data) { this.Data = Data; }

        }
        #endregion

        #region Value
        private Node Head { get; set; }
        private Node Tail { get; set; }
        private Node CurrentNode { get; set; }
        private int count { get; set; }
        #endregion

        #region Index
        public T this[int index]
        {
            get
            {              
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Выход за пределы коллекции");
                }
                Node tmp = Head;

                while (index != 0)
                {
                    tmp = tmp.Next;
                    index--;
                }
                return tmp.Data;
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Выход за пределы коллекции");
                }
                Node tmp = Head;

                while (index != 0)
                {
                    tmp = tmp.Next;
                    index--;
                }


                tmp.Data = value;
            }
        }
        #endregion

        #region IndexOutOfRange
        //private bool CheckIndex(int index)
        //{
        //    try
        //    {
        //        if (index < 0 || index >= Count)
        //        {
        //            throw new IndexOutOfRangeException("Выход за пределы коллекции");
        //        }
        //    }
        //    catch (IndexOutOfRangeException ex) 
        //    {
        //        Console.WriteLine(ex.Message);
        //        return true;
        //    }
        //    return false;
        //}
        #endregion

        #region Count
        public int Count => count;
        #endregion

        #region Add
        public void Add(T item)
        {
            Node node = new Node(item);

            CurrentNode = new Node(item);
            CurrentNode.Next = Head;

            if (Head == null)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }
            Tail = node;
            count++;
        }
        #endregion

        #region Remove
        public void Remove(T item)
        {
            bool CheckOnRemove = false;

            Node current = Head;
            Node prev = null;

            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    CheckOnRemove = true;
                    //Если не голова
                    if (prev != null)
                    {
                        prev.Next = current.Next;
                        if (current.Next == null)
                        {
                            Tail = prev;
                        }
                    }
                    else
                    {
                        Head = Head.Next;
                        if (Head == null)
                        {
                            Tail = null;
                        }

                    }
                    count--;
                }
                prev = current;
                current = current.Next;
            }

            try
            {
                if (!CheckOnRemove)
                {
                    throw new Item_Was_nt_Remove_Exeption("Объект с такими данными отсутствует в коллекции.");
                }
            }
            catch (Item_Was_nt_Remove_Exeption ex) { Console.WriteLine(ex.Message); }
      
         
        }
        #endregion

        #region RemoveCurrent
        public T RemoveCurrent()
        {

            Remove(CurrentNode.Data);
            return CurrentNode.Data;

        }
        #endregion

        #region Next_Reset_Current
        public T Current() => CurrentNode.Data;
        public void Next()
        {

            CurrentNode = CurrentNode.Next;
            if (CurrentNode == null)
            {
                CurrentNode = Tail;
            }
        }

        public void Reset()
        {
            CurrentNode = new Node();
            CurrentNode.Next = Head;
        }

        #endregion

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new MyCustomEnumerator<T>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        #region MyCustomEnumerator<K>
        private class MyCustomEnumerator<K> : IEnumerator<T>
        {
            private MyCustomCollection<T> tmp;

            private int position = -1;
            public MyCustomEnumerator(MyCustomCollection<T> tmp)
            {
                this.tmp = tmp; 
            }
            public bool MoveNext()
            {
                if(position == tmp.Count - 1)
                {
                    Reset();
                    return false;
                }

                position++;
                return true;
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current => tmp[position];

            T IEnumerator<T>.Current => tmp[position];

            void IDisposable.Dispose() { }
        }
        #endregion

        #region MyCustomException

        private class Item_Was_nt_Remove_Exeption : Exception
        {
            public Item_Was_nt_Remove_Exeption(string message) : base(message) { }
         
        }
        #endregion
     
    }
}

