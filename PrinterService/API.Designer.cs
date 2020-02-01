using System.Drawing;
using System.Windows.Forms;

namespace PrinterService
{
    partial class API
    {
        private RichTextBox apiBox;
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
            this.apiBox = new RichTextBox();
            base.SuspendLayout();
            this.apiBox.Location = new Point(12, 12);
            this.apiBox.Name = "apiBox";
            this.apiBox.ReadOnly = true;
            this.apiBox.Size = new Size(679, 355);
            this.apiBox.TabIndex = 0;
            this.apiBox.Text = "\n调用方式\n\n    Socket\n\n端口号\n\n    7777\n\n参数格式\n\n    打印机,模板路径,模板绑定的文本数据库路径,数据库名称（或者别名，推荐使用别名）\n    如：test,D:\\\\xx.btw,D:\\\\xx.txt,xx\n\n返回值\n\n    数字:说明\n        1\t打印成功\n        20\t找不到打印机(某某打印机)\n        21\t打印失败的原因";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(692, 379);
            base.Controls.Add(this.apiBox);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "API";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "API说明";
            base.ResumeLayout(false);
        }

        #endregion
    }
}