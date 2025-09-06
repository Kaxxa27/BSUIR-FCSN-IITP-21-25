#ifndef Pol_Stack_H
#define Pol_Stack_H

#include <iostream>


template<class T>
struct NodeStack
{
    T data;
    NodeStack<T>* Prev;
};

template<class T>
class Pol_Stack
{
    NodeStack<T>* Top;
public:
    Pol_Stack() : Top(nullptr){};
    ~Pol_Stack()
    {
        Clear();
    };
    void Push(T item);
    void Pop();
    int Count();
    void Clear();
    bool Empty();
    T ReturnTop()
    {
        return Top->data;
    };
};


template<class T>
void Pol_Stack<T>::Push(T data)
{
    NodeStack<T>* temp = new NodeStack<T>;

    temp->data = data;
    temp->Prev = Top;
    Top = temp;

}

template<class T>
void Pol_Stack<T>::Pop()
{
    if(Top == nullptr)
    {
        throw std::exception();
    }

    NodeStack<T>* temp;
    temp = Top;
    Top = Top->Prev;
    delete temp;
}

template<class T>
int Pol_Stack<T>::Count()
{
    if(Top == nullptr)
    {
        return 0;
    }
    else
    {
        NodeStack<T>* temp = Top;
        int count = 0;

        while(temp != nullptr)
        {
            count++;
            temp = temp->Prev;
        }
    }
}

template<class T>
void Pol_Stack<T>::Clear()
{
    NodeStack<T>* temp;
    NodeStack<T>* temp2;

    temp = Top;

    while(temp != nullptr)
    {
        temp2 = temp;
        temp = temp->Prev;
        delete temp2;
    }
    Top = nullptr;

}

template<class T>
bool Pol_Stack<T>::Empty()
{
    return !Top;
}
#endif // Pol_Stack_H
