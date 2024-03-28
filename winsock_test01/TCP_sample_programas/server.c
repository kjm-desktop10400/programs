#pragma comment(lib, "Ws2_32.lib")
#pragma warning(suppress : 4996)

#include<stdio.h>
#include<WinSock2.h>
#include<WS2tcpip.h>
#include<winerror.h>

void DieWithError(int ErrorCode, char* errorMessage);

int main(int argc, char* argv[])
{

    WSADATA wsaData;
    


    WSAStartup(MAKEWORD(2,0), &wsaData);

}