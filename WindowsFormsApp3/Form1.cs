using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Image image;
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    this.pictureBox1.Size = image.Size;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            image = scr(image, image.Width, image.Height, (int)numericUpDown1.Value);
            pictureBox2.Image = image;
        }

        // скремблирование
        Bitmap scr(Bitmap image, int width, int height, int count)
        {
            
            Bitmap scr_img = new Bitmap(width, height);
            for (int k = 0; k < count; k++)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int x1 = (i + j) % width;
                        int y1 = (i + (2 * j)) % width;
                        scr_img.SetPixel(x1, y1, image.GetPixel(i, j));
                    }
                }
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < width; j++)
                        image.SetPixel(i, j, scr_img.GetPixel(i, j));

            }
            return scr_img;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox2.Image);
            image = descr(image, image.Width, image.Height, (int)numericUpDown1.Value);
            pictureBox3.Image = image;
        }
        //дескремблирование
        Bitmap descr(Bitmap image, int width, int height, int count)
        {
            int N = height;
            Bitmap descr_img = new Bitmap(width, height);

            for (int k = 0; k < count; k++)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int x1 = (2 * i - j);
                        int y1 = (-i + j);

                        if (x1 >= 0) x1 = x1 % width;
                        else
                        {
                            x1 = width - ((-x1) % width);
                        }
                        if (y1 >= 0) y1 = y1 % width;

                        else
                        {
                           y1 = width -((-y1 ) % width);
                        }
                        descr_img.SetPixel(x1, y1, image.GetPixel(i, j));
                    }
                }

                for (int i = 0; i < width; i++)
                    for (int j = 0; j < width; j++)                   
                        image.SetPixel(i, j, descr_img.GetPixel(i,j));

            }
            return image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.InitialDirectory = "c:\\";
            savedialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (savedialog.FileName != null)
                    {
                        Bitmap saveBitmap = new Bitmap(pictureBox3.Image);
                        saveBitmap.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }
    }
}