using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace NeuronWinform
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.gridSamples = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colClassification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.cbClasses = new System.Windows.Forms.ComboBox();
            this.panelClassification = new System.Windows.Forms.Panel();
            this.labelGesture = new System.Windows.Forms.Label();
            this.menuFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveDataDialog = new System.Windows.Forms.SaveFileDialog();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rotate_chk = new System.Windows.Forms.CheckBox();
            this.scale_chk = new System.Windows.Forms.CheckBox();
            this.trans_chk = new System.Windows.Forms.CheckBox();
            this.chooseImgBtt = new System.Windows.Forms.Button();
            this.openImgDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSamples)).BeginInit();
            this.panelClassification.SuspendLayout();
            this.menuFile.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(824, 502);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TCP/IP Illustration";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(102, 81);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(102, 56);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 19);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "7003";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(102, 31);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 19);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "192.168.12.12";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Server Port :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP :";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 664);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 28);
            this.button4.TabIndex = 22;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnFile_MouseDown);
            // 
            // gridSamples
            // 
            this.gridSamples.AllowUserToAddRows = false;
            this.gridSamples.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridSamples.BackgroundColor = System.Drawing.Color.White;
            this.gridSamples.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridSamples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSamples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.colClassification});
            this.gridSamples.GridColor = System.Drawing.SystemColors.ControlLight;
            this.gridSamples.Location = new System.Drawing.Point(824, 12);
            this.gridSamples.Name = "gridSamples";
            this.gridSamples.Size = new System.Drawing.Size(204, 484);
            this.gridSamples.TabIndex = 34;
            // 
            // colImage
            // 
            this.colImage.DataPropertyName = "Bitmap";
            this.colImage.FillWeight = 30F;
            this.colImage.HeaderText = "Gesture";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            // 
            // colClassification
            // 
            this.colClassification.DataPropertyName = "OutputName";
            this.colClassification.FillWeight = 40F;
            this.colClassification.HeaderText = "Class";
            this.colClassification.Name = "colClassification";
            this.colClassification.ReadOnly = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(222, 7);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(225, 21);
            this.btnClear.TabIndex = 27;
            this.btnClear.Text = "Forget my Gesture";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(222, 34);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(225, 35);
            this.btnInsert.TabIndex = 27;
            this.btnInsert.Text = "Insert my Gesture into the database!";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsertGesture_Click);
            // 
            // cbClasses
            // 
            this.cbClasses.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbClasses.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbClasses.Location = new System.Drawing.Point(53, 34);
            this.cbClasses.Name = "cbClasses";
            this.cbClasses.Size = new System.Drawing.Size(101, 20);
            this.cbClasses.TabIndex = 23;
            // 
            // panelClassification
            // 
            this.panelClassification.BackColor = System.Drawing.Color.White;
            this.panelClassification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelClassification.Controls.Add(this.btnClear);
            this.panelClassification.Controls.Add(this.cbClasses);
            this.panelClassification.Controls.Add(this.btnInsert);
            this.panelClassification.Location = new System.Drawing.Point(227, 51);
            this.panelClassification.Name = "panelClassification";
            this.panelClassification.Size = new System.Drawing.Size(461, 74);
            this.panelClassification.TabIndex = 36;
            this.panelClassification.Visible = false;
            // 
            // labelGesture
            // 
            this.labelGesture.AutoSize = true;
            this.labelGesture.Location = new System.Drawing.Point(442, 672);
            this.labelGesture.Name = "labelGesture";
            this.labelGesture.Size = new System.Drawing.Size(19, 12);
            this.labelGesture.TabIndex = 28;
            this.labelGesture.Text = ".......";
            // 
            // menuFile
            // 
            this.menuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuFile.Name = "contextMenuStrip1";
            this.menuFile.Size = new System.Drawing.Size(105, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem1.Text = "Open";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.openDataStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem2.Text = "Save";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.saveDataStripMenuItem_Click);
            // 
            // openDataDialog
            // 
            this.openDataDialog.FileName = "openFileDialog1";
            this.openDataDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveDataDialog
            // 
            this.saveDataDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveDataDialog_FileOk);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(104, 670);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 16);
            this.radioButton1.TabIndex = 40;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "SaveGesture";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_SaveState);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(198, 670);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 41;
            this.radioButton2.Text = "RecogGesture";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_RecogState);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(955, 679);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(73, 12);
            this.statusLabel.TabIndex = 42;
            this.statusLabel.Text = "                 ";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 672);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "Recognition Result : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rotate_chk);
            this.groupBox2.Controls.Add(this.scale_chk);
            this.groupBox2.Controls.Add(this.trans_chk);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(238, 43);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transform Options";
            // 
            // rotate_chk
            // 
            this.rotate_chk.AutoSize = true;
            this.rotate_chk.Location = new System.Drawing.Point(169, 18);
            this.rotate_chk.Name = "rotate_chk";
            this.rotate_chk.Size = new System.Drawing.Size(58, 16);
            this.rotate_chk.TabIndex = 2;
            this.rotate_chk.Text = "Rotate";
            this.rotate_chk.UseVisualStyleBackColor = true;
            this.rotate_chk.CheckedChanged += new System.EventHandler(this.rotate_CheckedChanged);
            // 
            // scale_chk
            // 
            this.scale_chk.AutoSize = true;
            this.scale_chk.Location = new System.Drawing.Point(100, 18);
            this.scale_chk.Name = "scale_chk";
            this.scale_chk.Size = new System.Drawing.Size(52, 16);
            this.scale_chk.TabIndex = 1;
            this.scale_chk.Text = "Scale";
            this.scale_chk.UseVisualStyleBackColor = true;
            this.scale_chk.CheckedChanged += new System.EventHandler(this.scale_CheckedChanged);
            // 
            // trans_chk
            // 
            this.trans_chk.AutoSize = true;
            this.trans_chk.Checked = true;
            this.trans_chk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.trans_chk.Location = new System.Drawing.Point(12, 18);
            this.trans_chk.Name = "trans_chk";
            this.trans_chk.Size = new System.Drawing.Size(72, 16);
            this.trans_chk.TabIndex = 0;
            this.trans_chk.Text = "Translate";
            this.trans_chk.UseVisualStyleBackColor = true;
            this.trans_chk.CheckedChanged += new System.EventHandler(this.translate_CheckedChanged);
            // 
            // chooseImgBtt
            // 
            this.chooseImgBtt.Location = new System.Drawing.Point(247, 12);
            this.chooseImgBtt.Name = "chooseImgBtt";
            this.chooseImgBtt.Size = new System.Drawing.Size(91, 23);
            this.chooseImgBtt.TabIndex = 45;
            this.chooseImgBtt.Text = "Choose Image..";
            this.chooseImgBtt.UseVisualStyleBackColor = true;
            this.chooseImgBtt.Click += new System.EventHandler(this.chooseImgBtt_Click);
            // 
            // openImgDialog
            // 
            this.openImgDialog.DefaultExt = "png";
            this.openImgDialog.FileName = "loadImg";
            this.openImgDialog.Filter = "PNG files (*.png)|*.png|JPEG Image Files (*.jpg)|*.jpg|All files (*.*)|*.*";
            this.openImgDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.lfd_FileOk);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 700);
            this.Controls.Add(this.chooseImgBtt);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.labelGesture);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.panelClassification);
            this.Controls.Add(this.gridSamples);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainWindow";
            this.Text = "Gesture Recognition by HMM(Neuron Reader)_Demo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSamples)).EndInit();
            this.panelClassification.ResumeLayout(false);
            this.menuFile.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        //public Canvas myCanvas;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btnConnect;
        public System.Windows.Forms.TextBox txtPort;
        public System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.DataGridView gridSamples;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassification;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnInsert;
        public System.Windows.Forms.ComboBox cbClasses;
        public System.Windows.Forms.Panel panelClassification;
        private System.Windows.Forms.ContextMenuStrip menuFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.OpenFileDialog openDataDialog;
        private System.Windows.Forms.SaveFileDialog saveDataDialog;
        public Label labelGesture;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        public Label statusLabel;
        private Label label3;
        private GroupBox groupBox2;
        private CheckBox rotate_chk;
        private CheckBox scale_chk;
        private CheckBox trans_chk;
        private Button chooseImgBtt;
        private OpenFileDialog openImgDialog;
    }
}

