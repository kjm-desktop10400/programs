#include<stdio.h>
#include<stdlib.h>
#include<math.h>

//関数マクロ　絶対値を返す 
#define abs(x) ((x) < 0 ? (-1 * x) : (x))

FILE* input;
FILE* output;
FILE* output_check;

int main(int argc, char *argv[]){   //コマンドライン引数で読み込みファイルを指定

    double coefficent = 0;
    double exponent = 0;
    double buf = 0;
    char line[100];
    char judge = 0;
    int char_count = 0;

    //リンクリストの宣言
    struct data{
        double time = 0;
        double amplitude = 0;
        data* before = NULL;
        data* after = NULL;
        bool sign = 0;
    };

    data* current;
    data* first;

    for(int i = 0; i < 100; i++)
        line[i] = '\0';

    //データファイルの読み込み
    input = fopen(argv[1], "r");

    //書き込みファイルの準備
    output_check = fopen("zero_cross_check.txt", "w");
    output = fopen("zero_cross_time.txt", "w");

    //リストにcsvを読み込み
    current = (data*)malloc(sizeof(data));
    first = current;
    char_count = 0;
    while(1){

        line[char_count] = fgetc(input);

        if(line[char_count] == EOF){
            break;
        }

        //仮数部を検出
        if(line[char_count] == 'e'){
            line[char_count] = '\0';
            coefficent = atof(line);
            char_count = 0;
            continue;
        }

        //timeの指数部を抽出、timeに代入
        if((line[char_count] == ' ') || (line[char_count] == ',') || (line[char_count] == '\t')){
            line[char_count] = '\0';
            exponent = atof(line);
            current->time = coefficent * pow(10, exponent);
            char_count = 0;
            continue;
        }

        //amplitudeの指数部を抽出、amplitudeに代入
        if(line[char_count] == '\n'){
            line[char_count] = '\0';
            exponent = atof(line);
            current->amplitude = coefficent * pow(10, exponent);
            char_count = 0;

            //amplitudeの符号ごとにsignを分ける
            if(current->amplitude < 0){
                current->sign = false;
            }
            if(current->amplitude >= 0){
                current->sign = true;
            }

            //printf("%lf\t%lf\n", current->time, current->amplitude);

            //次のリストの用意
            current->after = (data*)malloc(sizeof(data));
            current->after->before = current;
            current = current->after;
            current->after = NULL;

            continue;
        }

        char_count++;
    }

    char_count = 0;
    current = first;

    //リンクリストから符号が入れ替わる場所を検出
    while(1){
        
        if(current->after == NULL){
            break;
        }

        fprintf(output_check, "%lf %lf", current->time, current->amplitude);

        if(((current->sign == true) && (current->after->sign == false)) || ((current->sign == false) && (current->after->sign == true))){
        //    fprintf(output, "%lf 0\n", current->time);
            fprintf(output_check, "\t*****");
            //前後の時刻を振幅の絶対値で内分
            fprintf(output, "%.32lf\n", current->time + (current->after->time - current->time) * abs(current->amplitude / (current->after->amplitude - current->amplitude)));
        }
        putc('\n', output_check);

        current = current->after;
    }

    //ファイルのクローズ
    fclose(output);
    fclose(input);
    fclose(output_check);

    return 0;
}