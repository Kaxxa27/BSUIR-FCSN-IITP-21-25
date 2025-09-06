#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    Item = new List();
    setFixedSize(900,500);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::File(QFile &File)
{
        Item->ClearList();
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

            QString GroupOfItem , ModelOfItem, FIO, Ready;

            GroupOfItem = stream.readLine();
            ModelOfItem = stream.readLine();
            FIO = stream.readLine();
            Ready = stream.readLine();

            Item->push_back(GroupOfItem, ModelOfItem, FIO, Ready);
        }

        File.close();
        ui->DeleteSpinBox->setRange(1,Item->GetSizeList());

}

void MainWindow::SetInfoTable()
{
    ui->InfoTable->clear();
    for(int i = 0; i < Item->GetSizeList(); ++i)
    {
        ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Группа изделий: " + Item->PrintElement(i,1) + "\n"
        "   Марка изделий: "+ Item->PrintElement(i,2) + "\n"
        "   ФИО: "+ Item->PrintElement(i,3) + "\n"
        "   Готовность: "+ Item->PrintElement(i,4) + "\n");
    }

}

void MainWindow::on_FileWay_clicked()
{
    MyFile= QFileDialog::getOpenFileName(this, tr("Open"), "C://", "All files(*.txt*)");
    QFile file(MyFile);
    File(file);
}


void MainWindow::on_ShowButton_clicked()
{
    SetInfoTable();
}


void MainWindow::on_AddButton_clicked()
{
    QFile EditFile;
    EditFile.setFileName(MyFile);
    EditFile.open(QIODevice::Append | QIODevice::Text);
    Item->push_back(ui->CroupOfItemName->text(),
                          ui->ModelIfItemName->text(),
                          ui->FIOLine->text(),
                          ui->ReadyName->text());

    QTextStream stream(&EditFile);
    stream  << ui->CroupOfItemName->text() << "\n";
    stream  << ui->ModelIfItemName->text() << "\n";
    stream  << ui->FIOLine->text() << "\n";
    stream  << ui->ReadyName->text() << "\n";

    EditFile.close();
    ui->InfoTable->clear();
}


void MainWindow::on_DeleteButton_clicked()
{
    QFile NFile;
    NFile.setFileName(MyFile);

    if (NFile.open(QIODevice::WriteOnly | QIODevice::Truncate)){
      NFile.close();
    }

    QTextStream stream(&NFile);
    if(Item->GetSizeList() == 0){
        QMessageBox::warning(this,"Ошибка","В файле отсутствуют данные!");
        return;
    }


    NFile.open(QIODevice::Append | QIODevice::Text);

    Item->pop_back();

    for(int i = 0; i < Item->GetSizeList();i++){
        stream <<  Item->PrintElement(i,1) << "\n";
        stream <<  Item->PrintElement(i,2) << "\n";
        stream <<  Item->PrintElement(i,3) << "\n";
        stream <<  Item->PrintElement(i,4) << "\n";
    }
    NFile.close();
    File(NFile);
    ui->DeleteSpinBox->setRange(1,Item->GetSizeList());
}


void MainWindow::on_pushButton_2_clicked()
{//NOT_READY
    ui->InfoTable->clear();
    bool check = true;
    QString Num = "NO";
    for(int i = 0; i < Item->GetSizeList();i++){
        if(Item->PrintElement(i,4) == Num)
        {
            ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Группа изделий: " + Item->PrintElement(i,1) + "\n"
            "   Марка изделий: "+ Item->PrintElement(i,2) + "\n"
            "   ФИО: "+ Item->PrintElement(i,3) + "\n"
            "   Готовность: "+ Item->PrintElement(i,4) + "\n");
            check = false;
        }

    }
    if(check)
    {
        ui->InfoTable->insertPlainText("Данные отсутствуют!");
    }
}


void MainWindow::on_ReadyButton_clicked()
{
    ui->InfoTable->clear();
    bool check = true;
    QString Num = "YES";
    for(int i = 0; i < Item->GetSizeList();i++){
        if(Item->PrintElement(i,4) == Num)
        {
            ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Группа изделий: " + Item->PrintElement(i,1) + "\n"
            "   Марка изделий: "+ Item->PrintElement(i,2) + "\n"
            "   ФИО: "+ Item->PrintElement(i,3) + "\n"
            "   Готовность: "+ Item->PrintElement(i,4) + "\n");
            check = false;
        }

    }
    if(check)
    {
        ui->InfoTable->insertPlainText("Данные отсутствуют!");
    }
}

