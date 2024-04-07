#include<stdio.h>
#include<stdlib.h>

#define BLOCKSIZE 64

#define sigma0(x) (Rot(*(x - 2), 7) ^ Rot(*(x - 2), 18) ^ Shift(*(x - 2), 3))
#define sigma1(x) (Rot(*(x - 15), 17) ^ Rot(*(x- 15), 19) ^ Shift(*(x - 15), 10))

const unsigned int K[] = {
    0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
    0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
    0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
    0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
    0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
    0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
    0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
    0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
};

void Pudding(char* msg, int msg_size, char* modified);
int Size_pudding(char* msg);

unsigned int Rot(unsigned int obj, int arg);
unsigned int Shift(unsigned int obj, int arg);

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

int main(int args, char *argv[]){

    int msg_size_p;

    typedef unsigned int Word;
    typedef Word* MessageSchedule;

    MessageSchedule* schedulaArr;

    unsigned char* message;

    int msg_count = 0;
    int word_count = 0;
    MessageSchedule current = 0;

    unsigned char msg[] = "abcdefghijklmnopqrstuvwxyg12345";

    msg_size_p = Size_pudding(msg);
    message = (char*)malloc(msg_size_p);
    Pudding(msg, sizeof(msg) - 1, message);

    schedulaArr = (Word*)malloc(msg_size_p / 64 * sizeof(Word));

    for(int i = 0; i < msg_size_p; i++)
    {
        printbc(*(message + i));
        putchar(' ');
        if(i % 4 == 3)
        {
            putchar('\n');
        }
    }

    for(int i = 0; i < msg_size_p * 4 * sizeof(Word); i++)
    {
        current = *((*schedulaArr) + (i / sizeof(Word)));

        if(word_count < 16)
        {
            *current = *(message + msg_count);
            msg_count++;
        }
        else
        {
            *(current) = sigma1(current) + *(current -7) + sigma0(current), *(current - 16);
        }

        if(word_count < 64)
        {
            word_count = 0;
        }
        else
        {
            word_count++;
        }
    }

    for(int i = 0; i < msg_size_p * 4 * sizeof(Word); i++)
    {
        printb(*((*schedulaArr) + (i / sizeof(Word))) + i);
        putchar('\n');
    }

    return 0;
}