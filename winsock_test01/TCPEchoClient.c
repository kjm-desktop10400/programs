#pragma comment(lib, "Ws2_32.lib")

#include<stdio.h>
#include<winsock2.h>
#include<WS2tcpip.h>


#define RCVBUFSIZE 32


void DieWithError(char* errorMessage);

int main(int argc, char *argv[]) {

	int sock;								/*ソケットディスクリプタ*/
	struct sockaddr_in echoServAddr;		/*エコーサーバのアドレス*/
	unsigned short echoServPort;			/*エコーサーバのポート番号*/
	char* servIP;							/*サーバのIPアドレス（ドット１０進表記）*/
	char* echoString;						/*エコーサーバに送信する文字列*/
	char echoBuffer[RCVBUFSIZE];			/*エコー文字列用のバッファ*/
	unsigned int echoStringLen;				/*エコーする文字列のサイズ*/
	int bytesRcvd, totalBytesRcvd;			/*1階のrecv()で読み取られるバイト数と全バイト数*/

	WSADATA wsaData;						/*winsock2用*/

	if ((argc < 3) || (argc > 4))			/*引数の数が正しいか確認*/
	{
		fprintf(stderr, "Usage: %s <Server IP> <Echo Word> [Echo Port>]\n", argv[0]);
		exit(1);
	}

	servIP = argv[1];						/*1つ目の引数 : サーバのIPアドレス(ドット10進表記)*/
	echoString = argv[2];					/*2つ目の引数 : エコー文字列*/

	WSAStartup(MAKEWORD(2, 0), &wsaData);

	if (argc == 4)
	{
		echoServPort = (unsigned short)atoi(argv[3]);		/*指定のポートがあれば使用*/
	}
	else
	{
		echoServPort = 7;					/*7はエコーサービスのwell-knownポート*/
	}

	/*TCPによる信頼性の高いストリームソケットを作成*/
	if ((sock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithErrorShowCode("socket() failed", WSAGetLastError());
	}

	/*サーバのアドレス構造体を作成*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = PF_INET;
	inet_pton(PF_INET, servIP, &(echoServAddr.sin_addr.s_addr));
	echoServAddr.sin_port = htons(echoServPort);

	/*エコーサーバへの接続を確立*/
	if (connect(sock, (struct sockaddr*)&echoServAddr, sizeof(echoServAddr)) != 0);
	{
		DieWithErrorShowCode("connect() failed", WSAGetLastError());
	}

	echoStringLen = strlen(echoString);


	/*文字列をサーバに送信*/
	if (send(sock, echoString, echoStringLen, 0) != echoStringLen)
		DieWithErrorShowCode("send() sent a different numbeer of bytes than expected", WSAGetLastError());


	/*同じ文字列をサーバから受信*/
	totalBytesRcvd = 0;
	printf("Received : ");					/*エコーさせた文字列を表示させるための準備*/
	while (totalBytesRcvd < echoStringLen)
	{										/*バッファサイズに達するまで(NULL文字用の1バイトを除く)サーバからデータを受信する*/	
		if ((bytesRcvd = recv(sock, echoBuffer, RCVBUFSIZE - 1, 0)) <= 0)
		{

			DieWithErrorShowCode("recv() failed or connection closed prematurely", WSAGetLastError());
		}
		totalBytesRcvd += bytesRcvd;		/*総バイト数の集計*/
		echoBuffer[bytesRcvd] = '\0';		/*文字列の終了*/
		printf(echoBuffer);					/*エコーバッファの表示*/
	}

	printf("\n");							/*最後の改行を出力*/

	closesocket(sock);
	WSACleanup();

	exit(0);	
	
}