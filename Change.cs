<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

// raw画像の変換
namespace ImageChange
{
    class Change
    {
        int i = 0;
        int j = 0;
        public static string Pass { get; set; }

        BinaryReader br;

        public Change(string pass)
        {
            Pass = pass;
        }

        public Change()
        {
            Pass = null;
        }

        const int XSIZE = 256;  //画像横サイズ
        const int YSIZE = 256;　//画像縦サイズ

        byte[,] date = new byte[XSIZE, YSIZE]; //画像データを格納するためのbyte型の2次元配列

        //bitmap作成のためのBitmapオブジェクトを作成
        Bitmap bitmap = new Bitmap(XSIZE, YSIZE);



        public byte[,] imagepix()
        {
            //BinaryReaderクラスでファイルストリームを読み込むためのオブジェクトを作成
            br = new BinaryReader(File.Open(Pass, FileMode.Open));

            for (i = 0; i < XSIZE; i++)
            {
                for (j = 0; j < YSIZE; j++)
                {
                    date[i, j] = br.ReadByte();
                }
            }
            br.Close();
            return date;
        }

        public Bitmap changeRaw()
        {
            //BinaryReaderクラスでファイルストリームを読み込むためのオブジェクトを作成
            br = new BinaryReader(File.Open(Pass, FileMode.Open));

            for (i = 0; i < XSIZE; i++)
            {
                for (j = 0; j < YSIZE; j++)
                {
                    date[i, j] = br.ReadByte();
                }
            }

            br.Close();

            //配列の値をbitmapオブジェクトのピクセル値に代入
            for (i = 0; i < XSIZE; i++)
            {
                for (j = 0; j < YSIZE; j++)
                {
                    bitmap.SetPixel(
                        i,
                        j,
                        Color.FromArgb(
                            (int)date[j, i],
                            (int)date[j, i],
                            (int)date[j, i])
                        );
                }
            }

           

            return bitmap;

        }

        public Bitmap imageListChange(byte[,] imageDatas)
        {
            Bitmap BitmapDatas = new Bitmap(XSIZE, YSIZE);

            for (i = 0; i < XSIZE; i++)
            {
                for (j = 0; j < YSIZE; j++)
                {

                    BitmapDatas.SetPixel(
                        i,
                        j,
                        Color.FromArgb(
                            (int)imageDatas[j, i],
                            (int)imageDatas[j, i],
                            (int)imageDatas[j, i])
                        );
                }
            }

            return BitmapDatas;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

// raw画像の変換
namespace ImageChange
{
    class Change
    {
        int i = 0;
        int j = 0;
        public static string Pass { get; set; }

        BinaryReader br;

        public Change(string pass)
        {
            Pass = pass;


        }

        const int XSIZE = 256;  //画像横サイズ
        const int YSIZE = 256;　//画像縦サイズ

        byte[,] date = new byte[XSIZE, YSIZE]; //画像データを格納するためのbyte型の2次元配列

        //bitmap作成のためのBitmapオブジェクトを作成
        Bitmap bitmap = new Bitmap(XSIZE, YSIZE);



        public byte[,] imagepix()
        {
            //BinaryReaderクラスでファイルストリームを読み込むためのオブジェクトを作成
            br = new BinaryReader(File.Open(Pass, FileMode.Open));

            for (i = 0; i < YSIZE; i++)
            {
                for (j = 0; j < XSIZE; j++)
                {
                    date[i, j] = br.ReadByte();
                }
            }
            br.Close();
            return date;
        }

        public Bitmap changeRaw()
        {
            //BinaryReaderクラスでファイルストリームを読み込むためのオブジェクトを作成
            br = new BinaryReader(File.Open(Pass, FileMode.Open));

            for (i = 0; i < YSIZE; i++)
            {
                for (j = 0; j < XSIZE; j++)
                {
                    date[i, j] = br.ReadByte();
                }
            }

            br.Close();

            //配列の値をbitmapオブジェクトのピクセル値に代入
            for (i = 0; i < YSIZE; i++)
            {
                for (j = 0; j < XSIZE; j++)
                {
                    bitmap.SetPixel(
                        i,
                        j,
                        Color.FromArgb(
                            (int)date[j, i],
                            (int)date[j, i],
                            (int)date[j, i])
                        );
                }
            }

           

            return bitmap;

        }
    }
}
>>>>>>> origin/master
