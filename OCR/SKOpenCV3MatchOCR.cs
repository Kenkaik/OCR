using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MRVisionLib;
using System.Drawing;

namespace MRVisionLib
{
    class SKOpenCV3MatchOCR
    {
        private int MaximunOccurence = 5;
        public int MinimumArea = 128;
        public enum AlignAlgorithm { patternMatch, edgePatternMatch, lineScan };

        public SKOpenCV3MatchOCR()
        {
            CvInvoke.UseOpenCL = true;
        }

        public MatchPosition[] OcrMatch(Mat sample, Mat template, Double thresholdScore)
        {
            MatchPosition[] mp = new MatchPosition[MaximunOccurence];
            if (sample.IsEmpty || template.IsEmpty) return mp;

            int level = CalPryDownLevel(template);
            PryDown(sample, template, out Mat s, out Mat t, level);
            mp = MultipleMatch(s, t, level, thresholdScore, out int occurenceTimes);

            //mp = MultipleMatch(sample, template, 0, thresholdScore, out int occurenceTimes);
            
            for (int o = MaximunOccurence - 1; o > occurenceTimes - 1; o--)
                mp[o] = null;

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
                while(true)
                {
                    CvInvoke.PyrDown(s, s, Emgu.CV.CvEnum.BorderType.Default);
                    CvInvoke.PyrDown(t, t, Emgu.CV.CvEnum.BorderType.Default);
                    level--;
                    if (level <= 0) break;
                }
                break;
            }
        }

        private MatchPosition[] MultipleMatch(Mat sample, Mat template, int level, double thresholdScore, out int occurenceTimes)
        {
            MatchPosition[] mp = new MatchPosition[MaximunOccurence];
            occurenceTimes = 0;
            
            while(true)
            {
                Matrix<float> ret = new Matrix<float>(sample.Cols - template.Cols + 1, sample.Rows - template.Rows + 1);

                CvInvoke.MatchTemplate(sample, template, ret, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);
                ret.MinMax(out double minValue, out double maxValue, out Point minLocation, out Point maxLocation);

                if (maxValue < thresholdScore || occurenceTimes > 4)
                    break;
                else
                    CvInvoke.Rectangle(sample, new Rectangle(maxLocation.X, maxLocation.Y, template.Width, template.Height), new Emgu.CV.Structure.MCvScalar(), -1, Emgu.CV.CvEnum.LineType.FourConnected);

                
                double a = Convert.ToDouble(2); double b = Convert.ToDouble(level); double y = Math.Pow(a, b);

                Point originSizeLoaction = new Point((int)(maxLocation.X * y), (int)(maxLocation.Y * y));
                mp[occurenceTimes] = new MatchPosition(originSizeLoaction.X, originSizeLoaction.Y, (float)maxValue, template.Size);
                occurenceTimes++;
            }

            //CvInvoke.Imshow("sample", sample);
            //CvInvoke.WaitKey(1);
            return mp;

        }
    }

}
