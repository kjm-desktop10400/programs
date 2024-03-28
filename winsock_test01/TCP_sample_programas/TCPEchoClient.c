#pragma comment(lib, "Ws2_32.lib")

#include<stdio.h>
#include<winsock2.h>
#include<WS2tcpip.h>


#define RCVBUFSIZE 32


void DieWithError(char* errorMessage, int WSAErrorCode);

int main(int argc, char *argv[]) {

	int sock;								/*�\�P�b�g�f�B�X�N���v�^*/
	struct sockaddr_in echoServAddr;		/*�G�R�[�T�[�o�̃A�h���X*/
	unsigned short echoServPort;			/*�G�R�[�T�[�o�̃|�[�g�ԍ�*/
	char* servIP;							/*�T�[�o��IP�A�h���X�i�h�b�g�P�O�i�\�L�j*/
	char* echoString;						/*�G�R�[�T�[�o�ɑ��M���镶����*/
	char echoBuffer[RCVBUFSIZE];			/*�G�R�[������p�̃o�b�t�@*/
	unsigned int echoStringLen;				/*�G�R�[���镶����̃T�C�Y*/
	int bytesRcvd, totalBytesRcvd;			/*1�K��recv()�œǂݎ����o�C�g���ƑS�o�C�g��*/

	WSADATA wsaData;						/*winsock2�p*/

	if ((argc < 3) || (argc > 4))			/*�����̐������������m�F*/
	{
		fprintf(stderr, "Usage: %s <Server IP> <Echo Word> [Echo Port>]\n", argv[0]);
		exit(1);
	}

	servIP = argv[1];						/*1�ڂ̈��� : �T�[�o��IP�A�h���X(�h�b�g10�i�\�L)*/
	echoString = argv[2];					/*2�ڂ̈��� : �G�R�[������*/

	WSAStartup(MAKEWORD(2, 0), &wsaData);

	if (argc == 4)
	{
		echoServPort = (unsigned short)atoi(argv[3]);		/*�w��̃|�[�g������Ύg�p*/
	}
	else
	{
		echoServPort = 7;					/*7�̓G�R�[�T�[�r�X��well-known�|�[�g*/
	}

	/*TCP�ɂ��M�����̍����X�g���[���\�P�b�g���쐬*/
	if ((sock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithError("socket() failed", WSAGetLastError());
	}

	/*�T�[�o�̃A�h���X�\���̂��쐬*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = PF_INET;
	inet_pton(PF_INET, servIP, &(echoServAddr.sin_addr.s_addr));
	echoServAddr.sin_port = htons(echoServPort);

	/*�G�R�[�T�[�o�ւ̐ڑ����m��*/
	if (connect(sock, (struct sockaddr*)&echoServAddr, sizeof(echoServAddr)) != 0);
	{
		DieWithError("connect() failed", WSAGetLastError());
	}

	echoStringLen = strlen(echoString);


	/*��������T�[�o�ɑ��M*/
	if (send(sock, echoString, echoStringLen, 0) != echoStringLen)
		DieWithError("send() sent a different numbeer of bytes than expected", WSAGetLastError());


	/*������������T�[�o�����M*/
	totalBytesRcvd = 0;
	printf("Received : ");					/*�G�R�[�������������\�������邽�߂̏���*/
	while (totalBytesRcvd < echoStringLen)
	{										/*�o�b�t�@�T�C�Y�ɒB����܂�(NULL�����p��1�o�C�g������)�T�[�o����f�[�^����M����*/	
		if ((bytesRcvd = recv(sock, echoBuffer, RCVBUFSIZE - 1, 0)) <= 0)
		{

			DieWithError("recv() failed or connection closed prematurely", WSAGetLastError());
		}
		totalBytesRcvd += bytesRcvd;		/*���o�C�g���̏W�v*/
		echoBuffer[bytesRcvd] = '\0';		/*������̏I��*/
		printf(echoBuffer);					/*�G�R�[�o�b�t�@�̕\��*/
	}

	printf("\n");							/*�Ō�̉��s���o��*/

	closesocket(sock);
	WSACleanup();

	exit(0);	
	
}