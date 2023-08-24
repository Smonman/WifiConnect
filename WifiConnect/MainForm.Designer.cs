namespace WifiConnect
{
    partial class MainForm
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
            CamerasComboBox = new ComboBox();
            label1 = new Label();
            PictureBox = new PictureBox();
            StatusStrip = new StatusStrip();
            StatusLabel = new ToolStripStatusLabel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            StatusProgressBar = new ToolStripProgressBar();
            MessageLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)PictureBox).BeginInit();
            StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CamerasComboBox
            // 
            CamerasComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            CamerasComboBox.FormattingEnabled = true;
            CamerasComboBox.Location = new Point(69, 6);
            CamerasComboBox.Name = "CamerasComboBox";
            CamerasComboBox.Size = new Size(403, 23);
            CamerasComboBox.TabIndex = 0;
            CamerasComboBox.SelectedIndexChanged += this.CamerasComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 1;
            label1.Text = "Camera:";
            // 
            // PictureBox
            // 
            PictureBox.Location = new Point(12, 35);
            PictureBox.Name = "PictureBox";
            PictureBox.Size = new Size(460, 460);
            PictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            PictureBox.TabIndex = 2;
            PictureBox.TabStop = false;
            // 
            // StatusStrip
            // 
            StatusStrip.Items.AddRange(new ToolStripItem[] { StatusProgressBar, StatusLabel, MessageLabel });
            StatusStrip.Location = new Point(0, 539);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(484, 22);
            StatusStrip.TabIndex = 3;
            StatusStrip.Text = "statusStrip";
            // 
            // StatusLabel
            // 
            StatusLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(118, 17);
            StatusLabel.Text = "toolStripStatusLabel1";
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += this.Timer_Tick;
            // 
            // StatusProgressBar
            // 
            StatusProgressBar.Name = "StatusProgressBar";
            StatusProgressBar.Size = new Size(100, 16);
            StatusProgressBar.Style = ProgressBarStyle.Marquee;
            // 
            // MessageLabel
            // 
            MessageLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new Size(118, 17);
            MessageLabel.Text = "toolStripStatusLabel1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(484, 561);
            this.Controls.Add(StatusStrip);
            this.Controls.Add(PictureBox);
            this.Controls.Add(label1);
            this.Controls.Add(CamerasComboBox);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "WifiConnect";
            this.FormClosing += this.MainForm_FormClosing;
            this.Load += this.MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBox).EndInit();
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ComboBox CamerasComboBox;
        private Label label1;
        private PictureBox PictureBox;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Timer Timer;
        private ToolStripProgressBar StatusProgressBar;
        private ToolStripStatusLabel MessageLabel;
    }
}