using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SimpleMonads;
using Microsoft.Win32.SafeHandles;

using static SimpleMonads.Utility;

namespace MoreIO
{
    public static class SymbolicLink
    {
        private const uint GenericReadAccess = 0x80000000;

        private const uint FileFlagsForOpenReparsePointAndBackupSemantics = 0x02200000;

        private const int IoctlCommandGetReparsePoint = 0x000900A8;

        private const uint OpenExisting = 0x3;

        private const uint PathNotAReparsePointError = 0x80071126;

        private const uint ShareModeAll = 0x7; // Read, Write, Delete

        private const uint SymLinkTag = 0xA000000C;

        private const int TargetIsAFile = 0;

        private const int TargetIsADirectory = 1;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            int nInBufferSize,
            IntPtr lpOutBuffer,
            int nOutBufferSize,
            out int lpBytesReturned,
            IntPtr lpOverlapped);

        private static SafeFileHandle GetFileHandle(string path)
        {
            return CreateFile(path, GenericReadAccess, ShareModeAll, IntPtr.Zero, OpenExisting,
                FileFlagsForOpenReparsePointAndBackupSemantics, IntPtr.Zero);
        }

        #region PathSpec extension methods

        public static void CreateSymlinkTo(this PathSpec linkPath, PathSpec targetPath)
        {
            switch (targetPath.GetPathType())
            {
                case PathType.File:
                    if (!CreateSymbolicLink(linkPath.ToString(), targetPath.ToString(), TargetIsAFile))
                    {
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    }
                    break;
                default:
                    if (!CreateSymbolicLink(linkPath.ToString(), targetPath.ToString(), TargetIsADirectory) || Marshal.GetLastWin32Error() != 0)
                    {
                        try
                        {
                            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                        }
                        catch (COMException exception)
                        {
                            throw new IOException(exception.Message, exception);
                        }
                    }
                    break;
            }
        }

        public static bool IsSymlink(this PathSpec path)
        {
            return path.GetPathType() != PathType.None && path.GetSymlinkTarget().HasValue;
        }

        public static IMaybe<PathSpec> GetSymlinkTarget(this PathSpec path)
        {
            SymbolicLinkReparseData reparseDataBuffer;

            using (SafeFileHandle fileHandle = GetFileHandle(path.ToString()))
            {
                if (fileHandle.IsInvalid)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }

                int outBufferSize = Marshal.SizeOf(typeof(SymbolicLinkReparseData));
                IntPtr outBuffer = IntPtr.Zero;
                try
                {
                    outBuffer = Marshal.AllocHGlobal(outBufferSize);
                    int bytesReturned;
                    bool success = DeviceIoControl(
                        fileHandle.DangerousGetHandle(), IoctlCommandGetReparsePoint, IntPtr.Zero, 0,
                        outBuffer, outBufferSize, out bytesReturned, IntPtr.Zero);

                    fileHandle.Close();

                    if (!success)
                    {
                        if (((uint)Marshal.GetHRForLastWin32Error()) == PathNotAReparsePointError)
                        {
                            return Nothing<PathSpec>();
                        }
                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    }

                    reparseDataBuffer = (SymbolicLinkReparseData)Marshal.PtrToStructure(
                        outBuffer, typeof(SymbolicLinkReparseData));
                }
                finally
                {
                    Marshal.FreeHGlobal(outBuffer);
                }
            }
            if (reparseDataBuffer.ReparseTag != SymLinkTag)
            {
                return Nothing<PathSpec>();
            }

            string target = Encoding.Unicode.GetString(reparseDataBuffer.PathBuffer,
                reparseDataBuffer.PrintNameOffset, reparseDataBuffer.PrintNameLength);

            return target.ToPathSpec();
        }

        #endregion
    }
}
