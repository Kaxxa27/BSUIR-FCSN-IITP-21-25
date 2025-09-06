#ifndef STRINGLIB_H
#define STRINGLIB_H

#include "StringLib_global.h"
#include<iostream>
#include <windows.h>
using std::ostream;
using std::istream;
using namespace std;

size_t My_strlen(const char* str);

void* My_memcpy(void* dest, const void* src, size_t len);
void* My_memmove(void* dest, const void* src, size_t count);
int   My_memcmp(const void* buf1, const void* buf2, size_t count);
void* My_memset(void* buf, char z, size_t bytes);

char* My_strcpy(char* str1, const char* str2);
char* My_strncpy(char* dst, const char* src, size_t n);

char* My_strcat(char* str1, char* str2);

char* My_strncat(char* str1, char* str2, size_t n);

int My_strcmp(const char* s1, const char* s2);
int My_strncmp(const char* s1, const char* s2, size_t n);

char* my_strtok(char* str, const char* delim);

class STRINGLIB_EXPORT MyString
{
public:
    MyString();
    MyString(const char* str);
    MyString(const MyString& str);
    ~MyString();

    MyString& operator=(const MyString& str);
    MyString& operator=(const char* str);

    friend MyString operator+(const MyString& str1, const MyString& str2)
    {

       MyString temp;
       delete[] temp._str;
       temp._len = str1._len + str2._len;
       temp._capacity = 2 * temp._len + 1;
       temp._str = new char[temp._capacity];

       My_strcpy(temp._str, str1._str);
       My_strcpy(temp._str + str1._len , str2._str);

       return temp;
   }

    friend ostream& operator<<(ostream& os, const MyString& str2) {
        os << str2._str;
        return os;
    }
    friend istream& operator>>(istream& is, MyString& st)
    {
        char temp[256];
        is.get(temp, 256);
        if (is)
            st = temp;
        while (is && is.get() != '\n')
            continue;
        return is;
    }

    char& operator[](int i) {
        return _str[i];
    }
    const char& operator[](int i) const {
        return _str[i];
    }

    size_t size()
    {
        return _len;
    }

    size_t capacity()
    {
        return _capacity;
    }

    bool empty()
    {
        return !_len;
    }

    void clear()
    {
        delete[] _str;
        _len = 0;
        _capacity = 2 * _len + 1;
        _str = new char[_capacity];
        _str[0] = '\0';
    }

    MyString& append(const char* cs)
    {
        *this = *this + cs;

        return *this;
    }
    MyString& append(const MyString& s)
    {
        MyString str1;
        str1 = str1 + s;

        *this = _str;

        return *this;
    }

    int compare(const MyString& s) const
    {
        unsigned i, j;
        int compare;

        bool less = 1, greater = 1, equal = 1;

        if (_len != s._len) {
            equal = 0;
        }

        else {
            for (i = 0; i < s._len; i++)
            {
                if (_str[i] != s._str[i])
                    equal = 0;
            }
        }

        for (i = 0, j = 0; j < s._len; i++, j++)
        {
            if (_str[i] >= s._str[j])
                less = 0;
        }
        if (less || equal)
            greater = 0;

        if (less)
            compare = -1;

        if (greater)
            compare = 1;
        if (equal)
            compare = 0;
        return compare;
    }

    MyString& insert(const size_t& pos, const char* cs)
    {
        MyString tmpStr(cs);
        MyString str1(_str);

        *this = str1.insert(pos, tmpStr);

        return *this;
    }
    MyString& insert(const size_t& pos, const MyString& s)
    {
        if (pos > _len) {
            throw "index out of range!";
        }

        MyString str1(_str);
        unsigned i, j;

        _len = s._len + str1._len;
        _capacity = 2 * _len + 1;
        delete[] _str;

        _str = new char[_capacity];

        for (i = 0; i < pos; i++) {
            _str[i] = str1._str[i];
        }
        for (i = pos, j = 0; j < s._len; i++, j++) {
            _str[i] = s._str[j];
        }

        for (i = s._len + pos, j = pos; i < _len; i++, j++) {
            _str[i] = str1._str[j];
        }

        _str[_len] = '\0';

        return *this;
    }


private:
    char* _str;
    size_t _len;
    size_t _capacity;
};

inline MyString::MyString() : _len(0), _capacity(2 * _len + 1)
{
    _str = new char[_capacity];
    *_str = '\0';
}

inline MyString::MyString(const char *str) : _len(My_strlen(str)), _capacity(2 * _len + 1)
{
   _str = new char[_capacity];
   My_strcpy(_str,str);
}

inline MyString::MyString(const MyString &str) : _len(str._len), _capacity(2 * _len + 1)
{
    _str = new char[_capacity];
    My_strcpy(_str,str._str);
}

inline MyString::~MyString()
{
    delete[] _str;
}

inline MyString &MyString::operator=(const MyString &str)
{
    if (this == &str)
        return *this;
    delete[] _str;
    _len = str._len;
    _capacity = 2 * _len + 1;
    _str = new char[_capacity];
    My_strcpy(_str, str._str);

    return *this;
}

inline MyString &MyString::operator=(const char* str)
{
    delete[] _str;
    _len = My_strlen(str);
    _capacity = 2 * _len + 1;
    _str = new char[_capacity];
    My_strcpy(_str, str);

    return *this;
}

#endif // STRINGLIB_H
