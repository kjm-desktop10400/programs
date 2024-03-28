#pragma warning(suppress : 4996)
#pragma comment(lib, "Ws2_32.lib")

#include<stdio.h>
#include<WinSock2.h>
#include<winerror.h>
#include<WS2tcpip.h>

#define MAXPENDING 5

void DieWithError(char* errorMessage);
void DieWithErrorShowCode(char* errorMessage, int WSAErrorCode);
void HandleTCPClient(int clntSocket);

int main(int argc, char* argv[])
{

	int servSock;
	int clntSock;
	struct sockaddr_in echoServAddr;
	struct sockaddr_in echoClntAddr;
	unsigned short echoServPort;
	unsigned int clntLen;

	char echoClntAddr_str[sizeof("255.255.255.255")];				/*winsock2ではinet_ntoa()をセキュリティの問題で使用できない。そのための一時的変数*/
	WSADATA wsaData;												/*winsock用変数。WSAStartup()でwinsockのバージョンなどを格納する*/

	if (argc != 2)
	{
		fprintf(stderr, "Usage : %s <Server Port>\n", argv[0]);
		exit(1);
	}

	/*echoClntAddr、echoClntAddr_str、clntSock、echoServPortを0でパディング*/
	memset(&echoClntAddr, 0, sizeof(echoClntAddr));
	memset(&echoClntAddr_str, 0, sizeof(echoClntAddr_str));
	memset(&clntSock, 0, sizeof(clntSock));
	memset(&echoServPort, 0, sizeof(echoServPort));

	echoServPort = atoi(argv[1]);

	WSAStartup(2, &wsaData);										/*winsock用。最初の引数はwinsockのバージョン。後ろの変数はこの処理内容を入れるための変数。*/

	/*着信接続用のソケットを作成*/
	if ((servSock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithErrorShowCode("socket() failed", WSAGetLastError());
	}

	/*ローカルのアドレス構造体を作成*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = AF_INET;
	echoServAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	echoServAddr.sin_port = htons(echoServPort);

	/*ローカルアドレスへのバインド*/
	if (bind(servSock, (struct sockaddr*)&echoServAddr, sizeof(echoServAddr)) < 0)
	{
		DieWithErrorShowCode("bind() failed", WSAGetLastError());
	}

	/*「接続要求をリスン中」というマークをソケットにつける*/
	if (listen(servSock, MAXPENDING) < 0)
	{
		DieWithErrorShowCode("listen() failde", WSAGetLastError());
	}

	for (;;)
	{
		/*入出力パラメータのサイズをセット*/
		clntLen = sizeof(echoClntAddr);

		/*クライアントからの接続要求を待機*/
		if ((clntSock = accept(servSock, (struct sockaddr*)&echoClntAddr, &clntLen)) < 0)
		{
			DieWithErrorShowCode("accepr() failed", WSAGetLastError());
		}

		/*lcntSockはクライアントに接続済み*/
		inet_ntop(AF_INET, &(echoClntAddr.sin_addr), echoClntAddr_str, sizeof(echoClntAddr_str));
		printf("Handling client %s\n", echoClntAddr_str);

		HandleTCPClient(clntSock);

	}

	WSACleanup();

}
