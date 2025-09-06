#ifndef HASH_H
#define HASH_H

#include<my_stack.h>
#include<cmath>


class Hash
{    
protected:
    My_Stack<int>* _arr;

    int __arrsize;
    int _size;

    const double hash_num = 0.123456789;

    int getHash(int key) {

        double temp = key * hash_num;
        temp = fabs(temp - (int)temp);
        return (int)(__arrsize * temp);
    }

public:
    Hash(int _arrsize) : __arrsize(_arrsize) {

        _arr = new My_Stack<int>[__arrsize];
        _size = 0;
    }

    ~Hash() {
        clear();
        delete[] _arr;
    }

    void clear() {
        for (int i = 0; i < __arrsize; i++)
            _arr[i].clear();

        _size = 0;
    }

    void push(int key) {

        int hash = getHash(key);

        if (My_Stack<int>::Iterator(_arr[hash],key).check_element_in_stack())
        {
            _arr[hash].push(key);
            ++_size;
        }
    }

    void pop(int key) {

        int hash = getHash(key);

        if (_arr[hash].empty())
            return;

       My_Stack<int> tmp;

        while (!_arr[hash].empty() && _arr[hash].top() != key) {
            tmp.push(_arr[hash].top());
            _arr[hash].pop();
        }

        if (!_arr[hash].empty()) {
            _arr[hash].pop();
            --_size;
        }

        while (!tmp.empty()) {
            _arr[hash].push(tmp.top());
            tmp.pop();
        }

//        _arr[hash] = My_Stack<int>::Iterator(_arr[hash],key).del_element();
    }

    bool contains(int key) {

     int hash = getHash(key);

      return !My_Stack<int>::Iterator(_arr[hash],key).check_element_in_stack();
    }
};

class HashTable : public Hash
{
public:

    My_Stack<int>* get_arr() {

        return _arr;
    }

    int get_size() {

        return _size;
    }

    int get__arrsize() {

        return __arrsize;
    }

    int get_max() {

        int max = INT_MIN;

        for (int i = 0; i < __arrsize; i++) {

            My_Stack<int> tmp = _arr[i];

            while (!tmp.empty()) {
                max = tmp.top() > max ? tmp.top() : max;
                tmp.pop();
            }
        }

        return max;
    }

    HashTable(int size) : Hash(size){};
};


#endif // HASH_H
