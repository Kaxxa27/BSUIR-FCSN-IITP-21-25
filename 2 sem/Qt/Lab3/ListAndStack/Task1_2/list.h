#ifndef LIST_H
#define LIST_H

#include <QString>
#include <QVector>

class List
{
    class Node
    {
    public:
        //Данные
        QString GroupOfItem;
        QString ModelOfItem;
        QString FIO;
        QString Ready;

        int Next;
        int Prev;
    };

    int Head;
    int Tail;
    size_t SizeList;
    std::vector<Node> Vector_List;

public:
    //Constructor
    List();

    //Cs of Copy
    List(const List& CopyList);//???

    //Destructor
    ~List();

    //Size
    int GetSizeList(){ return SizeList; };

    //Get element of List
//    Node* GetElementOfList();//???

    //Clear
    void ClearList();

    //Insert???
//    void Insert(int index,QString GroupOfItem, QString ModelOfItem, QString FIO, QString Ready);

    //Remove
    void Remove(int index);

    //pop_back
    void pop_back();

    //pop_front
    void pop_front();

    //push_back
    void push_back(QString GroupOfItem, QString ModelOfItem, QString FIO, QString Ready);

    //push_front???
//    void push_front(QString GroupOfItem, QString ModelOfItem, QString FIO, QString Ready);

    //PrintList
//    void Print();//???

    //PrintElement
    QString PrintElement(int Element,size_t index);

//    Return AHW???
//    int ReturnAgeWeightHeight(int Element,size_t index);

    //empty
    bool is_empty(){return Head == -1;}
};

inline List::List() : Head(0), Tail(0), SizeList(0){};


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



inline void List::Remove(int index)
{
    if (is_empty()) return;

            if (Head == index) {
                pop_front();
                return;
            }
            else if (Tail == index) {
                pop_back();
                return;
            }

            int slow = Head;
            int fast = Vector_List[Head].Next;
            while (fast && fast != index) {
                fast = Vector_List[fast].Next;
                slow = Vector_List[slow].Next;
            }

            Vector_List[slow].Next = Vector_List[fast].Next;
            SizeList--;

}

inline void List::pop_back()
{
    if (is_empty()) return;

            if (Head == Tail) {
                pop_front();
                SizeList--;
                return;
            }
            int temp = Head;
            while (Vector_List[temp].Next != Tail)
            {
                temp = Vector_List[temp].Next;
            }
            Vector_List[temp].Next = -1;
            Tail = temp;
            SizeList--;
}

inline void List::pop_front()
{
    if (is_empty()) return;

    Head = Vector_List[Head].Next;
    Vector_List[Head].Prev = -1;//empty
    SizeList--;
}

inline void List::push_back(QString GroupOfItem, QString ModelOfItem, QString FIO, QString Ready)
{
    Node temp;

    temp.Next = -1;
    temp.Prev = Tail;

    temp.GroupOfItem = GroupOfItem;
    temp.ModelOfItem = ModelOfItem;
    temp.FIO = FIO;
    temp.Ready = Ready;

    Vector_List.push_back(temp);

    if (is_empty()) {

       SizeList = 1;
       Head = 0;
       Tail = 0;
       return;
    }

    SizeList++;
    Vector_List[Tail].Next = SizeList + 1 ;
    Vector_List[Vector_List.size() - 1].Prev = Tail;
    Tail = SizeList + 1;

}


inline QString List::PrintElement(int Element,size_t index)
{


    if(SizeList != 0)
    {

        int temp = Element;

//        int count = 0;
//        while(count != Element)
//        {
//            temp = Vector_List[temp].Next;
//            count++;
//        }
        switch(index)
        {
            case 1:
            {
                return Vector_List[temp].GroupOfItem;
            }
            case 2:
            {
                return Vector_List[temp].ModelOfItem;
            }
            case 3:
            {
                return Vector_List[temp].FIO;
            }
            case 4:
            {
                return Vector_List[temp].Ready;
            }

            default: return "-1";
        }
    }
}

#endif // LIST_H
