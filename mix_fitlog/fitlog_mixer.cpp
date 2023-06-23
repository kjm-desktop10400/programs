#include<stdio.h>
#include<stdlib.h>

FILE *pi;
FILE *po;

int main(int argc, char* argv[]){

    pi = _popen("dir *.fitlog", "r");
//    pi = _popen("dir", "r");

//    fprintf(pi, " *.fitlog");

    char buf[256];

    while (!feof(pi)) {
    		fgets(buf, sizeof(buf), pi);
    		printf("=> %s", buf);
    	}

    return 0;
}