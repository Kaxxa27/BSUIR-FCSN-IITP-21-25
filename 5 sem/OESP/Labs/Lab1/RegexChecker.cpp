#include <windows.h>
#include <regex>
#include "RegexCheckerDef.h"

// Global variables
HWND hMainWindow;

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

void CreateUIElements(HINSTANCE hInstance);

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    // Initializing the MainWindow
    WNDCLASSEX wcex;
    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.cbClsExtra = 0;
    wcex.cbWndExtra = 0;
    wcex.hInstance = hInstance;
    wcex.hIcon = LoadIcon(hInstance, IDI_APPLICATION);
    wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszMenuName = NULL;
    wcex.lpszClassName = L"SimpleApp";
    wcex.hIconSm = LoadIcon(wcex.hInstance, IDI_APPLICATION);

    if (!RegisterClassEx(&wcex))
    {
        MessageBox(NULL, L"Call to RegisterClassEx failed!", L"Error", MB_ICONERROR);
        return 1;
    }

    // Creating MainWindow
    hMainWindow = CreateWindow(L"SimpleApp", L"Regex Checker", WS_OVERLAPPEDWINDOW, CW_USEDEFAULT, CW_USEDEFAULT, 400, 200, NULL, NULL, hInstance, NULL);

    if (!hMainWindow)
    {
        MessageBox(NULL, L"Call to CreateWindow failed!", L"Error", MB_ICONERROR);
        return 1;
    }

    // Creating interface elements (fields and buttons)
    CreateUIElements(hInstance);

    // Showing MainWindow
    ShowWindow(hMainWindow, nCmdShow);
    UpdateWindow(hMainWindow);

    // The main message loop
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}

void CreateUIElements(HINSTANCE hInstance)
{

    // Creating a label for a regular expression
    CreateWindow(L"STATIC", L"Regex: ", WS_CHILD | WS_VISIBLE, 10, 20, 50, 20, hMainWindow, (HMENU)ID_REGEX_LABEL, hInstance, NULL);

    // Creating a editfield for a regular expression
    CreateWindow(L"EDIT", L"", WS_CHILD | WS_VISIBLE | WS_BORDER , 65, 20, 200, 20, hMainWindow, (HMENU)ID_REGEX_EDIT, hInstance, NULL);

    // Creating a label for a text
    CreateWindow(L"STATIC", L"Text: ", WS_CHILD | WS_VISIBLE, 10, 50, 50, 20, hMainWindow, (HMENU)ID_REGEX_LABEL, hInstance, NULL);

    // Creating a editfield for a text
    CreateWindow(L"EDIT", L"", WS_CHILD | WS_VISIBLE | WS_BORDER | ES_AUTOHSCROLL, 65, 50, 200, 20, hMainWindow, (HMENU)ID_TEXT_EDIT, hInstance, NULL);

    // Creating button
    CreateWindow(L"BUTTON", L"Check", WS_CHILD | WS_VISIBLE, 150, 80, 100, 30, hMainWindow, (HMENU)ID_CHECK_BUTTON, hInstance, NULL);
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    // Gets HWND for all controls

    HWND hRegexField = GetDlgItem(hWnd, ID_REGEX_EDIT);
    HWND hTextField = GetDlgItem(hWnd, ID_TEXT_EDIT);
    HWND hButton = GetDlgItem(hWnd, ID_CHECK_BUTTON);

    switch (message)
    {
    case WM_COMMAND:
        switch (LOWORD(wParam))
        {
        case ID_CHECK_BUTTON:
            if (HIWORD(wParam) == BN_CLICKED)
            {
                // Getting a regular expression from hRegexField
                WCHAR regexBuffer[256];
                GetWindowText(hRegexField, regexBuffer, sizeof(regexBuffer) / sizeof(regexBuffer[0]));

                // Getting a text from hTextField
                WCHAR textBuffer[256];
                GetWindowText(hTextField, textBuffer, sizeof(textBuffer) / sizeof(textBuffer[0]));

                // Converting a regular expression to an std::regex object
                std::wregex regex;
                try
                {
                    regex = std::wregex(regexBuffer);
                }
                catch (const std::regex_error&)
                {
                    MessageBox(hWnd, L"Invalid regular expression!", L"Error", MB_ICONERROR);
                    break;
                }

                // Checking whether the text matches the regular expression
                // Output of the result in MessageBox
                if (std::regex_match(textBuffer, regex))
                {
                    MessageBox(hWnd, L"Success", L"Result", MB_ICONINFORMATION);
                }
                else
                {
                    MessageBox(hWnd, L"Failure", L"Result", MB_ICONERROR);
                }
            }
            break;
        default:
            return DefWindowProc(hWnd, message, wParam, lParam);
            break;
        }
        break;
    case WM_DESTROY:
        PostQuitMessage(0);
        break;
    default:
        return DefWindowProc(hWnd, message, wParam, lParam);
        break;
    }

    return 0;
}
