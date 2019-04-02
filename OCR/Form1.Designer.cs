namespace OCR
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.skZoomAndPanWindow1 = new MRVisionLib.SKZoomAndPanWindow();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(881, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 59);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start camera";
            this.button1.UseCompatibleTextRendering = true;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // skZoomAndPanWindow1
            // 
            this.skZoomAndPanWindow1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.skZoomAndPanWindow1.EnableRectLineScanArea = false;
            this.skZoomAndPanWindow1.EnableRectRoi = false;
            this.skZoomAndPanWindow1.EnableTrackPattern = false;
            this.skZoomAndPanWindow1.EnableZoom = false;
            this.skZoomAndPanWindow1.Location = new System.Drawing.Point(30, 43);
            this.skZoomAndPanWindow1.MaskMp = null;
            this.skZoomAndPanWindow1.Name = "skZoomAndPanWindow1";
            this.skZoomAndPanWindow1.Size = new System.Drawing.Size(783, 660);
            this.skZoomAndPanWindow1.TabIndex = 0;
            this.skZoomAndPanWindow1.WaferMp = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 812);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.skZoomAndPanWindow1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MRVisionLib.SKZoomAndPanWindow skZoomAndPanWindow1;
        private System.Windows.Forms.Button button1;
    }
}

