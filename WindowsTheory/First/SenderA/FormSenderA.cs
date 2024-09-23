using CopyDataStruct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SenderA   
{
    public partial class FormSenderA : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        private const int WM_COPYDATA = 0x004A;

        public FormSenderA()
        {
            InitializeComponent();
            this.Text = "SenderA";
        }

        

        private void btnSend1_Click(object sender, EventArgs e)
        {
            string targetWindow = txtTargetWindow.Text;
            string message = txtMessage.Text;

            if (targetWindow != "ReceiverB")
            {
                MessageBox.Show("未找到ReceiverB.");
                return;
            }

            IntPtr hWnd = FindWindow(null, targetWindow);
            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("未找到ReceiverB.");
                return;
            }

            byte[] sarr = Encoding.Default.GetBytes(message);
            int len = sarr.Length;
            COPYDATASTRUCT cds;
            cds.dwData = IntPtr.Zero;
            cds.cbData = len + 1;
            cds.lpData = Marshal.AllocHGlobal(len + 1);
            Marshal.Copy(sarr, 0, cds.lpData, len);
            Marshal.WriteByte(cds.lpData, len, 0);
            SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref cds);

            Marshal.FreeHGlobal(cds.lpData);

        }

        private void btnSend2_Click(object sender, EventArgs e)
        {
            string targetWindow = txtTargetWindow.Text;
            string message = txtMessage.Text;

            if (targetWindow != "ReceiverC")
            {
                MessageBox.Show("未找到ReceiverC.");
                return;
            }

            IntPtr hWnd = FindWindow(null, targetWindow);
            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("未找到ReceiverC.");
                return;
            }

            byte[] sarr = Encoding.Default.GetBytes(message);
            int len = sarr.Length;
            COPYDATASTRUCT cds;
            cds.dwData = IntPtr.Zero;
            cds.cbData = len + 1;
            cds.lpData = Marshal.AllocHGlobal(len + 1);
            Marshal.Copy(sarr, 0, cds.lpData, len);
            Marshal.WriteByte(cds.lpData, len, 0);

            SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref cds);

            Marshal.FreeHGlobal(cds.lpData);
        }
    }
}
