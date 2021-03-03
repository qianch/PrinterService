using PrinterService.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PrinterService
{
    partial class PrinterService
    {
		private Button restart;

		private Button stop;

		private RichTextBox console;

		private Button start;

		private Label statistic_err;

		private Label statistic_suc;

		private Label statistic_all;

		private ListBox printerList;

		private Button reload;

		private Label statistic_err_not_found;

		private NotifyIcon mini;

		private ContextMenuStrip miniMenu;

		private ToolStripMenuItem miniMenu_Display;

		private ToolStripMenuItem miniMenu_Exit;

		private MenuStrip menu;

		private ToolStripMenuItem 使用方法ToolStripMenuItem;

		private ToolStripMenuItem menu_template;

		private ToolStripMenuItem menu_dev;

		private ToolStripMenuItem 日志ToolStripMenuItem;

		private ToolStripMenuItem menu_log_info;

		private ToolStripMenuItem menu_log_error;

		private ToolStripMenuItem menu_log_folder;

		private ToolStripMenuItem menu_api;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private Button button1;

		private GroupBox groupBox3;

		private ContextMenuStrip copyPrinterNameMenu;

		private ToolStripMenuItem copyPrinterName;

		private ContextMenuStrip clearLogMenu;

		private ToolStripMenuItem clearLogItem;
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrinterService));
            this.restart = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.console = new System.Windows.Forms.RichTextBox();
            this.clearLogMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearLogItem = new System.Windows.Forms.ToolStripMenuItem();
            this.start = new System.Windows.Forms.Button();
            this.statistic_err_not_found = new System.Windows.Forms.Label();
            this.statistic_err = new System.Windows.Forms.Label();
            this.statistic_suc = new System.Windows.Forms.Label();
            this.statistic_all = new System.Windows.Forms.Label();
            this.reload = new System.Windows.Forms.Button();
            this.printerList = new System.Windows.Forms.ListBox();
            this.copyPrinterNameMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPrinterName = new System.Windows.Forms.ToolStripMenuItem();
            this.mini = new System.Windows.Forms.NotifyIcon(this.components);
            this.miniMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miniMenu_Display = new System.Windows.Forms.ToolStripMenuItem();
            this.miniMenu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.使用方法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_api = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_dev = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_template = new System.Windows.Forms.ToolStripMenuItem();
            this.日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log_info = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log_error = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log_folder = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clearLogMenu.SuspendLayout();
            this.copyPrinterNameMenu.SuspendLayout();
            this.miniMenu.SuspendLayout();
            this.menu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // restart
            // 
            this.restart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.restart.BackColor = System.Drawing.Color.LightGray;
            this.restart.Enabled = false;
            this.restart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.restart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.restart.Location = new System.Drawing.Point(852, 238);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(176, 24);
            this.restart.TabIndex = 10;
            this.restart.Text = "重启服务";
            this.restart.UseVisualStyleBackColor = false;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stop.BackColor = System.Drawing.Color.LightGray;
            this.stop.Enabled = false;
            this.stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.stop.Location = new System.Drawing.Point(852, 208);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(176, 24);
            this.stop.TabIndex = 2;
            this.stop.Text = "关闭服务";
            this.stop.UseMnemonic = false;
            this.stop.UseVisualStyleBackColor = false;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // console
            // 
            this.console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.console.ContextMenuStrip = this.clearLogMenu;
            this.console.ForeColor = System.Drawing.Color.Navy;
            this.console.Location = new System.Drawing.Point(13, 15);
            this.console.Margin = new System.Windows.Forms.Padding(10);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.console.Size = new System.Drawing.Size(570, 387);
            this.console.TabIndex = 3;
            this.console.Text = "";
            // 
            // clearLogMenu
            // 
            this.clearLogMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearLogItem});
            this.clearLogMenu.Name = "contextMenuStrip2";
            this.clearLogMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // clearLogItem
            // 
            this.clearLogItem.Name = "clearLogItem";
            this.clearLogItem.Size = new System.Drawing.Size(100, 22);
            this.clearLogItem.Text = "清空";
            this.clearLogItem.Click += new System.EventHandler(this.clearLogItem_Click);
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.start.BackColor = System.Drawing.Color.LightGray;
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.start.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.start.ForeColor = System.Drawing.Color.Red;
            this.start.Location = new System.Drawing.Point(852, 178);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(176, 24);
            this.start.TabIndex = 4;
            this.start.Text = "启动服务";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // statistic_err_not_found
            // 
            this.statistic_err_not_found.AutoSize = true;
            this.statistic_err_not_found.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statistic_err_not_found.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.statistic_err_not_found.Location = new System.Drawing.Point(6, 100);
            this.statistic_err_not_found.Name = "statistic_err_not_found";
            this.statistic_err_not_found.Size = new System.Drawing.Size(117, 12);
            this.statistic_err_not_found.TabIndex = 3;
            this.statistic_err_not_found.Text = "找不到打印机 0 次";
            // 
            // statistic_err
            // 
            this.statistic_err.AutoSize = true;
            this.statistic_err.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statistic_err.ForeColor = System.Drawing.Color.Maroon;
            this.statistic_err.Location = new System.Drawing.Point(6, 76);
            this.statistic_err.Name = "statistic_err";
            this.statistic_err.Size = new System.Drawing.Size(91, 12);
            this.statistic_err.TabIndex = 2;
            this.statistic_err.Text = "打印失败 0 次";
            // 
            // statistic_suc
            // 
            this.statistic_suc.AutoSize = true;
            this.statistic_suc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statistic_suc.ForeColor = System.Drawing.Color.Green;
            this.statistic_suc.Location = new System.Drawing.Point(6, 52);
            this.statistic_suc.Name = "statistic_suc";
            this.statistic_suc.Size = new System.Drawing.Size(91, 12);
            this.statistic_suc.TabIndex = 1;
            this.statistic_suc.Text = "打印成功 0 次";
            // 
            // statistic_all
            // 
            this.statistic_all.AutoSize = true;
            this.statistic_all.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statistic_all.Location = new System.Drawing.Point(6, 28);
            this.statistic_all.Name = "statistic_all";
            this.statistic_all.Size = new System.Drawing.Size(91, 12);
            this.statistic_all.TabIndex = 0;
            this.statistic_all.Text = "打印请求 0 次";
            // 
            // reload
            // 
            this.reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.reload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reload.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.reload.Location = new System.Drawing.Point(6, 20);
            this.reload.Name = "reload";
            this.reload.Size = new System.Drawing.Size(199, 24);
            this.reload.TabIndex = 5;
            this.reload.Text = "重新加载打印机";
            this.reload.UseVisualStyleBackColor = true;
            this.reload.Click += new System.EventHandler(this.reload_Click);
            // 
            // printerList
            // 
            this.printerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.printerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.printerList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.printerList.ContextMenuStrip = this.copyPrinterNameMenu;
            this.printerList.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printerList.FormattingEnabled = true;
            this.printerList.HorizontalScrollbar = true;
            this.printerList.ItemHeight = 21;
            this.printerList.Location = new System.Drawing.Point(6, 60);
            this.printerList.Name = "printerList";
            this.printerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.printerList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.printerList.Size = new System.Drawing.Size(199, 336);
            this.printerList.TabIndex = 0;
            // 
            // copyPrinterNameMenu
            // 
            this.copyPrinterNameMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPrinterName});
            this.copyPrinterNameMenu.Name = "contextMenuStrip1";
            this.copyPrinterNameMenu.ShowCheckMargin = true;
            this.copyPrinterNameMenu.Size = new System.Drawing.Size(123, 26);
            // 
            // copyPrinterName
            // 
            this.copyPrinterName.Name = "copyPrinterName";
            this.copyPrinterName.Size = new System.Drawing.Size(122, 22);
            this.copyPrinterName.Text = "复制";
            this.copyPrinterName.Click += new System.EventHandler(this.copyPrinterName_Click);
            // 
            // mini
            // 
            this.mini.ContextMenuStrip = this.miniMenu;
            this.mini.Icon = ((System.Drawing.Icon)(resources.GetObject("mini.Icon")));
            this.mini.Text = "MES系统打印服务";
            this.mini.Visible = true;
            this.mini.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mini_DoubleClicked);
            // 
            // miniMenu
            // 
            this.miniMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miniMenu_Display,
            this.miniMenu_Exit});
            this.miniMenu.Name = "miniMenu";
            this.miniMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // miniMenu_Display
            // 
            this.miniMenu_Display.Name = "miniMenu_Display";
            this.miniMenu_Display.Size = new System.Drawing.Size(100, 22);
            this.miniMenu_Display.Text = "显示";
            this.miniMenu_Display.Click += new System.EventHandler(this.miniMenu_Display_Click);
            // 
            // miniMenu_Exit
            // 
            this.miniMenu_Exit.Name = "miniMenu_Exit";
            this.miniMenu_Exit.Size = new System.Drawing.Size(100, 22);
            this.miniMenu_Exit.Text = "退出";
            this.miniMenu_Exit.Click += new System.EventHandler(this.miniMenu_Exit_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.LightGray;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.使用方法ToolStripMenuItem,
            this.日志ToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menu.Size = new System.Drawing.Size(1040, 28);
            this.menu.TabIndex = 7;
            this.menu.Text = "menuStrip1";
            // 
            // 使用方法ToolStripMenuItem
            // 
            this.使用方法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_api,
            this.menu_dev,
            this.menu_template});
            this.使用方法ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.使用方法ToolStripMenuItem.Name = "使用方法ToolStripMenuItem";
            this.使用方法ToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
            this.使用方法ToolStripMenuItem.Text = "集成开发";
            // 
            // menu_api
            // 
            this.menu_api.Name = "menu_api";
            this.menu_api.Size = new System.Drawing.Size(218, 24);
            this.menu_api.Text = "API";
            this.menu_api.Click += new System.EventHandler(this.menu_api_Click);
            // 
            // menu_dev
            // 
            this.menu_dev.Name = "menu_dev";
            this.menu_dev.Size = new System.Drawing.Size(218, 24);
            this.menu_dev.Text = "各种开发语言调用示例";
            this.menu_dev.Click += new System.EventHandler(this.menu_dev_Click);
            // 
            // menu_template
            // 
            this.menu_template.Name = "menu_template";
            this.menu_template.Size = new System.Drawing.Size(218, 24);
            this.menu_template.Text = "模板制作";
            this.menu_template.Click += new System.EventHandler(this.menu_template_Click);
            // 
            // 日志ToolStripMenuItem
            // 
            this.日志ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_log_info,
            this.menu_log_error,
            this.menu_log_folder});
            this.日志ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.日志ToolStripMenuItem.Name = "日志ToolStripMenuItem";
            this.日志ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.日志ToolStripMenuItem.Text = "日志";
            // 
            // menu_log_info
            // 
            this.menu_log_info.Name = "menu_log_info";
            this.menu_log_info.Size = new System.Drawing.Size(148, 24);
            this.menu_log_info.Text = "常规日志";
            this.menu_log_info.Click += new System.EventHandler(this.menu_log_info_Click);
            // 
            // menu_log_error
            // 
            this.menu_log_error.Name = "menu_log_error";
            this.menu_log_error.Size = new System.Drawing.Size(148, 24);
            this.menu_log_error.Text = "异常日志";
            this.menu_log_error.Click += new System.EventHandler(this.menu_log_error_Click);
            // 
            // menu_log_folder
            // 
            this.menu_log_folder.Name = "menu_log_folder";
            this.menu_log_folder.Size = new System.Drawing.Size(148, 24);
            this.menu_log_folder.Text = "日志文件夹";
            this.menu_log_folder.Click += new System.EventHandler(this.menu_log_folder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.statistic_err_not_found);
            this.groupBox1.Controls.Add(this.statistic_all);
            this.groupBox1.Controls.Add(this.statistic_err);
            this.groupBox1.Controls.Add(this.statistic_suc);
            this.groupBox1.Location = new System.Drawing.Point(832, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 134);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印统计";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.printerList);
            this.groupBox2.Controls.Add(this.reload);
            this.groupBox2.Location = new System.Drawing.Point(12, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(211, 415);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "打印机列表";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(852, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.console);
            this.groupBox3.Location = new System.Drawing.Point(230, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(596, 415);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "即时日志";
            // 
            // PrinterService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1040, 465);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.start);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.restart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "PrinterService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条码打印服务(Bartender文本文件数据库版本)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrinterService_FormClosing);
            this.SizeChanged += new System.EventHandler(this.PrinterService_SizeChanged);
            this.clearLogMenu.ResumeLayout(false);
            this.copyPrinterNameMenu.ResumeLayout(false);
            this.miniMenu.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion
    }
}

