using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;

using Basler.Pylon;
using Emgu.CV;

namespace MRVisionLib
{
    class Pylon5NetToMat
    {

        private Camera Cam;
        private Mat GrabImage;
        private object Lock = new object();
        AutoResetEvent GrabEvent = new AutoResetEvent(false);

        public Pylon5NetToMat(string serialNum)
        {
            try
            {
                Cam = new Camera(serialNum);
                CvInvoke.UseOpenCL = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Environment.Exit(Environment.ExitCode);
            }
            SerialNum = serialNum;
        }
        public Pylon5NetToMat()
        {
            
        }

        public bool IsOpen
        {
            get;
            set;
        }

        public string SerialNum
        {
            get;
            set;
        }

        public double CameraGainValue
        {
            get;
            set;
        }
        public double CameraGammaValue
        {
            get;
            set;
        }
        public double CameraBlackLevelValue
        {
            get;
            set; 
        }


        public void OpenCamera()
        {
            Cam.StreamGrabber.ImageGrabbed += OnImageGrabbed;
            Cam.Open();

            // load parameters
            Cam.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
            Cam.Parameters[PLCameraInstance.OutputQueueSize].SetValue(1);

            // Start the grabbing of images until grabbing is stopped.
            Cam.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
            Cam.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);

            //CameraGainValue = Cam.Parameters[PLCamera.Gain].GetValuePercentOfRange();
            //CameraGammaValue = Cam.Parameters[PLCamera.Gamma].GetValuePercentOfRange();
            //CameraBlackLevelValue = Cam.Parameters[PLCamera.BlackLevel].GetValuePercentOfRange();


            if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerUsb")
            {
                CameraGainValue = Cam.Parameters[PLCamera.Gain].GetValuePercentOfRange();
                CameraGammaValue = Cam.Parameters[PLCamera.Gamma].GetValuePercentOfRange();
                CameraBlackLevelValue = Cam.Parameters[PLCamera.BlackLevel].GetValuePercentOfRange();
            }
            if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerGigE")
            {
                Cam.Parameters[PLCamera.GammaEnable].TrySetValue(true);
                CameraGainValue = Cam.Parameters[PLCamera.GainRaw].GetValuePercentOfRange();
                CameraGammaValue = Cam.Parameters[PLCamera.Gamma].GetValuePercentOfRange();
                CameraBlackLevelValue = Cam.Parameters[PLCamera.BlackLevelRaw].GetValuePercentOfRange();
            }


            //if (DeviceType == "BaslerGigE") Cam.Parameters[PLCamera.ExposureTimeAbs].SetValue(35000, FloatValueCorrection.ClipToRange);
            //if (DeviceType == "BaslerUsb") Cam.Parameters[PLCamera.ExposureTime].SetValue(35000, FloatValueCorrection.ClipToRange);
            IsOpen = true;
        }

        public Mat Grab()
        {
            if (!GrabEvent.WaitOne(1000))
                return null;
            Mat img;
            lock (Lock)
            {
                //img = new Mat(GrabImage.Size, Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                //img.SetTo<byte>(GrabImage.Bytes);
                img = GrabImage.Clone();
            }
            return img;
        }

        private void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            Mat mat = null;
            //try
            //{
                IGrabResult grabResult = e.GrabResult;

                if (grabResult.IsValid)
                {
                    mat = new Mat(new Size(grabResult.Width, grabResult.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                    mat.SetTo<byte>((byte[])grabResult.PixelData);

                    lock (Lock)
                    {
                        if (GrabImage != null) GrabImage.Dispose();
                        GrabImage = mat;
                        GrabEvent.Set();
                    }
                }
                if(!grabResult.GrabSucceeded)
                {
                    MessageBox.Show("Camera disconnect", "PylonNet5GrayUMat", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Environment.Exit(Environment.ExitCode);
                    return;
                }
            //}
            //catch (Exception ex)
            //{
           //     MessageBox.Show(ex.Message, "PylonNet5GrayUMat", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //finally
            //{
                // Dispose the grab result if needed for returning it to the grab loop.
           //     e.DisposeGrabResultIfClone();
            //}
            GC.Collect();
        }

        /*  old function 
        public bool OpenCamera()
        {
            try
            {
                Cam.CameraOpened += Configuration.AcquireContinuous;
                Cam.Open();
                Cam.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                Cam.Parameters[PLCameraInstance.OutputQueueSize].SetValue(1);
                Cam.StreamGrabber.Start();

                //pass parm
                CameraGainValue = Cam.Parameters[PLCamera.Gain].GetValuePercentOfRange();
                CameraGammaValue = Cam.Parameters[PLCamera.Gamma].GetValuePercentOfRange();
                CameraBlackLevelValue = Cam.Parameters[PLCamera.BlackLevel].GetValuePercentOfRange();
                IsOpen = Cam.IsOpen;
                return true;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Mat Grab()
        {
            lock (Lock)
            {
                try
                {
                    IGrabResult grabResult = Cam.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                    using (grabResult)
                    {
                        if (GrabImage != null || DisplayImage != null)
                        {
                            GrabImage.Dispose();
                            DisplayImage.Dispose();
                        }
                        // Image grabbed successfully?
                        if (grabResult.GrabSucceeded)
                        {
                            GrabImage = new Mat(new Size(grabResult.Width, grabResult.Height), Emgu.CV.CvEnum.DepthType.Cv8U, 1);
                            GrabImage.SetTo<byte>((byte[])grabResult.PixelData);
                            DisplayImage = GrabImage;
                        }
                        else
                        {
                            MessageBox.Show("Camera disconnect");
                            Environment.Exit(Environment.ExitCode);
                        }
                        grabResult.Dispose();
                        return DisplayImage;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        */
        public void StopCamera()
        {
            if (IsOpen)
            {
                Cam.StreamGrabber.Stop();
                Cam.Close();
                IsOpen = false;
            }
            
        }


        public void CameraGain(double value)
        {
            CameraGainValue = value;
            try
            {
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerUsb")
                    Cam.Parameters[PLCamera.Gain].SetValuePercentOfRange(value);
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerGigE")
                    Cam.Parameters[PLCamera.GainRaw].SetValuePercentOfRange(value);
            }
            catch
            {
                throw;
            }
        }

        public void CameraFrameRate(double frameRate)
        {
            try
            {
                Cam.Parameters[PLCamera.AcquisitionFrameRateEnable].TrySetValue(true);
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerUsb")
                    Cam.Parameters[PLCamera.AcquisitionFrameRate].TrySetValue(frameRate);
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerGigE")
                    Cam.Parameters[PLCamera.AcquisitionFrameRateAbs].TrySetValue(frameRate);
            }
            catch
            {
                throw;
            }
        }

        public void CameraGamma(double value)
        {
            CameraGammaValue = value;
            try
            {
                //Cam.Parameters[PLCamera.Gamma].SetValuePercentOfRange(value);
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerUsb")
                    Cam.Parameters[PLCamera.Gamma].SetValuePercentOfRange(value);
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerGigE")
                    Cam.Parameters[PLCamera.Gamma].SetValuePercentOfRange(value);
            }
            catch
            {
                throw;
            }
        }

        public void CameraBlackLevel(double value)
        {
            CameraBlackLevelValue = value;
            try
            {
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerUsb")
                    Cam.Parameters[PLCamera.BlackLevel].SetValuePercentOfRange(value);
                if (Cam.CameraInfo[CameraInfoKey.DeviceType] == "BaslerGigE")
                    Cam.Parameters[PLCamera.BlackLevelRaw].SetValuePercentOfRange(value);
            }
            catch
            {
                throw;
            }
        }
    }
}
