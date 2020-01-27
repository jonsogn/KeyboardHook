using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardHook.Model
{
    public class LowLevelKeyEventArgs : EventArgs
    {
        public uint VirtualKeyCode { get; set; }
        public EventType EventType { get; set; }
        public uint ScanCode { get; set; }
        public uint Timestamp { get; set; }
        public EventFlags EventFlags { get; set; }
        public uint ExtraInfo { get; set; }

        public LowLevelKeyEventArgs()
        {
            VirtualKeyCode = 0;
            ScanCode = 0;
            Timestamp = 0;
            EventFlags = 0;
            ExtraInfo = 0;
        }

        internal LowLevelKeyEventArgs(EventType eventType, KBDLLHOOKSTRUCT data)
        {
            VirtualKeyCode = data.vkCode;
            EventType = eventType;
            ScanCode = data.scanCode;
            Timestamp = data.time;
            EventFlags = (EventFlags)data.flags;
            ExtraInfo = (uint)data.dwExtraInfo;
        }
    }
}
