﻿using System.Drawing;
using System.Windows.Forms;
using System;

namespace Censor_Me
{
    public partial class Source : Form
    {
        public Source()
        {
            InitializeComponent();
            this.RunButton.Enabled = false;

            timer = new Timer();
            timer.Interval = 5000;

            //タイマーイベントを追加
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            StatusLabel.Text = "待機中";
            timer.Stop();
        }

        private string filename;
        private Timer timer;
        private Image Load_Image;
        private Detect detect = new Detect();

        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Filter = "Windows画像形式(*.bmp *.jpg *.jpeg *.png *.emf) | *.bmp; *.jpg; *.jpeg; *.png; *.emf" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filename = ofd.FileName;
                    Load_Image = Image.FromFile(filename);
                    MainPictureBox.Image = Load_Image;
                    this.RunButton.Enabled = true;
                }
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "画像を解析しています... ｜ " + filename;

            //UIの更新
            Application.DoEvents();

            try
            {
                MainPictureBox.Image = detect.Face(Load_Image);
                StatusLabel.Text = "画像の解析が完了しました。";
                timer.Start();
            }
            catch(Exception ex)
            {
                StatusLabel.Text = ex.Message;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
