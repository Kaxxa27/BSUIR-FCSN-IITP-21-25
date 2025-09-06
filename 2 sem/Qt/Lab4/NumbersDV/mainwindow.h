#ifndef MAINWINDOW_H
#define MAINWINDOW_H

using namespace std;
#include <QMainWindow>
#include<VectorLib.h>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

template<class T1, class T2>
struct MyPair {
public:
    T1 first{};
    T2 second{};
    MyPair() {};
    MyPair(const T1& x, const T2& y) : first(x), second(y) {};
    MyPair(const MyPair<T1, T2>& p)
        : first(p.first), second(p.second) {};
    void operator=(const MyPair& other) {
        first = other.first;
        second = other.second;

    }
    void swap(MyPair& other) noexcept {
        MyPair<T1, T2> tmp(*this);
        *this = other;
        other = tmp;
    }

    bool operator<(const MyPair& other) {
        if (this->first == other.first) {
            return this->second < other.second;
        }
        return this->first < other.first;
    }
    bool operator>(const MyPair& other) {
        if (this->first == other.first) {
            return this->second > other.second;
        }
        return this->first > other.first;
    }
    bool operator<=(const MyPair& other) {
        return this->first <= other.first;
    }
    bool operator>=(const MyPair& other) {
        return this->first >= other.first;
    }
};
#include<vector>
#define pair std::pair
template<class T1, class T2>
bool operator==(const MyPair<T1, T2>& lhs, const MyPair<T1, T2>& rhs) {
    if (lhs.first == rhs.first && lhs.second == rhs.second) {
        return true;
    }
    return false;
};
#define MyVector std::vector
template<class T1, class T2>
bool operator!=(const MyPair<T1, T2>& lhs, const MyPair<T1, T2>& rhs) {
    if (lhs.first == rhs.first && lhs.second == rhs.second) {
        return false;
    }
    return true;
};

template<class T1, class T2>
MyPair<T1, T2>& make_MyPair(T1 lhs, T2 rhs) {
    return *(new MyPair<T1, T2>(lhs, rhs));

}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_number_editingFinished();

private:
    Ui::MainWindow *ui;
    MyVector<int> t;
    MyPair<int,int> h;
};


#endif // MAINWINDOW_H
