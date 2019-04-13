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
            this.buttonLearn = new System.Windows.Forms.Button();
            this.comboBoxSelectChar = new System.Windows.Forms.ComboBox();
            this.vidOcrLearnImage = new MRVisionLib.SKZoomAndPanWindow();
            this.SuspendLayout();
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(872, 88);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(132, 49);
            this.buttonLearn.TabIndex = 1;
            this.buttonLearn.Text = "LearnChar";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // comboBoxSelectChar
            // 
            this.comboBoxSelectChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectChar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxSelectChar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSelectChar.FormattingEnabled = true;
            this.comboBoxSelectChar.Location = new System.Drawing.Point(872, 22);
            this.comboBoxSelectChar.Name = "comboBoxSelectChar";
            this.comboBoxSelectChar.Size = new System.Drawing.Size(132, 28);
            this.comboBoxSelectChar.TabIndex = 2;
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
            this.Controls.Add(this.comboBoxSelectChar);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.vidOcrLearnImage);
            this.Name = "DialogOcrLearn";
            this.Text = "DialogOcrLearn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogOcrLearn_FormClosing);
            this.Load += new System.EventHandler(this.DialogOcrLearn_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MRVisionLib.SKZoomAndPanWindow vidOcrLearnImage;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.ComboBox comboBoxSelectChar;
    }
}