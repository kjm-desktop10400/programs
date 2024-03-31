#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<math.h>

#define BLOCK_SIZE 64

char ctob(char str);
void itooct(int num, unsigned char* oct);                                   //与えられた数値をしたから埋めた8byteに変換。octにはchar[8]以上のメモリを渡すこと。

//return required byte size to allocate memory
int Size_pudding(char* msg)                                                 //Pudding()で必要となるメモリをあらかじめ計算しておき、Pudding()の呼び出し側でメモリを確保させる。
{
    return ((strlen(msg) * sizeof(char)) / BLOCK_SIZE + 1) * BLOCK_SIZE;
}

//msg : message, moddified : address of post pudding message. Call this func before allocate memory with Size_pudding()
void Pudding(char* msg, char* modified)                                     //ブロックサイズが64byteの倍数になるようメッセージをパディングする。modifiedには同プログラムSize_pudding()関数で得られるサイズをmallocにより確保しておくこと。
{

    int msg_size = strlen(msg) * sizeof(char);                              //msgのバイト数
    int block_num = msg_size / BLOCK_SIZE + 1;                              //パディング後の合計ブロック数
    int write_count = 0;                                                    //パディング後のアドレスに書き込んだメッセージのサイズ
    char* inblock = NULL;                                                   //inblockの文字列表記。16進数

    //ブロック数分のメモリを確保
    modified = (char*)malloc(BLOCK_SIZE * block_num);

    //msgのサイズ記録用配列
    itooct(msg_size, inblock);
    
    for(int i = 0; i < BLOCK_SIZE * block_num; i++)
    {

        if(i < msg_size)                                                    //
        {
            *(modified + i) = *(msg + i);
        }
        else if(i == msg_size)
        {
            *(modified + i) = 0x80;
        }
        else if(BLOCK_SIZE * block_num - 8 <= i)
        {
            *(modified + i) = *(inblock + (i - BLOCK_SIZE * block_num + 8));
        }
        else
        {
            *(modified + i) = NULL;
        }

    }
}

void itooct(int num, unsigned char* oct)
{

    //
    //      基本方針はchar型を16進数2bitと考えてシフト演算を行う。右シフトで下の桁を削り、
    //      すでに記録した上の桁を引くことでchar型に保存する16進数1bitを切り出す
    //  
    //      ex)     100000          ->          0x186a0
    //                              >>0x100  (256)
    //              390             ->          0x186
    //                              >>0x100  (256)
    //              1               ->          0x1
    //
    //                      0x86 = 0x186 - 0x1 * 0x100
    //                      0xa0 = 0x186a0 - 0x186 * 0x100
    //
    //      このようにして16進数1bitづつを切り出す
    //

    int byte_size = 0;
    int num_buf = num;
    int upper_cut = 0;
    unsigned char inv[8];

    while(num_buf != 0)
    {
        num_buf /= 0xff;
        byte_size++;
    }

    for(int i = 0; i < byte_size; i++)
    {
        num_buf = num;
        upper_cut = num;

        for(int j = 0; j < byte_size - i; j++)
        {
            upper_cut /= 0x100;
        }

        for(int j = 0; j < byte_size - i; j++)
        {
            upper_cut *= 0x100;
        }

        num_buf = num - upper_cut;

        for(int j = 0; j < byte_size - 1 - i; j++)
        {
            num_buf /= 0x100;
        }

        inv[i] = num_buf;

    }

    for(int i = 0; i < 8; i++)
    {
        if(i < 8 - byte_size)
        {
            *(oct + i) = 0;
        }
        else
        {
            *(oct + i) = inv[i - (8 - byte_size)];
        }
    }
}