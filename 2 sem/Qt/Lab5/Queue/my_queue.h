#ifndef MY_QUEUE_H
#define MY_QUEUE_H

#include<QDebug>
#include<QString>

template<class T>
class my_queue
{
public:
    my_queue();   

    ~my_queue();

    void clear();

    bool empty();

    int& Size();

    QString to_String();

    void pop();
    void push(T data);
    T& front();
    T& back();

    void search_min();

    void view();

private:

    struct Node
    {
      T data;
      Node* prev;
    };

    Node* Head;
    Node* Tail;
    int size;
};

template<class T> my_queue<T>::my_queue() : Head(nullptr), Tail(nullptr), size(0) {};

 template<class T> my_queue<T>::~my_queue()
{
    clear();
    delete Head;
};

 template<class T> void my_queue<T>::clear()
{
    while(size)
    {
        pop();
    }
};

 template<class T> bool my_queue<T>::empty()
{
    return !size;
}

template<class T> int& my_queue<T>::Size()
{
    return size;
}

template<class T> QString my_queue<T>::to_String()
{
    Node* temp = new Node();
    temp = Head;

    QString str;

    for(int i = 0; i < size - 1; ++i)
    {
        str +=  QString::number(temp->data) + " ";
        temp = temp->prev;
    }

    return str;
};

 template<class T> void my_queue<T>::pop()
{
    if(empty()) return;

    Node* del = Head;
    Head = Head->prev;
    delete del;
    size--;
};

 template<class T> void my_queue<T>::push(T data)
{
    Node* temp = new Node();
    temp->data = data;
    temp->prev = nullptr;

    if(size)
    {
        Tail->prev = temp;
        Tail = temp;

    } else {
        Head = Tail =  temp;
    }

    size++;
};

 template<class T> T& my_queue<T>::front()
{
    return Head->data;
};

 template<class T> T& my_queue<T>::back()
{
    return Tail->data;
};

template<class T> void my_queue<T>::search_min()
{
    if(empty()) return;

    Node* temp = new Node();
    Node* min_value = new Node();


     min_value =  Head;
     temp = Head->prev;
    for(int i = 0; i < size - 1; ++i)
    {
        if(min_value->data < temp->data)
        {

            temp = temp->prev;
        }
        else
        {
             min_value = temp;
            temp = temp->prev;
        }

    }

    temp = Head;



    Node* pered_min_value = new Node();
    pered_min_value = temp;


    for(int i = 0; i < size; ++i)
    {
        if(pered_min_value->prev == min_value) break;
        pered_min_value = pered_min_value->prev;
    }

    Node* posle_min_value = new Node();
    posle_min_value = temp;


    for(int i = 0; i < size; ++i)
    {
        if(min_value->prev == posle_min_value) break;
        posle_min_value = posle_min_value->prev;
    }

    Head = min_value;
    Head->prev = temp->prev;

    pered_min_value->prev = temp;
    temp->prev = posle_min_value;
}

template<class T> void my_queue<T>::view()
{
    Node* temp = new Node();
    temp = Head;
    for(int i = 0; i < size; ++i)
    {
        qDebug() << temp->data;
        temp = temp->prev;
    }

};




#endif // MY_QUEUE_H
