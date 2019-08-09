namespace CSharpTest {
    partial class FormAsyncAwaitMs {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Start_Button = new System.Windows.Forms.Button();
            this.lblReport = new System.Windows.Forms.Label();
            this.pbrReport = new System.Windows.Forms.ProgressBar();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Location = new System.Drawing.Point(263, 59);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(75, 23);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "Start";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Location = new System.Drawing.Point(12, 38);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(38, 12);
            this.lblReport.TabIndex = 2;
            this.lblReport.Text = "label1";
            // 
            // pbrReport
            // 
            this.pbrReport.Location = new System.Drawing.Point(12, 12);
            this.pbrReport.Name = "pbrReport";
            this.pbrReport.Size = new System.Drawing.Size(407, 23);
            this.pbrReport.TabIndex = 3;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Location = new System.Drawing.Point(344, 59);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 0;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // FormAsyncAwaitMs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 107);
            this.Controls.Add(this.pbrReport);
            this.Controls.Add(this.lblReport);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.Start_Button);
            this.Name = "FormAsyncAwaitMs";
            this.Text = "FormAsyncAwaitMs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.ProgressBar pbrReport;
        private System.Windows.Forms.Button Cancel_Button;
    }
}