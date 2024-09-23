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

namespace ReceiverB
{
    public partial class Form1 : Form
    {
        private const int WM_COPYDATA = 0x004A;
        private ListBox listBoxMessages;

        public Form1()
        {
            InitializeComponent();
            this.Text = "ReceiverB";
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // 初始化列表框
            listBoxMessages = new ListBox();
            listBoxMessages.Location = new Point(10, 10);
            listBoxMessages.Size = new Size(260, 200);
            this.Controls.Add(listBoxMessages);
        }

        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_COPYDATA)
            {
                COPYDATASTRUCT cds = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));
                byte[] messageBytes = new byte[cds.cbData];
                Marshal.Copy(cds.lpData, messageBytes, 0, cds.cbData);

                MessageStruct message = DeserializeMessage(messageBytes);
                listBoxMessages.Items.Add($"{message.Timestamp}: {message.Content}");

                // 调试输出
                MessageBox.Show($"接收到消息: {message.Content}\n时间: {message.Timestamp}");
                Console.WriteLine($"接收到消息: {message.Content}\n时间: {message.Timestamp}");
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }




        private MessageStruct DeserializeMessage(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (MessageStruct)formatter.Deserialize(memoryStream);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        public IntPtr lpData;
    }
}
