using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageChange
{
    class Kaityou
    {
        public int[,] kaityou_click(byte[,] data, double ganma)
        {
            double beki;
            beki = 1.0 / ganma;

            double bunnbo;

            double beki_kekka;

            double kekka;

            int[,] kekka_kaityou = new int[256, 256];

            const int SIZE = 256;

            for(int i = 0; i < SIZE; i++)
            {
                for(int j = 0; j < SIZE; j++)
                {
                    bunnbo = (double)(data[i, j]) / 255;

                    beki_kekka = Math.Pow(bunnbo, beki);

                    kekka = 255 * beki_kekka;

                    if(kekka < 0)
                    {
                        kekka = 0;
                    }

                    if(kekka > 255)
                    {
                        kekka = 255;
                    }

                    kekka_kaityou[i, j] = (int)kekka;

                   
                }
            }

            return kekka_kaityou;
        }
    }
}
