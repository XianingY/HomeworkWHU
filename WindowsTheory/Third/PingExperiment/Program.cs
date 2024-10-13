using System;
using System.Diagnostics;
using System.IO;

class Program1
{
    static void Main(string[] args)
    {
        Console.WriteLine("请输入IP地址或域名（默认为www.sohu.com和www.whu.edu.cn）:");
        string input = Console.ReadLine().Trim();
        string ipAddress = string.IsNullOrEmpty(input) ? "www.sohu.com" : input;

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c ping {ipAddress} -n 10",
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            
            Console.WriteLine("输出结果:");
            Console.WriteLine(output);
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("错误信息:");
                Console.WriteLine(error);
            }
        }
    }
}