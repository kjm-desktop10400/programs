#include<stdio.h>
#include<string.h>
#include<stdlib.h>

void itooct(int num, unsigned char* oct);
int Size_pudding(char* msg);
void Pudding(char* msg, char* modified);

int main(void)
{
    char msg[] = "100000";

    printf("msg : %x\n", msg);

    char* buf = (char*)malloc(Size_pudding(msg));
    Pudding(msg, buf);

    printf("pudding end\n");

    for(int i = 0; i < Size_pudding(msg); i++)
    {
        printf("%x", *(buf + i));

        if(i % 16 == 0)
        {
            printf("\n");
        }
        else if(i % 4 == 0)
        {
            printf(" ");
        }
    }

}