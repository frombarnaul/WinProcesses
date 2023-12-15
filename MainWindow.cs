using System.Diagnostics;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;

namespace WinProcesses
{
    public partial class MainWindow : Form
    {
        private List<int> currListViewProcesses = new List<int>();
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Text = "WinProcesses";
            listView1.Columns.Add("ID процесса", 80);
            listView1.Columns.Add("Имя процесса", -2);

            Log.Logger = new LoggerConfiguration().WriteTo.File($"log-{DateTime.Now:yyyyMMdd-HHmmss}.txt").CreateLogger();
            Log.Information("App started");

            Task.Run(StartRefreshTaskAsync);
        }


        private async Task StartRefreshTaskAsync()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                await Task.Delay(1000);
                FillProcessesListView();
            }
        }

        private void FillProcessesListView()
        {
            UpdateListUI(ProcessesInfo.GetProcesses());
        }

        private void UpdateListUI(List<Process> processes)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() =>
                {
                    UpdateListUI(processes);
                }));
            else
            {
                listView1.BeginUpdate();

                foreach (var process in processes)
                {
                    if (!currListViewProcesses.Contains(process.Id))
                    {
                        ListViewItem item = new ListViewItem(process.Id.ToString());
                        item.SubItems.Add(process.ProcessName);
                        listView1.Items.Add(item);
                        currListViewProcesses.Add(process.Id);
                    }
                }

                var itemsToRemove = listView1.Items.Cast<ListViewItem>()
                  .Where(item => !processes.Any(process => process.Id.ToString() == item.Text))
                  .ToArray();

                foreach (var itemToRemove in itemsToRemove)
                {
                    listView1.Items.Remove(itemToRemove);
                    currListViewProcesses.Remove(int.Parse(itemToRemove.Text));
                }
                listView1.EndUpdate();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                int processId;
                if (int.TryParse(selectedItem.Text, out processId))
                {
                    ShowProcessInfoForm(processId);
                }
            }
        }

        private void ShowProcessInfoForm(int processId)
        {
            Log.Information($"Open information on the process ID: {processId}");
            ProcessInfoWindow processInfoForm = new ProcessInfoWindow(processId);
            processInfoForm.ShowDialog();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource.Token.IsCancellationRequested)
            {
                cancellationTokenSource = new CancellationTokenSource();
                Task.Run(StartRefreshTaskAsync);
                UpdatePauseButtonText("Пауза");
                Log.Information("Refresh processes list enabled");
            }
            else
            {
                cancellationTokenSource.Cancel();
                UpdatePauseButtonText("Возобновить");
                Log.Information("Refresh processes list disabled");
            }
        }

        private void UpdatePauseButtonText(string text)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() =>
                {
                    UpdatePauseButtonText(text);
                }));
            else
                PauseButton.Text = text;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Information("App closed");
        }
    }
}