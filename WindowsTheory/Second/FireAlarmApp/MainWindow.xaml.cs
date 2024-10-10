using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FireAlarmApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FireAlarmTrigger fireAlarmTrigger;
        private FireAlarmHandler fireAlarmHandler;

        public MainWindow()
        {
            InitializeComponent();

            // 初始化事件触发者和事件响应者
            fireAlarmTrigger = new FireAlarmTrigger();
            fireAlarmHandler = new FireAlarmHandler();

            // 设置按钮点击事件
            btnBind1.Click += (sender, e) => Bind1();
            btnBind2.Click += (sender, e) => Bind2();
            btnUnbind1.Click += (sender, e) => Unbind1();
            btnUnbind2.Click += (sender, e) => Unbind2();
            btnTrigger1.Click += (sender, e) => TriggerEvent1();
            
        }

        // 动态绑定事件响应者1
        private void Bind1()
        {
            btnBind1.Background = new SolidColorBrush(Colors.Red);


            fireAlarmTrigger.FireAlarmTriggered += fireAlarmHandler.HandleFireAlarmLevel1;


            Task.Delay(500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => btnBind1.Background = new SolidColorBrush(Color.FromRgb(45, 79, 107)));
            });
        }

        // 动态绑定事件响应者2
        private void Bind2()
        {
            btnBind2.Background = new SolidColorBrush(Colors.Red);

            fireAlarmTrigger.FireAlarmTriggered += fireAlarmHandler.HandleFireAlarmLevel2;



            Task.Delay(500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => btnBind2.Background = new SolidColorBrush(Color.FromRgb(45, 79, 107)));
            });
        }

        // 动态解绑事件响应者1
        private void Unbind1()
        {
            btnUnbind1.Background = new SolidColorBrush(Colors.Red);


            fireAlarmTrigger.FireAlarmTriggered -= fireAlarmHandler.HandleFireAlarmLevel1;


            Task.Delay(500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => btnUnbind1.Background = new SolidColorBrush(Color.FromRgb(45, 79, 107)));
            });
        }

        // 动态解绑事件响应者2
        private void Unbind2()
        {
            btnUnbind2.Background = new SolidColorBrush(Colors.Red);

            
            fireAlarmTrigger.FireAlarmTriggered -= fireAlarmHandler.HandleFireAlarmLevel2;


            Task.Delay(500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => btnUnbind2.Background = new SolidColorBrush(Color.FromRgb(45, 79, 107)));
            });
        }

        // 触发火警事件
        private void TriggerEvent1()
        {
            
            btnTrigger1.Background = new SolidColorBrush(Colors.Red);

            fireAlarmTrigger.TriggerFireAlarm("厨房", "严重");

            
            Task.Delay(500).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() => btnTrigger1.Background = new SolidColorBrush(Color.FromRgb(45, 79, 107)));
            });
        }
        
    }
}
