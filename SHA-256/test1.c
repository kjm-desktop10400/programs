#include<stdio.h>
#include<string.h>
#include<stdlib.h>

unsigned int Shift(unsigned int obj, int arg);
unsigned int Rot(unsigned int obj, int arg);

void printb(unsigned int v) {
  unsigned int mask = 1 << (sizeof(v) * 8 - 1);
  do putchar(mask & v ? '1' : '0');
  while (mask >>= 1);
}

void putb(unsigned int v) {
  putchar('0'), putchar('b'), printb(v), putchar('\n');
}

int main(void)
{

    unsigned int i1 = 0;
    unsigned int in = 1;

    for(int i = 0; i < 32; i++)
    {
        i1 <<= 1;

        if((i % 8 == 0) && (i != 0))
        {

            if (in == 1)
            {
                in = 0;
            }
            else
            {
                in = 1;
            }
            
        }

        i1 |= in;
    }

    for(int i = 0; i < 32; i++)
    {
        printb(Rot(i1, i));
        putchar('\n');
    }


}