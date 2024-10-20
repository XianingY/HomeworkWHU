using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSyncPing_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                ip = "www.sohu.com"; 
            }

            string command = $"ping {ip} -n 10";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true, 
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                using (StreamWriter writer = process.StandardInput)
                {
                    // 向进程的输入流中写入数据
                    writer.WriteLine("额外的数据");
                }

                StringBuilder output = new StringBuilder();
                using (StreamReader reader = process.StandardOutput)
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        output.AppendLine(line);
                    }
                }

                process.WaitForExit(); // 等待进程结束

                // 将输出保存到文件
                string outputFilePath = "PingOutputSync.txt";
                File.WriteAllText(outputFilePath, output.ToString());

                // 读取文件内容并显示在textBox2中
                textBox2.Text = File.ReadAllText(outputFilePath);

                // 删除临时创建的文件
                //File.Delete(outputFilePath);
            }
        }

        private void btnAsyncPing_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                ip = "www.sohu.com";
            }

            string command = $"ping {ip} -n 10";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = startInfo };
            process.Start();

            using (StreamWriter writer = process.StandardInput)
            {
                // 向进程的输入流中写入数据
                writer.WriteLine("额外的数据");
            }

            StringBuilder output = new StringBuilder();
            process.BeginOutputReadLine(); // 开始异步读取输出

            process.OutputDataReceived += (sender1, e1) =>
            {
                if (!string.IsNullOrEmpty(e1.Data))
                {
                    output.AppendLine(e1.Data);
                    textBox2.Invoke((MethodInvoker)delegate
                    {
                        textBox2.AppendText(e1.Data + Environment.NewLine);
                    });
                }
            };

            process.ErrorDataReceived += (sender1, e1) =>
            {
                if (!string.IsNullOrEmpty(e1.Data))
                {
                    output.AppendLine(e1.Data);
                }
            };

            process.EnableRaisingEvents = true; // 启用事件

            
            // 将输出保存到文件
            string outputFilePath = "PingOutputAsync.txt";
            File.WriteAllText(outputFilePath, output.ToString());

            // 删除临时创建的文件
            //File.Delete(outputFilePath);
        }
    }
}
