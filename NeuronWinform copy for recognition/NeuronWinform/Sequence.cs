// Accord.NET Sample Applications
// http://accord-framework.net
//
// Copyright © 2009-2014, César Souza
// All rights reserved. 3-BSD License:
//
//   Redistribution and use in source and binary forms, with or without
//   modification, are permitted provided that the following conditions are met:
//
//      * Redistributions of source code must retain the above copyright
//        notice, this list of conditions and the following disclaimer.
//
//      * Redistributions in binary form must reproduce the above copyright
//        notice, this list of conditions and the following disclaimer in the
//        documentation and/or other materials provided with the distribution.
//
//      * Neither the name of the Accord.NET Framework authors nor the
//        names of its contributors may be used to endorse or promote products
//        derived from this software without specific prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//  DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
//  DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//  (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//  ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;
using Accord.Math;
using NeuronWinform;

namespace Gestures.HMMs
{
    [Serializable]
    public class Sequence : ICloneable
    {
        [XmlIgnore]
        [NonSerialized]
        private double[][] input;

        [XmlIgnore]
        [NonSerialized]
        private Bitmap bitmap;


        public BindingList<String> Classes { get; set; }

        public PointXYZ[] SourcePath { get; set; }

        public int Output { get; set; }

        public int RecognizedAs { get; set; }

        public Sequence()
        {
            RecognizedAs = -1;
        }


        public string OutputName
        {
            get { return Classes[Output]; }
        }

        public string RecognizedAsName
        {
            get { return RecognizedAs >= 0 ? Classes[RecognizedAs] : "-"; }
        }


        public double[][] Input
        {
            get
            {
                if (input == null)
                    input = Preprocess(SourcePath);
                return input;
            }
        }


        public Bitmap Bitmap
        {
            get
            {
                if (bitmap == null && SourcePath != null)
                    bitmap = ToBitmap(SourcePath);
                return bitmap;
            }
        }


        public static double[][] Preprocess(PointXYZ[] sequence)
        {
            double[][] result = new double[sequence.Length][];
            for (int i = 0; i < sequence.Length; i++)
                result[i] = new double[] { sequence[i].Px, sequence[i].Py, sequence[i].Pz };

            double[][] zscores = Accord.Statistics.Tools.ZScores(result);

            return zscores.Add(10);
        }

        public static Bitmap ToBitmap(PointXYZ[] sequence)
        {
            if (sequence.Length == 0)
                return null;

            int xmax = (int)sequence.Max(x => x.Px);
            int xmin = (int)sequence.Min(x => x.Px);

            int ymax = (int)sequence.Max(x => x.Py);
            int ymin = (int)sequence.Min(x => x.Py);

            int width = xmax - xmin;
            int height = ymax - ymin;


            Bitmap bmp = new Bitmap(width + 16, height + 16);

            Graphics g = Graphics.FromImage(bmp);

            using (Brush brush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(brush, 16))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, (int)sequence[0].Px - xmin, (int)sequence[0].Py - ymin, (int)sequence[1].Px - xmin, (int)sequence[1].Py - ymin);
                g.DrawLine(pen, (int)sequence[1].Px - xmin, (int)sequence[1].Py - ymin, (int)sequence[2].Px - xmin, (int)sequence[2].Py - ymin);
                g.DrawLine(pen, (int)sequence[1].Px - xmin, (int)sequence[1].Py - ymin, (int)sequence[3].Px - xmin, (int)sequence[3].Py - ymin);
                g.DrawLine(pen, (int)sequence[3].Px - xmin, (int)sequence[3].Py - ymin, (int)sequence[4].Px - xmin, (int)sequence[4].Py - ymin);
                g.DrawLine(pen, (int)sequence[4].Px - xmin, (int)sequence[4].Py - ymin, (int)sequence[5].Px - xmin, (int)sequence[5].Py - ymin);
                g.DrawLine(pen, (int)sequence[5].Px - xmin, (int)sequence[5].Py - ymin, (int)sequence[6].Px - xmin, (int)sequence[6].Py - ymin);
                g.DrawLine(pen, (int)sequence[1].Px - xmin, (int)sequence[1].Py - ymin, (int)sequence[7].Px - xmin, (int)sequence[7].Py - ymin);
                g.DrawLine(pen, (int)sequence[7].Px - xmin, (int)sequence[7].Py - ymin, (int)sequence[8].Px - xmin, (int)sequence[8].Py - ymin);
                g.DrawLine(pen, (int)sequence[8].Px - xmin, (int)sequence[8].Py - ymin, (int)sequence[9].Px - xmin, (int)sequence[9].Py - ymin);
                g.DrawLine(pen, (int)sequence[9].Px - xmin, (int)sequence[9].Py - ymin, (int)sequence[10].Px - xmin, (int)sequence[10].Py - ymin);
            }

            return bmp;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
