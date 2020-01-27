using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsDemo
{
    public partial class FormMain : Form
    {
        KeyboardHook.KeyboardHook KeyboardHook;

        private void InitializeAdditionalComponent()
        {
            KeyboardHook = new KeyboardHook.KeyboardHook(false);
            KeyboardHook.KeyboardEvent += KeyboardHook_KeyboardEvent;
        }

        private void KeyboardHook_KeyboardEvent(object sender, KeyboardHook.Model.LowLevelKeyEventArgs e)
        {
            tbOutput.Text += $"Time: {e.Timestamp}, Key: {(Keys)e.VirtualKeyCode}, Event: {e.EventType}" + Environment.NewLine;
        }

        public FormMain()
        {
            InitializeComponent();
            InitializeAdditionalComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (KeyboardHook.IsHookInstalled)
            {
                KeyboardHook.UninstallHook();
            }

            Environment.Exit(0);
        }

        private void BtnHook_Click(object sender, EventArgs e)
        {
            KeyboardHook.InstallHook();
        }

        private void BtnUnhook_Click(object sender, EventArgs e)
        {
            KeyboardHook.UninstallHook();
        }
    }
}
