#ifndef BRACKETSTAK_H
#define BRACKETSTAK_H
#include <iostream>

struct bracket
{
    char item;
    int row;
    int column;
};

template<class T>
struct Node
{
    T data;
    Node<T>* Prev;
};

template<class T>
class BracketStak
{
    Node<T>* Top;
public:
    BracketStak() : Top(nullptr){};
    ~BracketStak()
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
void BracketStak<T>::Push(T data)
{
    Node<T>* temp = new Node<T>;

    temp->data = data;
    temp->Prev = Top;
    Top = temp;

}

template<class T>
void BracketStak<T>::Pop()
{
    if(Top == nullptr)
    {
        throw std::exception();
    }

    Node<T>* temp;
    temp = Top;
    Top = Top->Prev;
    delete temp;
}

template<class T>
int BracketStak<T>::Count()
{
    if(Top == nullptr)
    {
        return 0;
    }
    else
    {
        Node<T>* temp = Top;
        int count = 0;

        while(temp != nullptr)
        {
            count++;
            temp = temp->Prev;
        }
    }
}

template<class T>
void BracketStak<T>::Clear()
{
    Node<T>* temp;
    Node<T>* temp2;

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
bool BracketStak<T>::Empty()
{
    return !Top;
}

#endif // BRACKETSTAK_H
