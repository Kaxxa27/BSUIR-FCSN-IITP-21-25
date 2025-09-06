#ifndef MYSHARED_H
#define MYSHARED_H

#include<algorithm>
#include<utility>


template<class T>
class MyShared
{  
public:
    MyShared() : ptr(nullptr) , RefCount(new int(0)){};

    MyShared(T* ptr) : ptr(ptr) , RefCount(new int(1)){};

    MyShared(T* ptr, int* x) : ptr(ptr) , RefCount(x){
        (*RefCount)++;
    };


    MyShared(MyShared<T> const& OtherShared_ptr) : ptr(OtherShared_ptr.ptr) , RefCount(OtherShared_ptr.RefCount){//cs copy
//        if(ptr == nullptr) return;
        if(nullptr != OtherShared_ptr.ptr){
            (*this->RefCount)++;
        }
    }
    MyShared(MyShared<T> const&& OtherShared_ptr) : ptr(std::move(OtherShared_ptr.ptr)) , RefCount(std::move(OtherShared_ptr.RefCount)){//cs move
        if(ptr == nullptr) return;

        OtherShared_ptr.ptr = nullptr;
        OtherShared_ptr.RefCount = nullptr;

         ++(*RefCount);   //???
    }
    MyShared<T>& operator=(MyShared<T>& OtherShared_ptr){
        if(this == &OtherShared_ptr) return *this;

        (*RefCount)--;
        if(*RefCount == 0){
            delete ptr;
            delete RefCount;
        }

        ptr = OtherShared_ptr.ptr;
        RefCount = OtherShared_ptr.RefCount;

        if(nullptr != OtherShared_ptr.ptr){
            (*this->RefCount)++;
        }

        return *this;
    };
    MyShared<T>& operator=(MyShared<T>&& OtherShared_ptr){

        if(this == &OtherShared_ptr) return *this;


        (*RefCount)--;
        if(*RefCount == 0){
            delete ptr;
            delete RefCount;
        }


        ptr = (OtherShared_ptr.ptr);
        RefCount = (OtherShared_ptr.RefCount);

        OtherShared_ptr.ptr = nullptr;
        OtherShared_ptr.RefCount = nullptr;

        return *this;
    };
    ~MyShared()
    {
        if(!RefCount) return;
        --*RefCount;
        if(!*RefCount){
            delete ptr;
            delete RefCount;
        }
    }

    T* operator->()
    {
        return ptr;
    };
    T& operator*() const noexcept{
        return *ptr;
    };

    void IncrementCountForWeak(){
        RefCount++;
    }
    void reset(T* newptr)//???
    {
//        delete ptr;
//        ptr = newptr;


    };

    void swap (MyShared<T>& OtherShared_ptr) noexcept{
        std::swap(ptr, OtherShared_ptr.ptr);
        std::swap(RefCount, OtherShared_ptr.RefCount);

    };

    //returns a pointer or nullptr
    T* get() const noexcept
    {
        return ptr;
    };

    T* get_ReFCount() const noexcept
    {
        return RefCount;
    };

    bool Unique()
    {
       return *RefCount == 1 ? true : false;
    }

    int Use_Count()
    {
        return *RefCount;
    }

private:   
     T* ptr;
     int* RefCount;//ReferenceCount
};

//template<class T>
//class MyShared
//{
//public:
//    MyShared() {};
//    MyShared(T* ptr){
//        Helper = new ControlBlock;
//        Helper->ptr = ptr;
//        Helper->Count = new int(1);
//    };
//    MyShared(const MyShared<T>& OtherShared_ptr){//cs copy

//        Helper->ptr = OtherShared_ptr.Helper->ptr;
//        Helper->Count = OtherShared_ptr.Helper->Count;
//        Helper->Count++;
//    }
//    MyShared(MyShared<T> const&& OtherShared_ptr){//cs move
//        OtherShared_ptr.Helper = Helper;
//        Helper->ptr(nullptr);
//    }
//    MyShared<T>& operator=(MyShared<T>& OtherShared_ptr){
//       Helper->Count -= 1;
//        if(Helper->Count == 0)
//        {
//            delete Helper->ptr;
//            delete Helper->Count;
//        }
//        this->Helper= OtherShared_ptr.Helper;
//        Helper->Count++;
//        return *this;
//    };
//    MyShared<T>& operator=(MyShared<T>&& OtherShared_ptr){

//       this->Helper = OtherShared_ptr.Helper;
//       Helper->Count++;
//       return *this;
//    };
//    ~MyShared()
//    {
//        if(!&Helper->Count) return;
//        --Helper->Count;
//        if(!Helper->Count){
//            delete Helper->ptr;
//        }
//    }

//    T* operator->()
//    {
//        return Helper->ptr;
//    };
//    T& operator*() const noexcept{
//        return *Helper->ptr;
//    };

//    template <class U>
//    void reset(U* newptr)
//    {
//        delete Helper;
//        Helper->ptr = newptr;
//    };

//    void swap (MyShared<T>& OtherShared_ptr) noexcept{
//        std::swap(Helper->ptr, OtherShared_ptr.Helper->ptr);
//    };

//    //returns a pointer or nullptr
//    T* get() const noexcept
//    {
//        return Helper->ptr;
//    };

//    bool Unique()
//    {
//       return Helper->Count == 1 ? true : false;
//    }

//    int Use_Count()
//    {
//        return Helper->Count;
//    }

//private:
//    struct ControlBlock{
//        T* ptr;
//        int* Count;//ReferenceCount
//    };
//    ControlBlock* Helper;
//    //Secret private cs for ControlBlock
//};
#endif // MYSHARED_H
