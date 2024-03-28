#include<stdio.h>
#include<stdlib.h>

#pragma warning(suppress : 4996)
#pragma comment(lib, "Ws2_32.lib")

void DieWithError(char* errorMessage, int WSAErrorCode)
{

	fprintf(stderr, "WSA error : %d\n", WSAErrorCode);
	perror(errorMessage);
	exit(1);

}