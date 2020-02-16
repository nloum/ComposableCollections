using System.Text;

namespace MoreIO.Ini
{
    // Code from http://www.codeproject.com/Articles/1966/An-INI-file-handling-class-using-C
    public static class Extensions
    {
        public static void WriteIni(this PathSpec pathSpec, string section, string key, string value)
        {
            NativeMethods.WritePrivateProfileString(section, key, value, pathSpec.ToString());
        }

        public static string ReadIni(this PathSpec pathSpec, string section, string key)
        {
            var temp = new StringBuilder(255);
            int i = NativeMethods.GetPrivateProfileString(section, key, "", temp,
                255, pathSpec.ToString());
            return temp.ToString();
        }
    }
}
