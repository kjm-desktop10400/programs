#include<stdio.h>
#include<string.h>
#include<stdlib.h>

void itooct(int num, unsigned char* oct);
int Size_pudding(char* msg);
void Pudding(char* msg, int msg_size, char* modified);

int main(void)
{
    char msg[7] = {"abcdef"};

    printf("msg : %s\n", msg);

    unsigned char* buf = (char*)malloc(Size_pudding(msg));
    Pudding(msg, 6, buf);

    for(int i = 0; i < Size_pudding(msg); i++)
    {
        if (i < 8)
        {
            printf("%x ", *(buf + i));
        }
        else
        {
            printf("%x ", *(buf + i));
        }

        if ((i + 1) == 0)
        {
            continue;
        }

        if((i + 1) % 16 ==0)
        {
            printf("\n");
        }
        else if((i + 1) % 4 == 0)
        {
            printf("  ");
            if ((i + 1) % 8 == 0)
            {
                printf("\t");
            }
        }
    }

    printf("\nmsg[7] : %d\n", *(buf + 6));

}