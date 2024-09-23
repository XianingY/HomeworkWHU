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
using CopyDataStruct;

namespace ReceiverB
{
    public partial class FormReceiverB : Form
    {
        public FormReceiverB()
        {
            InitializeComponent();
            this.Text = "ReceiverB";
        }


        private const int WM_COPYDATA = 0x004A;



        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_COPYDATA)
            {
                COPYDATASTRUCT cds = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                string message = Marshal.PtrToStringAnsi(cds.lpData);
                lstMessages.Items.Add($"ReceiverB接收到消息: {message} ，时间为 {DateTime.Now}");
            }
            else
            {
                base.DefWndProc(ref m);
            }
        }
    }
}
