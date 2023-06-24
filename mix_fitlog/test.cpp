#include<stdio.h>
#include<stdlib.h>

int main(){

    FILE *fp = fopen("1.fitlog", "r");
    FILE *out = fopen("test.out", "w");

    while(!feof(fp)){
        char buf = fgetc(fp);
        fputc(buf, out);
        fputc('@', out);
    }

    return 0;
}