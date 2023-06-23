#include <iostream>
using namespace std;

//simple power n^e
int pow(double n, int e){
    if(e == 0) return 1;
    else if(e > 0){
        for(int i = 0; i < e; i++) n *= n;
    }
    else if(e < 0){
        for(int i = 0; i >= e; i++) n /= n;
    }
    return n;
}

class algebra{
    private:
        double cofficient;
        char char_list[10];
        int exponet_list[10];

        init(){
            for(int i = 0; i < 10; i++){
                char_list[i] = '\0';
                exponet_list[i] = 0;
            }
            cofficient = 0;
        }

    public:
        void set_algebra(char* line);
        void show(){cout<<cofficient;}


          
};

class frac{
    private:
        int numerator;
        int denominator;


};

int main(){

    algebra a;
    a.set_algebra("124");
    a.show();

    return 0;

}

void algebra::set_algebra(char* line){

    int coff = 0;
    char buf[10];
    char unsorted_char_list[10];
    int unsorted_exponet_list[10];
    char sorted_char_list[10];
    int sorted_exponet_list[10];
    
    int i = 0;
    int char_sign = 1;

    for (int i = 0; i < 10; i++)
    {
       unsorted_char_list[i] = buf[i] = sorted_char_list[i] = '\0';
    }
    

    i = 0;
    if(*(line) == '-'){
        char_sign = -1;
        i++;
    }

    while(1){
        if(48 <= *(line + i) && *(line + i) <= 57){
            buf[i] = *(line + i);
            i++;
        }
        else break;
    }

    for(int j = (char_sign == 1 ? 0 : 1); j < i; j++){
        coff += buf[i - j] * pow(10, i -j);
    }

    for(int k = 0; k < 10; k++) buf[k] = '\0';

        if(*(line + i) == '.'){
            int mini = i;
            int j = 0;
            i++;

            while(1){
            if(48 <= *(line + i) && *(line + i) <= 57){
                buf[j] = *(line + i);
                i++;
            }
            else break;

            for(int k = 0; k < j; k++){
                coff += buf[k] * pow(10, -(k + 1));
            }
        }

    }

    this->cofficient = coff * char_sign;


}