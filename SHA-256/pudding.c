#include<stdio.h>
#include<string.h>
#include<stdlib.h>

#define BLOCK_SIZE 64

void itooct(int num, unsigned char* oct);                                   //与えられた数値をしたから埋めた8byteのcharに変換。octにはchar[8]以上のメモリを渡すこと。

//return required byte size to allocate memory
int Size_pudding(char* msg)                                                 //Pudding()で必要となるメモリをあらかじめ計算しておき、Pudding()の呼び出し側でメモリを確保させる。
{
    return ((strlen(msg) * sizeof(char) + 8) / BLOCK_SIZE + 1) * BLOCK_SIZE;
}

//msg : message, msg_size : input message byte size, moddified : address of post pudding message. Call this func before allocate memory with Size_pudding()
void Pudding(char* msg, int msg_size, char* modified)                                     //ブロックサイズが64byteの倍数になるようメッセージをパディングする。modifiedには同プログラムSize_pudding()関数で得られるサイズをmallocにより確保しておくこと。
{

    int block_num = ((msg_size * sizeof(char) + 8) / BLOCK_SIZE + 1);                              //パディング後の合計ブロック数
    char* inblock = (char*)malloc(8 * sizeof(char));                        //inblockの文字列表記。16進数

    unsigned char buf = 0;
    int count = 0;

    //msgのサイズ記録用配列
    itooct(msg_size, inblock);
    
    for(int i = 0; i < BLOCK_SIZE * block_num; i++)
    {
        buf = 0;

        if(i < msg_size)                                                    //messageの内容を書き込み
        {
            buf = *(msg + i);
        }
        else if(i == msg_size)                                              //messageとパディングの区切り文字を挿入
        {
            buf = 0x80;
        }
        else if(BLOCK_SIZE * block_num - 8 <= i)                            //最後の8bytesにメッセージサイズ(bit)を付加
        {
            buf = *(inblock + (i - BLOCK_SIZE * block_num + 8));
        }
        else
        {
            buf = 0;
        }

        *(modified + i) = buf;

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
    //      このようにして16進数2bitづつを切り出す
    //

    int byte_size = 0;
    int num_buf = num * 4;
    int upper_cut = 0;
    unsigned char inv[8];
    num *= 8;

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