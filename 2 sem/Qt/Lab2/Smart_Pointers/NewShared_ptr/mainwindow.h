#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <newshared.h>

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

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private:
    Ui::MainWindow *ui;
    void MainFunc();
    MyShared<int>* SharedArr;
};
#endif // MAINWINDOW_H
