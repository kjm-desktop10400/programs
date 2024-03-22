#include<stdio.h>
#include<WinSock2.h>


#define RCVBUFSIZE 32

void DieWithError(char* errorMessage);

int main(int argc, char *argv[]) {

	int sock;								/*�\�P�b�g�f�B�X�N���v�^*/
	struct sockaddr_in echoServAddr;		/*�G�R�[�T�[�o�̃A�h���X*/
	unsigned short echoServPort;			/*�G�R�[�T�[�o�̃|�[�g�ԍ�*/
	char* servIP;							/*�T�[�o��IP�A�h���X�i�h�b�g�P�O�i�\�L�j*/
	char* echoString;						/*�G�R�[�T�[�o�ɑ��M���镶����*/
	char echoBuffer[RCVBUFSIZE];			/*�G�R�[������p�̃o�b�t�@*/
	unsigned int echoStringLen;				/*�G�R�[���镶����̃T�C�Y*/
	int bytesRcvd, totalBytesRcvd;			/*1�K��recv()�œǂݎ����o�C�g���ƑS�o�C�g��*/

	if ((argc < 3) || (argc > 4))			/*�����̐������������m�F*/
	{
		fprintf(stderr, "Usage: %s <Server IP> <Echo Word> [Echo Port>]\n", argv[0]);
		exit(1);
	}

	servIP = argv[1];						/*1�ڂ̈��� : �T�[�o��IP�A�h���X(�h�b�g10�i�\�L)*/
	echoString = argv[2];					/*2�ڂ̈��� : �G�R�[������*/

	if (argc == 4)
	{
		echoServPort = atoi(argv[3]);		/*�w��̃|�[�g������Ύg�p*/
	}
	else
	{
		echoServPort = 7;					/*7�̓G�R�[�T�[�r�X��well-known�|�[�g*/
	}

	/*TCP�ɂ��M�����̍����X�g���[���\�P�b�g���쐬*/
	if ((sock = socket(PF_INET, SOCK_STREAM, IPPROTO_TCP)) < 0)
	{
		DieWithError("socket() failed");
	}

	/*�T�[�o�̃A�h���X�\���̂��쐬*/
	memset(&echoServAddr, 0, sizeof(echoServAddr));
	echoServAddr.sin_family = AF_INET;
	echoServAddr.sin_addr.s_addr = inet_addr(servIP);
	echoServAddr.sin_port = htons(echoServPort);

	/*�G�R�[�T�[�o�ւ̐ڑ����m��*/


	/*��������T�[�o�ɑ��M*/


	/*������������T�[�o�����M*/


	
	
}