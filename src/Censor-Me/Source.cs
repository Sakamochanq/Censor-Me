using System.Drawing;
using System.Windows.Forms;
using System;
using System.Drawing.Imaging;
using System.IO;

namespace Censor_Me
{
    public partial class Source : Form
    {
        public Source()
        {
            InitializeComponent();
            this.RunButton.Enabled = false;
            this.ApplyMosaicBox.Enabled = false;
            this.ApplyMosaicBox.Checked = true;

            timer = new Timer();
            timer.Interval = 7000;

            //タイマーイベントを追加
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            StatusLabel.Text = "待機中";
            timer.Stop();
        }

        private bool Is;
        private string filename;
        private Timer timer;
        private Image Load_Image;
        private Detect detect = new Detect();

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (ApplyMosaicBox.Enabled == true)
            {
                ApplyMosaicBox.Enabled = false;
            }

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
            Is = ApplyMosaicBox.Checked;

            //UIの更新
            Application.DoEvents();

            try
            {
                MainPictureBox.Image = detect.Face(Load_Image, Is);
                StatusLabel.Text = "画像の解析が完了しました。";

                if(detect.face == false)
                {
                    ApplyMosaicBox.Enabled = false;
                }
                else
                {
                    ApplyMosaicBox.Enabled = true;
                }

                timer.Start();
            }
            catch(Exception ex)
            {
                StatusLabel.Text = ex.Message;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(MainPictureBox.Image != null)
            {
                using (var sfd = new SaveFileDialog() { Filter = "BitMap形式(*.bmp) | *.bmp; | PNG形式(*.png) | *.png; | JPEG形式(*.jpg *.jpeg) | *.jpg; *.jpeg; | Windowsメタ形式(*.wmf) | *.wmf;" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string FileName = sfd.FileName;

                            switch (Path.GetExtension(FileName).ToLower())
                            {
                                case ".bmp":
                                    MainPictureBox.Image.Save(FileName, ImageFormat.Bmp);
                                    StatusLabel.Text = "画像を保存しました。 | " + FileName;
                                    timer.Start();
                                    break;

                                case ".png":
                                    MainPictureBox.Image.Save(FileName, ImageFormat.Png);
                                    StatusLabel.Text = "画像を保存しました。 | " + FileName;
                                    timer.Start();
                                    break;

                                case ".jpg":
                                case ".jpeg":
                                    MainPictureBox.Image.Save(FileName, ImageFormat.Jpeg);
                                    StatusLabel.Text = "画像を保存しました。 | " + FileName;
                                    timer.Start();
                                    break;
                                case ".wmf":
                                    MainPictureBox.Image.Save(FileName, ImageFormat.Wmf);
                                    StatusLabel.Text = "画像を保存しました。 | " + FileName;
                                    timer.Start();
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("保存できる画像がありません。", "Censor-Me", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ApplyMosaicBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MainPictureBox.Image != null)
            {
                Is = ApplyMosaicBox.Checked;
                MainPictureBox.Image = detect.Face(Load_Image, Is);
            }
        }
    }
}
