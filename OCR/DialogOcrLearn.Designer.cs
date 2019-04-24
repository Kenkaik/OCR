namespace OCR
{
    partial class DialogOcrLearn
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
            this.buttonLearn = new System.Windows.Forms.Button();
            this.lvChar = new System.Windows.Forms.ListView();
            this.ilOcrImage = new System.Windows.Forms.ImageList(this.components);
            this.lvImage = new System.Windows.Forms.ListView();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSelectRoiArea = new System.Windows.Forms.Button();
            this.pictureBoxRoiArea = new System.Windows.Forms.PictureBox();
            this.buttonStartOcr = new System.Windows.Forms.Button();
            this.textBoxOcrResult = new System.Windows.Forms.TextBox();
            this.pictureBoxOcrResultImage = new System.Windows.Forms.PictureBox();
            this.textBoxOcrCalTime = new System.Windows.Forms.TextBox();
            this.vidOcrLearnImage = new MRVisionLib.SKZoomAndPanWindow();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoiArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOcrResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(872, 91);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(132, 49);
            this.buttonLearn.TabIndex = 1;
            this.buttonLearn.Text = "Learn Char";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // lvChar
            // 
            this.lvChar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvChar.FullRowSelect = true;
            this.lvChar.GridLines = true;
            this.lvChar.Location = new System.Drawing.Point(872, 305);
            this.lvChar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lvChar.Name = "lvChar";
            this.lvChar.Size = new System.Drawing.Size(185, 256);
            this.lvChar.TabIndex = 3;
            this.lvChar.UseCompatibleStateImageBehavior = false;
            this.lvChar.View = System.Windows.Forms.View.Details;
            this.lvChar.Click += new System.EventHandler(this.lvChar_Click);
            // 
            // ilOcrImage
            // 
            this.ilOcrImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilOcrImage.ImageSize = new System.Drawing.Size(16, 16);
            this.ilOcrImage.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // lvImage
            // 
            this.lvImage.BackColor = System.Drawing.Color.Violet;
            this.lvImage.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvImage.Location = new System.Drawing.Point(1091, 305);
            this.lvImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lvImage.MultiSelect = false;
            this.lvImage.Name = "lvImage";
            this.lvImage.Size = new System.Drawing.Size(146, 256);
            this.lvImage.TabIndex = 4;
            this.lvImage.UseCompatibleStateImageBehavior = false;
            this.lvImage.Click += new System.EventHandler(this.lvImage_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(1145, 577);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(92, 26);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSelectRoiArea
            // 
            this.buttonSelectRoiArea.Location = new System.Drawing.Point(872, 22);
            this.buttonSelectRoiArea.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonSelectRoiArea.Name = "buttonSelectRoiArea";
            this.buttonSelectRoiArea.Size = new System.Drawing.Size(132, 47);
            this.buttonSelectRoiArea.TabIndex = 6;
            this.buttonSelectRoiArea.Text = "Select ROI Area";
            this.buttonSelectRoiArea.UseVisualStyleBackColor = true;
            this.buttonSelectRoiArea.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBoxRoiArea
            // 
            this.pictureBoxRoiArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxRoiArea.Location = new System.Drawing.Point(1022, 22);
            this.pictureBoxRoiArea.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxRoiArea.Name = "pictureBoxRoiArea";
            this.pictureBoxRoiArea.Size = new System.Drawing.Size(215, 72);
            this.pictureBoxRoiArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRoiArea.TabIndex = 7;
            this.pictureBoxRoiArea.TabStop = false;
            // 
            // buttonStartOcr
            // 
            this.buttonStartOcr.Location = new System.Drawing.Point(872, 577);
            this.buttonStartOcr.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStartOcr.Name = "buttonStartOcr";
            this.buttonStartOcr.Size = new System.Drawing.Size(98, 26);
            this.buttonStartOcr.TabIndex = 8;
            this.buttonStartOcr.Text = "Start OCR";
            this.buttonStartOcr.UseVisualStyleBackColor = true;
            this.buttonStartOcr.Click += new System.EventHandler(this.buttonStartOcr_Click);
            // 
            // textBoxOcrResult
            // 
            this.textBoxOcrResult.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOcrResult.Location = new System.Drawing.Point(1022, 114);
            this.textBoxOcrResult.Name = "textBoxOcrResult";
            this.textBoxOcrResult.Size = new System.Drawing.Size(212, 26);
            this.textBoxOcrResult.TabIndex = 9;
            this.textBoxOcrResult.Text = "Ocr Result : ";
            // 
            // pictureBoxOcrResultImage
            // 
            this.pictureBoxOcrResultImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxOcrResultImage.Location = new System.Drawing.Point(872, 158);
            this.pictureBoxOcrResultImage.Name = "pictureBoxOcrResultImage";
            this.pictureBoxOcrResultImage.Size = new System.Drawing.Size(362, 125);
            this.pictureBoxOcrResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOcrResultImage.TabIndex = 10;
            this.pictureBoxOcrResultImage.TabStop = false;
            // 
            // textBoxOcrCalTime
            // 
            this.textBoxOcrCalTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOcrCalTime.Location = new System.Drawing.Point(979, 577);
            this.textBoxOcrCalTime.Name = "textBoxOcrCalTime";
            this.textBoxOcrCalTime.Size = new System.Drawing.Size(156, 26);
            this.textBoxOcrCalTime.TabIndex = 11;
            this.textBoxOcrCalTime.Text = "Time(ms) = ";
            // 
            // vidOcrLearnImage
            // 
            this.vidOcrLearnImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.vidOcrLearnImage.EnableRectCommonRoi = false;
            this.vidOcrLearnImage.EnableRectLineScanArea = false;
            this.vidOcrLearnImage.EnableRectMaskAndWafer = false;
            this.vidOcrLearnImage.EnableTrackPattern = false;
            this.vidOcrLearnImage.EnableZoom = false;
            this.vidOcrLearnImage.Location = new System.Drawing.Point(23, 22);
            this.vidOcrLearnImage.MaskMp = null;
            this.vidOcrLearnImage.Name = "vidOcrLearnImage";
            this.vidOcrLearnImage.Size = new System.Drawing.Size(783, 660);
            this.vidOcrLearnImage.TabIndex = 0;
            this.vidOcrLearnImage.WaferMp = null;
            // 
            // DialogOcrLearn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 766);
            this.Controls.Add(this.textBoxOcrCalTime);
            this.Controls.Add(this.pictureBoxOcrResultImage);
            this.Controls.Add(this.textBoxOcrResult);
            this.Controls.Add(this.buttonStartOcr);
            this.Controls.Add(this.pictureBoxRoiArea);
            this.Controls.Add(this.buttonSelectRoiArea);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.lvImage);
            this.Controls.Add(this.lvChar);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.vidOcrLearnImage);
            this.Name = "DialogOcrLearn";
            this.Text = "DialogOcrLearn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogOcrLearn_FormClosing);
            this.Load += new System.EventHandler(this.DialogOcrLearn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoiArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOcrResultImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MRVisionLib.SKZoomAndPanWindow vidOcrLearnImage;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.ListView lvChar;
        private System.Windows.Forms.ImageList ilOcrImage;
        private System.Windows.Forms.ListView lvImage;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSelectRoiArea;
        private System.Windows.Forms.PictureBox pictureBoxRoiArea;
        private System.Windows.Forms.Button buttonStartOcr;
        private System.Windows.Forms.TextBox textBoxOcrResult;
        private System.Windows.Forms.PictureBox pictureBoxOcrResultImage;
        private System.Windows.Forms.TextBox textBoxOcrCalTime;
    }
}