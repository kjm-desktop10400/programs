#include<stdio.h>
#include<winsock.h>

#pragma warning(suppress : 4996)
#pragma comment(lib, "Ws2_32.lib")

#define RCVBUFSIZE 32

void DieWithError(char* errorMessage);

void HandleTCPClient(int clntSocket)
{
	char echoBuffer[RCVBUFSIZE];
	int recvMsgSize;

	/*クライアントからの受信メッセージ*/
	if ((recvMsgSize = recv(clntSocket, echoBuffer, RCVBUFSIZE, 0)) < 0)
	{
		DieWithError("recv() failed");
	}

	/*受信した文字列を送信し、転送が終了していなければ次を受信する*/
	while (recvMsgSize > 0)
	{
		/*メッセージをクライアントにエコーバックする*/
		if (send(clntSocket, echoBuffer, recvMsgSize, 0) != recvMsgSize)
		{
			DieWithError("send() failed");
		}

		/*受信するデータが残っていないか確認する*/
		if ((recvMsgSize = recv(clntSocket, echoBuffer, RCVBUFSIZE, 0)) < 0)
		{
			DieWithError("recv() failed");
		}

	}

	close(clntSocket);
}