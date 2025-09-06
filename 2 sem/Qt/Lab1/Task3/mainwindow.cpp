#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->SpinBirthDay->setDateRange(QDate(1,1,1), QDate(QDate::currentDate()));
    ui->SpinNewDate->setDateRange(QDate(1000,1,1), QDate(9999,12,31));

}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_SaveFileWay_clicked()
{
    QFile MyFile(ui->FileWay->text());
    File(MyFile);
}
void MainWindow::File(QFile &MyFile)
{
    //File doesnt exist
    if(!MyFile.exists()){
        QMessageBox::warning(this,"Ошибка поиска файла","Файл не был найден!");
        return;
    }
    //File broke
    if(!MyFile.open(QIODevice::ReadWrite)){
        QMessageBox::warning(this,"Ошибка открытия файла","Файл не удалось открыть!");
        return;
    }




    //Real size of Arr and StartSize
    int StartSize = 50;
    int RealSize = 0;


    QTextStream stream(&MyFile);
    stream.seek(0);


    while(!stream.atEnd()){


        for(int i = 0; i < 12; i++ ){
            CheckFile[i] = stream.read(1);
        }
        if(CheckFile[2] != "." ||CheckFile[5] != "."){
            Check = true;
            break;
        }
//        if(CheckFile[10] != "'\'"){
//            QMessageBox::warning(this,"Ошибка!","В файле введены некорректные данные.");
//            Check = true;
//            break;
//        }

        CheckNum(0);
        CheckNum(1);
        CheckNum(3);
        CheckNum(4);
        CheckNum(6);
        CheckNum(7);
        CheckNum(8);
        CheckNum(9);

    }

    if(Check){
        QMessageBox::warning(this,"Ошибка!","В файле введены некорректные данные.");
        return;
    }

    stream.seek(0);

    Date* DateArr = new Date[StartSize];

    //Read all File
    while(!stream.atEnd()){

        QString OneStrFromFile = stream.readLine();
        QList parts = OneStrFromFile.split(".");//по точке разделять

        Date date(parts[0].toInt(),parts[1].toInt(),parts[2].toInt());

        DateArr[RealSize] = date;
        RealSize++;

        if(RealSize == StartSize)
            DateArr = ExpansionMyDateArr(DateArr, StartSize, RealSize);
    }

    MyFile.close();

    QStandardItemModel* list = new QStandardItemModel(RealSize, 5, this);

    ui->NumPosDate->setRange(1,RealSize);
    ui->InfoTable->setModel(list);

    for(int i = 0; i < list->rowCount(); i++){

        Date Current_Date(QDate::currentDate().day(),QDate::currentDate().month(),QDate::currentDate().year());
        list->setData( list->index(i,0), DateArr[i].GetDate_str());
        list->setData( list->index(i,1), DateArr[i].NextDay().GetDate_str());
        list->setData( list->index(i,2), DateArr[i].Duration(Current_Date));
        list->setData( list->index(i,3), DateArr[i].PreviousDay().GetDate_str());
        list->setData( list->index(i,4), DateArr[i].WeekNumber());
    }

    list->setHeaderData(0,Qt::Horizontal, "Date");
    list->setHeaderData(1,Qt::Horizontal, "Next date");
    list->setHeaderData(2,Qt::Horizontal, "Durations");
    list->setHeaderData(3,Qt::Horizontal, "Previous date");
    list->setHeaderData(4,Qt::Horizontal, "Week number");

    delete[] DateArr;
}

bool MainWindow::CheckNum(int i)
{
    if(!(CheckFile[i] == "0" || CheckFile[i] == "1" || CheckFile[i] == "2" || CheckFile[i] == "3" || CheckFile[i] == "4" || CheckFile[i] == "5" || CheckFile[i] == "6" || CheckFile[i] == "7" || CheckFile[i] == "8" || CheckFile[i] == "9")){
        return Check = true;
    }
}

Date *MainWindow::ExpansionMyDateArr(Date *DateArr, int &StartSize, const int &RealSize)
{
    StartSize *= 2;
    Date* NewDateArr = new Date[StartSize];
        for (int i = 0; i < RealSize; i++) {
            NewDateArr[i] = DateArr[i];
        }
    delete[] DateArr;
    return NewDateArr;
}

void MainWindow::on_SaveNewDate_clicked()
{
    QFile MyFile(ui->FileWay->text());
    MyFile.open(QIODevice::Append | QIODevice::Text);

    QDate CopyDate = ui->SpinNewDate->date();
    Date NewDate(CopyDate.day(),CopyDate.month(),CopyDate.year());

    QString Date_str = NewDate.GetDate_str();

    QTextStream stream(&MyFile);
//    stream  << "\n";
    stream  << Date_str << "\n";

    MyFile.close();

    File(MyFile);
}


void MainWindow::on_SaveEditDate_clicked()
{
    QDate New_Edit_Date = ui->EditDay->date();
    Date New_Date(New_Edit_Date.day(),New_Edit_Date.month(),New_Edit_Date.year());

    QFile MyFile(ui->FileWay->text());

    MyFile.open(QIODevice::ReadWrite);

    QTextStream stream(&MyFile);
    stream.seek(12 * (ui->NumPosDate->text().toInt() - 1));


    QString Date_String = New_Date.GetDate_str();
    stream << Date_String;

    MyFile.close();

    File(MyFile);
}


void MainWindow::on_SaveBirthdayDate_clicked()
{
    QDate New_Edit_Date = ui->SpinBirthDay->date();
    Date Current_Date(QDate::currentDate().day(),QDate::currentDate().month(),QDate::currentDate().year());
    Date New_Date(New_Edit_Date.day(),New_Edit_Date.month(),New_Edit_Date.year());

    if(Current_Date.DaysTillYourBithday(New_Date) == 0 ){
        QMessageBox::information(this,"Поздравление!","Поздравляем вас с Днем Рождения!");
    }
    else{
        QMessageBox::information(this,"Упс!","Сегодня не ваш день рождения.\nДо вашего дня Рождения: " + QString::number(Current_Date.DaysTillYourBithday(New_Date)));
    }

}

