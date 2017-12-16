using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Auto_COM
{
    public class DelayCaller
    {
        private Queue<DelayTask> taskQueue = new Queue<DelayTask>();
        private Thread threadTask = null;

        private void Proc()
        {
            while (true)
            {
                try
                {
                    if (taskQueue.Count() == 0)
                    {
                        Thread.Sleep(0);
                        continue;
                    }
                    DelayTask task = taskQueue.Peek();
                    Thread.Sleep(task.DelayTime);
                    task = taskQueue.Dequeue();
                    if (!task.IsRun)
                    {
                        continue;
                    }
                    task.Action();
                    task.IsRun = false;
                }
                catch (ThreadAbortException)
                {
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public bool HasTask()
        {
            return taskQueue.Count() > 0;
        }
        public DelayTask NewCall(int timeout, Action action)
        {
            if (threadTask == null || !threadTask.IsAlive)
            {
                threadTask = new Thread(new ThreadStart(Proc));
                threadTask.Start();
            }
            DelayTask task = new DelayTask();
            task.DelayTime = timeout;
            task.Action = action;
            taskQueue.Enqueue(task);
            return task;
        }
        public void Quit()
        {
            if (threadTask != null)
            {
                threadTask.Abort();
                threadTask = null;
            }
        }
        public void CancelAll()
        {
            while (taskQueue.Count > 0)
            {
                DelayTask task = taskQueue.Dequeue();
                task.IsRun = false;
            }
        }
    }
}
