#include<stdio.h>
#include<stdlib.h>


//4 bytes of bit sequence right shift arg bit.
unsigned int Shift(unsigned int obj, int arg)
{
    return obj >> arg;
}

//4 bytes of bit sequence right shift arg bit and overflowed bit add head of sequence.
unsigned int Rot(unsigned int obj, int arg)
{
    unsigned int mask = 0;
    unsigned int buf = 0;

    for(int i = 0; i < arg; i++)
    {
        mask <<= 1;
        mask += 1;
    }

    buf = obj & mask;

    obj = Shift(obj, arg);

    buf <<= 31 - arg;

    return buf | obj;
    
}


