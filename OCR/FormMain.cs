using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Basler.Pylon;
using Emgu.CV;
using Emgu.CV.OCR;

using MRVisionLib;
using System.Diagnostics;

namespace OCR
{
    public partial class FormMain : Form
    {
        public static bool AppEnding = false;
        Mat Template;
        //Mat Sample;



        public FormMain()
        {
            InitializeComponent();
            this.Text = "OcrTest";
        }
        MRVisionLib.Pylon5NetToMat Cam;

        private void StartCamera_Click(object sender, EventArgs e)
        {
            Cam = new MRVisionLib.Pylon5NetToMat("22270744");
            ManualExposureSystem.DisplayCamVideoOnSKZoomWindow camThread = new ManualExposureSystem.DisplayCamVideoOnSKZoomWindow();
            camThread.Cam = this.Cam;
            camThread.Window = this.vidCameraLive;
            camThread.Window.EnableZoom = true;
            camThread.Window.EnableRectCommonRoi = true;
            camThread.IsLive = true;
            Thread t = new Thread(camThread.LiveDisplayCamVideo);
            t.Start();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Sure to Exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            if (Cam != null)
            {
                Cam.StopCamera();
            }
            
            //Environment.Exit(Environment.ExitCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rectangle commonRect = vidCameraLive.GetCommonRectangle();
            labelOcrRectX.Text = "X = " + commonRect.X.ToString();
            labelOcrRectY.Text = "Y = " + commonRect.Y.ToString();
            labelOcrRectWidth.Text = "Width = " + commonRect.Width.ToString();
            labelOcrRectHeight.Text = "Height = " + commonRect.Height.ToString();
        }

        private void buttonSaveTemplate_Click(object sender, EventArgs e)
        {
            Template = new Mat(vidCameraLive.GetSrcImage(), vidCameraLive.GetCommonRectangle());
            pictureBox1.Image = Template.Bitmap;
        }

        private void buttonFindTemplate_Click(object sender, EventArgs e)
        {
            OpenCV3MatchUMat matcher = new OpenCV3MatchUMat();
            MatchPosition mp;


            Stopwatch sw1 = Stopwatch.StartNew();
            Mat s = vidCameraLive.GetSrcImage();
            sw1.Stop();
            MessageBox.Show(sw1.ElapsedMilliseconds.ToString());
            Stopwatch sw2 = Stopwatch.StartNew();
            mp = matcher.MatchWithOption(s, Template, OpenCV3MatchUMat.AlignAlgorithm.patternMatch);
            string msg = "X = " + mp.X + " , " + "Y = " + mp.Y;
            sw2.Stop();
            MessageBox.Show(sw2.ElapsedMilliseconds.ToString());


            MessageBox.Show(msg, "FindTemplate");
        }

        private void buttonLearnChar_Click(object sender, EventArgs e)
        {
            //DialogOcrLearn Ocr = new DialogOcrLearn(vidCameraLive.GetSrcImage());
            DialogOcrLearn Ocr = new DialogOcrLearn(CvInvoke.Imread("100_1NID146-24ID.jpg", Emgu.CV.CvEnum.ImreadModes.Grayscale));
            Ocr.ShowDialog();
        }
    }
}
