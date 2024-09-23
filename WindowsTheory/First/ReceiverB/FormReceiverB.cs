using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            this.Text = "消息接受者";
        }


        const int WM_COPYDATA = 0x004A;
        

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT cds = new COPYDATASTRUCT();
                    Type t = cds.GetType();
                    cds = (COPYDATASTRUCT)m.GetLParam(t);
                    string strResult = cds.dwData.ToString() + ":" + cds.lpData;
                    lsvMsgList.Items.Add(strResult);
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
    }
}
