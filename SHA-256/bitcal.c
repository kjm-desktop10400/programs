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

    buf <<= 32 - arg;

    return buf | obj;
    
}

//
unsigned int Choice(unsigned int a, unsigned int b, unsigned int c)
{
    return ((a & b) ^ ((~a) & c));
}

//
unsigned int Majority(unsigned int a, unsigned int b, unsigned int c)
{
    return ((a & b) ^ (b & c) ^ (c & a));
}

//
unsigned int Sigma0(unsigned int obj)
{
    return (Rot(obj, 2) ^ Rot(obj, 13) ^ Rot(obj, 22));
}

//
unsigned int Sigma1(unsigned int obj)
{
    return (Rot(obj, 6) ^ Rot(obj, 11) ^ Rot(obj, 25));
}

