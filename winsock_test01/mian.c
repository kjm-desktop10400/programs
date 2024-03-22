#include<stdio.h>
#include<WinSock2.h>


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

	if ((argc < 3) || (argc > 4))			/*引数の数が正しいか確認*/
	{
		fprintf(stderr, "Usage: %s <Server IP> <Echo Word> [Echo Port>]\n", argv[0]);
		exit(1);
	}

	servIP = argv[1];						/*1つ目の引数 : サーバのIPアドレス(ドット10進表記)*/
	echoString = argv[2];					/*2つ目の引数 : エコー文字列*/

	if (argc == 4)
	{
		echoServPort = atoi(argv[3]);		/*指定のポートがあれば使用*/
	}
	else
	{
		echoServPort = 7;					/*7はエコーサービスのwell-knownポート*/
	}

	/*TCPによる信頼性の高いストリームソケットを作成*/
	if ((sock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithError("socket() failed");
	}

	/*サーバのアドレス構造体を作成*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = AF_INET;
	echoServAddr.sin_addr.s_addr = inet_addr(servIP);
	echoServAddr.sin_port = htons(echoServPort);

	/*エコーサーバへの接続を確立*/


	/*文字列をサーバに送信*/


	/*同じ文字列をサーバから受信*/


	
	
}