#ifndef UNOREDRED_MAP_H
#define UNOREDRED_MAP_H

#include<iostream>
#include<utility>
#include<forward_list>
#define SIZE 10

using namespace std;

template<class Key, class Value, class Hash = std::hash<Key>>
class unoredred_map
{

   forward_list<pair<const Key, Value>>* _arr;
   size_t _size;
public:
    unoredred_map();

private:
};

#endif // UNOREDRED_MAP_H
