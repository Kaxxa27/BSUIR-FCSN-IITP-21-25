#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QDate>
#include <QFile>
#include <QMessageBox>
#include <QStandardItemModel>
#include <QDateEdit>
#include <ostream>

#include <date.h>

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
    void on_SaveFileWay_clicked();

    void on_SaveNewDate_clicked();



    void on_SaveEditDate_clicked();

    void on_SaveBirthdayDate_clicked();

private:
    Ui::MainWindow *ui;
    QStandardItemModel *InfoTable;

    void File(QFile &MyFile);
    bool CheckNum(int i);
    Date* ExpansionMyDateArr(Date *DateArr, int& StartSize, const int& RealSize);
    QString CheckFile[12];
    bool Check = false;
};
#endif // MAINWINDOW_H
