using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MVCoin
{
    class PyCall
    {
        //呼叫python核心程式碼
        public string RunPythonScript(string sArgName, string args = "", params string[] teps)
        {
            Process p = new Process();
            string textOutput = "";
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sArgName;// 獲得python檔案的絕對路徑（將檔案放在c#的debug資料夾中可以這樣操作）
            path = @"..\..\py\" + sArgName;
            p.StartInfo.FileName = "python";//沒有配環境變數的話，可以寫python.exe的絕對路徑。如果配了，直接寫"python.exe"即可
            string sArguments = path;
            if (teps != null) 
            {
                foreach (string sigstr in teps)
                {
                    sArguments += " " + sigstr;//傳遞引數
                }
            }

            sArguments += " " + args;

            p.StartInfo.Arguments = sArguments;

            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardInput = true;

            p.StartInfo.RedirectStandardOutput = true;            

            p.StartInfo.RedirectStandardError = true;

            p.StartInfo.CreateNoWindow = true;

            p.Start();

            textOutput = p.StandardOutput.ReadToEnd();            
            
            //p.BeginOutputReadLine();
            //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            //Console.ReadLine();            
            p.WaitForExit();
            p.Close();
            return textOutput;

        }
        //輸出列印的資訊
        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                AppendText(e.Data + Environment.NewLine);
            }
        }
        public delegate void AppendTextCallback(string text);
        public static void AppendText(string text)
        {
            Console.WriteLine(text);     //此處在控制檯輸出.py檔案print的結果            
        }
    }
}
