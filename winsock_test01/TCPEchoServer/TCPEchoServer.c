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

	char echoClntAddr_str[sizeof("255.255.255.255")];				/*winsock2�ł�inet_ntoa()���Z�L�����e�B�̖��Ŏg�p�ł��Ȃ��B���̂��߂̈ꎞ�I�ϐ�*/
	WSADATA wsaData;												/*winsock�p�ϐ��BWSAStartup()��winsock�̃o�[�W�����Ȃǂ��i�[����*/

	if (argc != 2)
	{
		fprintf(stderr, "Usage : %s <Server Port>\n", argv[0]);
		exit(1);
	}

	/*echoClntAddr�AechoClntAddr_str�AclntSock�AechoServPort��0�Ńp�f�B���O*/
	memset(&echoClntAddr, 0, sizeof(echoClntAddr));
	memset(&echoClntAddr_str, 0, sizeof(echoClntAddr_str));
	memset(&clntSock, 0, sizeof(clntSock));
	memset(&echoServPort, 0, sizeof(echoServPort));

	echoServPort = atoi(argv[1]);

	WSAStartup(2, &wsaData);										/*winsock�p�B�ŏ��̈�����winsock�̃o�[�W�����B���̕ϐ��͂��̏������e�����邽�߂̕ϐ��B*/

	/*���M�ڑ��p�̃\�P�b�g���쐬*/
	if ((servSock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithErrorShowCode("socket() failed", WSAGetLastError());
	}

	/*���[�J���̃A�h���X�\���̂��쐬*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = AF_INET;
	echoServAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	echoServAddr.sin_port = htons(echoServPort);

	/*���[�J���A�h���X�ւ̃o�C���h*/
	if (bind(servSock, (struct sockaddr*)&echoServAddr, sizeof(echoServAddr)) < 0)
	{
		DieWithErrorShowCode("bind() failed", WSAGetLastError());
	}

	/*�u�ڑ��v�������X�����v�Ƃ����}�[�N���\�P�b�g�ɂ���*/
	if (listen(servSock, MAXPENDING) < 0)
	{
		DieWithErrorShowCode("listen() failde", WSAGetLastError());
	}

	for (;;)
	{
		/*���o�̓p�����[�^�̃T�C�Y���Z�b�g*/
		clntLen = sizeof(echoClntAddr);

		/*�N���C�A���g����̐ڑ��v����ҋ@*/
		if ((clntSock = accept(servSock, (struct sockaddr*)&echoClntAddr, &clntLen)) < 0)
		{
			DieWithErrorShowCode("accepr() failed", WSAGetLastError());
		}

		/*lcntSock�̓N���C�A���g�ɐڑ��ς�*/
		inet_ntop(AF_INET, &(echoClntAddr.sin_addr), echoClntAddr_str, sizeof(echoClntAddr_str));
		printf("Handling client %s\n", echoClntAddr_str);

		HandleTCPClient(clntSock);

	}

	WSACleanup();

}
