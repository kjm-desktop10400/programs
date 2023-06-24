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

    // write output file
    for(int i = 0; i < file_count; i++){

        getc(stdin);

        sprintf(file_name, "%d.fitlog\0", i + 1);
        log_file = fopen(file_name, "w");

        if(log_file == NULL){
            printf("failed open logfile : %s", file_name);
            return 0;
        }
        else {
            printf("open log file : %s", file_name);
        }

        char buf;

        while(!feof(log_file)){
            buf = getc(log_file);
            fprintf(out_file, "%d", (int)buf);
            fprintf(stdout, "%d", (int)buf);
            getc(stdin);

        }

        printf("finish log file : %s", file_name);

/*

        // set fitlog file name
        sprintf(file_name, "%d.fitlog", i + 1);

        //open fitlog
        log_file = fopen(file_name, "r");
        
        int count = 0;
        while(!feof(log_file)){

            fgets(buf, sizeof(buf), log_file);

            //ignor lines start with '#'
            if(*(buf) == '#'){
                continue;
            }

            //write numbers

            //file count
            fprintf(out_file, "%d", i + 1);

            //looking for start of numbers
            count = 0;
            while(1){
                if(*(buf + count) == '='){
                    count++;
                    break;
                }
                count++;
            }

            //write numberes
            while(1){
                if(*(buf + count) == '/n'){
                    fputc('\t', out_file);
                }
                putc(*(buf + count), out_file);
                count++;
            }

        }

        fputc('\n', out_file);
        fclose(log_file);

    */

    }
    fclose(out_file);

    system("timeout /t 1000 /nobreak");


    return 0;
}