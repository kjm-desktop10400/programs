#include<stdio.h>
#include<stdlib.h>

void RotWords(char* obj)                        //与えられた文字列を1byteローテーションする。RotR()から複数回呼び出すことを想定。
{
    char* buf = (char*)malloc(sizeof(char) * 4);
    
    for(int i = 0; i < 4; i++)
    {
        *(buf + i) = *(obj + i);
    }

    *(obj + 0) = *(buf + 3);
    *(obj + 1) = *(buf + 0);
    *(obj + 2) = *(buf + 1);
    *(obj + 3) = *(buf + 2);
}

void ShiftWords(char* obj)                      //与えられた文字列を1byteシフトする。ShiftR()から複数回呼び出すことを想定。
{
    char* buf = (char*)malloc(sizeof(char) * 4);
    
    for(int i = 0; i < 4; i++)
    {
        *(buf + i) = *(obj + i);
    }

    *(obj + 0) = 0;
    *(obj + 1) = *(buf + 0);
    *(obj + 2) = *(buf + 1);
    *(obj + 3) = *(buf + 2);
}

void RotR(char* obj, int arg)                   //与えられた4 bytesのbit列objをarg bit分だけ右にローテーションする。
{
    unsigned char* buf = (char*)malloc(sizeof(char) * 4);
    unsigned char tmp[4] = {0, 0, 0, 0};
    unsigned char mask = 255;
    int mod8 = arg % 8;

    //objをbufにコピー
    for(int i = 0; i < 4; i++)
    {
        *(buf + i) = *(obj + i);
    }

    //bufをbyte単位でローテンション。
    for(int i = 0; i < arg / 8; i++)
    {
        RotWords(buf);
    }

    //移動するbit数分のマスク
    mask >>= 8 - mod8;

    //tmp[i]に、前からi byte目の押し出されるbitを保存
    for(int i = 0; i < 4; i++)
    {
        tmp[i] = mask & *(buf + i);
    }

    //各byteで右シフト
    for(int i = 0; i < 4; i++)
    {
        *(buf + i) >>= mod8;
    }

    //押し出されたbitを先頭に移動
    for(int i = 0; i < 4; i++)
    {
        tmp[i] <<= (8 - mod8);
    }

    //押し出されたbitを次の1byteの先頭に付加。最後の1byteの押し出されたbitは先頭の1byteの頭につける
    *(buf + 0) |= tmp[3];
    *(buf + 1) |= tmp[0];
    *(buf + 2) |= tmp[1];
    *(buf + 3) |= tmp[2];

    for(int i = 0; i < 4; i++)
    {
        *(obj + i) = *(buf + i);
    }

}

void ShiftR(unsigned char* obj, int arg)        //与えられた4 bytesのbit列objをarg bit分だけ右にシフトする。
{
    unsigned char* buf = (char*)malloc(sizeof(char) * 4);
    unsigned char tmp[4] = {0, 0, 0, 0};
    unsigned char mask = 255;
    int mod8 = arg % 8;

    //objをbufにコピー
    for(int i = 0; i < 4; i++)
    {
        *(buf + i) = *(obj + i);
    }

    //bufをbyte単位でシフト。
    for(int i = 0; i < arg / 8; i++)
    {
        ShiftWords(buf);
    }

    //移動するbit数分のマスク
    mask >>= 8 - mod8;

    //tmp[i]に、前からi byte目の押し出されるbitを保存
    for(int i = 0; i < 4; i++)
    {
        tmp[i] = mask & *(buf + i);
    }

    //各byteで右シフト
    for(int i = 0; i < 4; i++)
    {
        *(buf + i) >>= mod8;
    }

    //押し出されたbitを先頭に移動
    for(int i = 0; i < 4; i++)
    {
        tmp[i] <<= (8 - mod8);
    }

    //押し出されたbitを次の1byteの先頭に付加。最後の1byteの押し出されたbitはカットする。
    *(buf + 1) |= tmp[0];
    *(buf + 2) |= tmp[1];
    *(buf + 3) |= tmp[2];

    for(int i = 0; i < 4; i++)
    {
        *(obj + i) = *(buf + i);
    }
}