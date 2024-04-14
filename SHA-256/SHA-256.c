#include<stdio.h>
#include<stdlib.h>
#include<string.h>

#define BLOCKSIZE 64

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
unsigned int Choice(unsigned int a, unsigned int b, unsigned int c);
unsigned int Majority(unsigned int a, unsigned int b, unsigned int c);
unsigned int Sigma0(unsigned int obj);
unsigned int Sigma1(unsigned int obj);


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

int main(int argc, char *argv[]){

    typedef unsigned int Word;          //32bit�P�ʂ�bit���Z���s���̂�typedef���Ă���
    typedef Word* MessageSchedule;      //�X�P�W���[�������l��typedef�Ő錾���Ă���

    int msg_size_p;                     //�p�f�B���O��̃��b�Z�[�W�T�C�Y
    int scheduleNum;                    //���b�Z�[�W�X�P�W���[���̐�
    int scheduleSize;                   //���b�Z�[�W�X�P�W���[��1������̃T�C�Y(byte)

    unsigned int wordBuf;               //unsigned char�^��message 4byte���o�b�t�@�����O����

    MessageSchedule* scheduleArr;       //�X�P�W���[���̃A�h���X���i�[����z��

    unsigned int workingValiable[8];    //���ʂ��v�Z����z��
    unsigned int Hash[] = { 0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a, 0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19 };      //hash�l�̏����l�ƌv�Z����

    unsigned char* message;             //�p�f�B���O��̃��b�Z�[�W������z��

    int msg_count = 0;                  //�C�e���[�V�����p�ϐ�

    if(argc == 1)
    {
        printf("usage : <message>");
        return 0;
    }

    msg_size_p = Size_pudding(argv[1]);                     //�p�f�B���O��̃��b�Z�[�W�T�C�Y���擾
    message = (char*)malloc(msg_size_p);                    //�p�f�B���O��̃��b�Z�[�W�p�Ƀ��������m��
    Pudding(argv[1],strlen(argv[1]), message);              //���b�Z�[�W���p�f�B���O


    scheduleNum = msg_size_p / BLOCKSIZE;
    scheduleSize = BLOCKSIZE * 4 * sizeof(Word);

    scheduleArr = (MessageSchedule*)malloc(scheduleNum);

    //�g�p����schdule���̃��������m��
    for(int i = 0; i < scheduleNum; i++)
    {
        //�������ރX�P�W���[���̃��������m��
        *(scheduleArr + i) = (MessageSchedule)malloc(scheduleSize);

        //�X�P�W���[���̍쐬
        for (int j = 0; j < BLOCKSIZE; j++)
        {

            wordBuf = 0;
            if (j < 16)
            {
                //schedule�ɏ�������word���쐬
                for (int k = 0; k < sizeof(Word); k++)
                {
                    wordBuf += *(message + msg_count) << 8 * (sizeof(Word) - 1 - k);
                    msg_count++;
                }
            }
            else
            {
                
                wordBuf += ((Rot(*(*(scheduleArr + i) + j - 2), 17)) ^ (Rot(*(*(scheduleArr + i) + j - 2), 19)) ^ (Shift(*(*(scheduleArr + i) + j - 2), 10)));
                wordBuf += (*(*(scheduleArr + i) + j - 7));
                wordBuf += (*(*(scheduleArr + i) + j - 16));
                wordBuf += ((Rot(*(*(scheduleArr + i) + j - 15), 7)) ^ (Rot(*(*(scheduleArr + i) + j - 15), 18)) ^ (Shift(*(*(scheduleArr + i) + j - 15), 3)));


            }

            *(*(scheduleArr + i) + j) = wordBuf;


        }

    }

    //working valiaable�̏�����
    for (int j = 0; j < 8; j++)
    {
        workingValiable[j] = Hash[j];
    }

    //hash�l�̌v�Z�B�X�P�W���[�����ɌJ��Ԃ�
    for (int i = 0; i < scheduleNum; i++)
    {

        for (int j = 0; j < BLOCKSIZE; j++)
        {

            unsigned int tmp1 = 0;
            unsigned int tmp2 = 0;

            unsigned int sig0 = Sigma0(workingValiable[0]);
            unsigned int sig1 = Sigma1(workingValiable[4]);
            unsigned int cho = Choice(workingValiable[4], workingValiable[5], workingValiable[6]);
            unsigned int maj = Majority(workingValiable[0], workingValiable[1], workingValiable[2]);

            tmp1 = workingValiable[7] + K[j] + *(*(scheduleArr + i) + j) + Sigma1(workingValiable[4]) + Choice(workingValiable[4], workingValiable[5], workingValiable[6]);

            tmp2 = Sigma0(workingValiable[0]) + Majority(workingValiable[0], workingValiable[1], workingValiable[2]);


            workingValiable[7] = workingValiable[6];
            workingValiable[6] = workingValiable[5];
            workingValiable[5] = workingValiable[4];
            workingValiable[4] = workingValiable[3] + tmp1;
            workingValiable[3] = workingValiable[2];
            workingValiable[2] = workingValiable[1];
            workingValiable[1] = workingValiable[0];
            workingValiable[0] = tmp1 + tmp2;

        }

        //hash�l�̍X�V
        for(int j = 0; j < 8; j++)
        {
            Hash[j] += workingValiable[j];
        }

    }

    for (int i = 0; i < 8; i++)
    {
        fprintf(stdout, "%x", Hash[i]);
    }

    return 0;
}