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
using System.Diagnostics;
using MRVisionLib;

namespace OCR
{
    public partial class DialogOcrLearn : Form
    {
        private Mat Sample;
        private string[] OcrFolderName = new string[] {"-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        , "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        private Mat[,] OcrMat = new Mat[37,64];
        bool IsStartLearning = false;

        Rectangle RoiArea;
        

        public DialogOcrLearn(Mat sample)
        {
            InitializeComponent();
            InitialFolder();
            InitialOcrImageFileToMat();
            UpdateListViewChar();
            //comboBoxSelectChar.SelectedIndex = 0;
            this.Sample = sample;
        }

        private void DialogOcrLearn_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(StartLearning);
            t.Start();
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

        private void InitialFolder()
        {
            if (!Directory.Exists("OCR"))
                Directory.CreateDirectory("OCR");

            foreach (string ocrChar in OcrFolderName)
            {
                string path = "OCR/" + ocrChar;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }

        }

        private void InitialOcrImageFileToMat()
        {
            for (int i=0; i<OcrMat.GetLength(0); i++)
                for (int j=0; j<OcrMat.GetLength(1); j++)
                {
                    string path = "OCR/" + OcrFolderName[i] + "/" + j.ToString() + ".jpg";
                    if (System.IO.File.Exists(path))
                    {
                        OcrMat[i, j] = CvInvoke.Imread(path, Emgu.CV.CvEnum.ImreadModes.Grayscale);
                    }
                        
                }          
        }

        private void UpdateListViewChar()
        {
            lvChar.BeginUpdate();
            lvChar.Clear();

            lvChar.View = View.Details;
            lvChar.GridLines = true;
            lvChar.LabelEdit = false;
            lvChar.FullRowSelect = true;
            lvChar.Columns.Add("Char", 100);
            lvChar.Columns.Add("Quantity", 100);
            lvChar.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvChar.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            for (int i = 0; i < OcrFolderName.Length; i++)
            {
                var lvi = new ListViewItem(OcrFolderName[i]);
                int quantity = 0;
                for (int j = 0; j < OcrMat.GetLength(1); j++)
                    if (OcrMat[i, j] != null)
                        quantity++;
                lvi.SubItems.Add(quantity.ToString());
                lvChar.Items.Add(lvi);
            }
            lvChar.Refresh();
            lvChar.EndUpdate();
        }

        private void Updatelvimage(int index)
        {

            lvImage.Clear();
            ilOcrImage.Images.Clear();
            ilOcrImage.ImageSize = new Size(80, 80);
            lvImage.LargeImageList = ilOcrImage;

            int imageIndex = 0;
            for (int j = 0; j < OcrMat.GetLength(1); j++)
                if (OcrMat[index, j] != null)
                {
                    ilOcrImage.Images.Add(OcrMat[index, j].Bitmap);
                    lvImage.Items.Add(j.ToString(), imageIndex);
                    imageIndex++;
                }
            lvImage.Refresh();
            lvChar.Items[index].Selected = true;
            lvChar.Items[index].Focused = true;
            lvChar.Items[index].EnsureVisible();
            lvChar.Focus();
        }

        private void DialogOcrLearn_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsStartLearning = false;
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            Mat m = new Mat(Sample, vidOcrLearnImage.GetCommonRectangle());
            DirectoryInfo dirInfo = new DirectoryInfo("OCR/" + OcrFolderName[lvChar.FocusedItem.Index]);
            if (dirInfo.GetFiles("*.jpg").Length >= OcrMat.GetLength(1))
            {
                MessageBox.Show("Quantity of ocr image has to < 64", "OCR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int j = 0; j< OcrMat.GetLength(1); j++)
            {
                var path = "OCR/" + OcrFolderName[lvChar.FocusedItem.Index] + "/" + j.ToString() + ".jpg";
                if (!(System.IO.File.Exists(path)))
                {
                    OcrMat[lvChar.FocusedItem.Index, j] = m;
                    CvInvoke.Imwrite(path, m);
                    break;
                }   
            }

            int index = lvChar.FocusedItem.Index;
            UpdateListViewChar();
            Updatelvimage(index);
            
        }

        private void lvChar_Click(object sender, EventArgs e)
        {        
            Updatelvimage(lvChar.FocusedItem.Index);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string path = "OCR/" + OcrFolderName[lvChar.FocusedItem.Index] + "/" + lvImage.Items[lvImage.FocusedItem.Index].Text + ".jpg";

            System.IO.File.Delete(path);        
            OcrMat[lvChar.FocusedItem.Index, Convert.ToInt32(lvImage.Items[lvImage.FocusedItem.Index].Text)] = null;

            int index = lvChar.FocusedItem.Index;
            UpdateListViewChar();
            Updatelvimage(index);

            buttonDelete.Enabled = false;
        }

        private void lvImage_Click(object sender, EventArgs e)
        {
            buttonDelete.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sure to select ?", "Select ROI Area", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            RoiArea = vidOcrLearnImage.GetCommonRectangle();

            Mat m = new Mat(vidOcrLearnImage.GetSrcImage(), RoiArea);
            pictureBoxRoiArea.Image = m.Bitmap;
        }

        private void buttonDisplayRoiArea_Click(object sender, EventArgs e)
        {
            if (RoiArea.IsEmpty) return;
            Mat m = new Mat(vidOcrLearnImage.GetSrcImage(), RoiArea);
            CvInvoke.Imshow("m", m);
            CvInvoke.WaitKey(1000);
        }


        private string StartOcr()
        {
            SKOpenCV3MatchOCR matcher = new SKOpenCV3MatchOCR();
            MatchPosition[] mpTemp = new MatchPosition[30];

            string ocrResult = string.Empty;

            Mat sample = new Mat(vidOcrLearnImage.GetSrcImage(), RoiArea);


            int count = 0;
            for (int i=0; i<OcrMat.GetLength(0); i++)
                for (int j=0; j<OcrMat.GetLength(1); j++)
                {
                    if (OcrMat[i, j] == null) continue;
                    count++;
                    mpTemp[count] = matcher.MatchWithOption(sample, OcrMat[i, j], SKOpenCV3MatchOCR.AlignAlgorithm.patternMatch);
                    mpTemp[count].Char = Convert.ToChar(OcrFolderName[i]);
                }


            int RecgonizeCharQuantity = 0;

            for(int i=0; i<mpTemp.Length; i++)
            {
                if (mpTemp[i] != null)
                    RecgonizeCharQuantity++;
            }

            

            MatchPosition[] mp = new MatchPosition[RecgonizeCharQuantity];
            int count1 = 0;
            for (int i = 0; i < mpTemp.Length; i++)
                if (mpTemp[i] != null)
                {
                    mp[count1] = mpTemp[i];
                    count1++;
                }
                             
            for (int i = 0; i < mp.Length - 1; i++)
            {
                if (mp[i+1].X < mp[i].X)
                {
                    MatchPosition temp = mp[i];
                    mp[i] = mp[i + 1];
                    mp[i + 1] = temp;
                    i = -1;
                }
            }

            mp.Reverse<MatchPosition>();

            foreach (var ch in mp)
                ocrResult += ch.Char;


            return ocrResult;
        }

        private void buttonStartOcr_Click(object sender, EventArgs e)
        {
            MessageBox.Show(StartOcr());
        }
    }
}
