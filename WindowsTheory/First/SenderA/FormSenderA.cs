﻿using CopyDataStruct;
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
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        const int WM_COPYDATA = 0x004A;
        public FormSenderA()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            int hWnd = FindWindow(null, @"消息接受者");
            if (hWnd == 0)
            {
                MessageBox.Show("未找到消息接受者！");
            }
            else
            {
                byte[] sarr = System.Text.Encoding.Default.GetBytes(txtString.Text);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)Convert.ToInt16(txtInt.Text);
                cds.cbData = len + 1;
                cds.lpData = txtString.Text;
                SendMessage(hWnd, WM_COPYDATA, 0, ref cds);
            }
        }
    }
}
