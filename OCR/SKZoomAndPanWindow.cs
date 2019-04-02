using System;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;
using Emgu.CV;
using System.Threading;
using Emgu.CV.CvEnum;





namespace MRVisionLib
{
    public partial class SKZoomAndPanWindow : UserControl
    {
        //zoom parm//
        private int RatioIndex = 0;
        private int LastRatioIndex = 0;
        private float[] Ratio = new float[7];

        //image//
        private UMat SrcImage, FitWindowImage, DstImage;
        private int FitWindowImageWidth, FitWindowImageHeight;
        private float ImgMagnificationX;
        private float ImgMagnificationY;

        private Mat GettingImage;
        private bool IsGettingImage;
        private bool IsCreateMaskAndWaferRect = false;
        public MatchPosition WaferMp
        {
            get;set;
        }
        public MatchPosition MaskMp
        {
            get;set;
        }

        private SKWindowLearnPair Mask;
        private SKWindowLearnPair Wafer;
        


        //point//
        private bool IsMouseDown = false;
        private Point AftZoomRoiLeftUpPt = new Point(0, 0);
        private Point OldPt = new Point(0, 0);
        private Point PtMouseMove = new Point(0, 0);
        private Point PtMousedown = new Point(0, 0);
        private Point PtMouseWheel = new Point(0, 0);

        

        //switch//
        public bool EnableZoom
        {
            get; set;
        }
        public bool EnableRectRoi
        {
            get; set;
        }
        public bool EnableRectLineScanArea
        {
            get; set;
        }
        public bool EnableTrackPattern
        {
            get;set;
        }
        

        public SKZoomAndPanWindow()
        {
            InitializeComponent();
            EnableZoom = false;
            this.DoubleBuffered = true;
            this.Focus();

            Ratio[0] = 1;
            for (int i = 1; i < Ratio.Length; i++) Ratio[i] = Ratio[i - 1] * 1.5f;

        }

        private void SKZoomAndPanWindow_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            PtMousedown = e.Location;
            if (EnableRectRoi)
            {
                Mask.PatternRoiMouseDown(e);
                Wafer.PatternRoiMouseDown(e);
            }   
        }

        private void SKZoomAndPanWindow_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            OldPt = AftZoomRoiLeftUpPt;
            if (EnableRectRoi)
            {
                Mask.PatternRoiMouseUp();
                Wafer.PatternRoiMouseUp();
            }
        }



        private void SKZoomAndPanWindow_MouseMove(object sender, MouseEventArgs e)
        {
            PtMouseMove = e.Location;
            if (!IsCreateMaskAndWaferRect) return;
            if (EnableRectRoi)
            {
                if (!Mask.IsAdjustStatus)
                    Wafer.PatternRoiMouseMove(e, AftZoomRoiLeftUpPt, FitWindowImageWidth, FitWindowImageHeight, Ratio[RatioIndex]);
                if (!Wafer.IsAdjustStatus)
                    Mask.PatternRoiMouseMove(e, AftZoomRoiLeftUpPt, FitWindowImageWidth, FitWindowImageHeight, Ratio[RatioIndex]);
                if (Mask.IsAdjustStatus || Wafer.IsAdjustStatus) return;
                if (Mask.IsInRoi == false && Wafer.IsInRoi == false) Cursor = Cursors.Arrow;
            }
            if (IsMouseDown)
            {
                AftZoomRoiLeftUpPt.X = PtMousedown.X - e.Location.X + OldPt.X;
                AftZoomRoiLeftUpPt.Y = PtMousedown.Y - e.Location.Y + OldPt.Y;
                preventRectFail(ref AftZoomRoiLeftUpPt);
            }
        }

        private void SKZoomAndPanWindow_MouseWheel(object sender, MouseEventArgs e)
        {
            if (EnableZoom)
            {
                if (e.Delta > 0 && RatioIndex < Ratio.Length - 1) RatioIndex++;  
                if (e.Delta < 0 && RatioIndex > 0 ) RatioIndex--;
                if (LastRatioIndex == RatioIndex) return;

                AftZoomRoiLeftUpPt.X = (int)((e.X + AftZoomRoiLeftUpPt.X) * Ratio[RatioIndex] / Ratio[LastRatioIndex]) - e.X;
                AftZoomRoiLeftUpPt.Y = (int)((e.Y + AftZoomRoiLeftUpPt.Y) * Ratio[RatioIndex] / Ratio[LastRatioIndex]) - e.Y;
                OldPt = AftZoomRoiLeftUpPt;

                if (RatioIndex == 0)   //reset oldpt
                {
                    OldPt.X = 0;
                    OldPt.Y = 0;
                }

                LastRatioIndex = RatioIndex;
                preventRectFail(ref AftZoomRoiLeftUpPt);
            } 
        }

        public void ZoomMin()
        {
            RatioIndex = 0;
            AftZoomRoiLeftUpPt.X = 0;
            AftZoomRoiLeftUpPt.Y = 0;
            OldPt = AftZoomRoiLeftUpPt;
            LastRatioIndex = RatioIndex;
        }


        //avoid roi area not in image plane 
        private void preventRectFail(ref Point aftZoomRoiLeftUp)
        {
            if (aftZoomRoiLeftUp.X <= 0)
                aftZoomRoiLeftUp.X = 0;

            if (aftZoomRoiLeftUp.Y <= 0)
                aftZoomRoiLeftUp.Y = 0;

            if (aftZoomRoiLeftUp.X + ClientRectangle.Width >= (int)(FitWindowImageWidth * Ratio[RatioIndex]))
                aftZoomRoiLeftUp.X = (int)(FitWindowImageWidth * Ratio[RatioIndex]) - ClientRectangle.Width;
                
            if (aftZoomRoiLeftUp.Y + ClientRectangle.Height >= (int)(FitWindowImageHeight * Ratio[RatioIndex]))
                aftZoomRoiLeftUp.Y = (int)(FitWindowImageHeight * Ratio[RatioIndex]) - ClientRectangle.Height;
        }

        public void ImageInput(Mat image)
        {
            try
            {
                if (image == null) return;
                SrcImage =  image.GetUMat(Emgu.CV.CvEnum.AccessType.Fast);
                if (IsGettingImage)
                {
                    if (GettingImage != null) GettingImage.Dispose();
                    GettingImage = image.Clone();
                    IsGettingImage = false;
                }
                FitWindowImage = new UMat();
                CvInvoke.Resize(SrcImage, FitWindowImage, ClientSize, 0, 0, Emgu.CV.CvEnum.Inter.Linear); //to fit window
                FitWindowImageWidth = FitWindowImage.Cols;
                FitWindowImageHeight = FitWindowImage.Rows;
                if ((ClientSize.Width / image.Width - ClientSize.Height / image.Height) > 0.05F
                    || (ClientSize.Width / image.Width - ClientSize.Height / image.Height) < -0.05F)
                {
                    MessageBox.Show("警告 輸入影像與輸出影像比例不一", "SKZoomAndPanWindow", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                float w1 = FitWindowImageWidth, w2 = image.Width;
                ImgMagnificationX = w1 / w2;
                float w3 = FitWindowImageHeight, w4 = image.Height;
                ImgMagnificationY = w3 / w4;
                image.Dispose();
                if (!IsCreateMaskAndWaferRect)
                {
                    Mask = new SKWindowLearnPair(new Rectangle((int)(ClientSize.Width * 0.4), (int)(ClientSize.Height * 0.4), (int)(ClientSize.Width / 5), (int)(ClientSize.Width / 5)), this);
                    Wafer = new SKWindowLearnPair(new Rectangle((int)(ClientSize.Width * 0.45), (int)(ClientSize.Height * 0.45), (int)(ClientSize.Width / 10), (int)(ClientSize.Width / 10)), this);
                    IsCreateMaskAndWaferRect = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void drawImage()
        {
            try
            {
                if (!FitWindowImage.IsEmpty)
                {
                    
                    UMat roi = new UMat();
                    //Mat roi = new Mat();
                    int failTimes = 0;
                roiCheckPoint:   //to check if ZoomRatio changed after resize before DstImage = new Mat(roi, s);
                    CvInvoke.Resize(FitWindowImage, roi, new Size(0, 0), Ratio[RatioIndex], Ratio[RatioIndex], Emgu.CV.CvEnum.Inter.Linear);
                    Rectangle s = new Rectangle(AftZoomRoiLeftUpPt, ClientSize);
                    if (0 <= s.X && 0 <= s.Width && s.X + s.Width <= roi.Cols
                        && 0 <= s.Y && 0 <= s.Height && s.Y + s.Height <= roi.Rows)
                    {
                        DstImage = new UMat(roi, s);

                        CvInvoke.CvtColor(DstImage, DstImage, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);
                        if (EnableZoom) //draw zoomled
                        {
                            for (int i = 0; i < Ratio.Length; i++)
                            {
                                CvInvoke.Rectangle(DstImage, new Rectangle(10, 10 + 15 * i, 15, 8),
                                new Emgu.CV.Structure.MCvScalar(0, 125, 0), 2);
                            }
                            CvInvoke.Rectangle(DstImage, new Rectangle(10, 10 + 15 * RatioIndex, 15, 8),
                                new Emgu.CV.Structure.MCvScalar(0, 200, 0), -1);
                        }                       
                        if (EnableRectRoi)
                        {
                            DstImage = Mask.DrawPatternRectangle(DstImage, Ratio[RatioIndex], AftZoomRoiLeftUpPt, new Emgu.CV.Structure.MCvScalar(0, 255, 0));
                            DstImage = Wafer.DrawPatternRectangle(DstImage, Ratio[RatioIndex], AftZoomRoiLeftUpPt, new Emgu.CV.Structure.MCvScalar(0, 0, 255));
                            if (EnableRectLineScanArea)
                            {
                                CvInvoke.PutText(DstImage, "Vertical", new Point((int)Mask.PatternRoi.X - 10, (int)Mask.PatternRoi.Y - 10), FontFace.HersheyComplexSmall, 1, new Emgu.CV.Structure.MCvScalar(0, 255, 0));
                                CvInvoke.PutText(DstImage, "Horizontal", new Point((int)Wafer.PatternRoi.X - 10, (int)Wafer.PatternRoi.Y - 10), FontFace.HersheyComplexSmall, 1, new Emgu.CV.Structure.MCvScalar(0, 0, 255));
                            }
                        }
                        if (EnableTrackPattern)
                        {
                            if (MaskMp != null && WaferMp != null)
                            {
                                Point maskPt = Point.Round(new PointF(MaskMp.X * ImgMagnificationX * Ratio[RatioIndex], MaskMp.Y * ImgMagnificationY * Ratio[RatioIndex]));
                                Point waferPt = Point.Round(new PointF(WaferMp.X * ImgMagnificationX * Ratio[RatioIndex], WaferMp.Y * ImgMagnificationY * Ratio[RatioIndex]));
                                if (s.Contains(maskPt) || s.Contains(waferPt))
                                {
                                    CvInvoke.Line(DstImage, new Point(maskPt.X - s.X, maskPt.Y - s.Y - 30) , new Point(maskPt.X - s.X, maskPt.Y - s.Y + 30)
                                        ,new Emgu.CV.Structure.MCvScalar(0, 255, 0));
                                    CvInvoke.Line(DstImage, new Point(maskPt.X - s.X - 30, maskPt.Y - s.Y), new Point(maskPt.X - s.X + 30, maskPt.Y - s.Y)
                                        , new Emgu.CV.Structure.MCvScalar(0, 255, 0));
                                    CvInvoke.Line(DstImage, new Point(waferPt.X - s.X, waferPt.Y - s.Y - 30), new Point(waferPt.X - s.X, waferPt.Y - s.Y + 30)
                                        , new Emgu.CV.Structure.MCvScalar(0, 0, 255));
                                    CvInvoke.Line(DstImage, new Point(waferPt.X - s.X - 30, waferPt.Y - s.Y), new Point(waferPt.X - s.X + 30, waferPt.Y - s.Y)
                                        , new Emgu.CV.Structure.MCvScalar(0, 0, 255));
                                }
                            }
                        }
                    }
                    else
                    {
                        failTimes++;
                        if (failTimes > 3) return;
                        goto roiCheckPoint;
                    }
                    
                    roi.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        Graphics G;
        bool CreateGraphic = false;
        public void DisplayImage()
        {
            try
            {
                if (!FitWindowImage.IsEmpty)
                {
                    drawImage();
                    Bitmap showImg = DstImage.Bitmap;

                    if (!CreateGraphic)
                    {
                        G = this.CreateGraphics();
                        G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        G.InterpolationMode = InterpolationMode.NearestNeighbor;
                        G.PageUnit = GraphicsUnit.Pixel;
                        CreateGraphic = true;
                    }
                    G.DrawImage(showImg, ClientRectangle.Location);
                    showImg.Dispose();
                    SrcImage.Dispose();
                    FitWindowImage.Dispose();
                    DstImage.Dispose();
                    //G.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Rectangle GetMaskRectangle()
        {
            RectangleF correctRectF = new RectangleF(Mask.PatternRoi.X / ImgMagnificationX, Mask.PatternRoi.Y / ImgMagnificationY
                , Mask.PatternRoi.Width / ImgMagnificationX, Mask.PatternRoi.Height / ImgMagnificationY);
            Rectangle correctRect = Rectangle.Round(correctRectF);
            return correctRect;
        }
        public Rectangle GetWaferRectangle()
        {
            RectangleF correctRectF = new RectangleF(Wafer.PatternRoi.X / ImgMagnificationX, Wafer.PatternRoi.Y / ImgMagnificationY
                , Wafer.PatternRoi.Width / ImgMagnificationX, Wafer.PatternRoi.Height / ImgMagnificationY);
            Rectangle correctRect = Rectangle.Round(correctRectF);
            return correctRect;
        }

        public Mat GetSrcImage()
        {
            IsGettingImage = true;
            while(true)
            {
                if (IsGettingImage == false)
                    return GettingImage;
                Thread.Sleep(10);
            }
        }


    }
}
