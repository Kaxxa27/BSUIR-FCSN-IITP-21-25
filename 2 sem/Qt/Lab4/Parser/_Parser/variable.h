#ifndef VARIABLE_H
#define VARIABLE_H

#include <QPair>
#include <iostream>

class Variable
{
private:
    std::vector<QPair<int,int>> coordinates;
    std::vector<std::string> name;
    std::vector<int> position;
    std::vector<std::string> types;

public:
    Variable(){};
    void setName(std::string);
    void setPosition(int);
    void setCoordinates(int,int);
    void setVariable(std::string newName, int pos,int row,int column);
    void setVariable(std::string type, std::string newName, int pos,int row,int column);
    int size();
    int getRow(int i);
    int getColumn(int i);
    int getPos(int i);

    std::string getName(int i);
    QPair<int,int> getCoord(int i);
    Variable (Variable &other);
    void deleteElem(int i);
    std::string getType(int i);
};
#endif // VARIABLE_H
