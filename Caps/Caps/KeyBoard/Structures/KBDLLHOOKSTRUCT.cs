using System;
using System.Runtime.InteropServices;

namespace Caps.KeyBoard.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Kbdllhookstruct
    {
        public int VkCode { get; set; }
        public uint ScanCode { get; set; }
        public uint Flags { get; set; }
        public uint Time { get; set; }
        public IntPtr DwExtraInfo { get; set; }
    }
}
