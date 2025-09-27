using System;
using System.Threading;
using System.Threading.Tasks;

namespace Common.TaskHandler
{

    /// <summary>
    /// <see cref="TaskHandler"/> class creates an own <see cref="Task"/> for an <see cref="Func{Task}<"/> method
    /// and also provides possibilites to execute that method just once, or just to be repeatedly executed
    /// Also we can put that task to sleep from outside the task with <see cref="TaskShouldWait"/> and to continue
    /// with <see cref="TaskShouldContinue"/>
    /// In case of infinitly execution we can set the <see cref="interval"/> between 2 execution
    /// </summary>
    public class TaskHandler
    {
        private bool deleted;
        private Task task;
        private int interval;
        private bool executeOnce;
        private bool taskShouldWait;
        private AutoResetEvent stopEvent;
        private readonly Func<Task> taskFunction;

        public TaskHandler(Func<Task> taskFunctionParam, bool executeOnceParam, int intervalParam, CancellationTokenSource cancellationTokenSource)
        {
            taskShouldWait = true;
            executeOnce = executeOnceParam;
            taskFunction = taskFunctionParam;
            stopEvent = new AutoResetEvent(false);
            interval = intervalParam;

            if (interval < 0) interval = 0;

            task = new Task(async () =>
            {
                do
                {
                    if (taskShouldWait)
                    {
                        stopEvent.WaitOne();

                        if (deleted) { break; }
                    }

                    try
                    {
                        await taskFunction.Invoke();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    Thread.Sleep(interval);
                }
                while (true && !executeOnce && !cancellationTokenSource.IsCancellationRequested);

                task = null;
            });

            task.Start();
        }

        /// <summary>
        /// The method which does awakens a sleeping thread
        /// </summary>
        public void TaskShouldContinue()
        {
            if (task != null)
            {
                taskShouldWait = false;
                stopEvent.Set();
            }
        }

        /// <summary>
        /// The method which does put to sleep a thread
        /// </summary>
        public void TaskShouldWait()
        {
            if (task != null)
            {
                stopEvent.Reset();
                taskShouldWait = true;
            }
        }

        /// <summary>
        /// After there is no need for the task nomore, then we can stop it and delete it that task.
        /// </summary>
        public void DeleteTask()
        {
            deleted = true;
            stopEvent.Set();
            task = null;
        }
    }
}
