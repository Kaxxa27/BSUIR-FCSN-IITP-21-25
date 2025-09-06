#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <myshared.h>
#include <weak.h>

#include <QDebug>
#include <QWidget>
#include <utility>
#include <QStandardItemModel>
#include <QVector>
#include <QVariant>
#include <ostream>
#include <QString>
#include <QStandardItemModel>
#include <QPushButton>
#include <QSpinBox>
#include <QDialog>
#include <QLabel>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE


class MyDialog : public QDialog
{
    Q_OBJECT

public:
    MyDialog(QWidget *parent = nullptr);
    MyDialog(int Num,QWidget *parent = nullptr);

    QPushButton *ButtonSave;
    QPushButton *ButtonClose;

    QSpinBox *IndexBox;
    QSpinBox *IndexBox2;

    MyDialog* Dialog;

    int GETIndex();
    int GETIndex2();
};
class MyDialogWeak : public QDialog
{
    Q_OBJECT

public:
    MyDialogWeak(int Num,QWidget *parent = nullptr);

    QPushButton *ButtonSave;
    QPushButton *ButtonClose;

    QSpinBox *IndexBoxWeak;
    QSpinBox *IndexBox2Weak;

    MyDialog* Dialog;

    int GETIndexWeak();
    int GETIndex2Weak();
};

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_EditSharedButton_clicked();

    void on_AssignButton_clicked();

    void on_SwapSharedButton_clicked();

    void on_WeakToWeakButton_clicked();

    void on_SharedToWeakButton_clicked();

    void on_WeakToSharedButton_clicked();

private:
    Ui::MainWindow *ui;
    void MainFunc();

    MyShared<int>* SharedArr;
    Weak<int>* WeakArr;

    MyDialog* Dialog;
    MyDialogWeak* DialogWeak;
};
#endif // MAINWINDOW_H
