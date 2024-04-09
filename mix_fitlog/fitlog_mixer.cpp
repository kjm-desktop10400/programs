#include<stdio.h>
#include<stdlib.h>

FILE *pipe;
FILE *out_file;
FILE *log_file;

int main(int argc, char* argv[]){

    char command[256];

    if(argc == 1){
        printf("usage : fitlog_mixer [input file] <output file name>");
        getc(stdin);
        return 0;
    }

    char buf[256];
    int file_count = 0;

    fprintf(stdout, "%s\n", argv[1]);

    //open pipe
    //
    //  in .exe file, couldn't change directry "cd"
    //  so command whth path
    //
    sprintf(command, "dir %s /b\n", argv[1]);
    pipe = _popen(command, "r");

    //counting .fitlog files. it starts '2'. shortly, countig files start with '2'
    while (!feof(pipe)) {
    		fgets(buf, sizeof(buf), pipe);
            file_count++;
    	}
    _pclose(pipe);

    //.fitlog file include empty line in the end.
    file_count--;

    printf("\"%s\" includes %d .fitlog files.\n", argv[1], file_count);

    // set fp output file and pipe
    for(int i = 0; i < 256; i++){
        command[i] = '\0';
    }

    if(argc == 3)
    {
        sprintf(command, "%s\\..\\%s", argv[1], argv[2]);
    }
    else
    {
        sprintf(command, "%s\\..\\fit_param.dat", argv[1]);
    }    
    out_file = fopen(command, "w");
    char file_name[256];

    for(int i = 0; i < 256; i++){
        file_name[i] = '\0';
        buf[i] = '\0';
    }

    // write output file
    int char_count = 0;
    for(int i = 0; i < file_count; i++){

//        getc(stdin);

        sprintf(file_name, "%s\\%d.fitlog\0", argv[1],  i + 1);
        log_file = fopen(file_name, "r");

        fprintf(stdout, "input file : %s\n", file_name);

        if(log_file == NULL){
            printf("failed open logfile : %s\n", file_name);
            getc(stdin);
            return 0;
        }
        else {
            printf("open log file : %s\n", file_name);
        }

        for(int j = 0; j < 256; j++){
            buf[j] = '\0';
        }

        while(1){

            fgets(buf, sizeof(buf), log_file);

            if(feof(log_file)){
                break;
            }

            //ignore lines starat with '#'
            if(buf[0] == '#'){
                printf("detect \'#\',\n");
                continue;
            }

            //seek number init
            char_count = 0;
            while(buf[char_count] != '='){
                char_count++;
            }
            char_count += 2;

            // write numbers
            bool is_space = false;
            while(1){

                if(buf[char_count] == '\n'){
                    fputs("\t\t", out_file);
                    fprintf(stdout, "\tline feed\n");
                    is_space = false;
                    break;
                }
                else if(buf[char_count] == ' ' || buf[char_count] == '\t')
                {
                    is_space = true;
                    char_count++;
                }
                else{
                    if(is_space)
                    {
                        fputs("\t", out_file);
                        fputc('\t', stdout);
                        is_space = false;
                    }

                    fputc(buf[char_count], out_file);
                    fputc(buf[char_count], stdout);
                    char_count++;
                }

            }

            for(int j = 0; j < 256; j++){
                buf[j] = '\0';
            }

        }

        fputc('\n', out_file);
        
    }
    fclose(out_file);

    fgetc(stdin);

    getc(stdin);
    return 0;
}