using CopyDataStruct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SenderA
{
    public partial class Form1 : Form
    {
        private const int WM_COPYDATA = 0x004A;

        private TextBox textBoxTarget;
        private TextBox textBoxMessage;
        private Button buttonSend;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        

        private void InitializeCustomComponents()
        {
            // 初始化目标文本框
            textBoxTarget = new TextBox();
            textBoxTarget.Location = new Point(10, 10);
            textBoxTarget.Size = new Size(200, 20);
            textBoxTarget.Text = "*希望发送的目标*";
            this.Controls.Add(textBoxTarget);

            // 初始化消息文本框
            textBoxMessage = new TextBox();
            textBoxMessage.Location = new Point(10, 40);
            textBoxMessage.Size = new Size(200, 20);
            textBoxMessage.Text = "*希望发送的内容*";
            this.Controls.Add(textBoxMessage);

            // 初始化发送按钮
            buttonSend = new Button();
            buttonSend.Location = new Point(10, 70);
            buttonSend.Size = new Size(75, 23);
            buttonSend.Text = "发送";
            buttonSend.Click += new EventHandler(buttonSend_Click);
            this.Controls.Add(buttonSend);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string targetWindowName = textBoxTarget.Text;
            MessageStruct message = new MessageStruct
            {
                Type = MessageType.Message1,
                Content = textBoxMessage.Text,
                Timestamp = DateTime.Now
            };
            SendMessageToTarget(targetWindowName, message);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out IntPtr lpdwResult);

        private void SendMessageToTarget(string targetWindowName, MessageStruct message)
        {
            IntPtr hWnd = FindWindow(null, targetWindowName);
            if (hWnd != IntPtr.Zero)
            {
                // 序列化消息
                byte[] messageBytes = SerializeMessage(message);
                IntPtr messagePtr = Marshal.AllocHGlobal(messageBytes.Length);
                Marshal.Copy(messageBytes, 0, messagePtr, messageBytes.Length);

                // 发送消息
                IntPtr result;
                IntPtr sendResult = SendMessageTimeout(hWnd, WM_COPYDATA, IntPtr.Zero, messagePtr, 0x0002, 5000, out result);
                uint error = GetLastError();
                Marshal.FreeHGlobal(messagePtr);

                // 调试输出
                MessageBox.Show($"消息已发送到窗口: {targetWindowName}\n句柄: {hWnd}\n结果: {sendResult}\n错误代码: {error}");
                Console.WriteLine($"消息已发送到窗口: {targetWindowName}\n句柄: {hWnd}\n结果: {sendResult}\n错误代码: {error}");
            }
            else
            {
                // 调试输出
                MessageBox.Show($"未找到窗口: {targetWindowName}");
                Console.WriteLine($"未找到窗口: {targetWindowName}");
            }
        }



        private byte[] SerializeMessage(MessageStruct message)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, message);
                return memoryStream.ToArray();
            }
        }



        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }


    }
}
