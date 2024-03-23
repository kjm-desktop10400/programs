#pragma warning(suppress : 4996)
#pragma comment(lib, "Ws2_32.lib")

#include<stdio.h>
#include<winsock.h>

#define MAXPENDING 5

void DieWithError(char* errorMessage);
void HandleTCPClient(int clntSocket);

int main(int argc, char* argv[])
{

	int servSock;
	int clntSock;
	struct sockaddr_in echoServAddr;
	struct sockaddr_in echoClntAddr;
	unsigned short echoServPort;
	unsigned int clntLen;

	if (argc != 2)
	{
		fpritntf(stderr, "Usage : %s <Server Port>\n", argv[0]);
		exit(1);
	}

	echoServPort

}