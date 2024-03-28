#include<stdio.h>
#include<winsock.h>

#pragma comment(lib, "Ws2_32.lib")

#define RCVBUFSIZE 32

void DieWithError(char* errorMessage);
void DieWithErrorShowCode(char* errorMessage, int WSAErrorCode);

void HandleTCPClient(int clntSocket)
{
	char echoBuffer[RCVBUFSIZE];
	int recvMsgSize;

	/*クライアントからの受信メッセージ*/
	if ((recvMsgSize = recv(clntSocket, echoBuffer, RCVBUFSIZE, 0)) < 0)
	{
		DieWithErrorShowCode("recv() failed", WSAGetLastError());
	}

	/*受信した文字列を送信し、転送が終了していなければ次を受信する*/
	while (recvMsgSize > 0)
	{
		/*メッセージをクライアントにエコーバックする*/
		if (send(clntSocket, echoBuffer, recvMsgSize, 0) != recvMsgSize)
		{
			DieWithErrorShowCode("send() failed", WSAGetLastError());
		}

		/*受信するデータが残っていないか確認する*/
		if ((recvMsgSize = recv(clntSocket, echoBuffer, RCVBUFSIZE, 0)) < 0)
		{
			DieWithErrorShowCode("recv() failed", WSAGetLastError());
		}

	}

	close(clntSocket);
}