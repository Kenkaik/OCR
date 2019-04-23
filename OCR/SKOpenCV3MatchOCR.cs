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
        public int MinimumArea = 256;
        public enum AlignAlgorithm { patternMatch, edgePatternMatch, lineScan };

        public SKOpenCV3MatchOCR()
        {
            CvInvoke.UseOpenCL = true;
        }

        public List<MatchPosition> OcrMatch(Mat sample, Mat[,] template, string[] ocrChar ,Double thresholdScore)
        {
            List<MatchPosition> mp = new List<MatchPosition>();
            if (sample.IsEmpty) return mp;


            
            Parallel.For(0, template.GetLength(0), i =>
            {
                Parallel.For(0, template.GetLength(1), j =>
                {
                    if (template[i, j] != null)
                    {
                        int level = CalPryDownLevel(template[i, j]);
                        PryDown(sample, template[i, j], out Mat s, out Mat t, level);
                        mp.AddRange(MultipleMatch(s, t, level, thresholdScore, Convert.ToChar(ocrChar[i]), out int occurenceTimes));
                    }
                });
            });
            
            /*
            Parallel.For(0, template.GetLength(0), i =>
            {
                for (int j=0; j < template.GetLength(1); j++)
                {
                    if (template[i, j] != null)
                    {
                        int level = CalPryDownLevel(template[i, j]);
                        PryDown(sample, template[i, j], out Mat s, out Mat t, level);
                        mp.AddRange(MultipleMatch(s, t, level, thresholdScore, Convert.ToChar(ocrChar[i]), out int occurenceTimes));
                    }
                }
            });
            */

            mp = SortListMatchPosition(mp);

            //int level = CalPryDownLevel(template);
            //PryDown(sample, template, out Mat s, out Mat t, level);
            //mp = MultipleMatch(s, t, level, thresholdScore, out int occurenceTimes);

            //mp = MultipleMatch(sample, template, 0, thresholdScore, out int occurenceTimes);
            
            //for (int o = MaximunOccurence - 1; o > occurenceTimes - 1; o--)
            //    mp[o] = null;

            return mp;
        }

        private List<MatchPosition> SortListMatchPosition(List<MatchPosition> mp)
        {
            var tempMp = new List<MatchPosition>(mp);
            foreach (var value in mp)
                if (value == null)
                    tempMp.Remove(value);
            mp = tempMp;

            for(int i=0; i<mp.Count - 1; i++)
            {
                if (mp[i + 1].X < mp[i].X)
                {
                    MatchPosition temp = new MatchPosition();
                    temp = mp[i];
                    mp[i] = mp[i + 1];
                    mp[i + 1] = temp;
                    i = -1;
                }
            }


            for(int i=0; i<mp.Count - 1; i++)
            {
                if (Math.Abs(mp[i].X - mp[i+1].X) < 5)
                {
                    mp.RemoveAt(i + 1);
                    i = -1;
                }
            }
            
            return mp;
        }



        private int CalPryDownLevel(Mat template)
        {
            Size sz = template.Size;
            int level = 1;
            while (true)
            {
                sz.Width = sz.Width / 2;
                sz.Height = sz.Height / 2;
                if (sz.Width * sz.Height > MinimumArea)
                    level++;
                else
                    break;
            }
            return level;
        }

        private void PryDown(Mat sample, Mat template, out Mat s, out Mat t, int level)
        {
            s = new Mat();
            t = new Mat();
            while (true)
            {
                CvInvoke.PyrDown(sample, s, Emgu.CV.CvEnum.BorderType.Default);
                CvInvoke.PyrDown(template, t, Emgu.CV.CvEnum.BorderType.Default);
                level--;
                if (level <= 0) break;
                while (true)
                {
                    CvInvoke.PyrDown(s, s, Emgu.CV.CvEnum.BorderType.Default);
                    CvInvoke.PyrDown(t, t, Emgu.CV.CvEnum.BorderType.Default);
                    level--;
                    if (level <= 0) break;
                }
                break;
            }
        }

        private List<MatchPosition> MultipleMatch(Mat sample, Mat template, int level, double thresholdScore, char ocrChar ,out int occurenceTimes)
        {
            //MatchPosition[] mp = new MatchPosition[MaximunOccurence];
            List<MatchPosition> mp = new List<MatchPosition>();
            occurenceTimes = 0;
            
            while(true)
            {
                Matrix<float> ret = new Matrix<float>(sample.Cols - template.Cols + 1, sample.Rows - template.Rows + 1);

                CvInvoke.MatchTemplate(sample, template, ret, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);
                ret.MinMax(out double minValue, out double maxValue, out Point minLocation, out Point maxLocation);

                if (maxValue < thresholdScore || occurenceTimes > MaximunOccurence - 1)
                    break;
                else
                    CvInvoke.Rectangle(sample, new Rectangle(maxLocation.X, maxLocation.Y, template.Width, template.Height), new Emgu.CV.Structure.MCvScalar(), -1, Emgu.CV.CvEnum.LineType.FourConnected);

                
                double a = Convert.ToDouble(2); double b = Convert.ToDouble(level); double y = Math.Pow(a, b);

                Point originSizeLoaction = new Point((int)(maxLocation.X * y), (int)(maxLocation.Y * y));


                mp.Add(new MatchPosition(originSizeLoaction.X, originSizeLoaction.Y, (float)maxValue, template.Size) { Char = ocrChar});
                occurenceTimes++;
            }

            //CvInvoke.Imshow("sample", sample);
            //CvInvoke.WaitKey(1);
            return mp;
        }
    }

}
