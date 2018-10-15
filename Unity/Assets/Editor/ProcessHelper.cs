using System.Diagnostics;

public class ProcessHelper
{

    public static void RunInMain()
    {
        
    }

    public static void RunInBackground(string fileName, string arguments)
    {
    }

    public static void RunInBackgroundWithLog(string fileName, string arguments)
    {
        ProcessStartInfo psi = new ProcessStartInfo();            
        psi.FileName = fileName;            
        psi.UseShellExecute = false;
        psi.RedirectStandardError = true;
        psi.RedirectStandardOutput = true;
        psi.Arguments = arguments;

        Process proc = Process.Start(psi);                
        proc.WaitForExit();
        string errorOutput = proc.StandardError.ReadToEnd();
        string standardOutput = proc.StandardOutput.ReadToEnd();
        //zero is successful 
        if (proc.ExitCode != 0)
        {
            string err = "Run exit code: " + proc.ExitCode.ToString() + " " 
            + (!string.IsNullOrEmpty(errorOutput) ? " " + errorOutput : "") + " " 
            + (!string.IsNullOrEmpty(standardOutput) ? " " + standardOutput : "");
            ShowMacNoti("Run Error", err);
        } else {
            ShowMacNoti("Run success!", "ok");
        }
    }

    //show mac notification
    //osascript -e 'display notification "通知内容" with title "标题" subtitle "子标题"'
    public static void ShowMacNoti(string title, string content)
    {
        string arguments = string.Format("osascript -e 'display notification {0} with title {1}'", content, title);
        Process.Start("/bin/bash", arguments);
    }





    
}

