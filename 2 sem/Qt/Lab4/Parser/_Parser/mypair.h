#ifndef MYPAIR_H
#define MYPAIR_H

template<class T1, class T2>
struct MyPair
{

    T1 first;
    T2 second;

    MyPair(T1 t1, T2 t2) : first(t1), second(t2) {};

    MyPair() = default;
    ~MyPair() = default;

    MyPair<T1, T2>& operator=(MyPair<T1, T2> other)
       {
          this->first = other.first;
          this->second = other.second;

          return *this;
       }
    void swap(MyPair<T1, T2>& SecondPair)
       {
            MyPair<T1, T2> temp(first, second);

            temp = *this;
            *this = SecondPair;
            SecondPair = temp;
       }
};

#endif // MYPAIR_H
