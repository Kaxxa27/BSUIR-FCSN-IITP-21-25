#ifndef VECTORLIB_H
#define VECTORLIB_H

#include "VectorLib_global.h"
#include<iostream>
#include<exception>

template<class T>
class VECTORLIB_EXPORT MyVector
{
public:
    //iterator
    class iterator{
    public:

        iterator() : _ptr(nullptr) {}
        iterator(T* ptr) : _ptr(ptr) {}
        iterator(const MyVector<T>::iterator& other) : _ptr(other._ptr) {}

        iterator& operator++()
        {
            ++_ptr;
            return *this;
        }
        iterator& operator++(int)
        {
            iterator temp = *this;
            ++(*this);
            return temp;
        }
        iterator& operator--()
        {
            --_ptr;
            return *this;
        }
        iterator& operator--(int)
        {
            iterator temp = *this;
            --(*this);
            return temp;
        }

        //for _size
        int operator-(iterator temp)
        {
            return _ptr - temp._ptr;
        }
        int operator+(iterator temp)
        {
            return _ptr + temp._ptr;
        }

        iterator& operator+(int n)
        {
            ++_ptr;
            return *this;
        }

        iterator& operator=(const iterator& other_it)
        {
            _ptr = other_it._ptr;
            return *this;
        }

        T& operator*() const
        {
            return *_ptr;
        }

        bool operator==(const iterator& other_it) const
        {
            return _ptr == other_it._ptr;
        }
        bool operator!=(const iterator& other_it) const
        {
            return _ptr != other_it._ptr;
        }

    private:
        T* _ptr;
        friend MyVector<T>;
    };

    //default constructor
    MyVector() : _size(0), _capacity(2 * _size), _arr(nullptr) {};

    //only num constructor
    MyVector(size_t sz, const T& value) : _size(sz), _capacity(2 * _size)
    {
        _arr = new T[_capacity];
        for (size_t i = 0; i != _size; ++i)
        {
            _arr[i] = value;
        }
    }

    //vector constructor
    MyVector(const MyVector<T>& v) : _size(v._size), _capacity(v._capacity) {
        _arr = new T[_capacity];
        for (size_t i = 0; i != _size; ++i)
        {
            _arr[i] = v._arr[i];
        }
    };

    //iterators constructor
    MyVector(iterator start, iterator end) : _size(end - start), _capacity(2 * _size){
        _arr = new T[_capacity];
        for (size_t i = 0; i != _size; ++i)
        {
            _arr[i] = *start++;
        }
    }

    //destructor
    ~MyVector() {
        for (size_t i = 0; i < _size; i++)
        {
            (_arr + i)->~T();
        }
        delete[] _arr;
    };

    void reserve(size_t new_capacity)
    {
        if (_size < _capacity) return;

        T* newarr = new T[new_capacity];//BAD!!!, but pofig kak-to xd

        for (size_t i = 0; i < _size; ++i)
        {
            new(newarr + i) T(_arr[i]);//placement new
        }

        /*for (size_t i = 0; i < _size; ++i)
        {
            (_arr + i)->~T();
        }*/
        delete[] _arr;//Also Bad
        _arr = newarr;
        _capacity = new_capacity;
    }

    void resize(size_t new_size, const T& value = T())
    {
        if (new_size > _capacity) reserve(new_size);

        for (size_t i = _size; i < new_size; ++i)
        {
            new(_arr + i) T(value);
        }

        if (new_size < _size)
        {
            _size = new_size;

        }
    }

    void push_back(const T& value)
    {
        if (_capacity == _size) resize(2 * (_size + 1));

        _arr[_size++] = value;

    }

    void pop_back()
    {
        if (_size == 0) return;
        --_size;
        (_arr + _size)->~T();
    }

    size_t size() const
    {
        return _size;
    }
    size_t capacity() const
    {
        return _capacity;
    }

    void assign(iterator start, iterator end)
    {
        for (size_t i = 0; i < _size; ++i)
        {
            (_arr + i)->~T();
        }

        _size = end - start;
        _capacity = _size * 2;

        delete[] _arr;

        _arr = new T[_capacity];

        for (size_t i = 0; i != _size && start != end; ++i)
        {
            _arr[i] = *(start++);
        }
    }
    void assign(size_t count, const T& value)
    {
        for (size_t i = 0; i < _size; ++i)
        {
            (_arr + i)->~T();
        }

        _size = count;
        _capacity = _size * 2;

        delete[] _arr;

        _arr = new T[_capacity];

        for (size_t i = 0; i != _size; ++i)
        {
            _arr[i] = value;
        }
    }

    void clear()
    {
        for (size_t i = 0; i < _size; ++i)
        {
            (_arr + i)->~T();
        }

        _size = 0;
        _capacity = _size * 2;
    }

    bool empty() const
    {
        return _size == 0;
    }

    size_t max_size() const
    {
        return SIZE_MAX / sizeof(T);
    }

    //My erase
    void erase(size_t pos)
    {

        if (pos >= _size)
        {
            throw;
        }

        /*try {
            if (pos >= _size || pos < 0)
            {
                throw invalid_argument(" incorrect pos!");
            }
        }
        catch (invalid_argument&) {
            cout << "Warning! Erase don't work! Incorrect pos. " << endl;
        }*/

        size_t new_size = _size - 1;
        size_t new_capacity = new_size * 2;

        T* new_arr = new T[new_capacity];


        for(size_t i = 0, j = i; i < _size; ++i, ++j)
        {
            if (i == pos)
            {
                (_arr + i)->~T();
                new_arr[i] = _arr[j + 1];
                j++;
            }
            else
            {
                new_arr[i] = _arr[j];
            }
        }

        _size = new_size;
        _capacity = new_capacity;

        delete[] _arr;

        _arr= new_arr;

    }

    void swap( MyVector<T>& other)
    {
        std::swap(*this, other);
    }

    iterator begin()
    {
        return iterator(_arr);
    }
    const iterator cbegin()
    {
        return iterator(_arr);
    }

    iterator end()
    {
        return iterator(_arr + _size);
    }
    const iterator cend()
    {
        return iterator(_arr + _size);
    }

    iterator rbegin()
    {
        return iterator(end() - 1);
    }
    const iterator crbegin()
    {
        return iterator(end() - 1);
    }

    iterator rend()
    {
        return iterator(begin() - 1);
    }
    const iterator crend()
    {
        return iterator(begin() - 1);
    }

    T* data()
    {
        return _arr;
    }

    T& at(size_t index)
    {
        if (index >= _size)
        {
            throw;
        }
        return _arr[index];
    }
    const T& at(size_t index) const
    {
        if (index >= _size)
        {
            throw ;
        }
        return _arr[index];
    }

    T& back()
    {
        return _arr[_size - 1];
    }
    T& front()
    {
        return _arr[0];
    }


    T& operator[](int num)
    {
        return _arr[num];
    }


    //==, !=

private:
    T* _arr;
    size_t _size;
    size_t _capacity;
};

#endif // VECTORLIB_H
