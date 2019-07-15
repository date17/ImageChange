using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//自己相関
namespace ImageChange
{
    class Autocorrelation
    {
        //水平方向の自己相関
        public double autocorrelation_click_side(byte[,] data)
        {
            int total = 0;
            double average = 0;
            double aveSabunn = 0;
            double sabunn_zyo = 0;
            double hyoujunnhennsa = 0;
            double beforeAuto = 0;
            double auto = 0;
            int Y = 0;

            const int SIZE = 256;

            foreach (var i in data)
            {
                total += i;
            }

            average = total / data.Length;

            foreach (var i in data)
            {
                aveSabunn += Math.Pow((double)(i - average), 2.0);
            }

            sabunn_zyo = aveSabunn / data.Length;

            hyoujunnhennsa = Math.Sqrt(sabunn_zyo);

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Y = j - 1;

                    if (Y < 0)
                    {
                        Y = 0;
                    }

                    beforeAuto += (data[i, j] - average) * (data[i, Y] - average);

                }
            }

            auto = (beforeAuto / (SIZE * SIZE)) / sabunn_zyo;

            return auto;
        }

        //垂直方向の自己相関
        public double autocorrelation_click_virtical(byte[,] data)
        {
            int total = 0;
            double average = 0;
            double aveSabunn = 0;
            double sabunn_zyo = 0;
            double hyoujunnhennsa = 0;
            double beforeAuto = 0;
            double auto = 0;
            int X = 0;

            const int SIZE = 256;

            foreach (var i in data)
            {
                total += i;
            }

            average = total / data.Length;

            foreach (var i in data)
            {
                aveSabunn += Math.Pow((double)(i - average), 2.0);
            }

            sabunn_zyo = aveSabunn / data.Length;

            hyoujunnhennsa = Math.Sqrt(sabunn_zyo);

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    X = i - 1;

                    if (X < 0)
                    {
                        X = 0;
                    }

                    beforeAuto += (data[i, j] - average) * (data[X, j] - average);

                }
            }

            auto = (beforeAuto / (SIZE * SIZE)) / sabunn_zyo;

            return auto;
        }

        //斜め左上方向の自己相関
        public double autocorrelation_click_diagonal(byte[,] data)
        {
            int total = 0;
            double average = 0;
            double aveSabunn = 0;
            double sabunn_zyo = 0;
            double hyoujunnhennsa = 0;
            double beforeAuto = 0;
            double auto = 0;
            int X = 0;
            int Y = 0;

            const int SIZE = 256;

            foreach (var i in data)
            {
                total += i;
            }

            average = total / data.Length;

            foreach (var i in data)
            {
                aveSabunn += Math.Pow((double)(i - average), 2.0);
            }

            sabunn_zyo = aveSabunn / data.Length;

            hyoujunnhennsa = Math.Sqrt(sabunn_zyo);

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    X = i - 1;
                    Y = j - 1;

                    if(X < 0)
                    {
                        X = 0;
                    }

                    if (Y < 0)
                    {
                        Y = 0;
                    }

                    beforeAuto += (data[i, j] - average) * (data[X, Y] - average);

                }
            }

            auto = (beforeAuto / (SIZE * SIZE)) / sabunn_zyo;

            return auto;
        }

    }
}
