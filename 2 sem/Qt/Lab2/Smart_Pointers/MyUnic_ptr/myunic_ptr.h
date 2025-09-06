#ifndef MYUNIC_PTR_H
#define MYUNIC_PTR_H

#include <utility>
#include<algorithm>
#include <QDialog>
#include <QLabel>
#include <QSpinBox>
#include <QPushButton>

template<class T>
class MyUnic_ptr
{
public:
    MyUnic_ptr() : ptr(nullptr){};//nullptr
    MyUnic_ptr(T* ptr) : ptr(ptr) {};
    MyUnic_ptr(MyUnic_ptr<T>&& other) noexcept
        : ptr(other.release()) {}


    ~MyUnic_ptr() { delete ptr; };//destructor

    MyUnic_ptr<T>& operator=(MyUnic_ptr<T>&& RightRef){
        reset(RightRef.release());
        return *this;
    };
    T* operator->()
    {
        return ptr;
    };
    T& operator*() const noexcept{
        return *ptr;
    };

    //lvalue
    T* release() noexcept
    {
        T* tmp = ptr;
        ptr = nullptr;
        return tmp;
    };

    void reset(T* newptr)
    {
        delete ptr;
        ptr = newptr;
    };

    void swap (MyUnic_ptr<T>& other_ptr) noexcept{
        std::swap(ptr, other_ptr.ptr);
    };

    void Move (MyUnic_ptr<T>& other_ptr) noexcept{
        T* tmp = ptr;
        ptr = nullptr;
        other_ptr = tmp;
    };

    //returns a pointer or nullptr
    T* get() const noexcept
    {
        return ptr;
    };

    MyUnic_ptr(const MyUnic_ptr&) = delete;//only std::move
    MyUnic_ptr& operator=(const MyUnic_ptr&) = delete;

    private:
    T* ptr;
};




#endif // MYUNIC_PTR_H
