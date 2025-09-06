#ifndef NEWSHARED_H
#define NEWSHARED_H


#include<algorithm>

template<class T>
class MyShared
{
public:
    MyShared() {};
    MyShared(T* ptr) : ptr(ptr) , RefCount(new int(1)){};

    MyShared(const MyShared<T>& OtherShared_ptr) : ptr(OtherShared_ptr.ptr) , RefCount(OtherShared_ptr.RefCount){//cs copy
        if(ptr == nullptr) return;
        ++(*RefCount);    //???
    }
    MyShared(MyShared<T> const&& OtherShared_ptr) : ptr(std::move(OtherShared_ptr.ptr)) , RefCount(std::move(OtherShared_ptr.RefCount)){//cs move
        if(ptr == nullptr) return;
         ++(*RefCount);   //???
    }
    MyShared<T>& operator=(MyShared<T>& OtherShared_ptr){
        if(this == &OtherShared_ptr) return *this;

        if(!RefCount){
        (*RefCount)--;
        if(*RefCount == 0){
            delete ptr;
            delete RefCount;
        }
        }

        ptr = OtherShared_ptr.ptr;
        RefCount = OtherShared_ptr.RefCount;
        (*RefCount)++;

        return *this;
    };
    MyShared<T>& operator=(MyShared<T>&& OtherShared_ptr){
        if(this == &OtherShared_ptr) return *this;

        if(!RefCount){
        (*RefCount)++;
        if(*RefCount == 0){
            delete ptr;
            delete RefCount;
        }
        }

        ptr = std::move(OtherShared_ptr.ptr);
        RefCount = std::move(OtherShared_ptr.RefCount);
        (*RefCount)++;

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

    template <class U>
    void reset(U* newptr)
    {
        delete ptr;
        delete RefCount;

        ptr = newptr;
        RefCount = new int(1);
    };

    void swap (MyShared<T>& OtherShared_ptr) noexcept{
        std::swap(ptr, OtherShared_ptr.ptr);
    };

    //returns a pointer or nullptr
    T* get() const noexcept
    {
        return ptr;
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

#endif // NEWSHARED_H
