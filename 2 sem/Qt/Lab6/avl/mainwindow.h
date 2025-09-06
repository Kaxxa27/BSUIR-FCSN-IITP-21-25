#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "tree.h"
QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_PushButton_clicked();

    void on_RemoveButton_clicked();

    void on_ChildrenButton_clicked();

    void on_ViewButton_clicked();

    void on_SearchButton_clicked();

private:
    Ui::MainWindow *ui;
    AVL<int, QString> tree;
};
#endif // MAINWINDOW_H
