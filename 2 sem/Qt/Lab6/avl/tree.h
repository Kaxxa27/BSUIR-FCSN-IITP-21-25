#ifndef TREE_H
#define TREE_H
#include <QDebug>
#include <QPair>
#include<iostream>

static std::string ch_hor = "-", ch_ver = "|", ch_ddia = "/", ch_rddia = "\\", ch_udia = "\\", ch_ver_hor = "|-", ch_udia_hor = "\\-", ch_ddia_hor = "/-", ch_ver_spa = "| ";

using namespace std;

template<class T1, class T2>
class Tree {
protected:

    QString  str;
    string cs_str;

    size_t count_children = 0;
    struct Node {

       QPair<T1,T2> key;
       int height;
       Node* left;
       Node* right;
       Node(QPair<T1, T2> pair) { key = pair; left = right = 0; height = 1; }
    };

    Node* root;

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

    Node* insert(Node* p, QPair<T1, T2> pair) // вставка ключа key-pair в дерево с корнем p
    {
        if( !p ) return new Node(pair);
        if(pair.first < p->key.first)
            p->left = insert(p->left, pair);
        else
            p->right = insert(p->right, pair);
        return balance(p);
    }

    Node* SearchForPush(Node* p, QPair<T1, T2> pair) {
        Node* current = p;

        while (current && current->key.first  != pair.first) {
            if (pair.first < current->key.first)
                current = current->left;
            else
                current = current->right;
        }

        return current;
    }

    QString search(Node* p, QPair<T1, T2> pair) {
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

    Node* remove(Node* p, QPair<T1, T2> pair) // удаление ключа k из дерева p
    {
        if( !p ) return 0;

        if(pair.first < p->key.first)
            p->left = remove(p->left, pair);
        else if( pair.first > p->key.first )
            p->right = remove(p->right, pair);
        else //  k == p->key
        {
            Node* q = p->left;
            Node* r = p->right;
            delete p;
            if( !r ) return q;
            Node* min = findmin(r);
            min->right = removemin(r);
            min->left = q;
            return balance(min);
        }
        return balance(p);
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

    void printNode(Node* node, size_t level = 0) // for console
{
    if (node != nullptr) {

        printNode(node->right, level + 1);

        std::cout << '\n';

        if (node == Tree<T1, T2>::root)
        {
            std::cout << "root -> ";
        }
        else
        {
            std::cout << "          ";
        }
        for (size_t i = 0; i < level && node != Tree<T1, T2>::root; i++)
        {
            std::cout << "      ";
        }

        std::cout << node->key.first << "(" << node->key.second.toStdString() << ")";

        printNode(node->left, level + 1);
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

    void dump4(Node*  node, bool high = true, std::vector<std::string> const & lpref = std::vector<std::string>(), std::vector<std::string> const & cpref = std::vector<std::string>(), std::vector<std::string> const & rpref = std::vector<std::string>(), bool root = true, bool left = true, std::shared_ptr<std::vector<std::vector<std::string>>> lines = nullptr) {
    if (!node) return;
    typedef std::vector<std::string> VS;
    auto VSCat = [](VS const & a, VS const & b){ auto r = a; r.insert(r.end(), b.begin(), b.end()); return r; };
    if (root) lines = std::make_shared<std::vector<VS>>();
    if (node->left)
        dump4(node->left, high, VSCat(lpref, high ? VS({" ", " "}) : VS({" "})), VSCat(lpref, high ? VS({ch_ddia, ch_ver}) : VS({ch_ddia})), VSCat(lpref, high ? VS({ch_hor, " "}) : VS({ch_hor})), false, true, lines);
    auto sval = std::to_string(node->key.first);
    size_t sm = left || sval.empty() ? sval.size() / 2 : ((sval.size() + 1) / 2 - 1);
    for (size_t i = 0; i < sval.size(); ++i)
        lines->push_back(VSCat(i < sm ? lpref : i == sm ? cpref : rpref, {std::string(1, sval[i])}));
    if (node->right)
        dump4(node->right, high, VSCat(rpref, high ? VS({ch_hor, " "}) : VS({ch_hor})), VSCat(rpref, high ? VS({ch_rddia, ch_ver}) : VS({ch_rddia})), VSCat(rpref, high ? VS({" ", " "}) : VS({" "})), false, false, lines);
    if (root) {
        VS out;
        for (size_t l = 0;;++l) {
            bool last = true;
            std::string line;
            for (size_t i = 0; i < lines->size(); ++i)
                if (l < (*lines).at(i).size()) {
                    line += lines->at(i)[l];
                    last = false;
                } else line += " ";
            if (last) break;
            out.push_back(line);
        }
        for (size_t i = 0; i < out.size(); ++i)
           std::cout << out[i] << std::endl;
    }
}

    Tree() {
        root = nullptr;
    }
};

template<class T1, class T2>
class AVL : public Tree<T1, T2> {

public:

    bool contains(T1 first) {
        return this->SearchForPush(this->root, QPair<T1, T2>(first, T2()));
    }

    void push(QPair<T1, T2> pair) {
        if (!contains(pair.first))
            this->root = this->insert(this->root, pair);
    }

    void pop(T1 first) {
        this->root = this->remove(this->root, QPair<T1, T2>(first, T2()));
    }

    QString pr_children()
    {
        this->ret_bypass(this->root);
        return QString::number(this->count_children);
    }

    void bp() {
        this->bypass(this->root);
    }

    void prNode()
    {
        this->dump4(this->root);
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
       return this->search(this->root,QPair<int,QString>(key,""));;
   }

    QString print_str()
    {
        this->printNodeForUi(this->root);
        return this->str;
    }
};



#endif // TREE_H
