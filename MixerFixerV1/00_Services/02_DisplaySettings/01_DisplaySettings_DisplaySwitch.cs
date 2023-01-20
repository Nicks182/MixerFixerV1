using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using MixerFixerV1;
using DisplaySettings;
using System.Windows.Interop;
using System.Diagnostics;

namespace Services
{
    public partial class Srv_DisplaySettings
    {
        private void SetDisplayMode(DisplayMode mode)
        {
            var proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = "DisplaySwitch.exe";
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            switch (mode)
            {
                case DisplayMode.External:
                    proc.StartInfo.Arguments = "/external";
                    break;
                case DisplayMode.Internal:
                    proc.StartInfo.Arguments = "/internal";
                    break;
                case DisplayMode.Extend:
                    proc.StartInfo.Arguments = "/extend";
                    break;
                case DisplayMode.Duplicate:
                    proc.StartInfo.Arguments = "/clone";
                    break;
            }
            proc.Start();
            //proc.WaitForExit();
        }


        enum DisplayMode
        {
            Internal,
            External,
            Extend,
            Duplicate
        }
    }
}

