using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

//処理した画像データと適用したフィルタの名前のオブジェクト
namespace ImageChange
{
    class ImageList
    {
        public byte[,] ImageData { get; set; } //輝度情報
        public string ImageName { get; set; }  //処理の名前
        public Bitmap Picture { get; set; } //画像本体

       

        public ImageList(byte[,] imagedata, string imagename, Bitmap picture)
        {
            ImageData = imagedata;
            ImageName = imagename;
            Picture = picture;
        }
    }
}
