using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FireAlarmApp
{
    public class FireAlarmEventArgs : EventArgs
    {
        public string Location { get; set; }
        public string Severity { get; set; }

        public FireAlarmEventArgs(string location, string severity)
        {
            Location = location;
            Severity = severity;
        }
    }

    public class FireAlarmTrigger
    {
        // 使用事件和委托来触发事件
        public event EventHandler<FireAlarmEventArgs> FireAlarmTriggered;

        // 触发火警事件
        public void TriggerFireAlarm(string location, string severity)
        {
            // 创建事件参数
            FireAlarmEventArgs args = new FireAlarmEventArgs(location, severity);

            // 如果事件有响应者，则触发事件
            FireAlarmTriggered?.Invoke(this, args);
        }
    }

    public class FireAlarmHandler
    {
        // 处理火警事件的函数1
        public void HandleFireAlarmLevel1(object sender, FireAlarmEventArgs e)
        {
            MessageBox.Show($"火警 1 - 地点: {e.Location}, 严重性: {e.Severity}");
        }

        // 处理火警事件的函数2
        public void HandleFireAlarmLevel2(object sender, FireAlarmEventArgs e)
        {
            MessageBox.Show($"火警 2 - 地点: {e.Location}, 严重性: {e.Severity}");
        }
    }
}
