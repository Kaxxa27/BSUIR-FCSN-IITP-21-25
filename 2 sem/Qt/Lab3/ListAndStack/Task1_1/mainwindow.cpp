#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    ui->PlayerNumSpinBox->setRange(1,1000);
    ui->AgeSpinBox->setRange(1,300);
    ui->HeightSpinBox->setRange(1,500);
    ui->WeightSpinBox->setRange(1,500);
    ui->DeleteSpinBox->setRange(1,Competitors.GetSizeList());
    ui->WeghtSortSpinBox->setRange(1,500);
    ui->HeightSortSpinBox->setRange(1,500);
    ui->AgeSortSpinBox->setRange(1,300);

    setFixedSize(880,630);

}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_FileWay_clicked()
{
    MyFile= QFileDialog::getOpenFileName(this, tr("Open"), "C://", "All files(*.txt*)");
    QFile file(MyFile);
    File(file);
}

void MainWindow::File(QFile &File)
{
        Competitors.ClearList();
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

            QString CountryName, TeamName, FIO, PlayerNum, Age, Height, Weight;

            CountryName = stream.readLine();
            TeamName = stream.readLine();
            FIO = stream.readLine();
            PlayerNum = stream.readLine();
            Age = stream.readLine();
            Height = stream.readLine();
            Weight = stream.readLine();

            Competitors.push_back(CountryName, TeamName, FIO, PlayerNum.toInt(), Age.toInt(), Height.toInt(), Weight.toInt());
        }

        File.close();
        ui->DeleteSpinBox->setRange(1,Competitors.GetSizeList());

}

void MainWindow::SetInfoTable()
{
    ui->InfoTable->clear();
    for(int i = 0; i < Competitors.GetSizeList(); ++i)
    {
        ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Страна: " + Competitors.PrintElement(i,1) + "\n"
        "   Команда: "+ Competitors.PrintElement(i,2) + "\n"
        "   ФИО: "+ Competitors.PrintElement(i,3) + "\n"
        "   Игровой номер: "+ Competitors.PrintElement(i,4) + "\n"
        "   Возраст: "+ Competitors.PrintElement(i,5) + "\n"
        "   Рост: "+ Competitors.PrintElement(i,6) + "\n"
        "   Вес: "+ Competitors.PrintElement(i,7) + "\n");
    }

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
    Competitors.push_back(ui->CountryNameLine->text(),
                          ui->TeamNameLine->text(),
                          ui->FIOLine->text(),
                          ui->PlayerNumSpinBox->value(),
                          ui->AgeSpinBox->value(),
                          ui->HeightSpinBox->value(),
                          ui->WeightSpinBox->value());

    QTextStream stream(&EditFile);
    stream  << ui->CountryNameLine->text() << "\n";
    stream  << ui->TeamNameLine->text() << "\n";
    stream  << ui->FIOLine->text() << "\n";
    stream  << ui->PlayerNumSpinBox->value() << "\n";
    stream  << ui->AgeSpinBox->value() << "\n";
    stream  << ui->HeightSpinBox->value() << "\n";
    stream  << ui->WeightSpinBox->value() << "\n";

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
    if(Competitors.GetSizeList() == 0){
        QMessageBox::warning(this,"Ошибка","В файле отсутствуют данные!");
        return;
    }


    NFile.open(QIODevice::Append | QIODevice::Text);

    Competitors.Remove(ui->DeleteSpinBox->value());

    for(int i = 0; i < Competitors.GetSizeList();i++){
        stream <<  Competitors.PrintElement(i,1) << "\n";
        stream <<  Competitors.PrintElement(i,2) << "\n";
        stream <<  Competitors.PrintElement(i,3) << "\n";
        stream <<  Competitors.PrintElement(i,4) << "\n";
        stream <<  Competitors.PrintElement(i,5) << "\n";
        stream <<  Competitors.PrintElement(i,6) << "\n";
        stream <<  Competitors.PrintElement(i,7) << "\n";
    }
    NFile.close();
    File(NFile);
    ui->DeleteSpinBox->setRange(1,Competitors.GetSizeList());
}


void MainWindow::on_SaveWeightSortButton_clicked()
{
    ui->InfoTable->clear();
    bool check = true;
    int Num = ui->WeghtSortSpinBox->value();
    for(int i = 0; i < Competitors.GetSizeList();i++){
        if(Competitors.ReturnAgeWeightHeight(i,3) == Num)
        {
            ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Страна: " + Competitors.PrintElement(i,1) + "\n"
            "   Команда: "+ Competitors.PrintElement(i,2) + "\n"
            "   ФИО: "+ Competitors.PrintElement(i,3) + "\n"
            "   Игровой номер: "+ Competitors.PrintElement(i,4) + "\n"
            "   Возраст: "+ Competitors.PrintElement(i,5) + "\n"
            "   Рост: "+ Competitors.PrintElement(i,6) + "\n"
            "   Вес: "+ Competitors.PrintElement(i,7) + "\n");
            check = false;
        }

    }
    if(check)
    {
        ui->InfoTable->insertPlainText("Данные отсутствуют!");
    }
}


void MainWindow::on_HeightSortButton_clicked()
{
    ui->InfoTable->clear();
    bool check = true;
    int Num = ui->HeightSortSpinBox->value();
    for(int i = 0; i < Competitors.GetSizeList();i++){
        if(Competitors.ReturnAgeWeightHeight(i,2) >= Num)
        {
            ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Страна: " + Competitors.PrintElement(i,1) + "\n"
            "   Команда: "+ Competitors.PrintElement(i,2) + "\n"
            "   ФИО: "+ Competitors.PrintElement(i,3) + "\n"
            "   Игровой номер: "+ Competitors.PrintElement(i,4) + "\n"
            "   Возраст: "+ Competitors.PrintElement(i,5) + "\n"
            "   Рост: "+ Competitors.PrintElement(i,6) + "\n"
            "   Вес: "+ Competitors.PrintElement(i,7) + "\n");
            check = false;
        }

    }
    if(check)
    {
        ui->InfoTable->insertPlainText("Данные отсутствуют!");
    }
}


void MainWindow::on_AgeSortButton_clicked()
{
    ui->InfoTable->clear();
    bool check = true;
    int Num = ui->AgeSortSpinBox->value();
    for(int i = 0; i < Competitors.GetSizeList();i++){
        if(Competitors.ReturnAgeWeightHeight(i,1) == Num)
        {
            ui->InfoTable->insertPlainText(QString::number(i + 1) + ")Страна: " + Competitors.PrintElement(i,1) + "\n"
            "   Команда: "+ Competitors.PrintElement(i,2) + "\n"
            "   ФИО: "+ Competitors.PrintElement(i,3) + "\n"
            "   Игровой номер: "+ Competitors.PrintElement(i,4) + "\n"
            "   Возраст: "+ Competitors.PrintElement(i,5) + "\n"
            "   Рост: "+ Competitors.PrintElement(i,6) + "\n"
            "   Вес: "+ Competitors.PrintElement(i,7) + "\n");
            check = false;
        }

    }
    if(check)
    {
        ui->InfoTable->insertPlainText("Данные отсутствуют!");
    }
}

