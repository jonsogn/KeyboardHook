using KeyboardHook.Model;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardHook
{
    public class KeyboardHook : IDisposable
    {

        #region Private Fields

        private bool _isHookInstalled;
        private IntPtr _hookID;
        private LowLevelKeyboardProc _proc;

        internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion Private Fields

        #region Private Methods

        private void Initialize(bool installHook)
        {
            if (installHook)
            {
                SetHook();
            }
            else
            {
                _isHookInstalled = false;
            }
        }

        private bool SetHook()
        {
            if (!_isHookInstalled)
            {
                _proc = new LowLevelKeyboardProc(this.HookCallback);

                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    _hookID = WinAPI.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, _proc, WinAPI.GetModuleHandle(curModule.ModuleName), 0);

                    _isHookInstalled = true;

                    return true;
                }
            }

            return false;
        }

        private bool ClearHook()
        {
            if (_isHookInstalled)
            {
                if (_hookID != IntPtr.Zero)
                {
                    WinAPI.UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                    _isHookInstalled = false;
                    return true;
                }
            }

            return false;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                NewKeyboardEvent(wParam, hookStruct);
            }

            return WinAPI.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void NewKeyboardEvent(IntPtr wParam, KBDLLHOOKSTRUCT hookStruct)
        {            
            KeyboardEvent?.Invoke(this, new LowLevelKeyEventArgs((EventType)wParam, hookStruct));
        }

        #endregion Private Methods

        #region Public Fields/Methods

        public KeyboardHook()
        {
            Initialize(false);
        }

        public KeyboardHook(bool installHook)
        {
            Initialize(installHook);
        }

        public bool InstallHook()
        {
            return SetHook();
        }

        public bool UninstallHook()
        {
            return ClearHook();
        }

        public bool IsHookInstalled
        {
            get { return _isHookInstalled; }
        }

        public event System.EventHandler<LowLevelKeyEventArgs> KeyboardEvent;

        #endregion Public Fields/Methods

        #region IDisposable Support

        ~KeyboardHook()
        {
            Dispose(false);
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ClearHook();
                }

                disposed = true;
            }
        }

        #endregion IDisposable Support
    }
}