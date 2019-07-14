using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

//平均値
namespace ImageChange
{
    class Average : EventArgs
    {
        const int SIZE = 256;
        int total = 0;
        double average = 0;

        public void Average_click(byte[,] data)
        {
            foreach(var i in data)
            {
                total += i;
            }

            average = total / (SIZE * SIZE);

            MessageBox.Show("平均値は　" + average + "です。");
        }
    }
}
