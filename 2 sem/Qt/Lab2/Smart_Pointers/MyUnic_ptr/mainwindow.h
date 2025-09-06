#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <myunic_ptr.h>

#include <QDebug>
#include <QWidget>
#include <utility>
#include <QStandardItemModel>
#include <QVector>
#include <QVariant>
#include <ostream>
#include <QString>
#include <QStandardItemModel>


QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MyDialog : public QDialog
{
    Q_OBJECT

public:
    MyDialog(int Num, QWidget *parent = nullptr);
    MyDialog(int Num,int x,QWidget *parent = nullptr);

    QPushButton *ButtonSave;
    QPushButton *ButtonClose;

    QSpinBox *IndexBox;
    QSpinBox *IndexBox2;

    MyDialog* Dialog;

    int GETIndex();
    int GETIndex2();
};

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();


    template<class F>
    typename std::remove_reference<F>::type&& MyMove(F&& t) noexcept;


public slots:
    void on_MakeButton_clicked();

    void on_SwapButton_clicked();

    void on_RemoveButton_clicked();

    void on_MoveButton_clicked();

    void StartFunc();

private:
    Ui::MainWindow *ui;
    void MainFunction();

    MyUnic_ptr<int>*MyUnicArr;
    MyDialog* Dialog;
};



#endif // MAINWINDOW_H
