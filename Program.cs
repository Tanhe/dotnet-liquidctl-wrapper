using System;
using System.Diagnostics;
using System.Text;

namespace dotnet_liquidctl_wrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(LiquidctlController.GetLiquidTemp());
        }
    }

    class LiquidctlController
    {
        public static double GetLiquidTemp()
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = @"liquidctl",
                Arguments = @"status",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardErrorEncoding = Encoding.ASCII,
                StandardOutputEncoding = Encoding.ASCII,
                UseShellExecute = false,
                CreateNoWindow = false

            };

            var outputBuilder = new StringBuilder();
            var errorOutputBuilder = new StringBuilder();
            var process = new Process();
            process.StartInfo = startInfo;
            // enable raising events because Process does not raise events by default
            process.EnableRaisingEvents = true;
            // attach the event handler for OutputDataReceived before starting the process
            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
                    // append the new data to the data already read-in
                    outputBuilder.Append(e.Data);
                }
            );
            process.ErrorDataReceived += new DataReceivedEventHandler(
                delegate (object sender, DataReceivedEventArgs e)
                {
                    // append the new data to the data already read-in
                    errorOutputBuilder.Append(e.Data);
                }
                );
            // start the process
            // then begin asynchronously reading the output
            // then wait for the process to exit
            // then cancel asynchronously reading the output
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.CancelOutputRead();
            process.CancelErrorRead();

            // use the output
            string output = outputBuilder.ToString();
            string errorOutput = errorOutputBuilder.ToString();

            System.Console.WriteLine("StdOut: " + output);
            System.Console.WriteLine("StdErr: " + errorOutput);

            return 0;
        }
    }
}
