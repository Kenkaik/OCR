﻿namespace OCR
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
            this.vidOcrLearnImage = new MRVisionLib.SKZoomAndPanWindow();
            this.SuspendLayout();
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(1163, 110);
            this.buttonLearn.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(176, 61);
            this.buttonLearn.TabIndex = 1;
            this.buttonLearn.Text = "LearnChar";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // lvChar
            // 
            this.lvChar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvChar.FullRowSelect = true;
            this.lvChar.GridLines = true;
            this.lvChar.Location = new System.Drawing.Point(1163, 197);
            this.lvChar.Name = "lvChar";
            this.lvChar.Size = new System.Drawing.Size(245, 319);
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
            this.lvImage.Location = new System.Drawing.Point(1455, 197);
            this.lvImage.MultiSelect = false;
            this.lvImage.Name = "lvImage";
            this.lvImage.Size = new System.Drawing.Size(194, 319);
            this.lvImage.TabIndex = 4;
            this.lvImage.UseCompatibleStateImageBehavior = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(1527, 140);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(122, 31);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // vidOcrLearnImage
            // 
            this.vidOcrLearnImage.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.vidOcrLearnImage.EnableRectCommonRoi = false;
            this.vidOcrLearnImage.EnableRectLineScanArea = false;
            this.vidOcrLearnImage.EnableRectMaskAndWafer = false;
            this.vidOcrLearnImage.EnableTrackPattern = false;
            this.vidOcrLearnImage.EnableZoom = false;
            this.vidOcrLearnImage.Location = new System.Drawing.Point(31, 28);
            this.vidOcrLearnImage.Margin = new System.Windows.Forms.Padding(4);
            this.vidOcrLearnImage.MaskMp = null;
            this.vidOcrLearnImage.Name = "vidOcrLearnImage";
            this.vidOcrLearnImage.Size = new System.Drawing.Size(1044, 825);
            this.vidOcrLearnImage.TabIndex = 0;
            this.vidOcrLearnImage.WaferMp = null;
            // 
            // DialogOcrLearn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1661, 958);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.lvImage);
            this.Controls.Add(this.lvChar);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.vidOcrLearnImage);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DialogOcrLearn";
            this.Text = "DialogOcrLearn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogOcrLearn_FormClosing);
            this.Load += new System.EventHandler(this.DialogOcrLearn_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MRVisionLib.SKZoomAndPanWindow vidOcrLearnImage;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.ListView lvChar;
        private System.Windows.Forms.ImageList ilOcrImage;
        private System.Windows.Forms.ListView lvImage;
        private System.Windows.Forms.Button buttonDelete;
    }
}