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
using Emgu.CV.OCR;


namespace OCR
{
    public partial class FormMain : Form
    {
        public static bool AppEnding = false;

        public FormMain()
        {
            InitializeComponent();
            this.Text = "OcrTest";
        }
        MRVisionLib.Pylon5NetToMat Cam;
        private void button1_Click(object sender, EventArgs e)
        {
            Cam = new MRVisionLib.Pylon5NetToMat("22270744");
            ManualExposureSystem.DisplayCamVideoOnSKZoomWindow camThread = new ManualExposureSystem.DisplayCamVideoOnSKZoomWindow();
            camThread.Cam = this.Cam;
            camThread.Window = this.skZoomAndPanWindow1;
            camThread.Window.EnableZoom = true;
            camThread.IsLive = true;
            Thread t = new Thread(camThread.LiveDisplayCamVideo);
            t.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Sure to Exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            Cam.StopCamera();
            //Environment.Exit(Environment.ExitCode);
        }
    }
}
