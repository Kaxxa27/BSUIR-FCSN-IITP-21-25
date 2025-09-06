#ifndef MAP_H
#define MAP_H

#include<QString>
#include<QDebug>
#include<iostream>
#include<pair.h>

template<class T1, class T2>
class Tree {

protected:
    struct Node
    {
       pair<T1,T2> key;
       int height;
       Node* parent;
       Node* left;
       Node* right;

        Node(pair<T1,T2> p, Node* par_t = nullptr) { key = p; left = right = 0; parent = par_t; height = 1; }
    };

public:

    //COMPARE
    class compare
        {
        public:
            bool operator() (const pair<T1, T2>& x, const pair<T1, T2>& y) const
            {
                return x.first <  y.first;
            }
        };


    class iterator {
        private:
            Node* node;
            Node* root;

        public:

            iterator(Node* node_ = nullptr, Node* root_ = nullptr)
                : node(node_), root(root_) {

            }

            iterator& operator=(const iterator& other) {
                node = other.node;
                root = other.root;

                return *this;
            }

            pair<T1, T2>& operator*() {
                return node->key;
            }

            iterator& operator++() {

             //min elem
                if (!node) {

                    if (!root)
                        return *this;

                    node = root;

                    while (node->left != nullptr) {
                        node = node->left;
                    }

                    return *this;
                }

               //min left node
                if (node->right != nullptr) {

                    node = node->right;

                    while (node->left != nullptr) {
                        node = node->left;
                    }
                }
                else {
                    Node* p = node->parent;
                    while (p != nullptr && node == p->right) {
                        node = p;
                        p = p->parent;
                    }

                    node = p;
                }

                return *this;
            }

            iterator& operator--() {

                //max
                if (!node) {

                    if (!root)
                        return *this;

                    node = root;

                    while (node->right != nullptr)
                        node = node->right;

                    return *this;
                }

                //right max node
                if (node->left != nullptr) {

                    node = node->left;

                    while (node->right != nullptr) {
                        node = node->right;
                    }
                }
                else {
                    Node* p = node->parent;
                    while (p != nullptr && node == p->left) {
                        node = p;
                        p = p->parent;
                    }

                    node = p;
                }

                return *this;
            }

            iterator& operator+(int n) {

                for (int i = 0; i < n; i++)
                    operator++();

                return *this;
            }

            iterator& operator-(int n) {

                for (int i = 0; i < n; i++)
                    operator--();

                return *this;
            }

            bool operator==(const iterator& other) {
                return node == other.node;
            }

            bool operator!=(const iterator& other) {
                return node != other.node;
            }
        };

    iterator begin()
    {
        Node* node = root;
        while (node && node->left) node = node->left;
        return iterator(node);
    }

    iterator end()
    {
        return iterator(nullptr);
    }

    int height(Node* p)
    {
        return p?p->height:0;
    }

    int bfactor(Node* p)
    {
        return height(p->right)-height(p->left);
    }

    void fixheight(Node* p)
    {
        int hl = height(p->left);
        int hr = height(p->right);
        p->height = (hl>hr?hl:hr)+1;
    }

    Node* rotateright(Node* p) // правый поворот вокруг p
    {
        Node* q = p->left;
        p->left = q->right;
        q->right = p;
        fixheight(p);
        fixheight(q);
        return q;
    }

    Node* rotateleft(Node* q) // левый поворот вокруг q
    {
        Node* p = q->right;
        q->right = p->left;
        p->left = q;
        fixheight(q);
        fixheight(p);
        return p;
    }

    Node* balance(Node* p) // балансировка узла p
    {
        if (p == nullptr)
            return nullptr;
        fixheight(p);
        if( bfactor(p) == 2 )
        {
            if( bfactor(p->right) < 0 )
                p->right = rotateright(p->right);
            return rotateleft(p);
        }
        if( bfactor(p) == -2 )
        {
            if( bfactor(p->left) > 0  )
                p->left = rotateleft(p->left);
            return rotateright(p);
        }
        return p;
    }

    Node* insert(Node* p, pair<T1, T2> pair, Node* par) // вставка ключа key-pair в дерево с корнем p
    {
        if( !p )
        {
            _size++;
            return new Node(pair,par);
        }
        if(pair.first < p->key.first)
            p->left = insert(p->left, pair,par);
        else
            p->right = insert(p->right, pair,par);
        return balance(p);
    }

    void insert(pair<T1, T2> pair)
    {
        root = insert(root, pair, nullptr);
    }

    Node* SearchForPush(Node* p, pair<T1, T2> pair) {
        Node* current = p;

        while (current && current->key.first  != pair.first) {
            if (pair.first < current->key.first)
                current = current->left;
            else
                current = current->right;
        }

        return current;
    }

    T2& m_search(T1 key){
        Node** cur = &root;
        Node* prev = nullptr;
        while (*cur) {
            if (key < (*cur)->key.first) {
                prev = *cur;
                cur = &(*cur)->left;
            }
            else if (key > (*cur)->key.first) {
                prev = *cur;
                cur = &(*cur)->right;
            }
            else {
                return (*cur)->key.second;
            }
        }
        ++_size;
        *cur = new Node(pair<T1,T2>(key, T2()));
        (*cur)->parent = prev;
        T2& value = (*cur)->key.second;
        (*cur)->parent = balance((*cur)->parent);
        return value;
    }

    QString search(Node* p, pair<T1, T2> pair) {
        Node* current = p;

        while (current && current->key.first  != pair.first) {
            if (pair.first < current->key.first)
                current = current->left;
            else
                current = current->right;
        }
        if(!current) return 0;
        if(current->key.first != pair.first)
        {
            return 0;
        }
        QString str;
        str += QString::number(current->key.first) + " " + current->key.second;
        return str;
    }

    Node* findmin(Node* p) // поиск узла с минимальным ключом в дереве p
    {
        return p->left?findmin(p->left):p;
    }

    Node* removemin(Node* p) // удаление узла с минимальным ключом из дерева p
    {
        if( p->left==0 )
            return p->right;
        p->left = removemin(p->left);
        return balance(p);
    }

    Node* remove(Node* p, T1 key) // удаление ключа k из дерева p
    {     
        if( !p ) return 0;

        if(key < p->key.first)
            p->left = remove(p->left, key);
        else if( key > p->key.first )
            p->right = remove(p->right, key);
        else //  k == p->key
        {
            Node* q = p->left;
            Node* r = p->right;
            Node* parent = p->parent;
            delete p;
            _size--;

            if( !r ) return q;
            Node* min = findmin(r);
            min->right = removemin(r);
            min->left = q;
            min->parent = parent;

            if (min->right)
                min->right->parent = min;
            if (min->left)
                min->left->parent = min;
            return balance(min);
        }
        return balance(p);
    }

    void Delete(T1 key){
        root = remove(root, key);
    }

    void bypass(Node* p) {//simetric bypass
        if (p) {
            bypass(p->left);
            qDebug() << p->key.first << " " << p->key.second;
            bypass(p->right);
        }
    }

    void ret_bypass(Node* p) {//обратный bypass
        if (p) {
            ret_bypass(p->left);
            ret_bypass(p->right);
            if(p->left && p->right)
            {
                ++count_children;
            }
        }
    }

    void printNodeForUi(Node* node, size_t level = 0) // for ui
{
    if (node != nullptr) {

        printNodeForUi(node->right, level + 1);

        this->str += '\n';

        if (node == Tree<T1, T2>::root)
        {
            this->str += "root -> ";
        }
        else
        {
            this->str += "          ";
        }
        for (size_t i = 0; i < level && node != Tree<T1, T2>::root; i++)
        {
            this->str += "      ";
        }

        this->str = this->str + QString::number(node->key.first) + "(" + node->key.second + ")";

        printNodeForUi(node->left, level + 1);
    }
}

    void m_clear(Node* p)
    {
        if (p) {
            m_clear(p->left);
            m_clear(p->right);
            delete p;
        }
    }   
    Tree()
    {
        root = nullptr;
    }

protected:

    QString  str;


    size_t count_children = 0;
    size_t _size  = 0;

    Node* root;
    compare comp;
};

template<class T1, class T2>
class My_Map : public Tree< T1, T2> {

public:

    bool contains( T1 first) {
        return this->SearchForPush(this->root, pair<T1, T2>(first, T2()));
    }

    void insert(pair< T1,T2> pair){
        Insert(pair);
    }

    void erase( T1& key){
        this->Delete(key);
    }

    bool empty(){
        return this->_size == 0;
    }

    int size(){
        return this->_size;
    }

    T2& at(T1 key){
        return this->m_search(key);
    }

    T2& operator[](T1 key){
        return at(key);
    }


    QString pr_children()
    {
        this->ret_bypass(this->root);
        return QString::number(this->count_children);
    }

    void clear()
    {
        this->m_clear(this->root);
        this->root = nullptr;
        this->_size = 0;
    }

    void bp() {
        this->bypass(this->root);
    }

    void StrZero()
   {
        this->str.clear();
   }

    void ChildrenZero()
   {
        this->count_children = 0;
   }

    QString searching(int key)
   {
       return this->search(this->root,pair<const int,QString>(key,""));;
   }

    QString print_str()
    {
        this->printNodeForUi(this->root);
        return this->str;
    }
};

#endif // MAP_H
