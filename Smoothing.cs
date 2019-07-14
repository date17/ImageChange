using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//平滑化
namespace ImageChange
{
    class Smoothing
    {
        public int[,] smoothing_click(byte[,] data)
        {
            int[,] smooth = new int[,]
            {
                {1,1,1},
                {1,1,1},
                {1,1,1}
            };

            int X, Y;
            int[,] kekka_smooth = new int[256, 256];
            const int SIZE = 256;
            int total = 0;

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    total = 0;

                    for(int Xsmooth = -1; Xsmooth < 2; Xsmooth++)
                    {
                        for(int Ysmooth = -1; Ysmooth < 2; Ysmooth++)
                        {
                            X = Xsmooth + i;
                            Y = Ysmooth + j;

                            if(X < 0)
                            {
                                X = 0;
                            }

                            if (X > SIZE - 1)
                            {
                                X = SIZE - 1;
                            }

                            if(Y  < 0)
                            {
                                Y = 0;
                            }

                            if(Y > SIZE - 1)
                            {
                                Y = SIZE - 1;
                            }

                            total += smooth[Xsmooth + 1, Ysmooth + 1] * data[X, Y];
                        }
                    }

                    kekka_smooth[i, j] = total / 9;

                }
            }

            return kekka_smooth;
        }
    }
}
