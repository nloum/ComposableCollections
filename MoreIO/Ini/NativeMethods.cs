using System.Runtime.InteropServices;
using System.Text;

namespace MoreIO.Ini
{
    internal static class NativeMethods
    {
        [DllImport("kernel32")]
        internal static extern long WritePrivateProfileString(string section,
                                                              string key, string val, string filePath);

        [DllImport("kernel32")]
        internal static extern int GetPrivateProfileString(string section,
                                                           string key, string def, StringBuilder retVal,
                                                           int size, string filePath);
    }
}
