using Gestures.HMMs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NeuronWinform
{
    public class Neuron
    {
        public MainWindow mainwindow;

        private int _frameCount = 0;
        SocketStatusChanged _SocketStatusChanged;
        FrameDataReceived _CalcDataReceived;

        CalcDataHeader _calcHeader;
        private float[] _valuesBufferCalc = new float[338];

        public IntPtr sockTCPRef = IntPtr.Zero;
        public IntPtr sockUDPRef = IntPtr.Zero;

        public List<DataModel> DataList = new List<DataModel>();

        Hashtable table = new Hashtable();

        public bool isReadySavetoDB = false;

        public event EventHandler<Event> RaiseCustomEvent;
        public event EventHandler<EventChangeGesture> RaiseCustomEventChangeGesture;
        protected virtual void OnRaiseCustomEvent(Event e)
        {
            EventHandler<Event> handler = RaiseCustomEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected virtual void OnRaiseCustomEventChangeGesture(EventChangeGesture e)
        {
            EventHandler<EventChangeGesture> handler = RaiseCustomEventChangeGesture;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void Load()
        {
            _CalcDataReceived = new FrameDataReceived(calcDataReceived);
            NeuronDataReader.BRRegisterCalculationDataCallback(IntPtr.Zero, _CalcDataReceived);

            _SocketStatusChanged = new SocketStatusChanged(socketStatusChanged);
            NeuronDataReader.BRRegisterSocketStatusCallback(IntPtr.Zero, _SocketStatusChanged);
        }

        public void Close()
        {
            if (sockTCPRef != IntPtr.Zero)
            {
                NeuronDataReader.BRCloseSocket(sockTCPRef);
                sockTCPRef = IntPtr.Zero;
            }

            if (sockUDPRef != IntPtr.Zero)
            {
                NeuronDataReader.BRCloseSocket(sockUDPRef);
                sockUDPRef = IntPtr.Zero;
            }
        }

        bool isCountable = true;
        int tempX = 0,tempY = 0,tempZ = 0;
        int countSameNo = 0;
        private void UpdateCalcDataUI(float[] calcData)
        {
            DataModel model;
            if (isStart)
            {
                initialDataModel(calcData);
            }
            model = (DataModel)table[13];
            model.Px = (calcData[13 * 16 + 1] * 500) + (810 / 2) - 100;
            model.Py = (calcData[13 * 16 + 2] * 500) + (622 / 2) + 200;
            model.Pz = (calcData[13 * 16 + 0] * 500) + (622 / 2);
            table[14] = model;

            if (mainwindow.CurrentState == 0) // Save Gesture
            {
                if (_frameCount > 120) _frameCount = 0;
                if (isCountable)
                {
                    _frameCount++;
                    mainwindow.sequence.Add(new PointXYZ(Convert.ToInt32(model.Px), Convert.ToInt32(model.Py), Convert.ToInt32(model.Pz)));
                }

                if (_frameCount == 120)
                {
                    isCountable = false;
                    mainwindow.panelClassification.Visible = true;
                }
            }
            else if (mainwindow.CurrentState == 1 && mainwindow.database.Classes.Count > 0) // Recog Gesture
            {
                if (_frameCount > 120) _frameCount = 0;
                _frameCount++;
                if (countSameNo < 20)
                {
                    mainwindow.sequence.Add(new PointXYZ(Convert.ToInt32(model.Px), Convert.ToInt32(model.Py), Convert.ToInt32(model.Pz)));

                }
                   
                else if (countSameNo >= 20)
                {
                    double[][] input = Sequence.Preprocess(mainwindow.GetSequence());
                    if (mainwindow.myHMM == null) return;
                    int index = (mainwindow.myHMM.hcrf != null) ?
                                mainwindow.myHMM.hcrf.Compute(input) : mainwindow.myHMM.hmm.Compute(input);
                    mainwindow.sequence.Clear();
                    string label = mainwindow.database.Classes[index];
                    if (mainwindow._option == MainWindow.TransferOption.OnlyTranslate)
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.TranslateRotate)
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.TranslateScale)
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.OnlyScale)
                    {
                        switch (label)
                        {
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.OnlyRotate)
                    {
                        switch (label)
                        {
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.ScaleRotate)
                    {
                        switch (label)
                        {
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    countSameNo = 0;
                }
                else if (_frameCount == 120)
                {
                    double[][] input = Sequence.Preprocess(mainwindow.GetSequence());
                    if (mainwindow.myHMM == null) return;
                    int index = (mainwindow.myHMM.hcrf != null) ?
                                mainwindow.myHMM.hcrf.Compute(input) : mainwindow.myHMM.hmm.Compute(input);

                    string label = mainwindow.database.Classes[index];
                    if (mainwindow._option == MainWindow.TransferOption.OnlyTranslate)
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.TranslateRotate)
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.TranslateScale)
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.OnlyScale)
                    {
                        switch (label)
                        {
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.OnlyRotate)
                    {
                        switch (label)
                        {
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else if (mainwindow._option == MainWindow.TransferOption.ScaleRotate)
                    {
                        switch (label)
                        {
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    else
                    {
                        switch (label)
                        {
                            case "Right":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Right));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Left":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Left));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Up":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Up));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "Down":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.Down));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomOut":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomOut));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "ZoomIn":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.ZoomIn));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateR":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateRight));
                                mainwindow.labelGesture.Text = label;
                                break;
                            case "RotateL":
                                OnRaiseCustomEventChangeGesture(new EventChangeGesture(MainWindow.Position.RotateLeft));
                                mainwindow.labelGesture.Text = label;
                                break;
                        }
                    }
                    _frameCount = 0;
                    countSameNo = 0;
                }

                if (tempX == Convert.ToInt32(model.Px) || (tempX - Convert.ToInt32(model.Px) == 1) || tempX - Convert.ToInt32(model.Px) == -1
                    || tempY == Convert.ToInt32(model.Py) || (tempY - Convert.ToInt32(model.Py) == 1) || tempY - Convert.ToInt32(model.Py) == -1
                    || tempZ == Convert.ToInt32(model.Pz) || (tempZ - Convert.ToInt32(model.Pz) == 1) || tempZ - Convert.ToInt32(model.Pz) == -1)
                {
                    countSameNo++;
                }
                tempX = Convert.ToInt32(model.Px);
                tempY = Convert.ToInt32(model.Py);
                tempZ = Convert.ToInt32(model.Pz);
            }
            //System.Console.WriteLine(_frameCount);
            OnRaiseCustomEvent(new Event(table));
        }

        bool isStart = true;

        public void addGesture()
        {
            string selectedItem = mainwindow.cbClasses.SelectedItem as String;
            string classLabel = String.IsNullOrEmpty(selectedItem) ?
                mainwindow.cbClasses.Text : selectedItem;

            if (mainwindow.database.Add(mainwindow.GetSequence(), classLabel) != null)
            {
                mainwindow.ClearSequence();

                if (mainwindow.database.Classes.Count >= 2 &&
                    mainwindow.database.SamplesPerClass() >= 3)
                {
                }
            }
            mainwindow.panelClassification.Visible = false;
            isCountable = true;
        }

        public void CancelGesture()
        {
            mainwindow.panelClassification.Visible = false;
            isCountable = true;
        }

        private void initialDataModel(float[] bvhData)
        {
            DataModel model;
            model = new DataModel();
            model.Index = 0;
            model.BoneID = 13;
            table.Add(13, model);

            DataList.Add(model);
            isStart = false;
        }

        private void calcDataReceived(IntPtr customObject, IntPtr sockRef, IntPtr header, IntPtr data)
        {
            _calcHeader = (CalcDataHeader)Marshal.PtrToStructure(header, typeof(CalcDataHeader));
            // Change the buffer length if necessary
            if (_calcHeader.DataCount != _valuesBufferCalc.Length)
            {
                _valuesBufferCalc = new float[_calcHeader.DataCount];
            }

            Marshal.Copy(data, _valuesBufferCalc, 0, (int)_calcHeader.DataCount);

            if (sockRef == this.sockTCPRef)
            {
                if (mainwindow.InvokeRequired)
                {
                    try
                    {
                        a = mainwindow.BeginInvoke((MethodInvoker)delegate
                        {
                            UpdateCalcDataUI(_valuesBufferCalc);
                        });
                        mainwindow.EndInvoke(a);
                    }
                    catch (InvalidOperationException ex)    // pump died before we were processed
                    {
                        System.Console.Write(ex);
                        if (mainwindow.IsHandleCreated) throw;    // not the droids we are looking for
                    }
                }
                else
                {
                    UpdateCalcDataUI(_valuesBufferCalc);
                }

            }
        }

        public delegate void InvokeDelegate(float[] bvhData, bool withDisp);
        IAsyncResult a;

        private void socketStatusChanged(IntPtr customObject, IntPtr sockRef, SocketStatus status, [MarshalAs(UnmanagedType.LPStr)]string msg)
        {
            mainwindow.Invoke(new Action(delegate()
            {
                //txtSockLog.Text = msg;
            }));
        }
        
        public void btnConnect_Click(object sender, EventArgs e)
        {
            if (NeuronDataReader.BRGetSocketStatus(sockTCPRef) == SocketStatus.CS_Running)
            {
                NeuronDataReader.BRCloseSocket(sockTCPRef);
                sockTCPRef = IntPtr.Zero;

                mainwindow.btnConnect.Text = "Connect";
            }
            else
            {
                sockTCPRef = NeuronDataReader.BRConnectTo(mainwindow.txtIP.Text, int.Parse(mainwindow.txtPort.Text));
                if (sockTCPRef == IntPtr.Zero)
                {
                    string msg = NeuronDataReader.strBRGetLastErrorMessage();
                    MessageBox.Show(msg, "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                mainwindow.btnConnect.Text = "Disconnect";
                _frameCount = 0;
            }
        }

    }
}
