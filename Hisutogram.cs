using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows;
using System.Drawing;

//ヒストグラムを表示するフォーム

namespace ImageChange
{
    class Hisutogram : Form
    {
        Chart chart1; //ヒストグラムを表示するためのChartクラス

        int[] kidoValue = new int[256]; //各輝度値の発生回数を格納

        

        public Hisutogram(byte[,] data)
        {
            Text = "ヒストグラム";
            ClientSize = new System.Drawing.Size(1600, 900);

            int kido = 0; //引数で取得した画像の処理する画素を格納する


            //Chartクラスのインスタンスを生成
            chart1 = new Chart()
            {
                Size = new System.Drawing.Size(1450, 700),
                Location = new System.Drawing.Point(0, 0)
            };




            //Chartコントロール内のグラフ、凡例、目盛り領域を削除
            chart1.Series.Clear();
            chart1.Legends.Clear();
            chart1.ChartAreas.Clear();

            //目盛り領域の設定
            var memori = chart1.ChartAreas.Add("Histgram");

            //x軸
            memori.AxisX.Title = "輝度値";                                //タイトル
            memori.AxisX.Minimum = 0;                                     //最小値
            memori.AxisX.Maximum = 255;                                   //最大値
            memori.AxisX.Interval = 1;                                    //目盛りの間隔
            memori.AxisX.MajorGrid.Enabled = false;                       //X軸の目盛り螺旋をなくす

           

            //y軸
            memori.AxisY.Title = "発生回数";           //タイトル
            memori.AxisY.Minimum = 0;                  //最小値
            memori.AxisY.MajorGrid.Enabled = false;    //Y軸の目盛りの螺旋をなくす

            //データの追加

            //各画素の輝度値の発生回数を取得する
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    kido = (int)data[i, j];
                    for (int x = 0; x < 256; x++)
                    {
                        
                        if (kido == x)
                        {
                            kidoValue[x]++;
                            break;
                        }
                    }
                }
            }

            var hist = new System.Windows.Point[256]; //構造体Point(double x, double y)
           
            for(int i = 0; i < 256; i++)
            {
                hist[i] = new System.Windows.Point((double)i, (double)kidoValue[i]);
            }

            //グラフの系列を追加
            var s = chart1.Series.Add("Histgram");

            //棒グラフに設定
            s.ChartType = SeriesChartType.Column;



            //データの設定
            for(int i = 0; i < hist.Length; i++)
            {
                s.Points.AddXY(hist[i].X, hist[i].Y);
            }

            Controls.Add(chart1);
            
        }
    }
}
