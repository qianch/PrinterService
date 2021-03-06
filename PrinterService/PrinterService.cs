﻿using Seagull.BarTender.Print;
using Seagull.BarTender.Print.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace PrinterService
{
    public partial class PrinterService : Form
    {
        private delegate void ConsoleCallback(string text);

        private delegate void ConsoleCallback2(Exception e);

        private delegate void StatisticCallback();
        
        private Dictionary<string, Printer>listPrinter;

        private int bufferSize = 5096;

        private long countAll = 0L;

        private long countSuc = 0L;

        private long countErr = 0L;

        private long countErrNotFound = 0L;

        private readonly string logDir = Path.Combine(Environment.CurrentDirectory, "logs");
        private readonly string dataDir = Path.Combine(Environment.CurrentDirectory, "data");

        public PrinterService()
        {
            this.InitializeComponent();

            try
            {
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }

                this.init();
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }
            catch (Exception ex)
            {
                error(ex);
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show("请确认本机已安装Bartender 10.1 以上的版本。");
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void init()
        {
            this.start_Click(null, null);
            this.reload_Click(null, null);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            error(ex);
        }

        private void start_Click(object sender, EventArgs e)
        {
            this.start.Enabled = false;
            this.stop.Enabled = false;
            this.restart.Enabled = false;
            this.initSocket();
            this.initEngine();
            this.stop.Enabled = true;
            this.restart.Enabled = true;
        }

        private void initEngine()
        {
            this.info("正在启动打印服务...");
            foreach (var p in listPrinter)
            {
                p.Value.engine = new Engine(true);
            }
            this.info("打印服务已启动");
        }

        private void initSocket()
        {
            listPrinter = new Dictionary<string, Printer>();
            Printer printer = new Printer();
            printer.name = "默认原始打印机";
            printer.port = 7777;
            listPrinter.Add(printer.name, printer);

            string constructorString = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            string printerTableSql = ConfigurationSettings.AppSettings["GetPrinterTable"].ToString();
            MySqlConnection myConnnect = new MySqlConnection(constructorString);
            myConnnect.Open();
            MySqlCommand comm = new MySqlCommand(printerTableSql, myConnnect);
            MySqlDataReader mySqlReader = comm.ExecuteReader();
            while (mySqlReader.Read())
            {
                string name = mySqlReader["printername"].ToString();
                string port =Convert.ToString ( mySqlReader["port"]);
                if(string.IsNullOrEmpty(port))
                {
                    continue;
                }    
                printer = new Printer();
                printer.name = name;
                printer.port = Convert.ToInt32(port);
                listPrinter.Add(printer.name, printer);

            }
            myConnnect.Close();

            foreach (var p in listPrinter)
            {
                p.Value.sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, p.Value.port);
                p.Value.sk.Bind(localEP);
                p.Value.sk.Listen(100);
                p.Value.skThread = new Thread(new ParameterizedThreadStart(this.listen));
                p.Value.skThread.IsBackground = true;
                p.Value.skThread.Start(p.Key);
                this.info("打印机"+p.Key+"的端口线程Port:" + p.Value.port+"已经启动!");
            }
            
            this.info("打印通信已启动,等待连接...");
        }

        private void stop_Click(object sender, EventArgs e)
        {
            this.start.Enabled = false;
            this.stop.Enabled = false;
            this.restart.Enabled = false;
            this.shutdownEngine();
            this.shutdownSocket();
            this.start.Enabled = true;
        }

        private void shutdownEngine()
        {
            foreach (var p in listPrinter)
            {
                p.Value.engine.Stop();
                p.Value.engine.Dispose();
            }

            this.info("打印服务已关闭");
        }

        private void shutdownSocket()
        {
            try
            {
                foreach (var p in listPrinter)
                {
                    p.Value.sk.Close();
                    p.Value.skThread.Abort();
                }
                this.info("打印通信已关闭");
            }
            catch (Exception ex)
            {
                this.error(ex);
            }
        }

        private void restart_Click(object sender, EventArgs e)
        {
            this.stop_Click(null, null);
            this.start_Click(null, null);
        }

        private void listen(object objPrintername)
        {
            while (true)
            {
                try
                {
                    Socket socket = listPrinter[objPrintername.ToString()].sk.Accept();
                    this.countAll += 1L;
                    if (this.countAll == 2147483647L)
                    {
                        this.countAll = 1L;
                        this.countSuc = 0L;
                        this.countErr = 0L;
                        this.countErrNotFound = 0L;
                    }
                    ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(this.recv);
                    new Thread(parameterizedThreadStart)
                    {
                        IsBackground = true
                    }.Start(socket);
                }
                catch (Exception ex)
                {
                    error(ex);
                }
            }
        }

        private void recv(object objSocket)
        {
            Socket socket = objSocket as Socket;
            if (socket.Poll(-1, SelectMode.SelectRead))
            {
                byte[] array = new byte[this.bufferSize];
                int num = 0;
                try
                {
                    num = socket.Receive(array);
                }
                catch
                {
                    num = 0;
                }
                if (num == 0)
                {
                    try
                    {
                        socket.Close();
                    }
                    catch (Exception ex)
                    {
                        this.error(ex);
                    }
                    return;
                }

                string printerContent = Encoding.UTF8.GetString(array, 0, num);

                int length = printerContent.Length - printerContent.Replace(",", "").Length;//判断有多少个,

                string textDBName = printerContent.Length > 8 ? printerContent.Substring(0, 2) : "";

                bool flag = textDBName.Equals("db") || textDBName.Equals("yu");

                if (!flag | length < 4)//三个,和db 如果,数量小于3，或者起始字符不等于db或者yu就报错
                {
                    socket.Send(Encoding.UTF8.GetBytes("ERROR"));
                    this.countErr += 1L;
                    this.error(string.Concat(new object[]
                    {
                        "来源IP:",
                        socket.RemoteEndPoint,
                        ",错误的参数:",
                        printerContent
                    }));
                    socket.Close();
                    return;
                }


                string[] array2 = printerContent.Split(new char[] { ',' });

                string printerName = array2[1];
                string btwFilePathName = array2[2];
                int count = int.Parse(array2[3]);

                int startindex = textDBName.Length + printerName.Length + btwFilePathName.Length + array2[3].Length + 4;
                string textContent = printerContent.Substring(startindex, printerContent.Length - startindex);
                try
                {

                    if (!this.exist(printerName))
                    {
                        this.countErrNotFound += 1L;
                        socket.Send(Encoding.UTF8.GetBytes("20:找不到打印机(" + printerName + ")"));
                        this.error(string.Concat(new object[]
                        {
                                "来源IP:",
                                socket.RemoteEndPoint,
                                ",打印机:",
                                printerName,
                                ",打印模板:",
                                btwFilePathName,
                                ",数据文件:",
                                textContent,
                                ",文本数据库名称:",
                                textDBName,
                                ",打印失败, 错误:找不到打印机[",
                                printerName,
                                "]"
                        }));
                        socket.Close();
                        return;
                    }
                    Result result = Result.Failure;
                    switch (textDBName)
                    {
                        case "db":
                            result = this.print(printerName, btwFilePathName, textContent, textDBName, count);
                            break;
                        case "yu":

                            List<BarCodePrintRecord> listBarCodePrintRecord = JsonHelper.JsonDeserialize<List<BarCodePrintRecord>>(textContent);
                            result = this.print(printerName, btwFilePathName, listBarCodePrintRecord, textDBName, count);
                            break;
                    }

                    string message = string.Concat(new object[]
                    {
                                "来源IP:",
                                socket.RemoteEndPoint,
                                ",打印机:",
                                printerName,
                                ",打印模板:",
                                btwFilePathName,
                                ",数据文件:",
                                textContent,
                                ",文本数据库名称:",
                                textDBName
                    });

                    switch (result)
                    {
                        case Result.Success:
                            socket.Send(Encoding.UTF8.GetBytes("10:SUCCESS"));
                            this.countSuc += 1L;
                            this.info(message + ",打印成功");
                            break;
                        case Result.Failure:
                            socket.Send(Encoding.UTF8.GetBytes("21:Failure"));
                            this.countErr += 1L;
                            this.error(message + ",打印失败(Failure)");
                            break;
                        case Result.Timeout:
                            socket.Send(Encoding.UTF8.GetBytes("21:Timeout"));
                            this.countErr += 1L;
                            this.error(message + ",打印超时(Timeout)");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.countErr += 1L;
                    try
                    {
                        socket.Send(Encoding.UTF8.GetBytes("21:" + ex.Message));
                    }
                    catch (Exception socketEx)
                    {
                        this.error(socketEx);
                    }

                    this.error(ex);
                    this.error(string.Concat(new object[]
                    {
                        "来源IP:",
                        socket.RemoteEndPoint,
                        ",打印机:",
                        printerName,
                        ",打印文件:",
                        btwFilePathName,
                        ",打印失败,错误: [",
                        ex.Message,
                        "]"
                    }));
                }
                finally
                {
                    this.count();
                    socket.Close();
                }
            }

        }

        private Result print(string printerName, string btwFilePathName, List<BarCodePrintRecord> listBarCodePrintRecord, string textDBName, int count)
        {
            Printer printer = listPrinter[printerName];

            printer.format = printer.engine.Documents.Open(btwFilePathName);
            printer.format.PrintSetup.IdenticalCopiesOfLabel = count;
            printer.format.PrintSetup.NumberOfSerializedLabels = 1;

            foreach (BarCodePrintRecord barCodePrintRecord in listBarCodePrintRecord)
            {
                string key = barCodePrintRecord.KEY.Replace("\r", "").Replace("\n", "") + "\r\n";

                if (printer.format.SubStrings.Any(p => p.Name == key))
                {
                    printer.format.SubStrings[key].Value = barCodePrintRecord.VALUE;
                }
                else if (printer.format.SubStrings.Any(p => p.Name == barCodePrintRecord.KEY))
                {
                    printer.format.SubStrings[barCodePrintRecord.KEY].Value = barCodePrintRecord.VALUE;
                }
            }

            printer.format.PrintSetup.PrinterName = printerName;
            return printer.format.Print();
        }

        private Result print(string printerName, string btwFilePathName, string textFilePathName, string textDBName, int count)
        {
            Printer printer = listPrinter[printerName];

            printer.format = printer.engine.Documents.Open(btwFilePathName);
            printer.format.PrintSetup.IdenticalCopiesOfLabel = count;
            printer.format.PrintSetup.NumberOfSerializedLabels = 1;

            string[] textFileArray = textFilePathName.Split('@');
            var fileUrl = textFileArray[0];
            if (textFileArray.Length == 2 && !File.Exists(fileUrl))
            {
                fileUrl = Path.Combine(dataDir, Guid.NewGuid().ToString() + ".txt");
                File.WriteAllText(fileUrl, textFileArray[1]);
            }

            ((TextFile)printer.format.DatabaseConnections[textDBName]).FileName = fileUrl;
            printer.format.PrintSetup.PrinterName = printerName;
            return printer.format.Print();
        }

       

        private bool exist(string printer)
        {
            PrinterSettings.StringCollection installedPrinters = PrinterSettings.InstalledPrinters;
            bool result;
            foreach (string text in installedPrinters)
            {
                if (text.ToLower().Trim() == printer.ToLower().Trim())
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        private void count()
        {
            if (this.console.InvokeRequired)
            {
                PrinterService.StatisticCallback method = new PrinterService.StatisticCallback(this.count);
                base.Invoke(method, new object[0]);
            }
            else
            {
                this.statistic_all.Text = "打印请求 " + this.countAll + " 次";
                this.statistic_suc.Text = "打印成功 " + this.countSuc + " 次";
                this.statistic_err.Text = "打印失败 " + this.countErr + " 次";
                this.statistic_err_not_found.Text = "找不到打印机 " + this.countErrNotFound + " 次";
            }
        }

        private void info(string msg)
        {
            if (this.console.InvokeRequired)
            {
                PrinterService.ConsoleCallback method = new PrinterService.ConsoleCallback(this.info);
                base.Invoke(method, new object[]
                {
                    msg
                });
            }
            else
            {
                int upperBound = this.console.Lines.GetUpperBound(0);
                string text = this.console.Text;
                if (upperBound > 200)
                {
                    this.console.Text = "";
                }
                this.console.AppendText(string.Concat(new string[]
                {
                    Environment.NewLine,
                    this.now(),
                    " [INFO] ",
                    msg,
                    Environment.NewLine
                }));
                this.console.ScrollToCaret();
                this.console.Focus();
                this.console.Select(this.console.TextLength, 0);
                this.console.ScrollToCaret();
                this.infoToFile(this.now() + " [INFO] " + msg);
            }
        }

        private void error(Exception e)
        {
            if (this.console.InvokeRequired)
            {
                PrinterService.ConsoleCallback2 method = new PrinterService.ConsoleCallback2(this.error);
                base.Invoke(method, new object[]
                {
                    e
                });
            }
            else
            {
                int upperBound = this.console.Lines.GetUpperBound(0);
                string text = this.console.Text;
                string text2 = e.Message + "\n" + e.StackTrace;
                if (upperBound > 500)
                {
                    this.console.Text = "";
                }
                this.console.SelectionColor = Color.FromArgb(192, 0, 0);
                this.console.AppendText(string.Concat(new string[]
                {
                    Environment.NewLine,
                    this.now(),
                    " [ERROR] ",
                    text2,
                    Environment.NewLine
                }));
                this.console.ScrollToCaret();
                this.console.Focus();
                this.console.Select(this.console.TextLength, 0);
                this.console.ScrollToCaret();
                this.errorToFile(this.now() + " [ERROR] " + text2);
            }
        }

        private void error(string msg)
        {
            if (this.console.InvokeRequired)
            {
                PrinterService.ConsoleCallback method = new PrinterService.ConsoleCallback(this.error);
                base.Invoke(method, new object[]
                {
                    msg
                });
            }
            else
            {
                int upperBound = this.console.Lines.GetUpperBound(0);
                string text = this.console.Text;
                if (upperBound > 500)
                {
                    this.console.Text = "";
                }
                this.console.SelectionColor = Color.FromArgb(192, 0, 0);
                this.console.AppendText(string.Concat(new string[]
                {
                    Environment.NewLine,
                    this.now(),
                    " [ERROR] ",
                    msg,
                    Environment.NewLine
                }));
                this.console.ScrollToCaret();
                this.console.Focus();
                this.console.Select(this.console.TextLength, 0);
                this.console.ScrollToCaret();
                this.errorToFile(this.now() + " [ERROR] " + msg);
            }
        }

        private void errorToFile(string msg)
        {
            try
            {
                string path = Path.Combine(logDir, DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd") + "_error.log");
                FileStream fileStream;
                if (File.Exists(path))
                {
                    fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                }
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(msg);
                streamWriter.Close();
                streamWriter.Dispose();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                this.error(ex);
            }
        }

        private void infoToFile(string msg)
        {
            try
            {
                string path = Path.Combine(logDir, DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd") + "_info.log");
                FileStream fileStream;
                if (File.Exists(path))
                {
                    fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                }
                else
                {
                    fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                }
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(msg);
                streamWriter.Close();
                streamWriter.Dispose();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                this.error(ex);
            }
        }

        private void clear()
        {
            this.console.Text = "";
        }

        private string now()
        {
            return "[" + DateTime.Now.ToLocalTime().ToString("MM-dd HH:mm:ss") + "]";
        }

        private void reload_Click(object sender, EventArgs e)
        {
            this.printerList.Items.Clear();
            PrinterSettings.StringCollection installedPrinters = PrinterSettings.InstalledPrinters;
            foreach (string item in installedPrinters)
            {
                this.printerList.Items.Add(item);
            }
        }

        private void mini_DoubleClicked(object sender, MouseEventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.WindowState = FormWindowState.Normal;
                base.Activate();
                base.ShowInTaskbar = true;
                this.mini.Visible = false;
            }
        }

        private void PrinterService_SizeChanged(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.ShowInTaskbar = false;
                this.mini.Visible = true;
            }
        }

        private void PrinterService_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？打印服务将会停止！", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.shutdownEngine();
                this.shutdownSocket();
                base.Dispose();
                base.Close();
            }
            else
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }            
        }

        private void miniMenu_Display_Click(object sender, EventArgs e)
        {
            base.WindowState = FormWindowState.Normal;
        }

        private void miniMenu_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序?打印服务将会停止!", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                base.Dispose();
                base.Close();
            }
        }

        private void menu_log_folder_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "/logs/");
        }

        private void menu_log_error_Click(object sender, EventArgs e)
        {
            string arguments = Path.Combine(logDir, DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd") + "_error.log").ToString();
            Process.Start("notepad.exe", arguments);
        }

        private void menu_log_info_Click(object sender, EventArgs e)
        {
            string arguments = Path.Combine(logDir, DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd") + "_info.log").ToString();
            Process.Start("notepad.exe", arguments);
        }

        private void menu_api_Click(object sender, EventArgs e)
        {
            new API().ShowDialog();
        }

        private void menu_dev_Click(object sender, EventArgs e)
        {
            new Dev().ShowDialog();
        }

        private void menu_template_Click(object sender, EventArgs e)
        {
            new Template().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？打印服务将会停止！", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                base.Dispose();
                base.Close();
            }
        }

        private void copyPrinterName_Click(object sender, EventArgs e)
        {
            string text = "";
            int num = 0;
            if (this.printerList.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何打印机");
            }
            foreach (string str in this.printerList.SelectedItems)
            {
                text = text + ((++num == 1) ? "" : ",") + str;
            }
            Clipboard.SetDataObject(text);
        }

        private void clearLogItem_Click(object sender, EventArgs e)
        {
            this.console.Clear();
        }
    }
}
