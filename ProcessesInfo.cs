using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace WinProcesses
{
    internal class ProcessesInfo
    {
        public static List<Process> GetProcesses()
        {
            List<Process> processesList = new List<Process>();

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                processesList.Add(process);
            }
            return processesList;
        }

        public static string GetProcessInfo(int processId)
        {
            string info = String.Empty;
            try
            {
                Process process = Process.GetProcessById(processId);
                try
                {
                    info = $"Имя процесса: {process.ProcessName}\r\n" +
                                              $"ID процесса: {process.Id}\r\n" +
                                              $"Приоритет: {process.BasePriority}\r\n" +
                                              $"Время запуска: {process.StartTime}\r\n" +
                                              $"Выделенная память (Выгружаемая): {Math.Round((double)process.PagedSystemMemorySize64 / 1024, 2)}KB\r\n" +
                                              $"Выделенная память (Не выгружаемая): {Math.Round((double)process.NonpagedSystemMemorySize64 / 1024, 2)}KB\r\n" +
                                              $"Выделенная физическая память: {Math.Round((double)process.WorkingSet64 / 1024, 2)}KB\r\n" +
                                              $"Количество потоков: {process.Threads.Count}\r\n";
                }
                catch (Exception)
                {
                    info = "Отказано в доступе к процессу!";
                }
            }
            catch (ArgumentException)
            {
                info = $"Процесс не найден!";
            }

            return info;
        }
    }
}
