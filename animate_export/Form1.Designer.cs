namespace animate_export
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.step1 = new System.Windows.Forms.Panel();
            this.selectFileLabel = new System.Windows.Forms.Label();
            this.selectFile = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.step1Label = new System.Windows.Forms.Label();
            this.step2Label = new System.Windows.Forms.Label();
            this.step3Label = new System.Windows.Forms.Label();
            this.step4Label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.step2 = new System.Windows.Forms.Panel();
            this.listView = new System.Windows.Forms.ListView();
            this.listHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.step3 = new System.Windows.Forms.Panel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.step4 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.step1.SuspendLayout();
            this.step2.SuspendLayout();
            this.step3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.step4.SuspendLayout();
            this.SuspendLayout();
            // 
            // step1
            // 
            this.step1.Controls.Add(this.selectFileLabel);
            this.step1.Controls.Add(this.selectFile);
            this.step1.Location = new System.Drawing.Point(27, 55);
            this.step1.Name = "step1";
            this.step1.Size = new System.Drawing.Size(322, 221);
            this.step1.TabIndex = 0;
            // 
            // selectFileLabel
            // 
            this.selectFileLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.selectFileLabel.Location = new System.Drawing.Point(3, 111);
            this.selectFileLabel.Name = "selectFileLabel";
            this.selectFileLabel.Size = new System.Drawing.Size(316, 23);
            this.selectFileLabel.TabIndex = 3;
            this.selectFileLabel.Text = "请选择一个Animations.rxdata文件";
            this.selectFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectFile
            // 
            this.selectFile.Location = new System.Drawing.Point(123, 69);
            this.selectFile.Name = "selectFile";
            this.selectFile.Size = new System.Drawing.Size(76, 27);
            this.selectFile.TabIndex = 0;
            this.selectFile.Text = "选择文件";
            this.selectFile.UseVisualStyleBackColor = true;
            this.selectFile.Click += new System.EventHandler(this.selectFile_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(274, 293);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "下一步";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Location = new System.Drawing.Point(27, 293);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 23);
            this.prevButton.TabIndex = 1;
            this.prevButton.Text = "上一步";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // step1Label
            // 
            this.step1Label.AutoSize = true;
            this.step1Label.Location = new System.Drawing.Point(26, 28);
            this.step1Label.Name = "step1Label";
            this.step1Label.Size = new System.Drawing.Size(53, 12);
            this.step1Label.TabIndex = 1;
            this.step1Label.Text = "选择文件";
            // 
            // step2Label
            // 
            this.step2Label.AutoSize = true;
            this.step2Label.Location = new System.Drawing.Point(116, 28);
            this.step2Label.Name = "step2Label";
            this.step2Label.Size = new System.Drawing.Size(53, 12);
            this.step2Label.TabIndex = 2;
            this.step2Label.Text = "选择动画";
            // 
            // step3Label
            // 
            this.step3Label.AutoSize = true;
            this.step3Label.Location = new System.Drawing.Point(206, 28);
            this.step3Label.Name = "step3Label";
            this.step3Label.Size = new System.Drawing.Size(53, 12);
            this.step3Label.TabIndex = 3;
            this.step3Label.Text = "预览效果";
            // 
            // step4Label
            // 
            this.step4Label.AutoSize = true;
            this.step4Label.Location = new System.Drawing.Point(296, 28);
            this.step4Label.Name = "step4Label";
            this.step4Label.Size = new System.Drawing.Size(53, 12);
            this.step4Label.TabIndex = 4;
            this.step4Label.Text = "导出动画";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "→";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(179, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "→";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "→";
            // 
            // step2
            // 
            this.step2.Controls.Add(this.listView);
            this.step2.Location = new System.Drawing.Point(27, 55);
            this.step2.Name = "step2";
            this.step2.Size = new System.Drawing.Size(322, 221);
            this.step2.TabIndex = 4;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.listHeader0,
            this.listHeader1,
            this.listHeader2,
            this.listHeader3});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.HideSelection = false;
            this.listView.LabelWrap = false;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(322, 221);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // listHeader0
            // 
            this.listHeader0.Text = "编号";
            this.listHeader0.Width = 43;
            // 
            // listHeader1
            // 
            this.listHeader1.Text = "名称";
            this.listHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.listHeader1.Width = 100;
            // 
            // listHeader2
            // 
            this.listHeader2.Text = "文件名";
            this.listHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.listHeader2.Width = 100;
            // 
            // listHeader3
            // 
            this.listHeader3.Text = "帧数";
            this.listHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.listHeader3.Width = 55;
            // 
            // step3
            // 
            this.step3.BackgroundImage = global::animate_export.Properties.Resources.bg;
            this.step3.Controls.Add(this.canvas);
            this.step3.Location = new System.Drawing.Point(27, 55);
            this.step3.Name = "step3";
            this.step3.Size = new System.Drawing.Size(322, 221);
            this.step3.TabIndex = 4;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Transparent;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(322, 221);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            // 
            // step4
            // 
            this.step4.Controls.Add(this.textBox1);
            this.step4.Controls.Add(this.checkBox1);
            this.step4.Controls.Add(this.label5);
            this.step4.Controls.Add(this.label4);
            this.step4.Controls.Add(this.label3);
            this.step4.Controls.Add(this.label1);
            this.step4.Controls.Add(this.exportButton);
            this.step4.Location = new System.Drawing.Point(27, 55);
            this.step4.Name = "step4";
            this.step4.Size = new System.Drawing.Size(322, 221);
            this.step4.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 78);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 21);
            this.textBox1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(35, 81);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "导出音效";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(19, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(288, 23);
            this.label5.TabIndex = 7;
            this.label5.Text = "如导出音效，请将音效文件复制到sounds目录下。";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(19, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(288, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "具体使用方法详见文档说明。";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(19, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "在HTML5魔塔样板中引用文件，即可使用动画。";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(19, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "将导出animate格式的文件，包含图片和帧数等信息。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(123, 39);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(76, 27);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "点此导出";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 337);
            this.Controls.Add(this.step4);
            this.Controls.Add(this.step3);
            this.Controls.Add(this.step1);
            this.Controls.Add(this.step2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.step4Label);
            this.Controls.Add(this.step3Label);
            this.Controls.Add(this.step2Label);
            this.Controls.Add(this.step1Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RM动画导出器 By 艾之葵";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.step1.ResumeLayout(false);
            this.step2.ResumeLayout(false);
            this.step3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.step4.ResumeLayout(false);
            this.step4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel step1;
        private System.Windows.Forms.Label step1Label;
        private System.Windows.Forms.Label step2Label;
        private System.Windows.Forms.Label step3Label;
        private System.Windows.Forms.Label step4Label;
        private System.Windows.Forms.Button selectFile;
        private System.Windows.Forms.Label selectFileLabel;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel step2;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader listHeader0;
        private System.Windows.Forms.ColumnHeader listHeader1;
        private System.Windows.Forms.ColumnHeader listHeader2;
        private System.Windows.Forms.ColumnHeader listHeader3;
        private System.Windows.Forms.Panel step3;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Panel step4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;

    }
}

