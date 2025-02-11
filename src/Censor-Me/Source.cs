using System.Drawing;
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
        }

        private Image Load_Image;
        private Detect detect = new Detect();

        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog() { Filter = "Windows画像形式(*.bmp *.jpg *.jpeg *.png *.emf) | *.bmp; *.jpg; *.jpeg; *.png; *.emf" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Load_Image = Image.FromFile(ofd.FileName);
                    MainPictureBox.Image = Load_Image;
                    this.RunButton.Enabled = true;
                }
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            MainPictureBox.Image = detect.Face(Load_Image);
        }
    }
}
