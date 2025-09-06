#define _SILENCE_EXPERIMENTAL_FILESYSTEM_DEPRECATION_WARNING

#include <iostream>
#include <string>
#include <fstream>
#include <Windows.h>
#include <filesystem>
#include <experimental/filesystem>
// вариант №7.1

using namespace std; 


class Human {
	string name;//фио
	string country;//страна
	string name_team;//название команды
	int nummember;// номер игрока
	int weight;// вес
	int age;// возраст
	int height;// рост

public:
	void SetName(string newname) {
		name = newname;
	}
	void SetCountry(string newcountry) {
		country = newcountry;
	}
	void SetNameTeam(string newname_team) {
		name_team = newname_team;
	}
	void SetNumMember(int newnummember) {
		nummember = newnummember;
	}
	void SetWeight(int newweight) {
		weight = newweight;
	}
	void SetAge(int newage) {
		age = newage;
	}
	void SetHeight(int newheight) {
		height = newheight;
	}

	string GetName() {
		return name;
	}
	string GetCountry() {
		return country;
	}
	string GetNameTeam() {
		return name_team;
	}
	int GetNumMember() {
		return nummember;
	}
	int GetAge() {
		return age;
	}
	int GetWeight() {
		return weight;
	}
	int GetHeight() {
		return height;
	}

};


int checknum();
string checkanswer();
int checkchoose();

void AddNewStructInFile(Human* arr, int& k, int add);//work with files
void DelStructInFile(Human* arr, int k, int number);
void EditStructInFile(Human* arr, int& k, int choose, int field);
void savedata(Human* arr, int& k);//save s reboot

Human* createarr(fstream& fs, int& k);
void prosmotr(Human* arr, int i);
Human* addstruct(Human* arr, int& k);
Human* deletestruct(Human* arr, int& k);
void EditArrData(Human* arr, int& k);
void sort(Human* arr, int& k);


int main() {

	setlocale(LC_CTYPE, "rus");

	string path = "MyData3.txt";
	fstream fs;
	fs.open(path, fstream::in | fstream::out | fstream::app);
	if (!fs.is_open()) {
		cout << ("Файл не удалось открыть!") << endl;
	}

	cout << endl;
	int k = 0;
	fs >> k;

	Human* arr = (Human*)malloc(k * sizeof(Human));

	arr = createarr(fs, k);//заполнение массива

	while (true) {
		int vb;
		cout << ("Программа для хранения и редактирования информации о участниках соревнования.\n\n");
		cout << ("\t\tМеню программы: \n");
		cout << ("\tПоказать список участников (1).\n");
		cout << ("\tДобавить нового участника (2).\n");
		cout << ("\tУдалить участника (3).\n");
		cout << ("\tИзменить данные участника (4).\n");
		cout << ("\tВывести участником старше 20 лет(5).\n");
		cout << ("\tСохранение с reboot изначальных данных. (6).\n");
		cout << ("\tВыход из программы. (7).\n\n");

		cout << ("Ваш ответ: ");
		vb = checkchoose();
		cout << endl;

		if (vb == 1) {
			if (k == 0) {
				cout << ("Список пуст!") << endl << endl;
			}
			else {
				for (int i = 0; i < k; i++) {
					prosmotr(arr, i);
				}
			}
		}
		else if (vb == 2) {
			arr = addstruct(arr, k);
		}
		else if (vb == 3) {
			if (k == 0) {
				cout << ("Список пуст! Вы не можете удалить элементы.\n\n");
			}
			else {
				arr = deletestruct(arr, k);
			}
		}
		else if (vb == 4) {
			if (k == 0) {
				cout << ("Список пуст! Вы не можете изменять элементы.\n\n");
			}
			else {
				EditArrData(arr, k);
			}
		}
		else if (vb == 5) {
			if (k == 0) {
				cout << ("Список пуст!\n\n");
			}
			else {
				sort(arr, k);
			}
		}
		else if (vb == 6) {
			savedata(arr, k);
		}
		else if (vb == 7) {
			cout << ("Программа завершена! Хорошего дня.\n");
			break;
		}
	}


	return 0;
}

int checknum() {

	int n;
	while (true) {
		cin >> n;
		if (cin.fail() || cin.peek() != '\n') {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}

	return n;
}
string checkanswer() {
	string answer;
	bool pr = true;
	while (pr) {
		pr = false;
		getline(cin, answer, '\n');
		for (int i = 0; i < size(answer); i++) {
			if (!((answer[i] >= 'a' && answer[i] <= 'z') || (answer[i] >= 'A' && answer[i] <= 'Z'))) {
				std::cout << "Неверное значение. Повторите ввод.\n";
				pr = true;
				break;
			}
		}
	}
	return  answer;
}
int checkchoose() {
	int  n;
	while (true) {
		cin >> n;
		if (n != 1 && n != 2 && n != 3 && n != 4 && n != 5 && n != 6 && n != 7) {
			cin.clear();
			cin.ignore(10000, '\n');
			cout << ("Неверное значение. Повторите ввод.\n");
		}
		else {
			break;
		}
	}
	return n;
}

Human* createarr(fstream& fs, int& k) {
	string name;
	string name_team;
	string country;
	int nummember, age, weight, height;

	Human* arr2 = new Human[k];
	for (int i = 0; i < k; i++) {
	
		Human h;
		fs >> country; h.SetCountry(country);
		fs >> name_team; h.SetNameTeam(name_team);
		fs >> name; h.SetName(name);
		fs >> nummember; h.SetNumMember(nummember);
		fs >> age; h.SetAge(age);
		fs >> weight; h.SetWeight(weight);
		fs >> height; h.SetHeight(height);
		arr2[i] = h;
	}
	return arr2;
}
void prosmotr(Human* arr, int i) {//просмотр данных

	cout << ("_______________________________") << endl;
	cout << ("Наименование страны: ") << arr[i].GetCountry() << endl;
	cout << ("Название команды: ") << arr[i].GetNameTeam() << endl;
	cout << ("Фамилия участника: ") << arr[i].GetName() << endl;
	cout << ("Игровой номер: ") << arr[i].GetNumMember() << endl;
	cout << ("Возраст участника: ") << arr[i].GetAge() << endl;
	cout << ("Вес участника: ") << arr[i].GetWeight() << endl;
	cout << ("Рост участника: ") << arr[i].GetHeight() << endl;
	cout << ("_______________________________") << endl << endl;
}

Human* addstruct(Human* arr, int& k) {
	int add;
	cout << ("Введите кол-во участников, которых нужно добавить: \n");
	add = checknum();
	cin.ignore(1000, '\n');
	Human* arr2 = new Human[k + add];
	for (int i = 0; i < k; i++) {
		arr2[i] = arr[i];
	}

	for (int i = k; i < k + add; i++) {
		cout << ("Введите данные ") << i + 1 << (" участника:") << endl;
		cout << ("Название страны: ");
		
		arr2[i].SetCountry(checkanswer());
		cout << ("Название команды: ");
		arr2[i].SetNameTeam(checkanswer());
		cout << ("Фамилия участника: ");
		arr2[i].SetName(checkanswer());
		cout << ("Игровой номер: ");
		arr2[i].SetNumMember(checknum());
		cout << ("Возраст участника: ");
		arr2[i].SetAge(checknum());
		cout << "Рост участника: ";
		arr2[i].SetHeight(checknum());
		cout << "Вес участника: ";
		arr2[i].SetWeight(checknum());
		cin.ignore();
		cout << endl;
	}

	AddNewStructInFile(arr2, k, add);

	k += add;
	delete[] arr;
	return arr2;
}
void AddNewStructInFile(Human* arr, int& k, int add) {
	fstream fs;
	fs.open("NewData.txt");
	fs << k + add;//кол-во участников 
	int sum_bytes = 2;//char and '\n' elements
	for (int i = 0; i < k; i++) {//Кол-во всех байтов
		sum_bytes += size(arr[i].GetCountry()) + 1;
		sum_bytes += size(arr[i].GetNameTeam()) + 1;
		sum_bytes += size(arr[i].GetName()) + 1;
		sum_bytes += size(to_string(arr[i].GetNumMember())) + 1;
		sum_bytes += size(to_string(arr[i].GetAge())) + 1;
		sum_bytes += size(to_string(arr[i].GetHeight())) + 1;
		sum_bytes += size(to_string(arr[i].GetWeight())) + 1;
		sum_bytes += 7;
	}
	
	fs.seekg(sum_bytes);//точка (end) для fs in txt
	
	for (int j = k; j < k + add; j++) {
		
		fs << '\n';
		fs << arr[j].GetCountry();
		fs << '\n';
		fs << arr[j].GetNameTeam();
		fs << '\n';
		fs << arr[j].GetName();
		fs << '\n';
		fs << arr[j].GetNumMember();
		fs << '\n';
		fs << arr[j].GetAge();
		fs << '\n';
		fs << arr[j].GetHeight();
		fs << '\n';
		fs << arr[j].GetWeight();
		
	}
}
Human* deletestruct(Human* arr, int& k) {

	cout << ("Введите номер участника  для удаления из списка: ");
	int number = checknum();

	Human* arr2 = new Human[k - 1];
	int count = 0;
	for (int i = 0, j = 0; j < k - 1; i++, j++) {
		if (i != number - 1)
			arr2[j] = arr[i];
		else j--;
	}

	DelStructInFile(arr, k, number);

	k -= 1;
	delete[] arr;
	return arr2;

}
void DelStructInFile(Human* arr, int k, int number) {
	fstream fs;
	fs.open("NewData.txt");
	fs << k - 1;
	int sum_bytes = 2;
	for (int i = 0; i < number - 1; i++) {
		sum_bytes += size(arr[i].GetCountry()) + 1;
		sum_bytes += size(arr[i].GetNameTeam()) + 1;
		sum_bytes += size(arr[i].GetName()) + 1;
		sum_bytes += size(to_string(arr[i].GetNumMember())) + 1;
		sum_bytes += size(to_string(arr[i].GetAge())) + 1;
		sum_bytes += size(to_string(arr[i].GetHeight())) + 1;
		sum_bytes += size(to_string(arr[i].GetWeight())) + 1;
		sum_bytes += 1;
	}
	sum_bytes--;

	experimental::filesystem::resize_file("NewData.txt", sum_bytes);
	fs.seekg(sum_bytes);
	for (int j = number; j < k; j++) {
		fs << '\n';
		fs << arr[j].GetCountry();
		fs << '\n';
		fs << arr[j].GetNameTeam();
		fs << '\n';
		fs << arr[j].GetName();
		fs << '\n';
		fs << arr[j].GetNumMember();
		fs << '\n';
		fs << arr[j].GetAge();
		fs << '\n';
		fs << arr[j].GetHeight();
		fs << '\n';
		fs << arr[j].GetWeight();
	}
}
void EditArrData(Human* arr, int& k) {
	cout << ("Введите номер участника, у которого вы хотите что-то изменить: ");
	int choose;
	int field;
	while (true) {
		cin >> choose;
		if (cin.fail() || cin.peek() != '\n' || choose < 1 || choose > k) {
			std::cin.clear();
			std::cin.ignore(10000, '\n');
			std::cout << "incorrect data, try again\n";
		}
		else break;
	}
	choose--;//подбор под arr
	cout << ("Введите цифру, чтобы вы хотели изменить.\n");
	cout << ("1.Название страны.\n");
	cout << ("2.Название команды.\n");
	cout << ("3.Фамилию участника.\n");
	cout << ("4.Номер игрока.\n");
	cout << ("5.Возраст участника.\n");
	cout << ("6.Рост участника.\n");
	cout << ("7.Вес участника.\n");


	field = checkchoose();

	cin.ignore(10000, '\n');
	cout << ("Введите новые данные: ");
	switch (field) {
	case(1): {
		arr[choose].SetCountry(checkanswer());
		break;
	}
	case(2): {
		arr[choose].SetNameTeam(checkanswer());
		break;
	}
	case(3): {
		arr[choose].SetName(checkanswer());
		break;
	}
	case(4): {
		arr[choose].SetNumMember(checknum());
		break;
	}
	case(5): {
		arr[choose].SetAge(checknum());
		break;
	}
	case(6): {
		arr[choose].SetHeight(checknum());
		break;
	}
	case(7): {
		arr[choose].SetWeight(checknum());
		break;
	}
	}
	EditStructInFile(arr, k, choose, field);
}
void EditStructInFile(Human* arr, int& k, int choose, int field) {
	fstream fs;
	fs.open("NewData.txt");
	int sum_bytes = 2;
	for (int i = 0; i < choose; i++) {
		sum_bytes += size(arr[i].GetCountry()) + 1;
		sum_bytes += size(arr[i].GetNameTeam()) + 1;
		sum_bytes += size(arr[i].GetName()) + 1;
		sum_bytes += size(to_string(arr[i].GetNumMember())) + 1;
		sum_bytes += size(to_string(arr[i].GetAge())) + 1;
		sum_bytes += size(to_string(arr[i].GetHeight())) + 1;
		sum_bytes += size(to_string(arr[i].GetWeight())) + 1;
		sum_bytes += 8;
	}
	sum_bytes++;
	int place_txt = 1;
	for (;;) {
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(arr[choose].GetCountry()) + 1;
		place_txt++;
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(arr[choose].GetNameTeam()) + 1;
		place_txt++;
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(arr[choose].GetName()) + 1;
		place_txt++;
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(to_string(arr[choose].GetNumMember())) + 1;
		place_txt++;
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(to_string(arr[choose].GetAge())) + 1;
		place_txt++;
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(to_string(arr[choose].GetHeight())) + 1;
		place_txt++;
		if (place_txt == field) {
			break;
		}
		sum_bytes += size(to_string(arr[choose].GetWeight())) + 1;

	}
	experimental::filesystem::resize_file("NewData.txt", sum_bytes);
	fs.seekg(sum_bytes);
	fs << '\n';
	if (place_txt <= 1) {
		fs << arr[choose].GetCountry();
		fs << '\n';
	}
	if (place_txt <= 2) {
		fs << arr[choose].GetNameTeam();
		fs << '\n';
	}
	if (place_txt <= 3) {
		fs << arr[choose].GetName();
		fs << '\n';
	}
	if (place_txt <= 4) {
		fs << arr[choose].GetNumMember();
		fs << '\n';
	}
	if (place_txt <= 5) {
		fs << arr[choose].GetAge();
		fs << '\n';
	}
	if (place_txt <= 6) {
		fs << arr[choose].GetHeight();
		fs << '\n';
	}
	if (place_txt <= 7) {
		fs << arr[choose].GetWeight();
		fs << '\n';
	}


	for (int j = choose; j < k; j++) {
		if (j + 1 < k) {
			fs << arr[j + 1].GetCountry();
			fs << '\n';
			fs << arr[j + 1].GetNameTeam();
			fs << '\n';
			fs << arr[j + 1].GetName();
			fs << '\n';
			fs << arr[j + 1].GetNumMember();
			fs << '\n';
			fs << arr[j + 1].GetAge();
			fs << '\n';
			fs << arr[j + 1].GetHeight();
			fs << '\n';
			fs << arr[j + 1].GetWeight();
			fs << '\n';
		}
	}
}
void sort(Human* arr, int& k) {
	for (int i = 0; i < k; i++) {
		if (arr[i].GetAge() > 20) {
			prosmotr(arr, i);
		}
	}
}
void savedata(Human* arr, int& k) {
	string path2 = "NewData.txt";
	ofstream fs;
	fs.open(path2);
	fs << k;
	for (int i = 0; i < k; i++) {
		fs << "\n";
		fs  << arr[i].GetCountry();
		fs << "\n";
		fs  << arr[i].GetNameTeam();
		fs << "\n";
		fs << arr[i].GetName();
		fs << "\n";
		fs << arr[i].GetNumMember();
		fs << "\n";
		fs << arr[i].GetAge();
		fs << "\n";
		fs  << arr[i].GetHeight();
		fs << "\n";
		fs  << arr[i].GetWeight();
	
		
	}
	cout << ("Файл успешно сохранен! ") << endl << endl;
}
