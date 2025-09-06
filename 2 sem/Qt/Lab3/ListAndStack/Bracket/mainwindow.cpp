#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
   // setFixedSize(530,510);
    setFixedSize(size());
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_FileButton_clicked()
{
    MyFile= QFileDialog::getOpenFileName(this, tr("Open"), "C://", "All files(*.txt*)");
    QFile file(MyFile);
    File(file);
}

void MainWindow::File(QFile &File)
{
        Stack.Clear();
        Text.clear();
        //File doesnt exist
        if(!File.exists()){
            QMessageBox::warning(this,"Ошибка поиска файла","Файл не был найден!");
            return;
        }
        //File broke
        if(!File.open(QIODevice::ReadWrite)){
            QMessageBox::warning(this,"Ошибка открытия файла","Файл не удалось открыть!");
            return;
        }

        QTextStream stream(&File);

        stream.seek(0);
        while(!stream.atEnd()){

            QString Line;
            Line = stream.readLine();
            Text.push_back(Line);
        }

        File.close();
        PrintToInfoTable();
}

void MainWindow::PrintToInfoTable()
{
    QString temp;

    for(QString str : Text)
    {
        temp += str + "\n";
    }
    ui->InfoTable->setText(temp);
}

void MainWindow::AddToFile()
{
    QFile file;
    file.setFileName(MyFile);

        if (file.open(QIODevice::Truncate | QIODevice::WriteOnly)) {

            QTextStream stream(&file);
            for (QString str : Text)
                stream << str << "\n";
        }
        else
        {
            qDebug() << "BAD OPEN";
        }

        file.close();
}

bool MainWindow::isStrCorrect(QString &str, int &row)
{
    for (int column = 0; column < str.size(); column++) {
            if (str[column] == '[') { Stack.Push(bracket{'[', row, column}); }
            if (str[column] == '{') { Stack.Push(bracket{'{', row, column}); }
            if (str[column] == '(') { Stack.Push(bracket{'(', row, column}); }

            if (str[column] == ']') {
                if (Stack.Empty() ||  Stack.ReturnTop().item != '[') {
                    Stack.Push(bracket{']', row, column});
                    return false;
                }
                else
                    Stack.Pop();
            }

            if (str[column] == '}') {
                if (Stack.Empty() || Stack.ReturnTop().item != '{'){
                    Stack.Push(bracket{'}', row, column});
                    return false;
                }
                else
                    Stack.Pop();
            }

            if (str[column] == ')') {
                if (Stack.Empty() ||  Stack.ReturnTop().item != '(') {
                    Stack.Push(bracket{')', row, column});
                    return false;
                }
                else
                    Stack.Pop();
            }
        }

    return true;
}

bool MainWindow::isListCorrect()
{
    Stack.Clear();
      Text = ui->InfoTable->toPlainText().split('\n');

       for (int row = 0; row < Text.size(); row++)
           if (!isStrCorrect(Text[row], row))
               return false;

       return Stack.Empty();
}

void MainWindow::on_SaveTextButton_clicked()
{
    Text = ui->InfoTable->toPlainText().split('\n');
    AddToFile();
}


void MainWindow::on_CheckButton_clicked()
{
    if (!isListCorrect())
    {
        QMessageBox::warning(this, " ",
                              "Скобка: " + QString(Stack.ReturnTop().item) +
                              "\nСтрока: " + QString::number(Stack.ReturnTop().row + 1) +
                              "\nСтолбец: " + QString::number(Stack.ReturnTop().column + 1));
    }
    else
    {
        QMessageBox::information(this, " ", "Success");
    }
}

