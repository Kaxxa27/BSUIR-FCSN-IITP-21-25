#ifndef MYDEQUE_H
#define MYDEQUE_H

using namespace std;

#include<iostream>
#include<QString>
#include<QTextStream>

#define SIZE 10

template <class T>
class MyDeque
{
private:
    T** _arr;
    size_t _size;
    size_t _capacity;

    pair<int,int> _front_pair, _end_pair;

public:

    class Iterator
    {

    private:
        T** iterator_arr;
        pair<int,int> current_element;
        friend MyDeque<T>;
    public:
        Iterator(T** temp_arr, pair<int,int> temp_pair )
        {
            iterator_arr = temp_arr;
            current_element = temp_pair;
        }

        T& operator*() //value
        {
            return iterator_arr[current_element.first][current_element.second];
        }

        Iterator& operator++()
        {
            if(current_element.second == SIZE - 1)
            {
                current_element.second = 0;
                ++current_element.first;
            }
            else
            {
                ++current_element.second;
            }
            return *this;
        }

        Iterator& operator++(int) //???
        {
            Iterator temp = *this;
            ++(*this);
            return temp;
        }

        Iterator& operator--()
        {
            if(current_element.second == SIZE - 10)
            {
                current_element.second = SIZE - 1;
                --current_element.first;
            }
            else
            {
                --current_element.second;
            }
            return *this;
        }

        Iterator& operator--(int) //???
        {
            Iterator temp = *this;
            --(*this);
            return temp;
        }

        Iterator& operator+=(int n)
        {

            if(n >= 0) while(n--) ++*this;
            else while(n--) --*this;
            return *this;
        }

        Iterator& operator+(int n)
         {
             return *this += n;
         }

        Iterator& operator-=(int n)
         {
             return *this += -n;

         }

        Iterator& operator-(int n)
         {
             return *this -= n;
         }

        bool operator!=(Iterator other)
        {
            return current_element != other.current_element;
        }

        bool operator<(Iterator other)
        {
            return current_element < other.current_element;
        }

        bool operator>(Iterator other)
        {
            return current_element > other.current_element;
        }

        bool operator==(Iterator other)
        {
            return current_element == other.current_element;
        }

    };

    MyDeque();
    ~MyDeque();

    void push_front(T value);
    void push_back(T value);

    void pop_front();
    void pop_back();

    void clear();

    bool empty();

    size_t size() {return _size;}
    size_t capacity() {return _capacity;}

    void resize();

    QString ToString();

    Iterator begin() { return Iterator(_arr, _front_pair); };
    Iterator end()   { return Iterator(_arr, _end_pair);};

};

template<class T> MyDeque<T>::MyDeque() : _size(0), _capacity(100)
{
    _arr = new T*[_capacity];
    for(int i = 0; i < _capacity; ++i)
    {
        _arr[i] = nullptr;
    }

    //нет еще массивов, пустой deque
    _front_pair.first = _front_pair.second = -1;
    _end_pair.first = _end_pair.second = -1;
}

template<class T> MyDeque<T>::~MyDeque()
{
    for(int i = _front_pair.first; i < _end_pair.first; ++i)
    {
        delete _arr[i];
    }
    delete _arr;
}

template<class T>void MyDeque<T>::push_front(T value)
{
    ++_size;

    if(_front_pair.second == -1)//нет массива
    {
        T* internal_arr =  new T[SIZE];
        _arr[_capacity / 2] = internal_arr;

        _front_pair.first = _end_pair.first = _capacity / 2;

        _front_pair.second = _end_pair.second = 0;

        _arr[_front_pair.first][_front_pair.second] = value;

    }
    else if(!_front_pair.second)
    {
        if(!_front_pair.first){
            resize();
        }
        --_front_pair.first;
        _front_pair.second = SIZE - 1;
        T* internal_arr = new T[SIZE];
        _arr[_front_pair.first] = internal_arr;
        _arr[_front_pair.first][_front_pair.second] = value;
    }
    else
    {
        --_front_pair.second;
        _arr[_front_pair.first][_front_pair.second] = value;
    }

}

template<class T> void MyDeque<T>::push_back(T value)
{
    ++_size;
    if(_end_pair.second == -1)//нет массива
    {
        T* internal_arr =  new T[SIZE];
        _arr[_capacity / 2] = internal_arr;

        _front_pair.first = _end_pair.first = _capacity / 2;

        _front_pair.second = _end_pair.second = 0;

        _arr[_end_pair.first][_end_pair.second] = value;

    }
    else if(_end_pair.second == SIZE - 1)
    {
        if(_end_pair.first == _capacity - 1)
        {
            resize();
        }

        ++_end_pair.first;

        T* internal_arr =  new T[SIZE];
        _arr[_end_pair.first] = internal_arr;

        _end_pair.second = 0;
        _arr[_end_pair.first][_end_pair.second] = value;
    }
    else
    {
        ++_end_pair.second;
        _arr[_end_pair.first][_end_pair.second] = value;
    }
}

template<class T>  void MyDeque<T>::pop_front()
{
    if(!_size) return;
    --_size;
    if(_front_pair.first < _end_pair.first)
    {
        if(_front_pair.second == SIZE - 1)
        {
            delete _arr[_front_pair.first];
            ++_front_pair.first;
            _front_pair.second = 0;
        }
        else
        {
            ++_front_pair.second;
        }
    }
    else if(_front_pair.second < _end_pair.second){
        ++_front_pair.second;
    }
    else if(_front_pair.second == _end_pair.second && _front_pair.first != -1)
    {
        delete _arr[_front_pair.first];
        _front_pair.first = _end_pair.first = -1;
        _front_pair.second = _end_pair.second = -1;
    }
}

template<class T> void MyDeque<T>::pop_back()
{
     if(!_size) return;
    --_size;
    if(_front_pair.first < _end_pair.first)
    {
        if(_end_pair.second == 0)
        {
            delete _arr[_end_pair.first];
            --_end_pair.first;
            _end_pair.second = SIZE - 1;
        }
        else
        {
            --_end_pair.second;
        }
    }
    else if(_front_pair.second < _end_pair.second){
        --_end_pair.second;
    }
    else if(_front_pair.second == _end_pair.second && _end_pair.first != -1)
    {
        delete _arr[_end_pair.first];
        _front_pair.first = _end_pair.first = -1;
        _front_pair.second = _end_pair.second = -1;
    }
}

template<class T> void MyDeque<T>::clear()
{
    if(!empty())
    {
        _capacity = 100;
        _size = 0;
        for(int i = _front_pair.first; i <= _end_pair.first; ++i){
            delete _arr[i];
        }
        _front_pair.first = _end_pair.first = -1;
        _front_pair.second = _end_pair.second = -1;
    }
}

template<class T> bool MyDeque<T>::empty()
{
    return !_size;
}

template<class T> void MyDeque<T>::resize()
{
    _capacity *= 2;
    T** new_arr = new T*[_capacity];

    int index = _capacity / 2 - (_end_pair.first - _front_pair.first) / 2;
    for(int i = _front_pair.first; i < _end_pair.first; ++i, ++index)
    {
        new_arr[index] = _arr[i];
    }
    _front_pair.first = index;
    _end_pair.first = _capacity / 2 + (_end_pair.first - _front_pair.first) / 2;
    delete _arr;

    _arr = new_arr;
}

template<class T> QString MyDeque<T>::ToString()
{
    QString str;
    if(_size){
        for(int i = _front_pair.first; i <= _end_pair.first; ++i)
        {
            if(i == _front_pair.first && i == _end_pair.first)
            {
                for(int j = _front_pair.second; j <= _end_pair.second; ++j)
                {
                   str += QString::number(_arr[i][j]) + ' ';
                }

            }
            else if(i == _front_pair.first)
            {
                for(int j = _front_pair.second; j < SIZE; ++j)
                {
                        str += QString::number(_arr[i][j]) + ' ';
                }

            }
            else if(i == _end_pair.first)
            {
                for(int j = 0; j <= _end_pair.second; ++j)
                {
                    str += QString::number(_arr[i][j]) + ' ';
                }

            }
            else
            {
                for(int j = 0; j < SIZE; ++j)
                {
                    str += QString::number(_arr[i][j]) + ' ';
                }

            }
        }
    }

    return str;
}



#endif // MYDEQUE_H
