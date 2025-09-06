#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QMessageBox>
#include <QFile>
#include <QString>
#include <QFileDialog>
#include <bracketstak.h>

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
    void on_FileButton_clicked();

    void on_SaveTextButton_clicked();

    void on_CheckButton_clicked();

private:
    Ui::MainWindow *ui;

    BracketStak<bracket> Stack;
    void File(QFile &MyFile);
    QString MyFile;
    void PrintToInfoTable();
    void AddToFile();
    QStringList Text;
    bool isStrCorrect(QString &str, int& row);
    bool isListCorrect();

};
#endif // MAINWINDOW_H
