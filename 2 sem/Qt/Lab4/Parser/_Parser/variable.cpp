#include "variable.h"

int Variable::getRow(int i){
    return coordinates[i].first;
}

int Variable::getColumn(int i){
    return coordinates[i].second;
}

std::string Variable::getName(int i){
    return name[i];
}

void Variable::setCoordinates(int row, int column){
    QPair<int,int> coord(row,column);
    coordinates.push_back(coord);
}

void Variable::setName(std::string newName){
    name.push_back(newName);
}

void Variable::setPosition(int pos ){
    position.push_back(pos);
}


void Variable::setVariable(std::string newName, int pos, int row, int column){
    QPair<int,int> coord(row,column);
    coordinates.push_back(coord);
    name.push_back(newName);
    position.push_back(pos);
}

int Variable::size(){
    return coordinates.size();
}

int Variable::getPos(int i){
    return position[i];
}

void Variable::deleteElem(int i){
    this->name.erase(name.begin()+i);
    this->position.erase(position.begin()+i);
    this->coordinates.erase(coordinates.begin()+i);
}

Variable::Variable(Variable &other){
    this->name=other.name;
    this->position=other.position;
    this->coordinates=other.coordinates;
}

void Variable::setVariable(std::string type, std::string newName, int pos, int row, int column){
    QPair<int,int> coord(row,column);
    coordinates.push_back(coord);
    name.push_back(newName);
    position.push_back(pos);
    types.push_back(type);
}
std::string Variable::getType(int i){
    return types[i];
}
