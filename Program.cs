using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;




namespace ImageChange
{

    class Program
    {


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        class Form1 : Form
        {
            List<string> progress = new List<string>(); //処理内容を追加していく

            const int SIZE = 256; //画像の縦横の長さ（変えない）

            Change change;　//Changeクラスのオブジェクトを作成（引数はパス）

            byte[,] imagepix; //引数をパスとして生成したChangeクラスのオブジェクトでimagepixメソッドを呼び出す。そして格納。

            byte[,] buffer; //元画像のデータを格納しておく（初期化に対応させるため）

            Bitmap bitmapraw = new Bitmap(SIZE, SIZE);  //画像の画素をセットしてpictureBox1に格納される。

            Average averages = new Average();                                  //平均値を求めるクラスAverageのインスタンスを生成
            StandardDiviation standardDiviations = new StandardDiviation();    //標準偏差を求めるクラスStandardDvisionクラスのインスタンスを生成


            TextBox passName; //画像のパスを代入する（raw）
            Button getImage;  //画像を作成する（raw to bmp）

            Label imageTitle = new Label(); //画像のタイトル

            PictureBox pictureBox1 = new PictureBox(); //画像を表示

            Button average = new Button(); //平均値
            Button hyoujun = new Button(); //標準偏差
            ComboBox autocorrelation; //自己相関関数（水平、垂直、斜め左上）

            Button sobel = new Button(); //ソーベルフィルタ
            Button laplacian = new Button(); //ラプラシアンフィルタ
            Button smoothing = new Button(); //平滑化フィルタ
            Button gaussian = new Button(); //ガウシアンフィルタ
            Button sharpening = new Button(); //鮮鋭化フィルタ

            Button binary = new Button(); //二値化
            TextBox binaryThreshold = new TextBox(); //二値化の閾値を決めるテキストボックス
            Label binaryName; //二値化の閾値を決めるテキストボックスの位置を示す

            Button kaityou = new Button(); //変換グラフを用いた階調変換
            TextBox ganma = new TextBox(); //変換グラフを用いた階調変換をする際のγの値を決める
            Label ganmaName;               //γの値を入力するテキストボックスの位置を示す

            Button initialize = new Button(); //初期化

            Button saveImage; //画像の保存
            TextBox saveImageName; //画像を保存する時にしたい名前を入力するテキストボックス
            Label saveNameBox; //画像を保存するときの名前を入力するテキストボックスの位置を示す

            TextBox progressImage; //処理の経過を格納するテキストボックス

            Button histgram; //ヒストグラムを表示するボタン



            public Form1()
            {

                Text = "画像処理";　//フォームのタイトル
                ClientSize = new Size(1000, 900);  //フォームのクライアント領域のサイズ


                //画像のパスを代入するテキストボックス
                passName = new TextBox()
                {
                    Location = new Point(20, 400),
                    Size = new Size(500, 20),
                    Multiline = false,
                };

                //画像のパスを用いて画像を作成する
                getImage = new Button
                {
                    Text = "画像作成",
                    Location = new Point(540, 400),
                    Size = new Size(100, 20),
                };
                getImage.Click += new EventHandler(GetImage_click);


                //元画像
                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
                pictureBox1.Location = new Point(322, 20);
                pictureBox1.Size = new Size(256, 256);
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;


                //元画像のタイトル
                imageTitle.Text = "画像はまだ生成されていません";
                imageTitle.Location = new Point(322, 5);
                imageTitle.AutoSize = true;

                //平均値を求めるボタン
                average.Text = "平均値";
                average.Location = new Point(20, 600);
                average.Size = new Size(100, 20);
                average.Click += new EventHandler(Average_click);

                //標準偏差を求めるボタン
                hyoujun.Text = "標準偏差";
                hyoujun.Location = new Point(140, 600);
                hyoujun.Size = new Size(100, 20);
                hyoujun.Click += new EventHandler(Hyoujun_click);

                //ソーベルフィルタをかけるボタン
                sobel.Text = "ソーベル";
                sobel.Location = new Point(20, 630);
                sobel.Size = new Size(100, 20);
                sobel.Click += new EventHandler(Sobel_click);

                //ラプラシアンフィルタをかけるボタン
                laplacian.Text = "ラプラシアン";
                laplacian.Location = new Point(140, 630);
                laplacian.Size = new Size(100, 20);
                laplacian.Click += new EventHandler(Laplacian_click);

                //平滑化フィルタをかけるボタン
                smoothing.Text = "平滑化";
                smoothing.Location = new Point(260, 630);
                smoothing.Size = new Size(100, 20);
                smoothing.Click += new EventHandler(Smoothing_click);

                //ガウシアンフィルタをかけるボタン
                gaussian.Text = "ガウシアン";
                gaussian.Location = new Point(380, 630);
                gaussian.Size = new Size(100, 20);
                gaussian.Click += new EventHandler(Gaussian_click);

                //鮮鋭化フィルタをかけるボタン
                sharpening.Text = "鮮鋭化";
                sharpening.Location = new Point(500, 630);
                sharpening.Size = new Size(100, 20);
                sharpening.Click += new EventHandler(Sharpening_click);

                //二値化をするボタン
                binary.Text = "二値化";
                binary.Location = new Point(620, 630);
                binary.Size = new Size(100, 20);
                binary.Click += new EventHandler(Binary_click);

                //二値化をするときに用いる閾値を設定するテキストボックス
                binaryThreshold.Location = new Point(780, 630);
                binaryThreshold.Size = new Size(100, 20);

                //二値化の閾値を入力するテキストボックスを示すラベル
                binaryName = new Label()
                {
                    Text = "閾値：",
                    Location = new Point(740, 635),
                    AutoSize = true
                };


                //変換グラフを用いる処理を実行するボタン
                kaityou.Text = "変換グラフ";
                kaityou.Location = new Point(20, 660);
                kaityou.Size = new Size(100, 20);
                kaityou.Click += new EventHandler(Kaityou_click);

                //γの値を決めるテキストボックス
                ganma.Location = new Point(170, 660);
                ganma.Size = new Size(100, 20);
                ganma.Multiline = false;

                //γの値を入力するテキストボックスの位置を示すラベル
                ganmaName = new Label()
                {
                    Text = "γ：",
                    Location = new Point(140, 665),
                    AutoSize = true
                };

                //初期化をするボタン
                initialize.Text = "初期化";
                initialize.Location = new Point(20, 690);
                initialize.Size = new Size(100, 20);
                initialize.Click += new EventHandler(Initialize_click);

                //自己相関を求めるコンボボックス
                autocorrelation = new ComboBox()
                {
                    Text = "自己相関関数",
                    Location = new Point(260, 600),
                    Size = new Size(100,20),
               
                };

                //自己相関を求めるコンボボックスの項目を追加
                autocorrelation.Items.Add("水平方向");
                autocorrelation.Items.Add("垂直方向");
                autocorrelation.Items.Add("斜め左上");
                autocorrelation.SelectedIndexChanged += new EventHandler(Autocorrelation_click);

                //画像の保存をする
                saveImage = new Button()
                {
                    Text = "画像の保存",
                    Location = new Point(20, 720),
                    Size = new Size(100, 20),
                };
                saveImage.Click += new EventHandler(SaveImage_click);

                //保存する画像の名前を決める
                saveImageName = new TextBox()
                {
                    Location = new Point(180, 720),
                    Size = new Size(200, 20),
                    Multiline = false
                };

                //保存する画像の名前を決めるテキストボックスを示すラベル
                saveNameBox = new Label()
                {
                    Text = "保存名：",
                    Location = new Point(130, 725),
                    AutoSize = true
                };

                //画像の処理を記録する
                progressImage = new TextBox()
                {
                    Text = "処理経過",
                    Location = new Point(650, 20),
                    Size = new Size(200, 256),
                    ScrollBars = ScrollBars.Vertical,
                    Multiline = true,
                };

                //ヒストグラムを表示するボタン
                histgram = new Button()
                {
                    Text = "ヒストグラム",
                    Location = new Point(20, 750),
                    Size = new Size(100, 20)
                };

                histgram.Click += new EventHandler(Histgam_click);



                Controls.Add(passName);
                Controls.Add(getImage);
                Controls.Add(pictureBox1);
                Controls.Add(imageTitle);
                Controls.Add(average);
                Controls.Add(hyoujun);
                Controls.Add(sobel);
                Controls.Add(laplacian);
                Controls.Add(smoothing);
                Controls.Add(initialize);
                Controls.Add(gaussian);
                Controls.Add(sharpening);
                Controls.Add(binary);
                Controls.Add(binaryThreshold);
                Controls.Add(binaryName);
                Controls.Add(autocorrelation);
                Controls.Add(saveImage);
                Controls.Add(saveImageName);
                Controls.Add(saveNameBox);
                Controls.Add(progressImage);
                Controls.Add(kaityou);
                Controls.Add(ganma);
                Controls.Add(ganmaName);
                Controls.Add(histgram);
            }


            //画像を作成する
            void GetImage_click(object sender, EventArgs e)
            {
                //「""」がパスの中に含まれているときの「""」の除去
                var provisionalPass = passName.Text.Split('"');
                if (provisionalPass.Length > 1)
                {
                    MessageBox.Show("パスの中に「\"」が含まれています。\nパスの中から「\"」を取り除いてください");
                }
                else
                {
                    //テキストボックスに書かれた画像のパスを代入
                    var pass = passName.Text;



                    change = new Change(pass); //Changeクラスのオブジェクトを作成（引数はパス）

                    imagepix = change.imagepix(); //上記で作成したChangeクラスのインスタンスでimagepixメソッドを呼び出す。そして格納。

                    buffer = new Change(pass).imagepix(); //元画像のデータを格納しておく（初期化に対応させるため）

                    pictureBox1.Image = change.changeRaw(); //生成したインスタンスでchangeRawメソッドを呼び出し、picureBoxの画像に代入。

                    imageTitle.Text = "元画像"; //画像のタイトル
                }

            }


  

            //平均値を求めるメソッド
            void Average_click(object sender, EventArgs e)
            {
                int total = 0;
                double average = 0;
                foreach (var i in imagepix)
                {
                    total += i;
                }

                average = total / imagepix.Length;

                MessageBox.Show("画像の平均値は　" + average + "です");
            }

            //標準偏差を求めるメソッド
            void Hyoujun_click(object sender, EventArgs e)
            {
                int total = 0;
                double average = 0;
                double aveSabunn = 0;
                double sabunn_zyo = 0;
                double hyoujunnhennsa = 0;

                foreach (var i in imagepix)
                {
                    total += i;
                }

                average = total / imagepix.Length;

                foreach (var i in imagepix)
                {
                    aveSabunn += Math.Pow((double)(i - average), 2.0);
                }

                sabunn_zyo = aveSabunn / imagepix.Length;

                hyoujunnhennsa = Math.Sqrt(sabunn_zyo);

                MessageBox.Show("画像の標準偏差は　" + hyoujunnhennsa + "です");


            }

            //ソーベルフィルタ
            void Sobel_click(object sender, EventArgs e)
            {
                Sobel sobels = new Sobel();

                var kekka_sobel = sobels.sobel_click(imagepix);

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        bitmapraw.SetPixel(
                       i,
                       j,
                       Color.FromArgb(
                           (int)kekka_sobel[j, i],
                           (int)kekka_sobel[j, i],
                           (int)kekka_sobel[j, i])
                       );

                        imagepix[i, j] = (byte)kekka_sobel[i, j];
                    }
                }

                pictureBox1.Image = bitmapraw;
                imageTitle.Text = "ソーベルフィルタ適用";
                progress.Add("ソーベルフィルタ適用");

                var text = "";
                foreach(var i in progress)
                {
                    text += i;
                    text += "\r\n";
                }
                progressImage.Text = text;


            }

            //ラプラシアンフィルタを適用
            void Laplacian_click(object sender, EventArgs e)
            {
                Laplacian laplacians = new Laplacian();

                var kekka_lap = laplacians.laplacian_click(imagepix);

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        bitmapraw.SetPixel(
                       i,
                       j,
                       Color.FromArgb(
                           (int)kekka_lap[j, i],
                           (int)kekka_lap[j, i],
                           (int)kekka_lap[j, i])
                       );

                        imagepix[i, j] = (byte)kekka_lap[i, j];
                    }
                }

                pictureBox1.Image = bitmapraw;
                imageTitle.Text = "ラプラシアンフィルタ適用";
                progress.Add("ラプラシアンフィルタ適用");

                var text = "";
                foreach (var i in progress)
                {
                    text += i;
                    text += "\r\n";
                }
                progressImage.Text = text;

            }

            //平滑化フィルタを適用
            void Smoothing_click(object sender, EventArgs e)
            {
                Smoothing smoothings = new Smoothing();

                var kekka_smoothing = smoothings.smoothing_click(imagepix);

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        bitmapraw.SetPixel(
                       i,
                       j,
                       Color.FromArgb(
                           (int)kekka_smoothing[j, i],
                           (int)kekka_smoothing[j, i],
                           (int)kekka_smoothing[j, i])
                       );

                        imagepix[i, j] = (byte)kekka_smoothing[i, j];
                    }
                }

                pictureBox1.Image = bitmapraw;
                imageTitle.Text = "平滑化フィルタ適用";
                progress.Add("平滑化フィルタ適用");

                var text = "";
                foreach (var i in progress)
                {
                    text += i;
                    text += "\r\n";
                }
                progressImage.Text = text;

            }

            //ガウシアンフィルタを適用
            void Gaussian_click(object sender, EventArgs e)
            {
                Gaussian gaussians = new Gaussian();

                var kekka_gaussian = gaussians.gaussian_click(imagepix);

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        bitmapraw.SetPixel(
                       i,
                       j,
                       Color.FromArgb(
                           (int)kekka_gaussian[j, i],
                           (int)kekka_gaussian[j, i],
                           (int)kekka_gaussian[j, i])
                       );

                        imagepix[i, j] = (byte)kekka_gaussian[i, j];
                    }
                }

                pictureBox1.Image = bitmapraw;
                imageTitle.Text = "ガウシアンフィルタ適用";
                progress.Add("ガウシアンフィルタ適用");

                var text = "";
                foreach (var i in progress)
                {
                    text += i;
                    text += "\r\n";
                }
                progressImage.Text = text;

            }

            //鮮鋭化フィルタを適用
            void Sharpening_click(object sender, EventArgs e)
            {
                Sharpening sharpenings = new Sharpening();

                var kekka_sharpening = sharpenings.sharpening_click(imagepix);

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        bitmapraw.SetPixel(
                       i,
                       j,
                       Color.FromArgb(
                           kekka_sharpening[j, i],
                           kekka_sharpening[j, i],
                           kekka_sharpening[j, i])
                       );

                        imagepix[i, j] = (byte)kekka_sharpening[i, j];

                    }
                }

                pictureBox1.Image = bitmapraw;
                imageTitle.Text = "鮮鋭化フィルタ適用";
                progress.Add("鮮鋭化フィルタ適用");

                var text = "";
                foreach (var i in progress)
                {
                    text += i;
                    text += "\r\n";
                }
                progressImage.Text = text;

            }

            //二値化をする
            void Binary_click(object sender, EventArgs e)
            {
                var value = binaryThreshold.Text;

                if(value.Length < 1)
                {
                    MessageBox.Show("閾値を設定してください。");
                }
                else
                {
                    var sikiiValue = int.Parse(value);

                    Binarization binary = new Binarization();

                    var kekka_binary = binary.Binary_click(imagepix, sikiiValue);

                    for(int i = 0; i < SIZE; i++)
                    {
                        for(int j = 0; j < SIZE; j++)
                        {
                            bitmapraw.SetPixel(
                                i,
                                j,
                                Color.FromArgb(
                                    kekka_binary[j, i],
                                    kekka_binary[j, i],
                                    kekka_binary[j, i]
                            ));

                            imagepix[i, j] = (byte)kekka_binary[i, j];
                            
                        }
                    }

                    pictureBox1.Image = bitmapraw;
                    imageTitle.Text = "二値化";

                    progress.Add("二値化処理(閾値 = " + value + ")");

                    var text = "";

                    foreach (var i in progress)
                    {
                        text += i;
                        text += "\r\n";
                    }

                    progressImage.Text = text;



                }
            }

            //自己相関を求める
            void Autocorrelation_click(object sender, EventArgs e)
            {
                Autocorrelation autocorrelations = new Autocorrelation();

                if(autocorrelation.SelectedIndex == 0)
                {
                    var auto = autocorrelations.autocorrelation_click_side(imagepix);

                    MessageBox.Show("水平方向の自己相関は" + auto + "です。");
                }
                
                if(autocorrelation.SelectedIndex == 1)
                {
                    var auto = autocorrelations.autocorrelation_click_virtical(imagepix);

                    MessageBox.Show("垂直方向の自己相関は" + auto + "です。");
                }

                if(autocorrelation.SelectedIndex == 2)
                {
                    var auto = autocorrelations.autocorrelation_click_diagonal(imagepix);

                    MessageBox.Show("斜め左上方向の自己相関は" + auto + "です。");
                }
            }


            //変換グラフを用いた階調変換
            void Kaityou_click(object sender, EventArgs e)
            {
                 if (ganma.Text.Length != 0)
                 {
                    var gannma = double.Parse(ganma.Text);

                    Kaityou kaityous = new Kaityou();
               
                    var kekka_kaityou = kaityous.kaityou_click(imagepix, gannma);

                    for (int i = 0; i < SIZE; i++)
                    {
                        for (int j = 0; j < SIZE; j++)
                        {
                            bitmapraw.SetPixel(
                           i,
                           j,
                           Color.FromArgb(
                               kekka_kaityou[j, i],
                               kekka_kaityou[j, i],
                               kekka_kaityou[j, i])
                           );

                            imagepix[i, j] = (byte)kekka_kaityou[i, j];

                        }
                    }

                    pictureBox1.Image = bitmapraw;
                    imageTitle.Text = "変換グラフを利用";

                    progress.Add("変換グラフを利用" + "(γ = " + ganma.Text + ")");
                   
                    var text = "";
                    foreach(var i in progress)
                    {
                        text += i;
                        text += "\r\n";
                    }

                     progressImage.Text = text;
                  
                }
                else
                {
                    MessageBox.Show("γの値が設定されていません。\nγの値を設定してください。");
                }
            }



            //画像を初期化する
            void Initialize_click(object sender, EventArgs e)
            {
                byte[,] initializeImage = new byte[SIZE, SIZE];

                for(int i = 0; i < SIZE; i++)
                {
                    for(int j = 0; j < SIZE; j++)
                    {
                        initializeImage[i, j] = buffer[i, j];
                    }
                }

                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        bitmapraw.SetPixel(
                       i,
                       j,
                       Color.FromArgb(
                          (int)initializeImage[j, i],
                          (int)initializeImage[j, i],
                          (int)initializeImage[j, i])
                       );

                        imagepix[i, j] = (byte)initializeImage[i, j];
                       
                    }
                }

                pictureBox1.Image = bitmapraw;
                imageTitle.Text = "元画像";
                progress.Clear();

                progressImage.Text = "初期化";

            }

            //画像を保存するメソッド
            void SaveImage_click(object sender, EventArgs e)
            {
                string saveName = "";
                if(saveImageName.Text.Length == 0)
                {
                    saveName = "newImage";
                }
                else
                {
                    saveName = saveImageName.Text;
                }
                bitmapraw.Save(
                    @"C:\\Users\\date\\Desktop\\" + saveName + ".jpg",
                    System.Drawing.Imaging.ImageFormat.Jpeg
                    );

                MessageBox.Show("画像が保存されました。（保存名 : " + saveName + "）");
            }

            //ヒストグラムを表示するメソッド
            void Histgam_click(object sender, EventArgs e)
            {
                Hisutogram forms = new Hisutogram(imagepix);

                forms.Show();
            }
        }

    }
}
