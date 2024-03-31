#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<math.h>

#define BLOCK_SIZE 64

char ctob(char str);
char* itooct(int num);                                                     //与えられた数値をしたから埋めた8byteに変換

//ブロックサイズが64byteの倍数になるようメッセージをパディングする。
char* Pudding(char* msg, char* modified)
{

    int msg_size = strlen(msg) * sizeof(char);                              //msgのバイト数
    int block_num = msg_size / BLOCK_SIZE + 1;                              //パディング後の合計ブロック数
    int write_count = 0;                                                    //パディング後のアドレスに書き込んだメッセージのサイズ
    char inblock_c[8];                                                      //inblockの文字列表記。16進数

    //ブロック数分のメモリを確保
    modified = (char*)malloc(BLOCK_SIZE * block_num);
    
    for(int i = 0; i < BLOCK_SIZE * block_num; i++)
    {

        if(i < msg_size)
        {
            *(modified + i) = *(msg + i);
        }
        else if(i == msg_size)
        {
            *(modified + i) = 0x80;
        }
        else if(i < BLOCK_SIZE * block_num)
        {

        }

    }

    return modified;
}

char* itooct(int num)
{

    int byte_size = 0;
    int num_tmp = num;
    char* oct[8];

    while(num_tmp != 0)
    {
        num_tmp /= 256;
        byte_size++;
    }

    for(int i = 0; i < 8; i++)
    {

        if(i < byte_size)
        {
            oct[i] = 0;
            continue;
        }

        oct[i] = (int)(num / (int)pow(256, (8 - i)) - ( num / (int)pow(256, (8 - i + 1)) * 256 ));
    }

    return oct;
}