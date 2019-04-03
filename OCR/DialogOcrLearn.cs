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
        private bool IsStartLearning = false;
        

        public DialogOcrLearn(Mat sample)
        {
            InitializeComponent();
            this.Sample = sample;

            IsStartLearning = true;
            Thread t = new Thread(StartLearning);
            t.Start();

            vidOcrLearnImage.Enabled = true;

            for (int i = 0; i < 10; i++)
                comboBoxSelectChar.Items.Add(i.ToString());
            for (int i = 0x41; i < 0x5B; i++)
                comboBoxSelectChar.Items.Add(Convert.ToChar(i));

        }
        private void StartLearning()
        {
            vidOcrLearnImage.EnableRectCommonRoi = true;
            vidOcrLearnImage.EnableZoom = true;
            while(IsStartLearning)
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
            if (!Directory.Exists("OCR"))
                Directory.CreateDirectory("OCR");
            Mat m = new Mat(Sample, vidOcrLearnImage.GetCommonRectangle());
            //CvInvoke.Imwrite()
        }
    }
}
