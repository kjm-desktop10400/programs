#include<stdio.h>
#include<string.h>

char* itooct(int num);

int main(void)
{
    int num = 100000;

    for(int i = 0; i < 8; i++)
    {
        printf("[%d] : %d\n", i, *(itooct(num) + i));
    }

}