#pragma warning(disable : 4996)

#include<stdio.h>
#include<stdlib.h>
#include<math.h>

//関数マクロ　絶対値を返す 
#define abs(x) ((x) < 0 ? (-1 * x) : (x))
//関数マクロ　サイズ100の配列を\0で初期化
#define init(x) for(int i = 0; i < 100; i++) *(x + i) = '\0'
//ファイル先頭のスキップする行数
#define SKIP 1

FILE* input;
FILE* output;

int main(int argc, char* argv[]) {

    double before_time = 0;
    double current_time = 0;
    double before_amplitude = 0;
    double current_amplitude = 0;

    char line[100];
    double buf = 0;
    int count = 0;
    bool first = true;
    bool end_flag = false;

    init(line);

    input = fopen("Draft2.txt", "r");

    if (input == NULL) {
        puts("error, couldnt open data file");
        fgetc(stdin);
        return 0;
    }

    

    output = fopen("zero_cross_ver2.txt", "w");

    if (output == NULL) {
        puts("error, couldnt open output file");
        fgetc(stdin);
        return 0;
    }

    for (int i = 0; i < SKIP; i++) {
        while (1) {
            if (fgetc(input) == '\n')
                break;
        }
    }

    count = 0;
    while (1) {

        line[count] = fgetc(input);

        //EOFを読んだら終了フラグを立てる
        if (line[count] == EOF) {
            break;
        }

        //数値か.+-以外を読むまでlineにためる
        if ((48 <= line[count] && line[count] <= 57) || (line[count] == '.') || (line[count] == '+') || (line[count] == '-')) {
            count++;
            continue;
        }

        //eが来たら仮数部をbufにdoubleで代入
        if (line[count] == 'e') {
            line[count] = '\0';
            buf = atof(line);
            count = 0;
            continue;
        }//半角スペース、カンマ、タブで時間を記録
        else if ((line[count] == ' ') || (line[count] == ',') || (line[count] == '\t')) {
            line[count] = '\0';
            if (first) {
                before_time = buf * pow(10, atof(line));
            }
            else {
                current_time = buf * pow(10, atof(line));
            }
            count = 0;
            continue;
        }//改行で振幅を記録、前ステップの振幅と比較し符号が変わっていたらゼロクロス時刻として記録
        else if (line[count] == '\n') {
            line[count] = '\0';
            count = 0;

            if (first) {
                before_amplitude = buf * pow(10, atof(line));
                first = false;
                continue;
            }

            current_amplitude = buf * pow(10, atof(line));

            //ここから比較、記録   
            if ((before_amplitude < 0 && current_amplitude > 0) /*|| (before_amplitude > 0 && current_amplitude < 0)*/) {
                //zero_cross_time = t1 + (t2 - t1) * |t1 / (t2 - t1)|
                fprintf(output, "%.32lf 0\n", before_time + (current_time - before_time) * abs(before_amplitude / (current_amplitude - before_amplitude)));
            }

            before_time = current_time;
            before_amplitude = current_amplitude;

        }

    }

    fclose(input);
    fclose(output);

    return 0;
}