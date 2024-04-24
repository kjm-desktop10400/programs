#include<stdio.h>

struct long_num
{
    int size;
    unsigned char* num;
};

void sub1(struct long_num* obj)
{
    *(obj->num + obj->size - 1) -= 1; 
}

struct long_num* 

void printb(unsigned int v) {
  unsigned int mask = 1 << (sizeof(v) * 8 - 1);
  do putchar(mask & v ? '1' : '0');
  while (mask >>= 1);
}
void printbc(unsigned char v) {
    unsigned  mask = 1 << (sizeof(v) * 8 - 1);
    do putchar(mask & v ? '1' : '0');
    while (mask >>= 1);
}
void printbs(struct long_num v) {
    
    unsigned mask = 1 << (sizeof(unsigned char) * 8 - 1);
    for(int i = 0; i < v.size; i++)
    {
        mask = 1 << (sizeof(unsigned char) * 8 - 1);
        for(int j = 0; j < sizeof(unsigned char) * 8; j++)
        {
            putchar(mask & *(v.num + i) ? '1' : '0');
            mask >>= 1;
        }
        putchar(' ');
    }

}


int main(int argc, char* argv[])
{

    unsigned char test[] = {255, 255, 255, 255};
    
    struct long_num a;
    a.size = sizeof(test);
    a.num = test;
    
    printf("before\t\t: ");
    printbs(a);

    sub1(&a);

    printf("\n\nafter\t\t: ");
    printbs(a);


    return 0;
}