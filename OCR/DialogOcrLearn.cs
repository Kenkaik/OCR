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

namespace OCR
{
    public partial class DialogOcrLearn : Form
    {
        private Mat Sample;
        private string[] OcrFolderName = new string[] {"-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        , "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
        private Mat[,] OcrMat = new Mat[37,64];
        private string[,] OcrImageFilePath = new string[37, 64];
        bool IsStartLearning = false;


        public DialogOcrLearn(Mat sample)
        {
            InitializeComponent();
            InitialFolder();
            InitialOcrImageFileToMat();
            InitialListViewChar();
            //comboBoxSelectChar.SelectedIndex = 0;
            this.Sample = sample;
        }

        private void InitialOcrImageFileToMat()
        {
            for (int i=0; i<37; i++)
                for (int j=0; j<64; j++)
                {
                    string path = "OCR/" + OcrFolderName[i] + "/" + j.ToString() + ".jpg";
                    if (System.IO.File.Exists(path))
                    {
                        OcrMat[i, j] = CvInvoke.Imread(path, Emgu.CV.CvEnum.ImreadModes.Grayscale);
                        OcrImageFilePath[i, j] = path;
                    }
                        
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

        private void InitialListViewChar()
        {
            lvChar.BeginUpdate();
            lvChar.Clear();

            lvChar.View = View.Details;
            lvChar.GridLines = true;
            lvChar.LabelEdit = false;
            lvChar.FullRowSelect = true;
            lvChar.Columns.Add("Char", 100);
            lvChar.Columns.Add("Quantity", 100);

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
            DirectoryInfo dirInfo = new DirectoryInfo("OCR/" + OcrFolderName[lvChar.FocusedItem.Index]);
            if (dirInfo.GetFiles("*.jpg").Length >= OcrMat.GetLength(1))
            {
                MessageBox.Show("Quantity of ocr image has to < 64", "OCR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i< OcrMat.GetLength(1); i++)
            {
                var path = "OCR/" + OcrFolderName[lvChar.FocusedItem.Index] + "/" + i.ToString() + ".jpg";
                if (!(System.IO.File.Exists(path)))
                {
                    CvInvoke.Imwrite(path, m);
                    break;
                }   
            }
            
            for (int i=0; i<OcrMat.GetLength(1); i++)
            {
                if (OcrMat[lvChar.FocusedItem.Index, i] == null)
                {
                    OcrMat[lvChar.FocusedItem.Index, i] = m;
                    break;
                }   
            }


            int index = lvChar.FocusedItem.Index;
            InitialListViewChar();
            Updatelvimage(index);
            
        }

        private void DialogOcrLearn_Load(object sender, EventArgs e)
        {
            Thread t = new Thread(StartLearning);
            t.Start();
        }

        private void lvChar_Click(object sender, EventArgs e)
        {        
            Updatelvimage(lvChar.FocusedItem.Index);
        }

        private void Updatelvimage(int index)
        {
            
            lvImage.Clear();
            lvImage.LargeImageList = ilOcrImage;
            ilOcrImage.Images.Clear();
            ilOcrImage.ImageSize = new Size(80, 80);

            for (int j = 0; j < OcrMat.GetLength(1); j++)
                if (OcrMat[index, j] != null)
                {
                    ilOcrImage.Images.Add(OcrMat[index, j].Bitmap);
                    lvImage.Items.Add("", j);
                }
            lvChar.Items[index].Selected = true;
            lvChar.Items[index].Focused = true;
            lvChar.Items[index].EnsureVisible();
            lvChar.Focus();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lvImage.FocusedItem.ImageIndex.ToString());
            int count = -1;
            for (int j = 0; j < OcrMat.GetLength(1); j++)
                if (OcrMat[lvChar.FocusedItem.Index, j] != null)
                {
                    count++;
                    if (count == lvImage.FocusedItem.ImageIndex)
                        break;
                }
            System.IO.File.Delete(OcrImageFilePath[lvChar.FocusedItem.Index, count]);        
            OcrMat[lvChar.FocusedItem.Index, lvImage.FocusedItem.ImageIndex] = null;
            Updatelvimage(lvChar.FocusedItem.Index);
        }
    }
}
