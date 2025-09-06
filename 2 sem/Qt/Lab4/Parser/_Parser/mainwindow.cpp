#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    setFixedSize(780,535);
    ui->setupUi(this);

};

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::findVariables(){
    smatch sm;

    string inp = currentText;
    regex reg("(int|char|float|double|bool|unsigned|long|short)\\s+(\\w+)\\s*(\\=|;|,)");
    int pos = 0;
    MyVector<int> positions;

    MyVector<std::string> typeList;
    while(regex_search(inp, sm, reg)){

        positions.push_back(pos+sm.position(0));

        typeList.push_back(sm[1].str());
        varList.push_back(sm[2].str());

        pos += sm.prefix().length() + sm.length();
        inp = sm.suffix();
    }

    int row = 0;
    int column = 0;
    int counter = 0;

    if(positions.size() == 0){
        //ui->pushButton_7->setDisabled(true);
        return;
    }

    inp = currentText;


    for(int i = 0; i < inp.length(); i++){

        if(i == positions[counter]){
            VarInfo.setVariable(typeList.at(counter),varList.at(counter), i, row, column);
            counter++;
        }

        column++;
        if(inp[i]=='\n'){
            row++;
            column=0;
        }
    }

    QString msgInt="int: ";
    QString msgchar="char: ";
    QString msgfloat="float: ";
    QString msgdouble="double:";
    QString msgbool="bool: ";
    QString msgunsigned="unsigned: ";
    QString msglong="long: ";
    QString msgshort="short: ";

    for(int i=0;i<VarInfo.size();i++){
        if(VarInfo.getType(i) == "int"){
            msgInt+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "char"){
            msgchar+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "float"){
            msgfloat+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "double"){
            msgdouble+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "bool"){
            msgbool+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "unsigned"){
            msgunsigned+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "long"){
            msglong+=QString::fromStdString(VarInfo.getName(i))+" ";
        }
        else if (VarInfo.getType(i) == "short"){
            msgshort+=QString::fromStdString(VarInfo.getName(i))+" ";
        }

    }
    QString mainMsg = msgInt + "\n" + msgbool + "\n" + msgchar + "\n" + msgdouble + "\n" + msgunsigned +
                    "\n" + msgfloat + "\n" + msglong + "\n" + msgshort;
    ui->InformationText->clear();
    ui->InformationText->setText(mainMsg);

}
void MainWindow::findFunctions(){

    smatch sm, sm2;

    string inp = ui->MainText->toPlainText().toStdString();
    string reg = "(int|char|float|double|void|string|std::string|MyString|vector)\\s+(\\w+)\\s*\\((.*)\\)";
    string re2 = "(int|char|float|double)\\s+\\w*,?";
    regex a(reg);
    regex b(re2);
     MyVector<int> positions;
     MyVector<std::string> names;

    int pos = 0;
    string msg;

    while (regex_search(inp, sm, a))
    {
        msg+= (sm[1].str() + " " + sm[2].str() + " " +"( ");
        names.push_back(sm[2]);

        string tmp = sm[3];
        while (regex_search(tmp, sm2, b))
        {
             msg+= (sm2[1].str() + " ");
             tmp = sm2.suffix();
        }

        positions.push_back(pos+sm.position(0));


        msg+= (")\n");
        pos += sm.prefix().length() + sm.length();
        inp = sm.suffix();
    }
    ui->InformationText->clear();
    ui->InformationText->setText(QString::fromStdString(msg));

    int row=0;
    int column=0;
    int counter=0;

    if(positions.size()==0){
        return;
    }

    inp=currentText;

    for(int i=0;i<inp.length();i++){

        if(i == positions[counter]){
            funcs.setVariable(names.at(counter),i,row,column);
            counter++;
        }

        column++;
        if(inp[i]=='\n'){
            row++;
            column=0;
        }
    }
}
void MainWindow::findVariableChange(){
    smatch sm;
    string inp=currentText;



    QString msg="Координаты изменений: ";
    for(int i=0;i<varList.size();i++){

        regex reg(varList[i]+"\\s*(\\=|\\+=|\\-=|\\*=|\\/=|\\++|\\--)");
        int pos = 0;


        while(regex_search(inp, sm, reg)){
            qDebug()<<'1';
            msg+= QString::number(sm.position(0) + pos) +"\n";
            pos += sm.prefix().length() + sm.length();
            inp = sm.suffix();
        }
    }


    ui->InformationText->clear();
    ui->InformationText->setText(msg);

}
void MainWindow::findLocalVariables(){
    Variable locals;
    string inp=currentText;
    int bracketsChange=0;
    int counter=0;



    for(int i=0;i < inp.length();i++){

        if(inp[i]=='{' || inp[i]== '}'){
            if(inp[i]=='{'){
                bracketsChange++;
            }

            else{
                bracketsChange--;
            }

        }

        if(bracketsChange < 0) return ;//&&

        if(i == VarInfo.getPos(counter)){
            if(bracketsChange!=0){
                locals.setVariable(VarInfo.getName(counter),VarInfo.getPos(counter),VarInfo.getRow(counter),VarInfo.getColumn(counter));
            }
            counter++;
        }
    }
    QString msg="Координаты локальных переменных: ";


    for(int i=0;i<locals.size();i++){
        msg+=QString::number(locals.getRow(i))+" "+QString::number(locals.getColumn(i))+"\n";
    }
    ui->InformationText->clear();
    ui->InformationText->setText(msg);
}
void MainWindow::findOverloads(){
    Variable copied(funcs);
    Variable overloads;
    bool was=false;
    for(int i=0;i<copied.size();i++){

        for(int j=i+1;j<copied.size();j++){

            if(copied.getName(i)==copied.getName(j)){
                if(!was){
                    overloads.setVariable(copied.getName(i),copied.getPos(i),copied.getRow(i),copied.getColumn(i));
                    was=true;

                }
                overloads.setVariable(copied.getName(j),copied.getPos(j),copied.getRow(j),copied.getColumn(j));
                copied.deleteElem(j);
                j--;
                was=false;
            }
        }
    }


    QString msg="Координаты перегрузок: ";

    for(int i =0 ; i < overloads.size();i++){
        msg+=QString::number(overloads.getRow(i))+" "+QString::number(overloads.getColumn(i))+"\n";
    }
    ui->InformationText->clear();
    ui->InformationText->setText(msg);
}
void MainWindow::findBranch(){



    smatch sm;
    string inp=currentText;
    regex reg("(((if|if else)\\s*(\\()(.*)(\\))\\s*)|else)(\\{)");



     MyVector<int> positions;
    int posit=0;

    while(regex_search(inp,sm,reg)){
        positions.push_back(posit+sm.position(0));

        posit += sm.prefix().length() + sm.length();

        inp = sm.suffix();

    }
    qDebug()<<positions.size();
    if(positions.size()==0){
        return;
    }
    int bracketsCounter;
     MyVector<QPair<int,int>> bracket;

    inp=currentText;

    for(int i=0; i<positions.size() ;i++){
        int pos=positions[i];
        bracketsCounter=2;
        QPair<int,int> tmp1(pos,bracketsCounter);
        bracket.push_back(tmp1);

        while(bracketsCounter!=1){

            if(positions[i+1]==pos){

                i++;
                bracketsCounter++;
                QPair<int,int> tmp(pos,bracketsCounter);
                bracket.push_back(tmp);

            }
            if(inp[pos]=='}'){

                bracketsCounter--;
                QPair<int,int> tmp(pos,bracketsCounter);
                bracket.push_back(tmp);

            }
            pos++;
        }
    }

     MyVector<QPair<int,int>> newOne;

    int row=0;
    int column=0;
    int counter=0;


    inp=currentText;


    for(int i=0;i<inp.length();i++){

        if(i==bracket[counter].first){
            QPair<int,int> tmp(row,bracket[counter].second);
            newOne.push_back(tmp);
            counter++;
        }
        column++;
        if(inp[i]=='\n'){
            row++;
            column=0;
        }
    }


    auto model = new QStandardItemModel(newOne.size(),2,this);

    ui->tableView->setModel(model);

    for(int i = 0; i < model->rowCount(); i++){
        model->setData( model->index(i,0), QString::number(newOne[i].first));
        model->setData( model->index(i,1), QString::number(newOne[i].second));
    }


    model->setHeaderData(0,Qt::Horizontal, "Строка");
    model->setHeaderData(1,Qt::Horizontal, "Глубина");

}
void MainWindow::findErrors(){

    ui->InformationText->clear();

    QString inp=QString::fromStdString(currentText);
    bool existStart=inp.contains("#ifndef");
    bool existEnd=inp.contains("#endif");
    QString msg("#ifdnef, #endif: ");

    if(existStart){
        if(existEnd){
            msg+="Ошибка не найдена\n";

        }
        else{
            msg+="Отсутствует #endif\n";
        }
    }
    else {
        if(existEnd){
            msg+="Отсутствует #ifndef\n";
        }
        else{
            msg+="Ошибка не найдена\n";
        }
    }

    smatch sm;

    regex reg("(while)\\s*(\\()\\s*(false)\\s*(\\))");

    string inp2=currentText;
     MyVector<int> positions;
    int posit=0;

    msg+="while(false) ошибка: ";

    while(regex_search(inp2,sm,reg)){
        positions.push_back(posit+sm.position(0));

        posit += sm.prefix().length() + sm.length();

        inp2 = sm.suffix();
    }

    int row=0;
    int column=0;
    int counter=0;
    inp2=currentText;

    if(positions.size()==0){
        msg+="Ошибки не найдены";
    }

    else{
        for(int i=0;i<inp2.length();i++){

            if(i==positions[counter]){
                msg+="Строка "+ QString::number(row)+"  ";
                counter++;
            }

            column++;
            if(inp2[i]=='\n'){
                row++;
                column=0;
            }
        }
    }

    ui->InformationText->setText(msg);
}
void MainWindow::findClasses(){

    ui->InformationText->clear();
    smatch classMatch, structMatch,arrayMatch,staticArruy;
    string inp=currentText;
    string inp2=currentText;
    string inp4=currentText;
    string inp3=currentText;

    regex classReg("(class)\\s+(\\w+)\\s*\\{");
    regex structReg("(struct)\\s+(\\w+)\\s*\\{");
    regex arrayReg("(\\w+)\\s*[*]\\s*(\\w+)\\s*[=]\\s*(new)\\s*(\\w+)\\s*(\\[)\\s*(\\w+|\\d+)\\s*(\\])");
    regex staticArrayReg("(\\w+)\\s*(\\w+)\\s*(\\[)\\s*(\\d+)\\s*(\\])");

    int pos=0;

    int coutner=0;
    QString msg;
    while(regex_search(inp,classMatch,classReg)){
        cout<<classMatch[1]<< " " <<classMatch[2]<<" " <<classMatch.position(0) + pos <<"\n";
        pos += classMatch.prefix().length() + classMatch.length();
        inp = classMatch.suffix();
        coutner++;
    }
    msg+="Классов: "+QString::number(coutner)+"\n";
    coutner=0;
    while(regex_search(inp2,structMatch,structReg)){
        cout<<structMatch[1]<< " " <<structMatch[2]<<" " <<structMatch.position(0) + pos <<"\n";
        pos += structMatch.prefix().length() + structMatch.length();
        inp2 = structMatch.suffix();
        coutner++;
    }
     msg+="Структур: "+QString::number(coutner)+"\n";
     coutner=0;

     while(regex_search(inp3,arrayMatch,arrayReg)){
        cout<<arrayMatch[1]<< " " <<arrayMatch[2]<<" " <<arrayMatch.position(0) + pos <<"\n";
        pos += arrayMatch.prefix().length() + arrayMatch.length();
        inp3 = arrayMatch.suffix();
        coutner++;
    }
    while(regex_search(inp4,staticArruy,staticArrayReg)){
        cout<<staticArruy[1]<< " " <<staticArruy[2]<<" " <<staticArruy.position(0) + pos <<"\n";
        pos += staticArruy.prefix().length() + staticArruy.length();
        inp4 = staticArruy.suffix();
        coutner++;
    }
     msg+="Массивов: "+QString::number(coutner)+"\n";
     ui->InformationText->setText(msg);
}


void MainWindow::on_FileButton_clicked()
{
    auto fileName = QFileDialog::getOpenFileName(0);
    QFile file(fileName);

    if(file.open(QIODevice::ReadOnly)){
        QTextStream stream(&file);

        currentText.clear();

        string line;
        while(!stream.atEnd()){
            line=stream.readLine().toStdString();
            currentText += line;
            currentText += '\n';
        }
    }

    else{

       QMessageBox::warning(this,"File problem","The file was not opened!");
    }

    file.close();

    ui->MainText->setText(QString::fromStdString(currentText));
}
void MainWindow::on_SaveButton_clicked()
{
    currentText = ui->MainText->toPlainText().toStdString();
}


void MainWindow::on_VariablesButton_clicked()
{
    findVariables();
}
void MainWindow::on_FuncButton_clicked()
{
     findFunctions();
}
void MainWindow::on_ClassesButton_clicked()
{
    findClasses();
}
void MainWindow::on_LocalVarButton_clicked()
{
    findLocalVariables();
}
void MainWindow::on_BranchButton_clicked()
{
    findBranch();
}
void MainWindow::on_EditButton_clicked()
{
    findVariableChange();
}
void MainWindow::on_OperatorButton_clicked()
{
    findOverloads();
}
void MainWindow::on_ErrorsButton_clicked()
{
    findErrors();
}

