using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace NeuronWinform
{
    public class DataModel : INotifyPropertyChanged
    {
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; OnPropertyChanged("Index"); }
        }
        private int boneID;
        public int BoneID
        {
            get { return boneID; }
            set { boneID = value; OnPropertyChanged("BoneID"); }
        }

        private float sx;
        public float Sx
        {
            get { return sx; }
            set { sx = value; OnPropertyChanged("Sx"); }
        }
        private float sy;
        public float Sy
        {
            get { return sy; }
            set { sy = value; OnPropertyChanged("Sy"); }
        }
        private float sz;
        public float Sz
        {
            get { return sz; }
            set { sz = value; OnPropertyChanged("Sz"); }
        }

        private float px;
        public float Px
        {
            get { return px; }
            set { px = value; OnPropertyChanged("Px"); }
        }
        private float py;
        public float Py
        {
            get { return py; }
            set { py = value; OnPropertyChanged("Py"); }
        }
        private float pz;
        public float Pz
        {
            get { return pz; }
            set { pz = value; OnPropertyChanged("Pz"); }
        }

        private double width;
        public double Width
        {
          get { return width; }
            set { width = value; OnPropertyChanged("Width"); }
        }
        

        private List<PointXYZ> pointList;
        public List<PointXYZ> PointList
        {
            get { return pointList; }
            set { pointList = value; OnPropertyChanged("PointList"); }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
