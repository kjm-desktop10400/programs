#include<stdio.h>
#include<string.h>

int main(void)
{

    FILE* pipe = popen(".\\SHA-256.exe 0", "r");

    char buf[65];
    for(int i = 0; i < strlen(buf); i++)
    {
        buf[i] = '\0';
    }

    fgets(buf, 65, pipe);
    
    printf("%s", buf);

    return 0;
}