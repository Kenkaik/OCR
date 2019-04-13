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
        private string[] OcrFolderName = new string[] {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        , "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        bool IsStartLearning = false;


        public DialogOcrLearn(Mat sample)
        {
            InitializeComponent();
            this.Sample = sample;

            if (!Directory.Exists("OCR"))
                Directory.CreateDirectory("OCR");

            for (int i = 0; i < OcrFolderName.Length; i++)
            {
                comboBoxSelectChar.Items.Add(OcrFolderName[i]);
                string path = "OCR/" + OcrFolderName[i];
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            comboBoxSelectChar.SelectedIndex = 0;

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
            var path = "OCR/" + OcrFolderName[comboBoxSelectChar.SelectedIndex] + "/1.jpg";
            CvInvoke.Imwrite(path, m);
        }

        private void DialogOcrLearn_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(StartLearning);
            t.Start();
        }
    }
}
