using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Models.Fields;
using Accord.Statistics.Models.Fields.Functions;
using Accord.Statistics.Models.Fields.Learning;
using Accord.Statistics.Models.Markov;
using Accord.Statistics.Models.Markov.Learning;
using Accord.Statistics.Models.Markov.Topology;
using Gestures.HMMs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuronWinform
{
    public partial class MainWindow : Form
    {
        public Neuron n;
        public HMM myHMM;
        public Database database;
        public int CurrentState = 0; //0 = Save , 1 = Recognize
        public List<PointXYZ> sequence;

        public enum Position
        {
            Left, Right, Up, Down, ZoomOut, ZoomIn,RotateLeft, RotateRight,RotateUp,RotateDown
        }
        public enum TransferOption
        {
            OnlyTranslate, OnlyScale, OnlyRotate, TranslateScale, TranslateRotate, ScaleRotate, All
        }

        public int _x;
        public int _y;
        public int _w;
        public int _h;
        public int angle = 0;
        public Position _objPosition;
        public TransferOption _option;
        Bitmap myBitmap;
        public MainWindow()
        {
            InitializeComponent();

            //myBitmap = new Bitmap(@"C:\Users\Nantana_Run\Desktop\for master hapyou\NeuronWinform copy for recognition\NeuronWinform\Resource\ganchan.jpg");
            _x = 423;
            _y = 313;
            _h = 64;
            _w = 75;
            _objPosition = Position.Down;

            n = new Neuron();
            n.mainwindow = this;
            //myCanvas.Neuron = n;

            myHMM = new HMM();
            myHMM.mainwindow = this;

            database = new Database();
            gridSamples.AutoGenerateColumns = false;
            cbClasses.DataSource = database.Classes;
            gridSamples.DataSource = database.Samples;

            openDataDialog.InitialDirectory = Path.Combine(Application.StartupPath, "Resources");

            sequence = new List<PointXYZ>();
            n.RaiseCustomEventChangeGesture += HandleCustomEventChangeGesture;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            n.Load();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            n.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            n.btnConnect_Click(sender, e);
        }

        private void btnLearnHMM_Click(object sender, EventArgs e)
        {
            if (gridSamples.Rows.Count == 0)
            {
                MessageBox.Show("Please load or insert some data first.");
                return;
            }

            myHMM.LearnHMM();
        }

        private void btnFile_MouseDown(object sender, MouseEventArgs e)
        {
            menuFile.Show(button4, button4.PointToClient(Cursor.Position));
        }

        private void openDataStripMenuItem_Click(object sender, EventArgs e)
        {
            openDataDialog.ShowDialog();
        }

        private void saveDataStripMenuItem_Click(object sender, EventArgs e)
        {
            saveDataDialog.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            myHMM.hmm = null;
            myHMM.hcrf = null;

            using (var stream = openDataDialog.OpenFile())
            {
                if(database.Load(stream) == "Load File Complete!!")
                {
                    myHMM.LearnHMM();
                    statusLabel.Text = "Load File Complete!!";
                }
                else
                {
                    statusLabel.Text = "Load File Error!!";
                }
            }
        }

        private void saveDataDialog_FileOk(object sender, CancelEventArgs e)
        {
            using (var stream = saveDataDialog.OpenFile())
                database.Save(stream);
        }

        private void btnInsertGesture_Click(object sender, EventArgs e)
        {
            n.addGesture();
        }

        private void noGestureBtt_Click(object sender, EventArgs e)
        {
            n.CancelGesture();
        }

        delegate void SetTextCallback(Label l,string text);
        private void SetTextLabel(Label l, string text)
        {
            if (l.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextLabel);
                this.Invoke(d, new object[] { l,text });
            }
            else
            {
                l.Text = text;
            }
        }

        delegate void SetVisiblePanelCallback(Panel p,bool isVisible);
        private void SetVisiblePanel(Panel p,bool isVisible)
        {
            if (p.InvokeRequired)
            {
                SetVisiblePanelCallback d = new SetVisiblePanelCallback(SetVisiblePanel);
                this.Invoke(d, new object[] { p, isVisible });
            }
            else
            {
                p.Visible = isVisible;
            }
        }

        delegate void SetVisibleLabelCallback(Label l, bool isVisible);
        private void SetVisibleLabel(Label l, bool isVisible)
        {
            if (l.InvokeRequired)
            {
                SetVisibleLabelCallback d = new SetVisibleLabelCallback(SetVisibleLabel);
                this.Invoke(d, new object[] { l, isVisible });
            }
            else
            {
                l.Visible = isVisible;
            }
        }

        delegate void SetItemComboboxCallback(ComboBox c, string text);
        private void SetItemCombobox(ComboBox c, string text)
        {
            if (c.InvokeRequired)
            {
                SetItemComboboxCallback d = new SetItemComboboxCallback(SetItemCombobox);
                this.Invoke(d, new object[] { c, text });
            }
            else
            {
                c.SelectedItem = text;
            }
        }
        delegate void SetEnebleButtonCallback(Button b, bool isVisible);
        private void SetEnebleButton(Button b, bool isVisible)
        {
            if (b.InvokeRequired)
            {
                SetEnebleButtonCallback d = new SetEnebleButtonCallback(SetEnebleButton);
                this.Invoke(d, new object[] { b, isVisible });
            }
            else
            {
                b.Enabled = isVisible;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            n.CancelGesture();
        }

        private void radioButton1_SaveState(object sender, EventArgs e)
        {
            CurrentState = 0;
        }

        private void radioButton2_RecogState(object sender, EventArgs e)
        {
            CurrentState = 1;
        }

        private void HandleCustomEventChangeGesture(Object sender, EventChangeGesture e)
        {
            _objPosition = e.State;
            if (_option == TransferOption.OnlyTranslate)
                Translate();
            else if (_option == TransferOption.TranslateScale)
                TranslateScale();
            else if (_option == TransferOption.TranslateRotate)
                TranslateRotate();
            else if (_option == TransferOption.OnlyScale)
                Scale();
            else if (_option == TransferOption.ScaleRotate)
                ScaleRotate();
            else if (_option == TransferOption.OnlyRotate)
                Rotate();
            else
                TranslateScaleRotate();
            this.Invalidate();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if(myBitmap != null)
                e.Graphics.DrawImage(myBitmap, _x, _y, _w, _h);
        }
        
        private void Translate()
        {
            if (_objPosition == Position.Right)
                Translate_Right();
            else if (_objPosition == Position.Left)
                Translate_Left();
            else if (_objPosition == Position.Up)
                Translate_Up();
            else if (_objPosition == Position.Down)
                Translate_Down();
            Invalidate();
        }
        private void TranslateScale()
        {
            if (_objPosition == Position.Right)
                Translate_Right();
            else if (_objPosition == Position.Left)
                Translate_Left();
            else if (_objPosition == Position.Up)
                Translate_Up();
            else if (_objPosition == Position.Down)
                Translate_Down();
            else if (_objPosition == Position.ZoomIn)
                Scale_ZoomIn();
            else if (_objPosition == Position.ZoomOut)
                Scale_ZoomOut();
            Invalidate();
        }
        private void Scale()
        {
            if (_objPosition == Position.ZoomIn)
                Scale_ZoomIn();
            else if (_objPosition == Position.ZoomOut)
                Scale_ZoomOut();
            Invalidate();
        }
        private void Rotate()
        {
            if (_objPosition == Position.RotateRight)
                myBitmap = Utilities.RotateImage(myBitmap, ++angle);
            else if (_objPosition == Position.RotateLeft)
                myBitmap = Utilities.RotateImage(myBitmap, --angle);
            Invalidate();
        }
        private void TranslateRotate()
        {
            if (_objPosition == Position.Right)
                Translate_Right();
            else if (_objPosition == Position.Left)
                Translate_Left();
            else if (_objPosition == Position.Up)
                Translate_Up();
            else if (_objPosition == Position.Down)
                Translate_Down();
            else if (_objPosition == Position.RotateRight)
                myBitmap = Utilities.RotateImage(myBitmap, angle++);
            else if (_objPosition == Position.RotateLeft)
                myBitmap = Utilities.RotateImage(myBitmap, angle--);
            Invalidate();
        }
        private void ScaleRotate()
        {
            if (_objPosition == Position.ZoomIn)
                Scale_ZoomIn();
            else if (_objPosition == Position.ZoomOut)
                Scale_ZoomOut();
            else if (_objPosition == Position.RotateRight)
                myBitmap = Utilities.RotateImage(myBitmap, angle++);
            else if (_objPosition == Position.RotateLeft)
                myBitmap = Utilities.RotateImage(myBitmap, angle--);
            Invalidate();
        }
        private void TranslateScaleRotate()
        {
            if (_objPosition == Position.Right)
                Translate_Right();
            else if (_objPosition == Position.Left)
                Translate_Left();
            else if (_objPosition == Position.Up)
                Translate_Up();
            else if (_objPosition == Position.Down)
                Translate_Down();
            else if (_objPosition == Position.ZoomIn)
                Scale_ZoomIn();
            else if (_objPosition == Position.ZoomOut)
                Scale_ZoomOut();
            else if (_objPosition == Position.RotateRight)
                myBitmap = Utilities.RotateImage(myBitmap, ++angle);
            else if (_objPosition == Position.RotateLeft)
                myBitmap = Utilities.RotateImage(myBitmap, --angle);
            Invalidate();
        }

        private void Translate_Right()
        {
            if (_x + 30 < 810)
                _x += 30;
        }
        private void Translate_Left()
        {
            if (_x - 30 > 0)
                _x -= 30;
        }
        private void Translate_Up()
        {
            if (_y - 30 > 0)
                _y -= 30;
        }
        private void Translate_Down()
        {
            if (_y + 30 < 600)
                _y += 30;
        }
        private void Scale_ZoomIn()
        {
            if (_w + 10 < 810 && _h + 10 < 640)
            {
                _w += 10;
                _h += 10;
            }
        }
        private void Scale_ZoomOut()
        {
            if (_w - 10 > 75 && _h - 10 > 64)
            {
                _w -= 10;
                _h -= 10;
            }
        }

        public PointXYZ[] GetSequence()
        {
            return sequence.ToArray();
        }

        public void ClearSequence()
        {
            sequence.Clear();
        }

        private void translate_CheckedChanged(object sender, EventArgs e)
        {
            checkOption();
        }

        private void scale_CheckedChanged(object sender, EventArgs e)
        {
            checkOption();
        }

        private void rotate_CheckedChanged(object sender, EventArgs e)
        {
            checkOption();
        }
        private void checkOption()
        {
            if (trans_chk.Checked && !scale_chk.Checked && !rotate_chk.Checked)
                _option = TransferOption.OnlyTranslate;
            else if (trans_chk.Checked && !scale_chk.Checked && rotate_chk.Checked)
                _option = TransferOption.TranslateRotate;
            else if (trans_chk.Checked && scale_chk.Checked && !rotate_chk.Checked)
                _option = TransferOption.TranslateScale;
            else if (!trans_chk.Checked && scale_chk.Checked && !rotate_chk.Checked)
                _option = TransferOption.OnlyScale;
            else if (!trans_chk.Checked && scale_chk.Checked && rotate_chk.Checked)
                _option = TransferOption.ScaleRotate;
            else if (!trans_chk.Checked && !scale_chk.Checked && rotate_chk.Checked)
                _option = TransferOption.OnlyRotate;
            else
                _option = TransferOption.All;
        }

        private void lfd_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                myBitmap = new Bitmap(openImgDialog.FileName);
                Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chooseImgBtt_Click(object sender, EventArgs e)
        {
            openImgDialog.InitialDirectory = Application.StartupPath;
            openImgDialog.ShowDialog();
        }
    }
}
