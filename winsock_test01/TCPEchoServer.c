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
		fprintf(stderr, "Usage : %s <Server Port>\n", argv[0]);
		exit(1);
	}

	echoServPort = atoi(argv[1]);

	/*着信接続用のソケットを作成*/
	if ((servSock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithError("socket() failed");
	}

	/*ローカルのアドレス構造体を作成*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = AF_INET;
	echoServAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	echoServAddr.sin_port = htons(echoServPort);

	/*echoClntAddrとclntSockを0でパディング*/
	memset(&echoClntAddr, 0, sizeof(echoClntAddr));
	memset(&clntSock, 0, sizeof(clntSock));

	/*ローカルアドレスへのバインド*/
	if (bind(servSock, (struct sockaddr*)&echoServAddr, sizeof(echoServAddr)) < 0)
	{
		DieWithError("bind() failed");
	}

	/*「接続要求をリスン中」というマークをソケットにつける*/
	for (;;)
	{
		/*入出力パラメータのサイズをセット*/
		clntLen = sizeof(echoClntAddr);

		/*クライアントからの接続要求を待機*/
		if ((clntSock = accept(servSock, (struct sockaddr*)&echoClntAddr, &clntLen)) < 0)
		{
			DieWithError("accepr() failed");
		}

		/*lcntSockはクライアントに接続済み*/
		printf("Handling client %s\n", inet_ntoa(echoClntAddr.sin_addr));

		HandleTCPClient(clntSock);

	}

	
}
