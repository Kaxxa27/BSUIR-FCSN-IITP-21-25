#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

#include<mypair.h>

#include<stringlib.h>
#include<vectorlib.h>

#include <regex>
#include <QRegularExpression>
#include "variable.h"
#include <QFile>
#include <QFileDialog>
#include <QStandardItem>
#include <QMessageBox>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

    void findVariables();
    void findFunctions();
    void findClasses();
    void findVariableChange();
    void findLocalVariables();
    void findOverloads();
    void findBranch();
    void findErrors();

private slots:
    void on_FileButton_clicked();

    void on_SaveButton_clicked();

    void on_VariablesButton_clicked();

    void on_FuncButton_clicked();

    void on_ClassesButton_clicked();

    void on_LocalVarButton_clicked();

    void on_BranchButton_clicked();

    void on_EditButton_clicked();

    void on_OperatorButton_clicked();

    void on_ErrorsButton_clicked();

private:
    Ui::MainWindow *ui;

    QString streamStr;
    std::vector<std::string> varList;
    Variable VarInfo;
    Variable funcs;
    string currentText;
};
#endif // MAINWINDOW_H
