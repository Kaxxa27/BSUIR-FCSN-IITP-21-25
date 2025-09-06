#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include<my_queue.h>

#include<QDebug>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

    void Present();
private slots:
    void on_PushButton_clicked();

    void on_ClearButton_clicked();

    void on_EmptyButton_clicked();

    void on_PopButton_clicked();

    void on_SizeButton_clicked();

    void on_Search_Button_clicked();

    void on_ViewButton_clicked();

private:
    Ui::MainWindow *ui;
    my_queue<int> q;


};
#endif // MAINWINDOW_H
