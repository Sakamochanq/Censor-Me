using System.Drawing;
using System.Windows.Forms;

namespace Censor_Me
{
    public partial class Source : Form
    {
        public Source()
        {
            InitializeComponent();
            this.RunButton.Enabled = false;
        }

        private void OpenButton_Click(object sender, System.EventArgs e)
        {
            using(var ofd = new OpenFileDialog() { Filter = "Windows画像形式(*.bmp *.jpg *.jpeg *.png *.emf) | *.bmp; *.jpg; *.jpeg; *.png; *.emf" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    MainPictureBox.Image = Image.FromFile(ofd.FileName);
                    this.RunButton.Enabled = true;
                }
            }
        }
    }
}
