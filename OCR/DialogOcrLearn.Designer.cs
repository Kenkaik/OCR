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
            this.comboBoxSelectChar = new System.Windows.Forms.ComboBox();
            this.lvCharImage = new System.Windows.Forms.ListView();
            this.ilOcrImage = new System.Windows.Forms.ImageList(this.components);
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
            // comboBoxSelectChar
            // 
            this.comboBoxSelectChar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectChar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxSelectChar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSelectChar.FormattingEnabled = true;
            this.comboBoxSelectChar.Location = new System.Drawing.Point(1163, 28);
            this.comboBoxSelectChar.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSelectChar.Name = "comboBoxSelectChar";
            this.comboBoxSelectChar.Size = new System.Drawing.Size(175, 32);
            this.comboBoxSelectChar.TabIndex = 2;
            this.comboBoxSelectChar.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectChar_SelectedIndexChanged);
            // 
            // lvCharImage
            // 
            this.lvCharImage.FullRowSelect = true;
            this.lvCharImage.GridLines = true;
            this.lvCharImage.Location = new System.Drawing.Point(1376, 28);
            this.lvCharImage.Name = "lvCharImage";
            this.lvCharImage.Size = new System.Drawing.Size(273, 344);
            this.lvCharImage.TabIndex = 3;
            this.lvCharImage.UseCompatibleStateImageBehavior = false;
            this.lvCharImage.View = System.Windows.Forms.View.Details;
            // 
            // ilOcrImage
            // 
            this.ilOcrImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilOcrImage.ImageSize = new System.Drawing.Size(16, 16);
            this.ilOcrImage.TransparentColor = System.Drawing.Color.Transparent;
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
            this.Controls.Add(this.lvCharImage);
            this.Controls.Add(this.comboBoxSelectChar);
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
        private System.Windows.Forms.ComboBox comboBoxSelectChar;
        private System.Windows.Forms.ListView lvCharImage;
        private System.Windows.Forms.ImageList ilOcrImage;
    }
}