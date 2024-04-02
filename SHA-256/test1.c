#include<stdio.h>
#include<string.h>
#include<stdlib.h>

void itooct(int num, unsigned char* oct);
int Size_pudding(char* msg);
void Pudding(char* msg, int msg_size, char* modified);
void RotR(char* obj, int arg);
void RotWords(char* obj);
void ShiftR(unsigned char* obj, int arg);

void printb(unsigned char v) {
  unsigned char mask = 1 << (sizeof(v) * 8 - 1);
  do putchar(mask & v ? '1' : '0');
  while (mask >>= 1);
}

void putb(unsigned int v) {
  putchar('0'), putchar('b'), printb(v), putchar('\n');
}

int main(void)
{
    char tmp[4] = {170, 0, 170, 0};
    char tmp2[4] = {1, 2, 4, 8};
    char test[4];

    for(int j = 0; j < 32; j++)
    {
        for(int i = 0; i < 4; i++)
        {
            test[i] = tmp[i];
        }

        for(int k = 0; k < 4; k++)
        {
            printb(test[k]);
            printf(" ");
        }

        printf("\t\t-- rot %d -->\t\t", j);
        RotR(test, j);

        for(int k = 0; k < 4; k++)
        {
            printb(test[k]);
            printf(" ");
        }

        printf("\n");

    }

}