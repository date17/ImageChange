using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ガウシアンフィルタ
namespace ImageChange
{
    class Gaussian
    {
        public int[,] gaussian_click(byte[,] data)
        {
            int[,] gaussian = new int[,]
            {
                {1,2,1},
                {2,4,2},
                {1,2,1}
            };

            int[,] kekka_gaussian = new int[256, 256];

            int X, Y;
            const int SIZE = 256;
            int total = 0;

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    total = 0;

                    for(int Xgau = -1; Xgau < 2; Xgau++)
                    {
                        for(int Ygau = -1;Ygau < 2; Ygau++)
                        {
                            X = i + Xgau;
                            Y = j + Ygau;

                            if(X < 0)
                            {
                                X = 0;
                            }

                            if(X > SIZE - 1)
                            {
                                X = SIZE - 1;
                            }

                            if( Y < 0)
                            {
                                Y = 0;
                            }

                            if(Y > SIZE - 1)
                            {
                                Y = SIZE - 1;
                            }

                            total += gaussian[Xgau + 1, Ygau + 1] * data[X, Y];
                        }
                    }

                    kekka_gaussian[i, j] = total / 16; 
                }
            }

            return kekka_gaussian;

        }
    }
}
