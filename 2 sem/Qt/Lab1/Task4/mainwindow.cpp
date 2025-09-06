#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
//    File();
    AddToComboBox();
    CountStartDeleteNum();

}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::File(QFile &File)
{

//    File.setFileName("C:/Qt/Project/Task4/File.txt");

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
    Worker.ClearMyVector();
    while(!stream.atEnd()){

        QString FIO, PrName,PrTask,Deadline,StartT,EndT;

        FIO = stream.readLine();
        PrName = stream.readLine();
        PrTask = stream.readLine();
        Deadline = stream.readLine();
        StartT = stream.readLine();
        EndT = stream.readLine();

        QList DeadParts = Deadline.split(".");//по точке разделять
        QList StartTParts = StartT.split(".");
        QList EndTParts = EndT.split(".");
        //Dead


       QDate Dead(DeadParts[0].toInt(),DeadParts[1].toInt(),DeadParts[2].toInt());
       QDate Start(StartTParts[0].toInt(),StartTParts[1].toInt(),StartTParts[2].toInt());
       QDate End(EndTParts[0].toInt(),EndTParts[1].toInt(),EndTParts[2].toInt());

       ui->SpinNumWorker->setMinimum(1);
       ui->SpinNumWorker->setMaximum(Worker.SizeOfMyVector() + 1);

       Worker.AddToStruct(FIO, PrName, PrTask, Deadline, StartT, EndT, Dead, Start, End);
       if(!Worker.CorrectDate(Deadline)){
           Worker.ClearMyVector();
           QMessageBox::warning(this,"Ошибка","В файле введены некорректные данные!\nПочистите файл!");
           return;
       }
       if(!Worker.CorrectDate(StartT)){
           Worker.ClearMyVector();
           QMessageBox::warning(this,"Ошибка","В файле введены некорректные данные!\nПочистите файл!");
           return;
       }
       if(!Worker.CorrectDate(EndT)){
           Worker.ClearMyVector();
           QMessageBox::warning(this,"Ошибка","В файле введены некорректные данные!\nПочистите файл!");
           return;
       }
    }
    File.close();

}

void MainWindow::AddToComboBox()
{
    ui->ComboBoxWorkers->clear();
    for(int i = 0; i < Worker.WorkerSize(); i++){
        ui->ComboBoxWorkers->addItem(Worker.AddToComboBoxWorkers(i));
    }
}

int MainWindow::CountStartDeleteNum()
{
    for(int i = 0; i < Worker.WorkerSize(); i++){
        DeleteNUM++;
    }
}

int MainWindow::PosOfTextInFile()
{
    QFile MyFile;
    MyFile.setFileName("C:/Qt/Project/Task4/File.txt");
    QTextStream stream(&MyFile);

    stream.seek(0);
    while(!stream.atEnd()){
      stream.readLine(1);
      CountSeek++;
    }
    return CountSeek;
}

void MainWindow::MainFunc()
{

}

void MainWindow::on_pushButton_clicked()
{

    ui->InfoTable->clear();
    ui->InfoTable->insertPlainText(Worker.ShowInfo());

}


void MainWindow::on_AddWorkers_clicked()
{
    QFile MyFile;
    MyFile.setFileName("C:/Qt/Project/Task4/File.txt");

    MyFile.open(QIODevice::Append | QIODevice::Text);

    QString NewFIO = ui->FIOline->text();
    QString NewProjectName = ui->ProjectNameline->text();
    QString NewProjectTask = ui->ProjectTaskline->text();

    QDate NewDeadline = ui->DeadlineDate->date();
    QDate NewStartTime = ui->StartTimeDate->date();
    QDate NewEndTime = ui->EndTimeDate->date();

    QString StrNewDeadLine = NewDeadline.toString("dd.MM.yyyy");
    QString StrNewStartTime = NewStartTime.toString("dd.MM.yyyy");
    QString StrNewEndTime = NewEndTime.toString("dd.MM.yyyy");

    QTextStream stream(&MyFile);
    stream  << NewFIO << "\n";
    stream  << NewProjectName << "\n";
    stream  << NewProjectTask << "\n";
    stream  << StrNewDeadLine << "\n";
    stream  << StrNewStartTime << "\n";
    stream  << StrNewEndTime << "\n";

    MyFile.close();
    DeleteNUM++;
    ui->InfoTable->clear();
    File(MyFile);
    AddToComboBox();
}



void MainWindow::on_DeleteButton_clicked()
{

    QFile MyFile;
    MyFile.setFileName("C:/Qt/Project/Task4/File.txt");

    if (MyFile.open(QIODevice::WriteOnly | QIODevice::Truncate)){
      MyFile.close();
    }

    QTextStream stream(&MyFile);
    if(DeleteNUM == 0){
        QMessageBox::warning(this,"Ошибка","В файле отсутствуют данные!");
        return;
    }


    MyFile.open(QIODevice::Append | QIODevice::Text);

    Worker.DeleteWorker(ui->SpinNumWorker->value() - 1);

    for(int i = 0; i < Worker.SizeOfMyVector();i++){
        stream << Worker.AddNewInfoToFile(i);
    }
    MyFile.close();
    DeleteNUM--;
    File(MyFile);
    AddToComboBox();

}


void MainWindow::on_SortButton_clicked()
{
    ui->InfoTable->clear();
    ui->InfoTable->insertPlainText("Сортировка по дате выполнения.\n");
    ui->InfoTable->insertPlainText("Название проекта / дата:\n");
    ui->InfoTable->insertPlainText(Worker.SortEndDate());

}


void MainWindow::on_SumTime_clicked()
{
    ui->InfoTable->clear();
    ui->InfoTable->insertPlainText("Вывод времени, потраченного работником над проектом.\n");
    ui->InfoTable->insertPlainText("ФИО / время:\n");
    ui->InfoTable->insertPlainText(Worker.ListSumTime());

}


void MainWindow::on_SaveFileWay_clicked()
{
    QString MyFile= QFileDialog::getOpenFileName(this, tr("Open"), "C://", "All files(*.txt*)");
    QFile file(MyFile);
    File(file);
}

