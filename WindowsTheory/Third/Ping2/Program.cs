using System;
using System.Diagnostics;
using System.IO;

class Program2
{ 
    static void Main(string[] args)
    {
    

        Console.WriteLine("\n请输入IP地址或域名进行异步Ping测试（默认为www.whu.edu.cn）:");
        string inputAsync = Console.ReadLine().Trim();
        string ipAddressAsync = string.IsNullOrEmpty(inputAsync) ? "www.whu.edu.cn" : inputAsync;

        ProcessStartInfo startInfoAsync = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c ping {ipAddressAsync} -n 10",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using (Process processAsync = new Process { StartInfo = startInfoAsync })
        {
            processAsync.Start();

            processAsync.OutputDataReceived += (sender, e) =>
            {
                Console.WriteLine("异步输出: " + e.Data);
            };

            processAsync.ErrorDataReceived += (sender, e) =>
            {
                Console.WriteLine("异步错误: " + e.Data);
            };

            processAsync.BeginOutputReadLine();
            processAsync.BeginErrorReadLine();

            processAsync.WaitForExit();
        }
    }

}