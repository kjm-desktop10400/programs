#include<stdio.h>
#include<C:\Program Files\OpenSSL-Win64\include\openssl\sha.h>

int compute_sha256(unsigned char * src, unsigned int src_len, unsigned char * buffer) {
  SHA256_CTX c;
  SHA256_Init(&c);
  SHA256_Update(&c, src, src_len);
  SHA256_Final(buffer, &c);
  return 0;
}

int main (void)
{

    char msg[] = {"0"};

    char buf[9];

    for(int i = 0; i < 9; i++)
    {
        buf[i] = '\0';
    }

    compute_sha256(msg, 1, buf);

    printf("msg : %s\n=>%s\n", msg, buf);

    return 0;
}