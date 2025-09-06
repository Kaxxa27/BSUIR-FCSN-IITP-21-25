#pragma once

// Identifiers of interface elements 
#define ID_MAIN_TIMER 1
#define ID_SPEED_LABEL 2

// Airplane Constants
CONST INT MAX_SPEED = 15;
CONST INT AIRPLANE_WIDTH = 150;
CONST INT AIRPLANE_HEIGHT = 50;

// FileMapping Constants
CONST WCHAR* fileName = L"flight_recorder.txt";
CONST INT FILESIZE = 1048576; // 32000 click = 1 mb

// Registry Constants
CONST LPCWSTR subKey = L"Software\\AirplaneAppWinApi";
CONST LPCWSTR valueNameX = L"AirplaneX";
CONST LPCWSTR valueNameY = L"AirplaneY";
CONST LPCWSTR valueNameSpeed = L"AirplaneSpeed";