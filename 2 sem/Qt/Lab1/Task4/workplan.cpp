#include "workplan.h"

WorkPlan::WorkPlan()
{

}

void WorkPlan::AddToStruct(QString FIO, QString ProjectName, QString ProjectTask,QString D,
                           QString S,QString E,QDate Deadline, QDate StartTime, QDate EndTime)
{//Добавление в структуру информации

    Worker Person;

    Person.FIO = FIO;
    Person.ProjectName = ProjectName;
    Person.ProjectTask = ProjectTask;
    Person.D = D;
    Person.S = S;
    Person.E = E;
    Person.Deadline = Deadline;
    Person.StartTime = StartTime;
    Person.EndTime = EndTime;

    Workers.push_back(Person);
}

QString WorkPlan::ShowInfo()
{
    QString WorkersInfo;
    for (int i = 0; i < Workers.size(); ++i){
        WorkersInfo += QString::number(i + 1) + ") " + Workers[i].FIO + "\n" + Workers[i].ProjectName + "\n" +
                Workers[i].ProjectTask + "\n" + Workers[i].D + "\n" + Workers[i].S + "\n" +
                Workers[i].E + "\n";
    }
    return WorkersInfo;
}

QString WorkPlan::AddToComboBoxWorkers(int i)
{
    return Workers[i].FIO;
}

int WorkPlan::WorkerSize()
{
    return Workers.size();
}

void WorkPlan::ClearMyVector()
{
    Workers.clear();
}

int WorkPlan::SizeOfMyVector()
{
    int Size = Workers.size();
    return Size;
}

void WorkPlan::DeleteWorker(int num)
{
    Workers.removeAt(num);
}

QString WorkPlan::AddNewInfoToFile(int i)
{
    QString NewInfo;
    NewInfo += Workers[i].FIO + "\n" +  Workers[i].ProjectName + "\n" + Workers[i].ProjectTask + "\n" +
            Workers[i].D + "\n" + Workers[i].S + "\n" + Workers[i].E + "\n";
    return NewInfo;
}

bool WorkPlan::CorrectDate(QString date)
{
        std::regex rx("((0[1-9]|[12][0-9]|3[01])[- /.]"
                     "(0[1-9]|1[012])[- /.]"
                     "([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3}))");


        if (date.length() < 10 ||  date[2] != '.'  || date[5] != '.')
               return false;

        if (!std::regex_match(date.toStdString().c_str(), rx))
            return false;

        QString sday = "";
        for (int i = 0; i < 2; i++) {
            QString tmp = date[i];
            sday += tmp;
        }

        QString smonth = "";
        for (int i = 3; i < 5; i++) {
            QString tmp = date[i];
            smonth += tmp;
        }

        QString syear = "";
        for (int i = 6; i < 10; i++) {
            QString tmp = date[i];
            syear += tmp;
        }

        int iday = sday.toInt();
        int imonth = smonth.toInt();
        int iyear = syear.toInt();

        int daysInMonth[12] = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        daysInMonth[1] = IsLeap(iyear) ? 29 : 28;

        if (iday > daysInMonth[imonth - 1])
            return false;

        return true;

}
bool WorkPlan::IsLeap(int Year)//високосный год
{
    return ((Year % 4 == 0 && Year % 100 != 0) || Year % 400 == 0);
}

QString WorkPlan::SortEndDate()
{
   QVector<Worker> EndDateArr;

   for(int i = 0; i < WorkerSize(); i++ ){
       EndDateArr.push_back(Workers[i]);
   }

//   Days
      for(int i = 0; i < WorkerSize() - 1; i++){
          for(int j = 0; j < WorkerSize() - i - 1; j++){

              QList parts = EndDateArr[j].E.split(".");
              QList parts2 = EndDateArr[j + 1].E.split(".");

              if(parts[0] > parts2[0]){
                   EndDateArr.swapItemsAt(j,j + 1);
              }
           }
        }
//   Month
         for(int i = 0; i < WorkerSize() - 1; i++){
             for(int j = 0; j < WorkerSize() - i - 1; j++){

                 QList parts = EndDateArr[j].E.split(".");
                 QList parts2 = EndDateArr[j + 1].E.split(".");

                 if(parts[1] > parts2[1]){
                      EndDateArr.swapItemsAt(j,j + 1);
                 }
              }
           }
//Year
   for(int i = 0; i < WorkerSize() - 1; i++){
       for(int j = 0; j < WorkerSize() - i - 1; j++){

           QList parts = EndDateArr[j].E.split(".");
           QList parts2 = EndDateArr[j + 1].E.split(".");

           if(parts[2] > parts2[2]){
                EndDateArr.swapItemsAt(j,j + 1);
           }
        }
     }

   QString SortSuccses;
   for(int i = 0; i < EndDateArr.size();i++){
       SortSuccses += EndDateArr[i].ProjectName + ":  " + EndDateArr[i].E  + "\n";
   }
   return SortSuccses;
}



QString WorkPlan::ListSumTime()
{
    QVector<Worker> SumDateArr;
    QVector<int> SumNumber;

    int SumDays;

    for(int i = 0; i < WorkerSize(); i++ ){
        SumDateArr.push_back(Workers[i]);
    }

 //   Days
       for(int i = 0; i < WorkerSize(); i++){

          QList partsE = SumDateArr[i].E.split(".");
          QList partsS = SumDateArr[i].S.split(".");

          int PDays = (partsE[0].toInt() - partsS[0].toInt());
          int PMonth = (partsE[1].toInt() - partsS[1].toInt()) * 31;
          int PYear = (partsE[2].toInt() - partsS[2].toInt()) * 365;

          SumDays = PDays + PMonth + PYear;


          SumNumber.push_back(SumDays);
        }

    QString SumDaysStr;
    for(int i = 0; i < SumNumber.size(); i++){
        SumDaysStr += SumDateArr[i].FIO + ": " + QString::number(SumNumber[i])  + "\n";
    }
    return SumDaysStr;
}
