#ifndef MY_STACK_H
#define MY_STACK_H

#include<iostream>
#include<QDebug>

template<class T>
class My_Stack
{
private:
    struct Node {
        T data;
        Node* prev;
    };

    Node* _up_element;
    int _size;

public:

    class Iterator
    {

    private:
        My_Stack iterator_stack;
        int key;

    public:

        Iterator(My_Stack temp_stack, int key )
        {
            iterator_stack = temp_stack;
            this->key = key;
        }

        bool check_element_in_stack() // false, when the element exists
        {

           bool check = false;

           if (iterator_stack.empty()) return check = true;

           while(!iterator_stack.empty() && iterator_stack.top() != key)
           {
               if(iterator_stack.empty()) break;
               iterator_stack.pop();
               qDebug() << "While succses!";
               check = false;
           }


           if (iterator_stack.empty()) check = true;
            qDebug() << "check succses!";
           return check;
        }

//        My_Stack<T> del_element()
//        {

//            if (iterator_stack.empty())
//                return iterator_stack;

//            My_Stack<T> new_stack;

//            while (!iterator_stack.empty() && iterator_stack.top() != key) {
//                new_stack.push(iterator_stack.top());
//                iterator_stack.pop();
//            }

//            if (!iterator_stack.empty()) {
//                iterator_stack.pop();
//                iterator_stack._size--;
//            }

//            while (!iterator_stack.empty()) {
//                new_stack.push(iterator_stack.top());
//                iterator_stack.pop();
//            }


//            return new_stack;
//        }

        T& operator*() //value
        {
            return iterator_stack->top();
        }

        bool operator!=(Iterator other)
        {
            return key != other.key;
        }

        bool operator<(Iterator other)
        {
            return key < other.key;
        }

        bool operator>(Iterator other)
        {
            return key > other.key;
        }

        bool operator==(Iterator other)
        {
            return key == other.key;
        }

    };

    My_Stack() : _up_element(nullptr), _size(0){};

    ~My_Stack() {
        while (_up_element)
            pop();
     }

    My_Stack(const My_Stack& other) {

        _up_element = nullptr;
        _size = 0;

        Node* temp = other._up_element;

        T* arr = new T[other._size];

        for (int i = 0; i < other._size; i++) {
            arr[i] = temp->data;
            temp = temp->prev;
        }

        for (int i = other._size - 1; i >= 0; i--)
            push(arr[i]);

        delete[] arr;
    }

    My_Stack& operator=(const My_Stack& other) {

        clear();

        Node* temp = other._up_element;

        T* arr = new T[other._size];

        for (int i = 0; i < other._size; i++) {
            arr[i] = temp->data;
            temp = temp->prev;
        }

        for (int i = other._size - 1; i >= 0; i--)
            push(arr[i]);

        delete[] arr;

        return *this;
    }

    void clear() {
        while (_up_element)
            pop();
    }

    void push(T data) {
        Node* temp = new Node;
        temp->data = data;
        temp->prev = _up_element;
        _up_element = temp;

        ++_size;
    }

    void pop() {
        if (_up_element == nullptr)
            throw(std::exception());

        if (_size > 1) {
            Node* del = _up_element;
            _up_element = _up_element->prev;
            delete del;
        } else {
            delete _up_element;
            _up_element = nullptr;
        }

        --_size;
    }

    T& top() {
        if (_up_element == nullptr)
            throw(std::exception());

        return _up_element->data;
    }

    bool empty() { return !_size; }

    bool size() { return _size; }
};

#endif // MY_STACK_H
