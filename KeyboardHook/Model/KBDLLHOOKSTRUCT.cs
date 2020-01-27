using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardHook.Model
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;      // Specifies a virtual-key code
        public uint scanCode;    // Specifies a hardware scan code for the key
        public uint flags;       // keyboard flags
        public uint time;        // Specifies the time stamp for this message
        public uint dwExtraInfo; // not used
    }
}
