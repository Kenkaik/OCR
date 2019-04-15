using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using System.Threading;
using System.Xml.Linq;
using System.IO;

namespace OCR
{
    public partial class DialogOcrLearn : Form
    {
        private Mat Sample;
        private string[] OcrFolderName = new string[] {"-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        , "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        private Mat[,] OcrMat= new Mat[37,64];
        bool IsStartLearning = false;


        public DialogOcrLearn(Mat sample)
        {
            InitializeComponent();
            InitialLvCharImage();
            InitialFolder();
            InitialImageFile();
            InitialLiOcrImage();
            comboBoxSelectChar.SelectedIndex = 0;
            lvCharImage.SmallImageList = ilOcrImage;
            int x = ilOcrImage.Images.Count;
            this.Sample = sample;
        }

        private void InitialLiOcrImage()
        {
            foreach (var ocrMat in OcrMat)
            {
                if (ocrMat != null)
                    ilOcrImage.Images.Add(ocrMat.Bitmap);
            }
                
        }

        private void InitialImageFile()
        {
            for (int i=0; i<37; i++)
                for (int j=1; j<65; j++)
                {
                    string path = "OCR/" + OcrFolderName[i] + "/" + j.ToString() + ".jpg";
                    if (System.IO.File.Exists(path))
                        OcrMat[i, j] = CvInvoke.Imread(path, Emgu.CV.CvEnum.ImreadModes.Grayscale);
                }
                    
        }

        private void InitialFolder()
        {
            if (!Directory.Exists("OCR"))
                Directory.CreateDirectory("OCR");
            foreach (string ocrChar in OcrFolderName)
            {
                comboBoxSelectChar.Items.Add(ocrChar);
                string path = "OCR/" + ocrChar;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
        }

        private void InitialLvCharImage()
        {
            lvCharImage.View = View.Details;
            lvCharImage.GridLines = true;
            lvCharImage.LabelEdit = false;
            lvCharImage.FullRowSelect = true;
            lvCharImage.Columns.Add("No", 100);
            lvCharImage.Columns.Add("Image", 100);
            
        }

        private void StartLearning()
        {
            vidOcrLearnImage.EnableRectCommonRoi = true;
            vidOcrLearnImage.EnableZoom = true;
            IsStartLearning = true;
            while (IsStartLearning)
            {
                vidOcrLearnImage.ImageInput(Sample.Clone());
                vidOcrLearnImage.DisplayImage();
                Thread.Sleep(30);
            }
        }

        private void DialogOcrLearn_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsStartLearning = false;
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            Mat m = new Mat(Sample, vidOcrLearnImage.GetCommonRectangle());
            DirectoryInfo dirInfo = new DirectoryInfo("OCR/" + OcrFolderName[comboBoxSelectChar.SelectedIndex]);
            if (dirInfo.GetFiles("*.jpg").Length >= 64)
            {
                MessageBox.Show("Quantity of char has to < 64", "OCR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 1; i<65; i++)
            {
                var path = "OCR/" + OcrFolderName[comboBoxSelectChar.SelectedIndex] + "/" + i.ToString() + ".jpg";
                if (!(System.IO.File.Exists(path)))
                {
                    CvInvoke.Imwrite(path, m);
                    break;
                }   
            }
        }

        private void DialogOcrLearn_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(StartLearning);
            t.Start();
        }

        private void comboBoxSelectChar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
