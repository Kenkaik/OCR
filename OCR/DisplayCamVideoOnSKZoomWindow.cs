using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Threading;
using Basler.Pylon;
using Emgu.CV;

namespace ManualExposureSystem
{
    class DisplayCamVideoOnSKZoomWindow
    {
        public MRVisionLib.Pylon5NetToMat Cam
        {
            get;set;
        }
        public MRVisionLib.SKZoomAndPanWindow Window
        {
            get;set;
        }
        public bool IsLive
        {
            get; set;
        }

        public void LiveDisplayCamVideo()
        {
            try
            {
                if (!Cam.IsOpen) Cam.OpenCamera();
                else
                {
                    MessageBox.Show("Camera opened already", "LiveDisplayCamVideo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Mat mat = null;
                while (Cam.IsOpen)
                {
                    while(IsLive)
                    {
                        mat = Cam.Grab();
                        if (mat == null) break;
                        Window.ImageInput(mat);
                        //Window.ImageInput(Cam.Grab());
                        Window.DisplayImage();
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DisplayCamVideoOnSKZoomWindow", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(Environment.ExitCode);
            }
        }
    }
}
