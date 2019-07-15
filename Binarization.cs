using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//二値化
namespace ImageChange
{
    class Binarization
    {
<<<<<<< HEAD
        public int[,] Binary_click(byte[,] data, int siki)
        {
            const int SIZE = 256;
            int[,] kekka_binary = new int[SIZE, SIZE];

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    if(data[i,j] < siki)
                    {
                        kekka_binary[i, j] = 0;
                    }
                    else
                    {
                        kekka_binary[i, j] = 255;
                    }
                }
            }

            return kekka_binary;
        }
=======
>>>>>>> 514173b9de7afb00bd1171dbfcc34dced302085c
    }
}
