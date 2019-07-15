using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ラプラシアン
namespace ImageChange
{
    class Laplacian
    {
        public int[,] laplacian_click(byte[,] data)
        {
            int[,] laplacian = new int[,]
            {
                {0, 0, 0},
                {1, -2,1},
                {0, 0 ,0}
            };

            int[,] kekka_lap = new int[256, 256];
            int total = 0;
            const int SIZE = 256;
            int X = 0;
            int Y = 0;

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    total = 0;

                    for(int Xlap = -1; Xlap < 2; Xlap++)
                    {
                        for (int Ylap = -1; Ylap < 2; Ylap++)
                        {
                            X = i + Xlap;
                            Y = j + Ylap;

                            if (X < 0)
                            {
                                X = 0;
                            }

                            if(X > SIZE - 1)
                            {
                                X = SIZE - 1;
                            }

                            if(Y < 0)
                            {
                                Y = 0;
                            }

                            if(Y > SIZE - 1)
                            {
                                Y = SIZE - 1;
                            }

                            total += laplacian[Xlap + 1, Ylap + 1] * data[X, Y];
                        }
                    }

                    if(total < 0)
                    {
                        total = -total;
                    }

                    if(total > 255)
                    {
                        total = 255;
                    }

                    kekka_lap[i, j] = total;
                }
            }

            return kekka_lap;
        }
    }
}
