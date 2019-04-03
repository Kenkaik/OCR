namespace OCR
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.StartCamera = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelOcrRectHeight = new System.Windows.Forms.Label();
            this.labelOcrRectWidth = new System.Windows.Forms.Label();
            this.labelOcrRectY = new System.Windows.Forms.Label();
            this.labelOcrRectX = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSaveTemplate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.vidCameraLive = new MRVisionLib.SKZoomAndPanWindow();
            this.buttonFindTemplate = new System.Windows.Forms.Button();
            this.buttonLearnChar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // StartCamera
            // 
            this.StartCamera.Location = new System.Drawing.Point(881, 43);
            this.StartCamera.Name = "StartCamera";
            this.StartCamera.Size = new System.Drawing.Size(157, 59);
            this.StartCamera.TabIndex = 1;
            this.StartCamera.Text = "Start camera";
            this.StartCamera.UseCompatibleTextRendering = true;
            this.StartCamera.UseVisualStyleBackColor = true;
            this.StartCamera.Click += new System.EventHandler(this.StartCamera_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelOcrRectHeight);
            this.groupBox1.Controls.Add(this.labelOcrRectWidth);
            this.groupBox1.Controls.Add(this.labelOcrRectY);
            this.groupBox1.Controls.Add(this.labelOcrRectX);
            this.groupBox1.Location = new System.Drawing.Point(881, 242);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 163);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OcrRectInfo";
            // 
            // labelOcrRectHeight
            // 
            this.labelOcrRectHeight.AutoSize = true;
            this.labelOcrRectHeight.Location = new System.Drawing.Point(7, 125);
            this.labelOcrRectHeight.Name = "labelOcrRectHeight";
            this.labelOcrRectHeight.Size = new System.Drawing.Size(48, 12);
            this.labelOcrRectHeight.TabIndex = 3;
            this.labelOcrRectHeight.Text = "Height = ";
            // 
            // labelOcrRectWidth
            // 
            this.labelOcrRectWidth.AutoSize = true;
            this.labelOcrRectWidth.Location = new System.Drawing.Point(6, 98);
            this.labelOcrRectWidth.Name = "labelOcrRectWidth";
            this.labelOcrRectWidth.Size = new System.Drawing.Size(46, 12);
            this.labelOcrRectWidth.TabIndex = 2;
            this.labelOcrRectWidth.Text = "Width = ";
            // 
            // labelOcrRectY
            // 
            this.labelOcrRectY.AutoSize = true;
            this.labelOcrRectY.Location = new System.Drawing.Point(9, 66);
            this.labelOcrRectY.Name = "labelOcrRectY";
            this.labelOcrRectY.Size = new System.Drawing.Size(25, 12);
            this.labelOcrRectY.TabIndex = 1;
            this.labelOcrRectY.Text = "Y = ";
            // 
            // labelOcrRectX
            // 
            this.labelOcrRectX.AutoSize = true;
            this.labelOcrRectX.Location = new System.Drawing.Point(7, 38);
            this.labelOcrRectX.Name = "labelOcrRectX";
            this.labelOcrRectX.Size = new System.Drawing.Size(25, 12);
            this.labelOcrRectX.TabIndex = 0;
            this.labelOcrRectX.Text = "X = ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(881, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 48);
            this.button1.TabIndex = 3;
            this.button1.Text = "GetOcrAreaInfo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSaveTemplate
            // 
            this.buttonSaveTemplate.Location = new System.Drawing.Point(881, 428);
            this.buttonSaveTemplate.Name = "buttonSaveTemplate";
            this.buttonSaveTemplate.Size = new System.Drawing.Size(157, 61);
            this.buttonSaveTemplate.TabIndex = 4;
            this.buttonSaveTemplate.Text = "SaveTemplate";
            this.buttonSaveTemplate.UseVisualStyleBackColor = true;
            this.buttonSaveTemplate.Click += new System.EventHandler(this.buttonSaveTemplate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(881, 505);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(157, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // vidCameraLive
            // 
            this.vidCameraLive.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.vidCameraLive.EnableRectCommonRoi = false;
            this.vidCameraLive.EnableRectLineScanArea = false;
            this.vidCameraLive.EnableRectMaskAndWafer = false;
            this.vidCameraLive.EnableTrackPattern = false;
            this.vidCameraLive.EnableZoom = false;
            this.vidCameraLive.Location = new System.Drawing.Point(30, 43);
            this.vidCameraLive.MaskMp = null;
            this.vidCameraLive.Name = "vidCameraLive";
            this.vidCameraLive.Size = new System.Drawing.Size(783, 660);
            this.vidCameraLive.TabIndex = 0;
            this.vidCameraLive.WaferMp = null;
            // 
            // buttonFindTemplate
            // 
            this.buttonFindTemplate.Location = new System.Drawing.Point(881, 665);
            this.buttonFindTemplate.Name = "buttonFindTemplate";
            this.buttonFindTemplate.Size = new System.Drawing.Size(157, 23);
            this.buttonFindTemplate.TabIndex = 6;
            this.buttonFindTemplate.Text = "FindTemplate";
            this.buttonFindTemplate.UseVisualStyleBackColor = true;
            this.buttonFindTemplate.Click += new System.EventHandler(this.buttonFindTemplate_Click);
            // 
            // buttonLearnChar
            // 
            this.buttonLearnChar.Location = new System.Drawing.Point(1104, 43);
            this.buttonLearnChar.Name = "buttonLearnChar";
            this.buttonLearnChar.Size = new System.Drawing.Size(143, 59);
            this.buttonLearnChar.TabIndex = 7;
            this.buttonLearnChar.Text = "Learn";
            this.buttonLearnChar.UseVisualStyleBackColor = true;
            this.buttonLearnChar.Click += new System.EventHandler(this.buttonLearnChar_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 749);
            this.Controls.Add(this.buttonLearnChar);
            this.Controls.Add(this.buttonFindTemplate);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonSaveTemplate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StartCamera);
            this.Controls.Add(this.vidCameraLive);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MRVisionLib.SKZoomAndPanWindow vidCameraLive;
        private System.Windows.Forms.Button StartCamera;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelOcrRectHeight;
        private System.Windows.Forms.Label labelOcrRectWidth;
        private System.Windows.Forms.Label labelOcrRectY;
        private System.Windows.Forms.Label labelOcrRectX;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSaveTemplate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonFindTemplate;
        private System.Windows.Forms.Button buttonLearnChar;
    }
}

