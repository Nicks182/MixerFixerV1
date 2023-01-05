using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Services
{
    public class Srv_Utils
    {
        public string _ImageToBase64(Bitmap P_Image)
        {
            if (P_Image == null)
            {
                return "";
            }
            using (MemoryStream m = new MemoryStream())
            {
                P_Image.Save(m, ImageFormat.Png);
                byte[] imageBytes = m.ToArray();

                return Convert.ToBase64String(imageBytes);
            }
        }

        public float _Volume_FromInt(int P_Value)
        {
            //float L_New = (float)(P_Value / 100);
            //float L_New = ((float)(P_Value / 100));

            //return L_New;
            return (float)Math.Round((P_Value / 100.0f),2);
        }

        public float _Volume_FromString(string P_Value)
        {
            return _Volume_FromInt(Convert.ToInt32(P_Value));
        }

        public string _GetIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }

    public static class Srv_Utils_NativeMethods
    {
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_SMALLICON = 1;
        private const uint SHGFI_LARGEICON = 0;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        public static Icon GetIconFromFile(string fileName, int index = 0, bool smallIcon = false)
        {
            if (fileName.Contains(",") && int.TryParse(fileName.Split(',')[1], out index)) fileName = fileName.Split(',')[0];
            fileName = Environment.ExpandEnvironmentVariables(fileName);
            if (fileName.Contains("@")) fileName = fileName.Replace("@", "");

            if (File.Exists(fileName))
            {
                SHFILEINFO shInfo = new SHFILEINFO
                {
                    szDisplayName = new string('\0', 260),
                    szTypeName = new string('\0', 80),
                    iIcon = index
                };

                var r = SHGetFileInfo(fileName, 0, ref shInfo, (uint)Marshal.SizeOf(shInfo), SHGFI_ICON | (smallIcon ? SHGFI_SMALLICON : SHGFI_LARGEICON));

                return shInfo.hIcon.ToInt32() == 0 ? null : Icon.FromHandle(shInfo.hIcon);
            }
            else
            {
                Console.WriteLine($"File not found: {fileName}");
            }

            return null;
        }
    }
}
