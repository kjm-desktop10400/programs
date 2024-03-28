#include<stdio.h>
#include<winsock.h>

#pragma comment(lib, "Ws2_32.lib")

#define RCVBUFSIZE 32

void DieWithError(char* errorMessage, int WSAErrorCode);

void HandleTCPClient(int clntSocket)
{
	char echoBuffer[RCVBUFSIZE];
	int recvMsgSize;

	/*�N���C�A���g����̎�M���b�Z�[�W*/
	if ((recvMsgSize = recv(clntSocket, echoBuffer, RCVBUFSIZE, 0)) < 0)
	{
		DieWithError("recv() failed", WSAGetLastError());
	}

	/*��M����������𑗐M���A�]�����I�����Ă��Ȃ���Ύ�����M����*/
	while (recvMsgSize > 0)
	{
		/*���b�Z�[�W���N���C�A���g�ɃG�R�[�o�b�N����*/
		if (send(clntSocket, echoBuffer, recvMsgSize, 0) != recvMsgSize)
		{
			DieWithError("send() failed", WSAGetLastError());
		}

		/*��M����f�[�^���c���Ă��Ȃ����m�F����*/
		if ((recvMsgSize = recv(clntSocket, echoBuffer, RCVBUFSIZE, 0)) < 0)
		{
			DieWithError("recv() failed", WSAGetLastError());
		}

	}

	closesocket(clntSocket);
}