using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MRVisionLib;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace MRVisionLib
{
    class SKOpenCV3MatchOCR
    {
        private int MaximunOccurence = 5;
        public int MinimumArea = 128;
        public enum AlignAlgorithm { patternMatch, edgePatternMatch, lineScan };
        private object Lock  = new object();


        public SKOpenCV3MatchOCR()
        {
            CvInvoke.UseOpenCL = true;
        }


        public List<MatchPosition> OcrMatch(Mat sample, Mat[,] template, string[] ocrChar ,double lowResolutionThresholdScore, double highResolutionThresholdScore)
        {
            List<MatchPosition> mp = new List<MatchPosition>();
            if (sample.IsEmpty) return mp;

            Parallel.For(0, template.GetLength(0), i =>
            {
                Parallel.For(0, template.GetLength(1), j =>
                {
                    if (template[i, j] != null)
                    {
                        lock(Lock)
                        {
                            int level = CalPryDownLevel(template[i, j]);
                            PryDown(sample, template[i, j], out Mat s, out Mat t, level);
                            List<MatchPosition> r = MultipleMatch(s, t, level, lowResolutionThresholdScore, out int occurenceTimes);
                            List<MatchPosition> r1 = new List<MatchPosition>();
                            r1 = r;
                            foreach (var item in r)
                            {
                                r1[r1.IndexOf(item)].Char = Convert.ToChar(ocrChar[i]);
                                r1[r1.IndexOf(item)].TemplateSize = template[i, j].Size;
                            }
                            r = r1;

                            r1 = PryUpMatch(sample, template[i, j], highResolutionThresholdScore, r);
                            r = r1;

                            mp.AddRange(r);
                        }  
                    }
                });
            });
            mp = SortListMatchPosition(mp);
            return mp;
        }

        private List<MatchPosition> SortListMatchPosition(List<MatchPosition> mp)
        {
            var tempMp = new List<MatchPosition>(mp);
            foreach (var value in mp)
                if (value == null)
                    tempMp.Remove(value);
            mp = tempMp;

            mp.Sort();

            for(int i=0; i<mp.Count - 1; i++)
            {
                if (Math.Abs(mp[i].X - mp[i+1].X) < 5)
                {
                    if (mp[i].Score > mp[i + 1].Score)
                        mp.RemoveAt(i + 1);
                    else
                        mp.RemoveAt(i);
                    i = -1;
                }
            }
            return mp;
        }



        private int CalPryDownLevel(Mat template)
        {
            Size sz = template.Size;
            int level = 0;
            while (true)
            {
                if (sz.Width * sz.Height > MinimumArea)
                    level++;
                else
                    break;
                sz.Width = sz.Width / 2;
                sz.Height = sz.Height / 2;  
            }
            return level;
        }

        private void PryDown(Mat sample, Mat template, out Mat s, out Mat t, int level)
        {
            s = new Mat();
            t = new Mat();
            while (true)
            {
                if (level <= 0)
                {
                    s = sample.Clone();
                    t = template.Clone();
                    break;
                }
                level--;
                CvInvoke.PyrDown(sample, s, Emgu.CV.CvEnum.BorderType.Default);
                CvInvoke.PyrDown(template, t, Emgu.CV.CvEnum.BorderType.Default);
                    while (true)
                    {
                        if (level <= 0) break;
                        level--;
                        CvInvoke.PyrDown(s, s, Emgu.CV.CvEnum.BorderType.Default);
                        CvInvoke.PyrDown(t, t, Emgu.CV.CvEnum.BorderType.Default);                
                    }
                break;
            }
        }

        private List<MatchPosition> MultipleMatch(Mat sample, Mat template, int level, double thresholdScore, out int occurenceTimes)
        {
            List<MatchPosition> mp = new List<MatchPosition>();
            occurenceTimes = 0;
            
            while(true)
            {
                Matrix<float> ret = new Matrix<float>(sample.Cols - template.Cols + 1, sample.Rows - template.Rows + 1);

                CvInvoke.MatchTemplate(sample, template, ret, Emgu.CV.    CvEnum.TemplateMatchingType.CcoeffNormed);
                ret.MinMax(out double minValue, out double maxValue, out Point minLocation, out Point maxLocation);

                Rectangle rectMask = new Rectangle(maxLocation.X + (int)(0.3*template.Width), maxLocation.Y + (int)(0.3*template.Height), (int)(0.7*template.Width), (int)(0.7*template.Height));

                if (maxValue < thresholdScore || occurenceTimes > MaximunOccurence - 1)
                    break;
                else
                    CvInvoke.Rectangle(sample, rectMask, new Emgu.CV.Structure.MCvScalar(), -1, Emgu.CV.CvEnum.LineType.FourConnected);

                double a = Convert.ToDouble(2); double b = Convert.ToDouble(level); double y = Math.Pow(a, b);
                Point originSizeLoaction = new Point((int)(maxLocation.X * y), (int)(maxLocation.Y * y));
                mp.Add(new MatchPosition(originSizeLoaction.X, originSizeLoaction.Y, (float)maxValue, template.Size));
                occurenceTimes++;
            }
            return mp;
        }
        
        private List<MatchPosition> PryUpMatch(Mat sample, Mat template, Double thresholdScore, List<MatchPosition> mp)
        {
            List<MatchPosition> mp1 = mp;
            for (int i=0; i<mp.Count; i++)
            {
                if (mp[i] != null)
                {
                    
                    RectangleF smallSampleRectF = new RectangleF(mp[i].X - 5, mp[i].Y - 5,
                        mp[i].TemplateSize.Width + 10, mp[i].TemplateSize.Height + 10);
                    Rectangle smallSampleRect = Rectangle.Round(smallSampleRectF);
                    if (smallSampleRect.Left <= 0 || smallSampleRect.Top <= 0 || smallSampleRect.Right >= sample.Width || smallSampleRect.Bottom >= sample.Height)
                    {
                        mp1[i] = null;
                        continue;
                    }
                    Mat smallSample = new Mat(sample, smallSampleRect);

                    Matrix<float> ret = new Matrix<float>(smallSample.Cols - template.Cols + 1, smallSample.Rows - template.Rows + 1);
                    CvInvoke.MatchTemplate(smallSample, template, ret, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);
                    ret.MinMax(out double minValue, out double maxValue, out Point minLocation, out Point maxLocation);

                    if (maxValue > thresholdScore)
                    {
                        mp1[i].X = maxLocation.X + (mp[i].X - 5);
                        mp1[i].Y = maxLocation.Y + (mp[i].Y - 5);
                        mp1[i].Score = (float)maxValue;
                    }
                    else
                        mp1[i] = null;

                    
                }
            }
            return mp1;    
        }
    }

}
