using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ImageChange
{
    class ImageAll : Form
    {
        int imageCount = 0; //画像の数をカウントする

        List <Bitmap> images = new List<Bitmap>(); //表示する画像を格納する

        List<string> imageName = new List<string>(); //表示する画像の名前を格納する。

        List<PictureBox> pictureBox = new  List<PictureBox>(); //ピクチャーボックス
        List<Label> label = new List<Label>(); //ラベル

        ComboBox saveImage = new ComboBox(); //保存する画像を選択するコンボボックス 
        TextBox saveImageName = new TextBox(); //保存する画像の名前をにゅうりょくする。

        int widCount;　//横軸調整
        int heiCount;  //縦軸調整


        public ImageAll(List<ImageList> imageLists) //引数はimageListクラス型
        {
            foreach(var picture in imageLists)　
            {
                images.Add(picture.Picture);              //Bitmap型のリストに引数で受け取ったデータ（画像）を格納
                imageName.Add(picture.ImageName);       //string型のリストに因数で受け取ったデータ（名前）を格納
            }

            imageCount = images.Count();


            Text = "全画像";
            ClientSize = new Size(1000, 900);
            AutoScroll = true;

            widCount = 50;
            heiCount = 50;

            for(int i = 0; i < imageCount; i++) //画像と画像名をfor文で回して格納
            {
                pictureBox.Add(new PictureBox
                {
                    Image = images[i],
                    BorderStyle = BorderStyle.None,
                    Location = new Point(widCount, heiCount),
                    Size = new Size(256,256),
                    SizeMode = PictureBoxSizeMode.CenterImage
                });

                var number = i.ToString(); //数値w文字列に変換

                label.Add(new Label　//画像の上につけるラベル（画像番号と名前）
                {
                    Text =number + "：" + imageName[i],
                    Location = new Point(widCount, heiCount- 20),
                    AutoSize = true
                });

                widCount += 300;

                if((i + 1) % 3 == 0)　　　//一列に3つまで画像を並べるため
                {
                    widCount = 50;
                    heiCount += 300;
                }

                saveImage.Items.Add(i);  //画像を格納するたびに、画像保存番号の追加をする。
               
            }


            //コンボボックスのプロパティの設定
            saveImage.Text = "保存する画像の番号";
            saveImage.Location = new Point(20, heiCount + 300);
            saveImage.Size = new Size(20, 130);

            //コンボボックスの選択がされたときに保存のメソッドを呼び出す。
            saveImage.SelectedIndexChanged += new EventHandler(saveImage_click);

            //画像の保存名のテキストボックスのプロパティの設定
            saveImageName.Location = new Point(170, heiCount + 300);
            saveImageName.Size = new Size(150, 20);
            saveImageName.Multiline = false;
            

             for (int i = 0; i < imageCount; i++)
                {
                    Controls.Add(pictureBox[i]);
                    Controls.Add(label[i]);
                }

            Controls.Add(saveImage);
            Controls.Add(saveImageName);


        }

        void saveImage_click(object sender, EventArgs e)
        {
            string saveName;
            if(saveImageName == null)
            {
                saveName = "newImage";
            }
            else
            {
                saveName = saveImageName.Text;
            }

            images[saveImage.SelectedIndex].Save(
                   @"C:\\Users\\date\\Desktop\\" + saveName + ".jpg",
                   System.Drawing.Imaging.ImageFormat.Jpeg
                   );

            MessageBox.Show(saveImage + "という名前で保存しました");
        }
    }
}
