using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//鮮鋭化
namespace ImageChange
{
    class Sharpening
    {
        public int[,] sharpening_click(byte[,] data)
        {
            int[,] sharpening = new int[,]
            {
                { 0, -1, 0 },
                { -1, 5, -1 },
                { 0, -1, 0 }
            };

            int[,] kekka_sharpening = new int[256, 256];
            int X;
            int Y;
            const int SIZE = 256;
            int total = 0;

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    total = 0;

                    for (int Xsharp = -1; Xsharp < 2; Xsharp++)
                    {
                        for (int Ysharp = -1; Ysharp < 2; Ysharp++)
                        {
                            X = i + Xsharp;
                            Y = j + Ysharp;

                            if (X < 0)
                            {
                                X = 0;
                            }

                            if (X > SIZE - 1)
                            {
                                X = SIZE - 1;
                            }

                            if (Y < 0)
                            {
                                Y = 0;
                            }

                            if (Y > SIZE - 1)
                            {
                                Y = SIZE - 1;
                            }

                            total += sharpening[Xsharp + 1, Ysharp + 1] * data[X, Y];
                        }
                    }

                    if (total < 0)
                    {
                        total = -total;
                    }

                    if (total > 255)
                    {
                        total = 255;
                    }

                    kekka_sharpening[i, j] = total;
                }
            }

            return kekka_sharpening;
        }
    }
}
