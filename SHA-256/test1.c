#include<stdio.h>
#include<string.h>
#include<stdlib.h>

unsigned int Shift(unsigned int obj, int arg);
unsigned int Rot(unsigned int obj, int arg);
void itooct(int num, unsigned char* oct);

void printb(unsigned char v) {
  unsigned int mask = 1 << (sizeof(v) * 8 - 1);
  do putchar(mask & v ? '1' : '0');
  while (mask >>= 1);
}

void putb(unsigned int v) {
  putchar('0'), putchar('b'), printb(v), putchar('\n');
}

int main(void)
{

    unsigned char out[8];

    itooct(300, out);

    for(int i = 0; i < 2; i++)
    {
        for(int j = 0; j < 4; j++)
        {
            printb(out[j + 4 * i]);
            printf(" ");
        }
        putchar('\n');
    }

}