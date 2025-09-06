#ifndef DATE_H
#define DATE_H

#include <QString>
#include <QDate>

class Date
{
    int Day,Month,Year;
    int MaxDay;

public:

    Date();
    Date(int day,int month,int year);

    Date NextDay();
    Date PreviousDay();

    int DaysTillYourBithday(Date BithdayDate);
    int Duration(Date Current_Date);
    int DurationBithday(Date Current_Date, Date Bithday);
    bool IsLeap(int Year);
    short WeekNumber();

    int FindMaxDay();
    int FindMaxDay(int i);
    int FindMinDay();

    QString GetDate_str();
};

#endif // DATE_H
