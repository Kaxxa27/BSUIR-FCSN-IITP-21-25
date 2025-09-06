#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <ctime>
#include <chrono>
#include <thread>
#include <Windows.h>
#include <vector>
using namespace std;

const string TIME_TO_INSERT = "20:00:00";
const wstring TEXT_TO_INSERT = L"Кахновский";
const vector<bool> COLUMNS = { 1, 0, 1, 1, 0 };

const int CHECK_TIME_DELAY_MS = 20;
const int MOVE_RIGHT_DELAY_MS = 100;

void setClipboardText(const std::wstring& text) {
    if (!OpenClipboard(nullptr)) {
        std::cout << "Не удалось открыть буфер обмена.\n";
        return;
    }

    if (!EmptyClipboard()) {
        CloseClipboard();
        std::cout << "Не удалось очистить буфер обмена.\n";
        return;
    }

    int textLengthBytes = (text.length() + 1) * sizeof(wchar_t);

    HGLOBAL hClipboardData = GlobalAlloc(GMEM_MOVEABLE, textLengthBytes);
    if (!hClipboardData) {
        CloseClipboard();
        std::cout << "Не удалось выделить память для буфера обмена.\n";
        return;
    }

    wchar_t* pClipboardData = static_cast<wchar_t*>(GlobalLock(hClipboardData));
    if (!pClipboardData) {
        CloseClipboard();
        GlobalFree(hClipboardData);
        std::cout << "Не удалось заблокировать память для буфера обмена.\n";
        return;
    }

    wcscpy_s(pClipboardData, text.length() + 1, text.c_str());

    GlobalUnlock(hClipboardData);

    if (SetClipboardData(CF_UNICODETEXT, hClipboardData) == nullptr) {
        std::cout << "Не удалось установить текст в буфер обмена.\n";
        CloseClipboard();
        GlobalFree(hClipboardData);
        return;
    }

    std::cout << "Текст успешно скопирован в буфер обмена.\n";

    CloseClipboard();
}

void parseTime(const string& timeString, int& hour, int& minute, int& second) {
    size_t pos1 = timeString.find(':');
    size_t pos2 = timeString.find(':', pos1 + 1);

    hour = stoi(timeString.substr(0, pos1));
    minute = stoi(timeString.substr(pos1 + 1, pos2 - pos1 - 1));
    second = stoi(timeString.substr(pos2 + 1));
}

void pressPasteKey() {
    keybd_event(VK_CONTROL, 0, 0, 0);
    keybd_event('V', 0, 0, 0);
    keybd_event('V', 0, KEYEVENTF_KEYUP, 0);
    keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
}

void pressRightArrowKey() {
    keybd_event(VK_RIGHT, 0, 0, 0);
    keybd_event(VK_RIGHT, 0, KEYEVENTF_KEYUP, 0);
}

void fill() {
    for (const auto& column : COLUMNS) {
        if (column) {
            pressPasteKey();
        }

        this_thread::sleep_for(chrono::milliseconds(MOVE_RIGHT_DELAY_MS));

        pressRightArrowKey();
    }
}

int main() {
    SetConsoleOutputCP(1251);

    setClipboardText(TEXT_TO_INSERT);

    int copy_hour, copy_minute, copy_second;
    parseTime(TIME_TO_INSERT, copy_hour, copy_minute, copy_second);

    while (true) {
        auto now = chrono::system_clock::to_time_t(chrono::system_clock::now());
        tm* current_time = localtime(&now);

        if (current_time->tm_hour >= copy_hour &&
            current_time->tm_min >= copy_minute &&
            current_time->tm_sec >= copy_second) {

            fill();
            break;
        }

        this_thread::sleep_for(chrono::milliseconds(CHECK_TIME_DELAY_MS));
    }

    return 0;
}
