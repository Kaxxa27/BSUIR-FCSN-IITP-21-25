#include <windows.h>
#include <corecrt_wstdio.h>
#include <string>
#include <fstream>
#include "AirplaneDef.h"


HINSTANCE hInst;
HWND hMainWnd;

// Variables
HHOOK landingGear_hKeyboardHook = NULL;
BOOL isLandingGear = TRUE;
BOOL isAirplaneMoving = TRUE;
BOOL isCrashed = FALSE;

// Start position
INT airplaneX = 70;
INT airplaneY = 510;

// Start speed
INT airplaneSpeed = 0;

// Memory mapped file 
HANDLE hFile = NULL;
HANDLE hMapFile = NULL;
LPVOID pMappedData = NULL;
SIZE_T mappedDataSize = 0;

// Win Event Mutex
HANDLE hEventLogMutex = CreateMutex(NULL, FALSE, NULL);
// Log in txt Mutex
HANDLE hFileMutex = CreateMutex(NULL, FALSE, NULL);

// Func
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

VOID DrawAirplane(HDC hdc, INT x, INT y, INT width, INT height);
VOID UpdateAirplanePosition(INT deltaX, INT deltaY);
VOID SwitchLandingGear();
VOID StartAirplaneMovement();
VOID StopAirplaneMovement();

// File & Thread func  
// Function for recording keystrokes to a file in separate thread
DWORD WINAPI RecordKeyPressThread(LPVOID lpParam);
VOID CallRecordKeyPressInThread(CONST CHAR* actionString);
VOID InitializeMappingFile();
VOID UninitializeMappingFile();

// Hook func
LRESULT CALLBACK KeyboardHookProc(INT nCode, WPARAM wParam, LPARAM lParam);
VOID SetKeyboardHook();
VOID UnhookKeyboardHook();

// Registry func
VOID SaveCoordinatesToRegistry();
VOID LoadCoordinatesFromRegistry();

// EventLog func
VOID WriteToEventLog(std::wstring message);


INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, INT nCmdShow) {

    InitializeMappingFile();

    // При запуске приложения, загрузите координаты из реестра
    LoadCoordinatesFromRegistry();

	hInst = hInstance;

    WNDCLASSEX wcex = {
        sizeof(WNDCLASSEX),
        CS_HREDRAW | CS_VREDRAW,
        WndProc,
        0L,
        0L,
        GetModuleHandle(NULL),
        NULL, NULL, (HBRUSH)(COLOR_WINDOW + 1),
        NULL, L"AirplaneApp", NULL
    };


    if (!RegisterClassEx(&wcex))
    {
        MessageBox(NULL, L"Call to RegisterClassEx failed!", L"Error", MB_ICONERROR);
        return 1;
    }

    hMainWnd = CreateWindow(
        L"AirplaneApp",
        L"Airplane App",
        WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT,
        800, 600, NULL, NULL, hInstance, NULL);

    // Creating a label for a airplaneSpeed
    CreateWindow(L"STATIC", L"0 km/h", WS_CHILD | WS_VISIBLE, 10, 20, 80, 20, hMainWnd, (HMENU)ID_SPEED_LABEL, hInstance, NULL);

    if (!hMainWnd)
    {
        MessageBox(NULL, L"Call to CreateWindow failed!", L"Error", MB_ICONERROR);
        return 1;
    }

    ShowWindow(hMainWnd, nCmdShow);
    UpdateWindow(hMainWnd);

    // Setting the hook
    SetKeyboardHook();
    // Setting the timer
    SetTimer(hMainWnd, ID_MAIN_TIMER, 20, NULL);

    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    // Перед закрытием приложения, сохраните текущие координаты в реестре
    SaveCoordinatesToRegistry();


    // Delete the hook
    UnhookKeyboardHook();
    UninitializeMappingFile();



    return (int)msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) {
    switch (message){
    case WM_PAINT: 
    {
        PAINTSTRUCT ps;
        HDC hdc = BeginPaint(hWnd, &ps);

        RECT rc;
        GetClientRect(hWnd, &rc);

        // Setting the background color to light blue (RGB(153, 204, 255))
        HBRUSH hLightBlueBrush = CreateSolidBrush(RGB(153, 204, 255));
        FillRect(hdc, &rc, hLightBlueBrush);
        DeleteObject(hLightBlueBrush);

        // Rendering the ground in light green colot (RGB(102, 255, 102))
        HBRUSH hLightGreenBrush = CreateSolidBrush(RGB(102, 255, 102));
        RECT groundRect = { rc.left, rc.bottom - 50, rc.right, rc.bottom };
        FillRect(hdc, &groundRect, hLightGreenBrush);
        DeleteObject(hLightGreenBrush);

        // Main Background the same as the system
        //FillRect(hdc, &rc, (HBRUSH)(COLOR_WINDOW + 1));
    
        DrawAirplane(hdc, airplaneX, airplaneY, AIRPLANE_WIDTH, AIRPLANE_HEIGHT);

        EndPaint(hWnd, &ps);
    }
        break;

    case WM_KEYDOWN:
        switch (wParam) {
        case VK_SPACE:
            SwitchLandingGear();
            InvalidateRect(hWnd, NULL, TRUE);
            break;

        case VK_LEFT:
            if (airplaneSpeed > 0)
                airplaneSpeed--;
            // Stop airplane movement
            else StopAirplaneMovement();       
            break;

        case VK_RIGHT:
            if (airplaneSpeed < MAX_SPEED)
                airplaneSpeed++;
            // Start airplane movement
            StartAirplaneMovement();
            break;

        case VK_UP:
            UpdateAirplanePosition(0, -airplaneSpeed);
            InvalidateRect(hWnd, NULL, TRUE);
            break;

        case VK_DOWN:
            UpdateAirplanePosition(0, airplaneSpeed);
            InvalidateRect(hWnd, NULL, TRUE);
            break;
        }

        // Updating text in ID_SPEED_LABEL
        {
            HWND hSpeedLabel = GetDlgItem(hWnd, ID_SPEED_LABEL);
            if (hSpeedLabel != NULL) {
                WCHAR speedText[16];
                swprintf_s(speedText, L"%d km/h", airplaneSpeed * 80);
                SetWindowTextW(hSpeedLabel, speedText);
            }
        }
        break;

    case WM_TIMER:
        if (wParam == ID_MAIN_TIMER && isAirplaneMoving) {
            UpdateAirplanePosition(airplaneSpeed, 0);
            InvalidateRect(hWnd, NULL, TRUE);
        }
        break;

    case WM_DESTROY:
        KillTimer(hWnd, ID_MAIN_TIMER);
        PostQuitMessage(0);
        break;

    default:
        return DefWindowProc(hWnd, message, wParam, lParam);  
    }
    return 0;
}

VOID DrawAirplane(HDC hdc, INT x, INT y, INT width, INT height) {
    
    // Coordinates for body
    INT left = x;
    INT right = x + width;
    INT top = y;
    INT bottom = y + height;

    // Drawing an airplane body
    {
        HBRUSH hBrush = CreateSolidBrush(RGB(255, 165, 0));
        HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);

        Rectangle(hdc, left, top, right, bottom);

        SelectObject(hdc, hOldBrush);
        DeleteObject(hBrush);
    }
    
    // Drawing an airplane cabin
    {
        HBRUSH hBrush = CreateSolidBrush(RGB(0, 191, 255));
        HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);

        // Drawing a semicircle (right side)
        INT radius = height / 2;
        INT centerX = x + width;
        INT centerY = y + (height / 2);

        //Arc(hdc, right - radius, top, right + radius, bottom, right, bottom, right, top);
        Chord(hdc, right - radius, top, right + radius, bottom, right, bottom, right, top);
        //Pie(hdc, right - radius, top, right + radius, bottom, right, bottom, right, top);

        SelectObject(hdc, hOldBrush);
        DeleteObject(hBrush);
    }

    // Drawing an airplane tail 
    {
        HBRUSH hBrush = CreateSolidBrush(RGB(255, 165, 0));
        HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);

        INT offsetT1 = 20;
        INT offsetT2 = offsetT1 * 1.7;
        // Creating an array of tail points
        POINT points[] = {
            {left, bottom},
            {left - offsetT1, bottom - offsetT1},
            {left - offsetT2, top - offsetT2},
            {left, top},
            {left, bottom}
        };


        // Creating a contour for painting
        HRGN hRegion = CreatePolygonRgn(points, sizeof(points) / sizeof(points[0]), WINDING);

        // Fill in the region with color
        FillRgn(hdc, hRegion, hBrush);

        Polyline(hdc, points, sizeof(points) / sizeof(points[0]));


        // Delete the created region and brush
        DeleteObject(hRegion);
        SelectObject(hdc, hOldBrush);
        DeleteObject(hBrush);
    }

    // Drawing an airplane wing 
    {
        HBRUSH hBrush = CreateSolidBrush(RGB(255, 165, 0));
        HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);

        INT offsetH = width / 4;
        INT offsetV = height / 2;
        // Creating an array of wing points
        POINT points[] = {
            {right - offsetH, top + offsetV},
            {left + offsetH * 0.6, bottom + offsetV * 1.3},
            {left + offsetH * 1.4, top + offsetV},
        };

        // Creating a contour for painting
        HRGN hRegion = CreatePolygonRgn(points, sizeof(points) / sizeof(points[0]), WINDING);

        // Fill in the region with color
        FillRgn(hdc, hRegion, hBrush);

        Polyline(hdc, points, sizeof(points) / sizeof(points[0]));


        // Delete the created region and brush
        DeleteObject(hRegion);
        SelectObject(hdc, hOldBrush);
        DeleteObject(hBrush);
    }

    // Drawing an airplane windows 
    {
        HBRUSH hBrush = CreateSolidBrush(RGB(0, 191, 255));
        HBRUSH hOldBrush = (HBRUSH)SelectObject(hdc, hBrush);

        INT winCount = 4;
        INT offsetH = width / winCount;
        INT offsetV = height / 2;

        INT offset = offsetH / 4;
        for (size_t i = 0; i < winCount; i++)
        {
            Ellipse(hdc, left + i * offsetH + offset, top + offset, left + (i + 1) * offsetH - offset, top + offsetV);
        }
   
        SelectObject(hdc, hOldBrush);
        DeleteObject(hBrush);
    }
    

    if (isLandingGear) {
        INT offsetX = width / 10;
        INT offsetY = width / 10;
        Ellipse(hdc, left + offsetX, bottom, left + offsetX * 2, bottom + offsetY);
        Ellipse(hdc, right - offsetX * 2, bottom, right - offsetX, bottom + offsetY);
    }
}

VOID SwitchLandingGear() {
    isLandingGear = !isLandingGear;
}

VOID UpdateAirplanePosition(INT deltaX, INT deltaY) {
    airplaneX += deltaX;
    airplaneY += deltaY;

    RECT clientRect;
    GetClientRect(hMainWnd, &clientRect);

    // Horizontal
    if (airplaneX + 100 <= 0) {
        airplaneX = clientRect.right;
    }
    else if (airplaneX >= clientRect.right) {
        airplaneX = -100;
    }

    // Vertical
    if (airplaneY <= 0) {
        airplaneY = clientRect.top;
    }
    else if (airplaneY >= clientRect.bottom - (50 + AIRPLANE_HEIGHT)) {
        airplaneY = clientRect.bottom - (50 + AIRPLANE_HEIGHT);
        
        // Collision with the ground
        if(!isLandingGear && !isCrashed)
        {
            StopAirplaneMovement();
            isCrashed = true;
            airplaneSpeed = 0;
            CallRecordKeyPressInThread("Airplane crashed. WARNING!");
            WriteToEventLog(L"Airplane crashed. WARNING!");
            MessageBox(hMainWnd, L"You lost control!\nBOOM!", L"Catastrophe", MB_ICONERROR);  
            SendMessage(hMainWnd, WM_DESTROY, 0, 0);
        }
    }
}

VOID StartAirplaneMovement() {
    isAirplaneMoving = true;
}

VOID StopAirplaneMovement() {
    isAirplaneMoving = false;
}

// Function for processing the left hook
LRESULT CALLBACK KeyboardHookProc(INT nCode, WPARAM wParam, LPARAM lParam) {
    if (nCode >= 0) {
        if (wParam == WM_KEYDOWN) {
            KBDLLHOOKSTRUCT* pKeyInfo = (KBDLLHOOKSTRUCT*)lParam;

            switch (pKeyInfo->vkCode) {
            case VK_SPACE:
                // Depending on the state of the landing gear, we output a message to Debug
                // using ! because hook before WM 
                if (!isLandingGear) {
                    OutputDebugString(L"Landing Gear Deployed\n"); 
                    CallRecordKeyPressInThread("Landing Gear Deployed.");
                }
                else {
                    OutputDebugString(L"Landing Gear Retracted\n");
                    CallRecordKeyPressInThread("Landing Gear Retracted.");
                }
                break;
            case VK_LEFT:
                if (airplaneSpeed > 0)
                    CallRecordKeyPressInThread("Speed reduced.");
                else CallRecordKeyPressInThread("Speed equals 0.");
                break;
            case VK_RIGHT:
                if (airplaneSpeed < MAX_SPEED)
                    CallRecordKeyPressInThread("Speed has increased.");
                else CallRecordKeyPressInThread("Speed equals MAX_SPEED.");
                break;
            case VK_UP:
                CallRecordKeyPressInThread("Airplane climb.");
                break;
            case VK_DOWN:
                CallRecordKeyPressInThread("Airplane landing.");
                break;
            }
        }
    }

    return CallNextHookEx(landingGear_hKeyboardHook, nCode, wParam, lParam);
}

// Function for setting the left hook
VOID SetKeyboardHook() {
    landingGear_hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProc, NULL, 0);
    if (landingGear_hKeyboardHook == NULL) {
        MessageBox(NULL, L"Failed to set keyboard hook", L"Error", MB_ICONERROR);
    }
}

// Function for removing the left hook
VOID UnhookKeyboardHook() {
    if (landingGear_hKeyboardHook != NULL) {
        UnhookWindowsHookEx(landingGear_hKeyboardHook);
        landingGear_hKeyboardHook = NULL;
    }
}

DWORD WINAPI RecordKeyPressThread(LPVOID lpParam) {

    WaitForSingleObject(hFileMutex, INFINITE); // Заблокировать мьютекс файла

    std::string actionString = (CHAR*)(lpParam);

    if (pMappedData != NULL) {
        // Getting current time
        SYSTEMTIME currentTime;
        GetLocalTime(&currentTime);

        // Forming a strings with information about the time and the key
        std::string time = "Time: " + std::to_string(currentTime.wHour) + ":" +
            std::to_string(currentTime.wMinute) + ":" + std::to_string(currentTime.wSecond);

        std::string keyInfo = time + " => Action: " + actionString + "\n";

        // Copying the information to a memory-mapped file
        SIZE_T dataSize = keyInfo.size() * sizeof(CHAR);
        OutputDebugString(L" Size of log string ");
        OutputDebugString(std::to_wstring(dataSize).c_str());
        OutputDebugString(L" bytes\n");

        if (mappedDataSize + dataSize >= FILESIZE)
        {
            UninitializeMappingFile();
            InitializeMappingFile();
            mappedDataSize = 0;
        }
        memcpy((CHAR*)pMappedData + mappedDataSize, keyInfo.c_str(), dataSize);
        mappedDataSize += dataSize;
    }

    ReleaseMutex(hFileMutex); // Разблокировать мьютекс файла

    return 0;
}

// Func for call RecordKeyPress in separate thread
VOID CallRecordKeyPressInThread(CONST CHAR* actionString) {
    HANDLE hRecordThread = CreateThread(NULL, 0, RecordKeyPressThread, (VOID*)actionString, 0, NULL);

    if (hRecordThread == NULL) {
        MessageBox(NULL, L"CreateThread failed!", L"Error", MB_ICONERROR);
        return;
    }
    else {
        // Waiting thread
        WaitForSingleObject(hRecordThread, INFINITE);

        // Close thread handle 
        CloseHandle(hRecordThread);
    }
}

VOID InitializeMappingFile() {

    // CreateFile
    hFile = CreateFile(
        fileName,                     // Filename
        GENERIC_READ | GENERIC_WRITE, // Access mode (read and write)
        0,                            // No sharing
        NULL,                         // Default protection
        CREATE_ALWAYS,                // Create a new file or overwrite an existing
        FILE_ATTRIBUTE_NORMAL,        // File attributes (normal)
        NULL                          // Template for creating files
    );

    if (hFile == INVALID_HANDLE_VALUE) {
        MessageBox(NULL, L"CreateFile failed!", L"Error", MB_ICONERROR);
        return;
    }

    // CreateFileMapping
    hMapFile = CreateFileMapping(
        hFile,                       // File descriptor
        NULL,                        // Default protection
        PAGE_READWRITE,              // Display access mode (read and write)
        0,                           // Display file size (0 means the whole file)
        FILESIZE,                    // Display file size (the highest byte)
        NULL                         // Display file name
    );

    if (hMapFile == NULL) {
        MessageBox(NULL, L"CreateFileMapping failed!", L"Error", MB_ICONERROR);
        CloseHandle(hFile);
        return;
    }

    // Display the mapped file in memory
    pMappedData = MapViewOfFile(hMapFile, FILE_MAP_ALL_ACCESS, 0, 0, FILESIZE);
    if (pMappedData == NULL) {
        MessageBox(NULL, L"MapViewOfFile failed!", L"Error", MB_ICONERROR);
        CloseHandle(hMapFile);
        CloseHandle(hFile);
        return;
    }
}

VOID UninitializeMappingFile() {
 
    if (pMappedData != NULL) {
        UnmapViewOfFile(pMappedData);
        pMappedData = NULL;
    }

    if (hMapFile != NULL) {
        CloseHandle(hMapFile);
        hMapFile = NULL;
    }

    if (hFile != INVALID_HANDLE_VALUE) {
        CloseHandle(hFile);
        hFile = INVALID_HANDLE_VALUE;
    }
}

// Function for writing coordinates to the registry
VOID SaveCoordinatesToRegistry() {
    HKEY hKey;
    if (RegCreateKey(HKEY_CURRENT_USER, subKey, &hKey) == ERROR_SUCCESS) {
        RegSetValueEx(hKey, valueNameX, 0, REG_DWORD, (const BYTE*)&airplaneX, sizeof(DWORD));
        RegSetValueEx(hKey, valueNameY, 0, REG_DWORD, (const BYTE*)&airplaneY, sizeof(DWORD));
        RegSetValueEx(hKey, valueNameSpeed, 0, REG_DWORD, (const BYTE*)&airplaneSpeed, sizeof(DWORD));
        RegCloseKey(hKey);
    }
}

// Function for reading coordinates from the registry
VOID LoadCoordinatesFromRegistry() {
    HKEY hKey;
    if (RegOpenKey(HKEY_CURRENT_USER, subKey, &hKey) == ERROR_SUCCESS) {
        DWORD x, y, speed;
        DWORD dataSize = sizeof(DWORD);

        if (RegQueryValueEx(hKey, valueNameX, 0, NULL, (LPBYTE)&x, &dataSize) == ERROR_SUCCESS &&
            RegQueryValueEx(hKey, valueNameY, 0, NULL, (LPBYTE)&y, &dataSize) == ERROR_SUCCESS &&
            RegQueryValueEx(hKey, valueNameSpeed, 0, NULL, (LPBYTE)&speed, &dataSize) == ERROR_SUCCESS)
        {
            airplaneX = x;
            airplaneY = y;
            airplaneSpeed = speed;
        }

        RegCloseKey(hKey);
    }
}

//function for writing logs to the windows event log
VOID WriteToEventLog(std::wstring message) {

    WaitForSingleObject(hEventLogMutex, INFINITE); // Заблокировать мьютекс журнала событий

    HANDLE hEventLog = RegisterEventSource(NULL, L"AirplaneСrash");

    if (hEventLog) {

        // Getting current time
        SYSTEMTIME currentTime;
        GetLocalTime(&currentTime);

        // Forming a strings with information about the time and the key
        std::wstring time = L"Time: " + std::to_wstring(currentTime.wHour) + L":" +
            std::to_wstring(currentTime.wMinute) + L":" + std::to_wstring(currentTime.wSecond);

        std::wstring logMessage = time + L" => Action: " + message;

        const wchar_t* messageStrings[1];
        messageStrings[0] = logMessage.c_str();

        ReportEvent(hEventLog, EVENTLOG_INFORMATION_TYPE, 0, 0, NULL, 1, 0, messageStrings, NULL);

        DeregisterEventSource(hEventLog);
    }

    ReleaseMutex(hEventLogMutex); // Разблокировать мьютекс журнала событий
}
