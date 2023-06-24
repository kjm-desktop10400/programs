#include<stdio.h>
#include<stdlib.h>

FILE *pipe;
FILE *out_file;
FILE *log_file;

int main(int argc, char* argv[]){

    char comand[256];

    if(argc == 1){
        printf("nofile input");
        return 0;
    }
    sprintf(comand, "cd %s", argv[1]);
//    printf("cd %s\n", argv[1]);
    system(comand);

    pipe = _popen("dir *.fitlog", "r");

    char buf[256];
    int file_count = 0;


    //counting .fitlog files. it starts '2'. shortly, countig files start with '2'
    while (!feof(pipe)) {
    		fgets(buf, sizeof(buf), pipe);

            if(*(buf) != '2'){
                continue;
            }

            file_count++;

    	}
    _pclose(pipe);

    printf("\"%s\" includes %d .fitlog files.", argv[1], file_count);

    // set fp output file and pipe
    out_file = fopen("fit_param.dat", "w");
    char file_name[256];

    for(int i = 0; i < 256; i++){
        file_name[i] = '\0';
        buf[i] = '\0';
    }

    // write output file
    int char_count = 0;
    char colum[14][256];
    for(int i = 0; i < 14; i++){
        for(int j = 0; j < 256; j++){
            colum[i][j] = '\0';
        }
    }
    for(int i = 0; i < file_count; i++){

        getc(stdin);

        sprintf(file_name, "%d.fitlog\0", i + 1);
        log_file = fopen(file_name, "r");


        if(log_file == NULL){
            printf("failed open logfile : %s\n", file_name);
            return 0;
        }
        else {
            //printf("open log file : %s\n", file_name);
        }

        for(int j = 0; j < 256; j++){
            buf[j] = '\0';
        }

        //ignor lines start with '#'
        int count = 0;
        
    }
    fclose(out_file);

    system("timeout /t 1000 /nobreak");


    return 0;
}