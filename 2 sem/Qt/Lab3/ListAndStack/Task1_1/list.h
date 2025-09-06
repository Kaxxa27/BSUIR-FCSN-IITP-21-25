#ifndef LIST_H
#define LIST_H

#include <QString>
#include <QMessageBox>

class List
{
    class Node
    {
    public:
        //Данные
        QString CountryName;
        QString TeamName;
        QString FIO;
        size_t PlayerNum;
        size_t Age;
        size_t Heght;
        size_t Weight;

        Node* Next;
        Node* Prev;
    };

    Node* Head;
    Node* Tail;
    size_t SizeList;

public:
    //Constructor
    List();

    //Cs of Copy
    List(const List& CopyList);

    //Destructor
    ~List();

    //Size
    int GetSizeList(){return SizeList;};

    //Get element of List
//    Node* GetElementOfList();//???

    //Clear
    void ClearList();

    //Insert
    void Insert(size_t index,QString CountryName, QString TeamName, QString FIO, size_t PlayerNum, size_t Age, size_t Hight, size_t Weight);

    //Remove
    void Remove(size_t index);

    //pop_back
    void pop_back();

    //pop_front
    void pop_front();

    //push_back
    void push_back(QString CountryName, QString TeamName, QString FIO, size_t PlayerNum, size_t Age, size_t Hight, size_t Weight);

    //push_front
    void push_front(QString CountryName, QString TeamName, QString FIO, size_t PlayerNum, size_t Age, size_t Hight, size_t Weight);

    //PrintList
//    void Print();//???

    //PrintElement
    QString PrintElement(int Element,size_t index);

    //Return AHW
    int ReturnAgeWeightHeight(int Element,size_t index);

};

inline List::List() : Head(nullptr), Tail(nullptr), SizeList(0){};

inline List::List(const List& CopyList) : Head(nullptr), Tail(nullptr), SizeList(0)
{
    Node* temp = CopyList.Head;

    while(temp != nullptr)
    {

    }

}

inline List::~List()
{
    ClearList();
}

inline void List::ClearList()
{
    while(SizeList)
    {
        pop_front();
    }
}

inline void List::Insert(size_t index,QString CountryName, QString TeamName, QString FIO, size_t PlayerNum, size_t Age, size_t Hight, size_t Weight)
{
    if(index == SizeList + 1)
    {
        push_back(CountryName, TeamName, FIO, PlayerNum, Age, Hight, Weight);
        return;
    }
    else if(index == 1)
    {
        push_front(CountryName, TeamName, FIO, PlayerNum, Age, Hight, Weight);
        return;
    }

    size_t counter = 1;

    Node* temp = Head;

    while(counter < index)
    {
        temp = temp->Next;
        counter++;
    }

    Node* NewElement = new Node;

    NewElement->CountryName = CountryName;
    NewElement->TeamName = TeamName;
    NewElement->FIO = FIO;
    NewElement->PlayerNum = PlayerNum;
    NewElement->Age = Age;
    NewElement->Heght = Hight;
    NewElement->Weight = Weight;

    Node* PrevElement = temp->Prev;

    if(PrevElement != nullptr && SizeList != 1)
    {
        PrevElement->Next = NewElement;
    }

    NewElement->Next = temp;
    NewElement->Prev = PrevElement;
    temp->Prev = NewElement;

    SizeList++;

}

inline void List::Remove(size_t index)
{
    if(SizeList == 0)
    {
        return;
    }
    size_t counter = 1;

    Node* Del = Head;

    while(counter < index)
    {
        Del = Del->Next;
        counter++;
    }

    Node* PrevElement = Del->Prev;
    Node* NextElement = Del->Next;


    if(PrevElement != nullptr && SizeList != 1)
    {
        PrevElement->Next = NextElement;
    }

    if(NextElement != nullptr && SizeList != 1)
    {
        NextElement->Prev = PrevElement;
    }

    if(index == 1)
    {
        Head = NextElement;
    }
    if(index == SizeList)
    {
        Tail = PrevElement;
    }

    delete Del;

    SizeList--;

}

inline void List::pop_back()
{
    Node* temp = Tail;

    Tail = Tail->Prev;

    delete temp;

    SizeList--;
}

inline void List::pop_front()
{
    Node* temp = Head;

    Head = Head->Next;

    delete temp;

    SizeList--;
}

inline void List::push_back(QString CountryName, QString TeamName, QString FIO, size_t PlayerNum, size_t Age, size_t Hight, size_t Weight)
{
    Node* temp = new Node;

    temp->Next = nullptr;
    temp->Prev = Tail;

    temp->CountryName = CountryName;
    temp->TeamName = TeamName;
    temp->FIO = FIO;
    temp->PlayerNum = PlayerNum;
    temp->Age = Age;
    temp->Heght = Hight;
    temp->Weight = Weight;

    if(Tail != nullptr)
    {
        Tail->Next = temp;
    }

    if(SizeList == 0)
    {
        Head = Tail = temp;
    }
    else
    {
        Tail = temp;
    }

    SizeList++;
}

inline void List::push_front(QString CountryName, QString TeamName, QString FIO, size_t PlayerNum, size_t Age, size_t Hight, size_t Weight)
{
    Node* temp = new Node;

    temp->Prev = nullptr;
    temp->Next = Head;

    temp->CountryName = CountryName;
    temp->TeamName = TeamName;
    temp->FIO = FIO;
    temp->PlayerNum = PlayerNum;
    temp->Age = Age;
    temp->Heght = Hight;
    temp->Weight = Weight;

    if(Head != nullptr)
    {
        Head->Prev = temp;
    }

    if(SizeList == 0)
    {
        Head = Tail = temp;
    }
    else
    {
        Head = temp;
    }

    SizeList++;
}

inline QString List::PrintElement(int Element,size_t index)
{


    if(SizeList != 0)
    {

        Node* temp = Head;

        int count = 0;
        while(count != Element)
        {
            temp = temp->Next;
            count++;
        }
        switch(index)
        {
            case 1:
            {
                return temp->CountryName;
            }
            case 2:
            {
                return temp->TeamName;
            }
            case 3:
            {
                return temp->FIO;
            }
            case 4:
            {
                return QString::number(temp->PlayerNum);
            }
            case 5:
            {
                return QString::number(temp->Age);
            }
            case 6:
            {
                return QString::number(temp->Heght);
            }
            case 7:
            {
                return QString::number(temp->Weight);
            }
            default: return "-1";
        }
    }
}

inline int List::ReturnAgeWeightHeight(int Element,size_t index)
{
    if(SizeList != 0)
    {

        Node* temp = Head;

        int count = 0;
        while(count != Element)
        {
            temp = temp->Next;
            count++;
        }
        switch(index)
        {
            case 1:
            {
                return temp->Age;
            }
            case 2:
            {
                return temp->Heght;
            }
            case 3:
            {
                return temp->Weight;
            }

            default: return -1;
        }
    }
};








#endif // LIST_H
