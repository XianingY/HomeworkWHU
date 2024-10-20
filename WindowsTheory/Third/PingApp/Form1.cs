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
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    process.WaitForExit(); // 等待进程结束
                    textBox2.Text = result;
                }
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
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = startInfo };
            process.Start();

            process.BeginOutputReadLine(); // 开始异步读取输出
            process.OutputDataReceived += (sender1, e1) =>
            {
                
                textBox2.Invoke((MethodInvoker)delegate
                {
                    textBox2.AppendText(e1.Data + Environment.NewLine);
                });
            };
            process.EnableRaisingEvents = true; // 启用事件
        }
    }
}
