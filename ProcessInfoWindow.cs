using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinProcesses
{
    public partial class ProcessInfoWindow : Form
    {
        public ProcessInfoWindow(int processId)
        {
            InitializeComponent();
            DisplayProcessInfo(processId);
        }
        private void DisplayProcessInfo(int processId)
        {
            txtBoxProcInfo.Text = ProcessesInfo.GetProcessInfo(processId);
            txtBoxProcInfo.Select(0, 0);
        }
    }
}