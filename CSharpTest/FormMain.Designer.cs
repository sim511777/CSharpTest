namespace CSharpTest {
    partial class FormMain {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
         this.panel1 = new System.Windows.Forms.Panel();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.lbxFunc = new System.Windows.Forms.ListBox();
         this.splitter4 = new System.Windows.Forms.Splitter();
         this.grdParameter = new System.Windows.Forms.PropertyGrid();
         this.tbxConsole = new System.Windows.Forms.TextBox();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.splitter1 = new System.Windows.Forms.Splitter();
         this.panel2 = new System.Windows.Forms.Panel();
         this.btnClear = new System.Windows.Forms.Button();
         this.tbxCode = new ICSharpCode.TextEditor.TextEditorControl();
         this.panel3 = new System.Windows.Forms.Panel();
         this.splitter2 = new System.Windows.Forms.Splitter();
         this.panel1.SuspendLayout();
         this.groupBox3.SuspendLayout();
         this.panel2.SuspendLayout();
         this.panel3.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.panel1.Controls.Add(this.groupBox3);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(258, 771);
         this.panel1.TabIndex = 0;
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.lbxFunc);
         this.groupBox3.Controls.Add(this.splitter4);
         this.groupBox3.Controls.Add(this.grdParameter);
         this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.groupBox3.Location = new System.Drawing.Point(0, 0);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(256, 769);
         this.groupBox3.TabIndex = 6;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Test Item";
         // 
         // lbxFunc
         // 
         this.lbxFunc.Dock = System.Windows.Forms.DockStyle.Fill;
         this.lbxFunc.FormattingEnabled = true;
         this.lbxFunc.ItemHeight = 12;
         this.lbxFunc.Location = new System.Drawing.Point(3, 17);
         this.lbxFunc.Name = "lbxFunc";
         this.lbxFunc.Size = new System.Drawing.Size(250, 655);
         this.lbxFunc.TabIndex = 4;
         this.lbxFunc.SelectedIndexChanged += new System.EventHandler(this.lbxTest_SelectedIndexChanged);
         // 
         // splitter4
         // 
         this.splitter4.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.splitter4.Location = new System.Drawing.Point(3, 672);
         this.splitter4.Name = "splitter4";
         this.splitter4.Size = new System.Drawing.Size(250, 3);
         this.splitter4.TabIndex = 5;
         this.splitter4.TabStop = false;
         // 
         // grdParameter
         // 
         this.grdParameter.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.grdParameter.HelpVisible = false;
         this.grdParameter.Location = new System.Drawing.Point(3, 675);
         this.grdParameter.Name = "grdParameter";
         this.grdParameter.PropertySort = System.Windows.Forms.PropertySort.Categorized;
         this.grdParameter.Size = new System.Drawing.Size(250, 91);
         this.grdParameter.TabIndex = 3;
         this.grdParameter.ToolbarVisible = false;
         this.grdParameter.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.grdParameter_PropertyValueChanged);
         // 
         // tbxConsole
         // 
         this.tbxConsole.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tbxConsole.Font = new System.Drawing.Font("굴림체", 9F);
         this.tbxConsole.Location = new System.Drawing.Point(0, 38);
         this.tbxConsole.Multiline = true;
         this.tbxConsole.Name = "tbxConsole";
         this.tbxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
         this.tbxConsole.Size = new System.Drawing.Size(936, 383);
         this.tbxConsole.TabIndex = 4;
         // 
         // statusStrip1
         // 
         this.statusStrip1.Location = new System.Drawing.Point(0, 771);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(1197, 22);
         this.statusStrip1.TabIndex = 0;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // splitter1
         // 
         this.splitter1.Location = new System.Drawing.Point(258, 0);
         this.splitter1.Name = "splitter1";
         this.splitter1.Size = new System.Drawing.Size(3, 771);
         this.splitter1.TabIndex = 2;
         this.splitter1.TabStop = false;
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.btnClear);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel2.Location = new System.Drawing.Point(0, 0);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(936, 38);
         this.panel2.TabIndex = 5;
         // 
         // btnClear
         // 
         this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.btnClear.Location = new System.Drawing.Point(808, 7);
         this.btnClear.Name = "btnClear";
         this.btnClear.Size = new System.Drawing.Size(116, 23);
         this.btnClear.TabIndex = 0;
         this.btnClear.Text = "Clear Console";
         this.btnClear.UseVisualStyleBackColor = true;
         this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
         // 
         // tbxCode
         // 
         this.tbxCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.tbxCode.ConvertTabsToSpaces = true;
         this.tbxCode.Dock = System.Windows.Forms.DockStyle.Top;
         this.tbxCode.IsReadOnly = false;
         this.tbxCode.Location = new System.Drawing.Point(261, 0);
         this.tbxCode.Name = "tbxCode";
         this.tbxCode.Size = new System.Drawing.Size(936, 347);
         this.tbxCode.TabIndex = 1;
         this.tbxCode.Text = "textEditorControl1";
         // 
         // panel3
         // 
         this.panel3.Controls.Add(this.tbxConsole);
         this.panel3.Controls.Add(this.panel2);
         this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel3.Location = new System.Drawing.Point(261, 350);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(936, 421);
         this.panel3.TabIndex = 7;
         // 
         // splitter2
         // 
         this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
         this.splitter2.Location = new System.Drawing.Point(261, 347);
         this.splitter2.Name = "splitter2";
         this.splitter2.Size = new System.Drawing.Size(936, 3);
         this.splitter2.TabIndex = 8;
         this.splitter2.TabStop = false;
         // 
         // FormMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1197, 793);
         this.Controls.Add(this.panel3);
         this.Controls.Add(this.splitter2);
         this.Controls.Add(this.tbxCode);
         this.Controls.Add(this.splitter1);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.statusStrip1);
         this.Name = "FormMain";
         this.Text = "C# Test";
         this.panel1.ResumeLayout(false);
         this.groupBox3.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.panel3.ResumeLayout(false);
         this.panel3.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PropertyGrid grdParameter;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TextBox tbxConsole;
        private System.Windows.Forms.ListBox lbxFunc;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClear;
        private ICSharpCode.TextEditor.TextEditorControl tbxCode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter2;
    }
}

