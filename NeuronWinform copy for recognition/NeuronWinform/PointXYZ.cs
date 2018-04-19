using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NeuronWinform
{
    public struct PointXYZ
    {
        public PointXYZ(int x, int y, int z)
        {
            Px = x;
            Py = y;
            Pz = z;
        }

        public int Px { get; set; }
        public int Py { get; set; }
        public int Pz { get; set; }
        //private double pz;
        //public double Pz
        //{
        //    get { return pz; }
        //    set { pz = value; OnPropertyChanged("Pz"); }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string PropertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        //}
    }
}
