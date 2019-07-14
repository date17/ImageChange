using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ソーベルフィルタ
namespace ImageChange
{
    class Sobel
    {

        public int[,] sobel_click(byte[,] data)
        {
           

            int[,] sobel = new int[,]{
                    { 1, 0, -1 },
                    { 2, 0, -2 },
                    { 1, 0, -1 }
                }; //ソーベルフィルタのマスク画像の重み


            int[,] sobel_kekka = new int[256, 256]; //ソーベルフィルタを適用した後の
            int total = 0;
            int X = 0;
            int Y = 0;


            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    total = 0;
                    for (int sobelX = -1; sobelX < 2; sobelX++)
                    {
                        for (int sobelY = -1; sobelY < 2; sobelY++)
                        {
                            X = i + sobelX;
                            Y = j + sobelY;

                            if (X < 0)
                            {
                                X = 0;
                            }

                            if (X > 255)
                            {
                                X = 255;
                            }

                            if (Y < 0)
                            {
                                Y = 0;
                            }

                            if (Y > 255)
                            {
                                Y = 255;
                            }

                            total += sobel[sobelX + 1, sobelY + 1] * data[X, Y];
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

                    sobel_kekka[i, j] = (byte)total;
                }
            }

            return sobel_kekka;



        }
    }
}
