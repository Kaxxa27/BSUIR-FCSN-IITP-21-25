#ifndef WEAK_H
#define WEAK_H

#include <myshared.h>
template<class T>
class Weak
{
public:
    Weak() : ptr(nullptr), SharedCount(nullptr), WeakCount(new int(0)) {};
    Weak(MyShared<T>& p) : ptr(p.get()), SharedCount(p.get_ReFCount()), WeakCount(new int(1))
    {
        if(nullptr != p.get()){
            (*this->WeakCount)++;
        }
    };

    Weak<T>& operator=(MyShared<T>& Shared_ptr){
        if(ptr == Shared_ptr.get()) return *this;
        (*WeakCount)--;
        if(*WeakCount == 0){
            delete ptr;
            delete WeakCount;
            delete SharedCount;
        }

        ptr = Shared_ptr.get();
        SharedCount = Shared_ptr.get_ReFCount();

       if(*WeakCount != 1){
           (*WeakCount)--;
           WeakCount = new int(1);
       }

        return *this;
    };

    Weak<T>& operator=(Weak<T>& Weak_ptr){
        if(this  == &Weak_ptr || WeakCount ==Weak_ptr.get_WeakCounter()) return *this;

        (*WeakCount)--;

//        if(*WeakCount == 0){
//            delete ptr;
//            delete WeakCount;
//        }

        ptr = Weak_ptr.ptr;
        SharedCount = Weak_ptr.SharedCount;
        WeakCount = Weak_ptr.WeakCount;
        (*WeakCount)++;

        return *this;
    };

    ~Weak()
    {
        if(!WeakCount) return;
        --*WeakCount;
        if(!*WeakCount){
            delete ptr;
            delete WeakCount;
        }
    }

    MyShared<T> Lock()
    {
        if(*SharedCount > 0){

            return MyShared<T>(ptr,SharedCount);
        }
        else{
            return MyShared<T>();
        }
    }
    T* operator->()
    {
        return ptr;
    };

    T* get_WeakCounter()
    {
        return WeakCount;
    };
    T& operator*() const noexcept{
        return *ptr;
    };

    int Use_Weak_Count()
    {
        return *WeakCount;
    }
    int Use_Shared_Count()
    {
        return *SharedCount;
    }



private:
    T* ptr;
    int* SharedCount;
    int* WeakCount;
};

#endif // WEAK_H
