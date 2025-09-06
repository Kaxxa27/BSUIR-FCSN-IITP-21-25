#ifndef UNOREDRED_MAP_H
#define UNOREDRED_MAP_H

#include<iostream>
#include<pair.h>
#include<list>
#include<vector>
#include<QDebug>

#define SIZE 10
#define PAIR pair<Key, Value>
#define MAX_LOAD_FACTOR 5


template<class Key, class Value, class Hash = std::hash<Key>>
class unoredred_map
{

   std::vector<std::list<PAIR>> _arr;
   int get_Hash(Key key);
   size_t count_elem ;

    void rehash();

public:
    unoredred_map();
    void insert(PAIR pr);
    bool contains(const Key key);
    void erase(const Key key);
    void clear();
    Value& operator[](const Key key);

    size_t size();
    size_t arr_size();
    int get_count_elem();
    int max_load_factor();

    std::vector<std::list<PAIR>> get_Arr();

private:
};

template<class Key, class Value, class Hash> int unoredred_map<Key, Value, Hash>::get_Hash(Key key)
{
    Hash functor;
    int hs = functor(key);
    return hs;
}

template<class Key, class Value, class Hash> void unoredred_map<Key, Value, Hash>::rehash()
{

    unoredred_map<Key,Value,Hash> mp;

    mp._arr.resize(_arr.size() * 2);


    //vector<pair<Key,Value>> new_arr(_arr.size() * 2);

    for(int i = 0; i < _arr.size(); ++i)
    {
        for (auto j : _arr[i])
        {
            mp.insert(j);
        }
    }
    *this = mp;

}

template<class Key, class Value, class Hash> unoredred_map<Key, Value, Hash>::unoredred_map()
{
    _arr.resize(SIZE);
    count_elem = 0;
}

template<class Key, class Value, class Hash> void unoredred_map<Key, Value, Hash>::insert(pair<Key, Value> pr)
{
    if(contains(pr.first)) return;


    if(count_elem / _arr.size() >= MAX_LOAD_FACTOR) rehash();


    int index = get_Hash(pr.first) % _arr.size();
    _arr[index].push_back(pr);

    count_elem++;
}

template<class Key, class Value, class Hash> bool unoredred_map<Key, Value, Hash>::contains(const Key key)
{
    int index = get_Hash(key) % _arr.size();

    for ( auto& i : _arr[index])
    {

        if (i.first == key)
            return true;
    }
    return false;
}

template<class Key, class Value, class Hash> void unoredred_map<Key, Value, Hash>::erase(const Key key)
{
    int index = get_Hash(key) % _arr.size();

    for (typename std::list<PAIR>::iterator it = _arr[index].begin(); it != _arr[index].end(); ++it) {

        if ((*it).first == key) {
            count_elem--;
            _arr[index].erase(it);
            break;
        }
    }

}

template<class Key, class Value, class Hash> void unoredred_map<Key, Value, Hash>::clear()
{
    for(int i = 0; i < _arr.size(); ++i)
    {
        _arr[i].clear();
    }
    count_elem = 0;
}

template<class Key, class Value, class Hash> Value& unoredred_map<Key, Value, Hash>::operator[](const Key key)
{
    if(!contains(key))
    {
        insert(PAIR(key,Value()));
    }

    int index = get_Hash(key) % _arr.size();

    for ( auto& i : _arr[index])
    {
        if (i.first == key)
        {
            return i.second;
        }
    }

}

template<class Key, class Value, class Hash> size_t unoredred_map<Key, Value, Hash>::size()
{
    return count_elem;
}

template<class Key, class Value, class Hash> size_t unoredred_map<Key, Value, Hash>::arr_size()
{
    return _arr.size();
}

template<class Key, class Value, class Hash>int unoredred_map<Key, Value, Hash>::get_count_elem()
{
    return count_elem;
}

template<class Key, class Value, class Hash> int unoredred_map<Key, Value, Hash>::max_load_factor()
{
    return MAX_LOAD_FACTOR;
}

template<class Key, class Value, class Hash> std::vector<std::list<pair<Key, Value> > > unoredred_map<Key, Value, Hash>::get_Arr()
{
    return _arr;
}

#endif // UNOREDRED_MAP_H
