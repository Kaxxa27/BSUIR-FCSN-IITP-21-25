#include <iostream>
#include <WinSock2.h>
#include <WS2tcpip.h>
#include <inaddr.h>
#include <stdio.h>
#include <vector>
#include <thread>

#pragma comment(lib, "ws2_32.lib")

using namespace std;

// Struct for Client Data
struct ClientData {
    string name;
    int age;
};

// Info about App
void Info()
{
    cout << endl << "[ Universal - Anonymus - Chat ]" << endl;
    cout << "[ Chat Rules ]" << endl;
    cout << "1. Do not insult other participants! " << endl;
    cout << "2. Enter \"help\" to help you use the chat! " << endl << endl;
}
void SendMessagesAsync(SOCKET clientSocket) {

    vector<char> clientBuff(1024);

    while (true) {
        cout << "Your message: ";
        fgets(clientBuff.data(), clientBuff.size(), stdin);

        // Check whether the client wants to stop chatting (exit) 
        if (clientBuff[0] == 'e'
            && clientBuff[1] == 'x'
            && clientBuff[2] == 'i'
            && clientBuff[3] == 't') {
            shutdown(clientSocket, SD_BOTH);
            closesocket(clientSocket);
            WSACleanup();
            return;
        }

        // Call help list (help) 
        if (clientBuff[0] == 'h'
            && clientBuff[1] == 'e'
            && clientBuff[2] == 'l'
            && clientBuff[3] == 'p') {

            Info();

            continue;
        }

        // Send the message to the server
        int packet_size = send(clientSocket, clientBuff.data(), clientBuff.size(), 0);

        if (packet_size == SOCKET_ERROR) {
            cout << "Can't send message to Server. Error # " << WSAGetLastError() << endl;
            closesocket(clientSocket);
            WSACleanup();
            return;
        }
    }
}

int main(void) {

    const char SERVER_IP[] = "127.0.0.1";	// Enter IPv4 address of Server
    const short SERVER_PORT_NUM = 17177;	// Enter Listening port on Server side
    const short BUFF_SIZE = 1024;			// Maximum size of buffer for exchange info between server and client

    int erStat;	// For checking errors in sockets functions

    //IP in string format to numeric format for socket functions. Data is in "ip_to_num"
    in_addr ip_to_num;
    inet_pton(AF_INET, SERVER_IP, &ip_to_num);


    // WinSock initialization
    WSADATA wsData;
    erStat = WSAStartup(MAKEWORD(2, 2), &wsData);

    if (erStat != 0) {
        cout << "Error WinSock version initializaion #";
        cout << WSAGetLastError();
        return 1;
    }
    else
        cout << "WinSock initialization is OK" << endl;

    // Socket initialization
    SOCKET ClientSock = socket(AF_INET, SOCK_STREAM, 0);

    if (ClientSock == INVALID_SOCKET) {
        cout << "Error initialization socket # " << WSAGetLastError() << endl;
        closesocket(ClientSock);
        WSACleanup();
    }
    else
        cout << "Client socket initialization is OK" << endl;

    // Establishing a connection to Server
    sockaddr_in servInfo;
    ZeroMemory(&servInfo, sizeof(servInfo));

    servInfo.sin_family = AF_INET;
    servInfo.sin_addr = ip_to_num;
    servInfo.sin_port = htons(SERVER_PORT_NUM);

    erStat = connect(ClientSock, (sockaddr*)&servInfo, sizeof(servInfo));

    if (erStat != 0) {
        cout << "Connection to Server is FAILED. Error # " << WSAGetLastError() << endl;
        closesocket(ClientSock);
        WSACleanup();
        return 1;
    }
    else
        cout << "Connection established SUCCESSFULLY. Ready to send a message to Server" << endl;


    Info(); // Info About App

    //Exchange text data between Server and Client. Disconnection if a Client send "exit"

    vector <char> servBuff(BUFF_SIZE), clientBuff(BUFF_SIZE); // Buffers for sending and receiving data
    short packet_size = 0;

    // Create thread for async sending message 
    std::thread sendThread(SendMessagesAsync, ClientSock);


    while (true) {
        // Receive messages from the server
        packet_size = recv(ClientSock, servBuff.data(), servBuff.size(), 0);

        if (packet_size == SOCKET_ERROR) {
            cout << "Can't receive message from Server. Error # " << WSAGetLastError() << endl;
            sendThread.join();
            closesocket(ClientSock);
            WSACleanup();
            return 1;
        }
        else if (packet_size > 0) {
            cout << "\n\n[Anonymous] Server message: " << servBuff.data() << endl;
            cout << "Your message: ";
        }
    }

    // Wait complete sendThread
    sendThread.join();

    closesocket(ClientSock);
    WSACleanup();

    return 0;
}
