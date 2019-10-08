using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

//処理直前の画像のデータを記録する
namespace ImageChange
{
    class Before_Bitmap
    {
        public Bitmap beforeBitmapChange(byte[,] imagebefore)
        {
            Bitmap afterBitmap = new Bitmap(256,256);

            for(int i = 0; i < 256; i++)
            {
                for(int j = 0; j < 256; j++)
                {
                    afterBitmap.SetPixel(
                        i,
                        j,
                        Color.FromArgb(
                            (int)imagebefore[j, i],
                            (int)imagebefore[j, i],
                            (int)imagebefore[j, i]
                            ));
                }
            }

            return afterBitmap;

        }
    }
}
