using System.Drawing;
using System.Windows.Forms;

namespace PrinterService
{
    partial class Template
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
            this.textBox1 = new TextBox();
            base.SuspendLayout();
            this.textBox1.Location = new Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(739, 567);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "模板制作注意：\r\n1.使用文本文件作为数据源\r\n2.域内容使用数据库字段，采用第几个域，不适用具名字段\r\n3.将数据源加上别名，统一命名，这样方便开发调用，采用固定的数据源名称既可";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(763, 591);
            base.Controls.Add(this.textBox1);
            base.Name = "Template";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "关于模板制作";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}