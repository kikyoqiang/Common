using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Common
{
    /// <summary>
    /// 任务队列
    /// </summary>
    /// <typeparam name="T">任务要处理的数据的数据类型</typeparam>
    /// 示例在末尾
    public class TaskQueue<T>
    {
        #region 成员、属性、事件
        /// <summary>
        /// 数量锁
        /// </summary>
        private string _countLock = "LockCount";
        /// <summary>
        /// 总数量锁
        /// </summary>
        private string _countLockMain = "LockCountMain";
        private bool _isAsyn = false;
        /// <summary>
        /// 当在分配子任务时，遇到最大可执行任务数阻塞时，线程休眠的时长。
        /// </summary>
        private int _sleepMilliSecondsWhenQueueSubTask = 100;
        /// <summary>
        /// 当在分配子任务时，遇到最大可执行任务数阻塞时，线程休眠的时长。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value小于等于0时触发。
        /// </exception>
        public int SleepMilliSecondsWhenQueueSubTask
        {
            get { return _sleepMilliSecondsWhenQueueSubTask; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value值必须大于0。");
                }
                _sleepMilliSecondsWhenQueueSubTask = value;
            }
        }
        /// <summary>
        /// 当在在等待子任务完成时，每次线程休眠的时长。
        /// </summary>
        private int _sleepMilliSecondsWhenWaitComplete = 100;
        /// <summary>
        /// 当在在等待子任务完成时，每次线程休眠的时长。
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value小于等于0时触发。
        /// </exception>
        public int SleepMilliSecondsWhenWaitComplete
        {
            get { return _sleepMilliSecondsWhenWaitComplete; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value值必须大于0。");
                }
                _sleepMilliSecondsWhenWaitComplete = value;
            }
        }
        /// <summary>
        /// 允许同时执行的子任务个数
        /// </summary>
        private int _maxRunningThreadCount = 20;
        /// <summary>
        /// 允许同时执行的子任务个数
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value等于0时触发。
        /// </exception>
        public int MaxRunningSubTaskCount
        {
            get { return _maxRunningThreadCount; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value值必须大于0。");
                }
                lock (_countLock)
                {
                    _maxRunningThreadCount = value;
                }
            }
        }
        /// <summary>
        /// 每个子任务要处理的数据个数
        /// </summary>
        private int _maxDataCountEverySubTask = 50;
        /// <summary>
        /// 每个子任务要处理的数据个数
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value等于0时触发。
        /// </exception>
        public int MaxDataCountEverySubTask
        {
            get { return _maxDataCountEverySubTask; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value值必须大于0。");
                }
                lock (_countLock)
                {
                    _maxDataCountEverySubTask = value;
                }
            }
        }
        /// <summary>
        /// 当前已分配子任务个数
        /// </summary>
        private int _currentSubTaskCount = 0;
        /// <summary>
        /// 当前已分配子任务个数
        /// </summary>
        public int CurrentSubTaskCount
        {
            get { return _currentSubTaskCount; }
        }
        /// <summary>
        /// 当前已执行成功的子任务个数
        /// </summary>
        private int _currentSuccessSubTaskCount = 0;
        /// <summary>
        /// 当前已执行成功的子任务个数
        /// </summary>
        public int CurrentSuccessSubTaskCount
        {
            get { return _currentSuccessSubTaskCount; }
        }
        /// <summary>
        /// 当前已执行失败的线程个数
        /// </summary>
        private int _currentFailSubTaskCount = 0;
        /// <summary>
        /// 当前已执行失败的线程个数
        /// </summary>
        public int CurrentFailSubTaskCount
        {
            get { return _currentFailSubTaskCount; }
        }

        /// <summary>
        /// 每个子任务执行的委托
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="executeData">要处理的数据</param>
        //public delegate void ExecuteDelegate(TaskQueue<T> taskQueue, List<T> executeData);
        public delegate void ExecuteDelegate(List<T> executeData);
        /// <summary>
        /// 需要子任务处理数据时触发
        /// </summary>
        public event ExecuteDelegate ExecuteData;
        /// <summary>
        /// 触发子任务处理数据
        /// </summary>
        /// <param name="executeData"></param>
        protected void OnExecuteData(List<T> executeData)
        {
            if (ExecuteData != null)
            {
                //ExecuteData(this, executeData);
                ExecuteData(executeData);
            }
        }
        /// <summary>
        /// 所有子任务处理结束后要执行的委托
        /// </summary>
        /// <param name="sender">发送者</param>
        //public delegate void TaskCompletedDelegate(TaskQueue<T> taskQueue);
        public delegate void TaskCompletedDelegate();
        /// <summary>
        /// (异步模式下才会触发)所有子任务处理结束后触发
        /// </summary>
        public event TaskCompletedDelegate TaskCompleted;
        /// <summary>
        /// 通知任务队列拥有者任务已执行完成。
        /// </summary>
        protected void OnTaskExecuted()
        {
            if (TaskCompleted != null)
            {
                //TaskCompleted(this);
                TaskCompleted();
            }
        }
        /// <summary>
        /// 任务要处理的所有数据
        /// </summary>
        private List<T> _taskData;
        /// <summary>
        /// 任务要处理的所有数据
        /// </summary>
        public List<T> TaskData
        {
            get { return _taskData; }
        }

        #endregion

        #region 方法
        /// <summary>
        /// (异步模式)写入任务队列要处理的数据。函数会立刻返回。
        /// </summary>
        /// <param name="taskData">要处理的数据</param>
        /// <exception cref="ArgumentNullException">
        /// taskData为null时触发。
        ///   </exception>
        ///   
        /// <exception cref="ArgumentOutOfRangeException">
        /// taskData元素个数等于0时触发。
        ///   </exception>
        public void QueueUserTaskDataAsync(List<T> taskData)
        {
            _isAsyn = true;
            ThreadPool.QueueUserWorkItem(QueueTask, taskData);
        }

        /// <summary>
        /// (同步模式)写入任务队列要处理的数据。函数会在所有子任务执行结束后返回。
        /// </summary>
        /// <param name="taskData">要处理的数据</param>
        /// <exception cref="ArgumentNullException">
        /// taskData为null时触发。
        ///   </exception>
        ///   
        /// <exception cref="ArgumentOutOfRangeException">
        /// taskData元素个数等于0时触发。
        ///   </exception>
        public void QueueUserTaskData(List<T> taskData)
        {
            QueueTask(taskData);
        }

        /// <summary>
        /// 写入任务队列要处理的数据
        /// </summary>
        /// <param name="taskData">要处理的数据</param>
        /// <exception cref="ArgumentNullException">
        /// taskData为null时触发。
        ///   </exception>
        ///   
        /// <exception cref="ArgumentOutOfRangeException">
        /// taskData元素个数等于0时触发。
        ///   </exception>
        private void QueueTask(object taskData)
        {
            _taskData = taskData as List<T>;
            if (_taskData == null)
                throw new ArgumentNullException("taskData不能为空。");
            if (_taskData.Count <= 0)
                throw new ArgumentOutOfRangeException("taskData元素个数至少有一个。");
            // 初始化数据
            lock (_countLock)
            {
                _currentSubTaskCount = _currentSuccessSubTaskCount = _currentFailSubTaskCount = 0;
            }
            // 分配数据给子任务，直到所有数据分配完毕
            do
            {
                // 取出子任务需处理的数据
                List<T> executeData = _taskData.Take(_maxDataCountEverySubTask).ToList();
                // 从总数据中移除
                _taskData.RemoveRange(0, executeData.Count);
                // 如果正在执行的子任务达到了最大限定数目，则休眠一次
                while (_maxRunningThreadCount <= _currentSubTaskCount - _currentSuccessSubTaskCount - _currentFailSubTaskCount)
                {
                    Thread.Sleep(_sleepMilliSecondsWhenQueueSubTask);
                }
                // 分配子任务
                ThreadPool.QueueUserWorkItem(ExecuteDataTask, executeData);
                // 累计总子任务数
                lock (_countLockMain)
                {
                    _currentSubTaskCount++;
                }
            }
            while (_taskData.Count > 0);
            // 等待所有子任务执行结束
            while (_currentSubTaskCount > _currentSuccessSubTaskCount + _currentFailSubTaskCount)
            {
                Thread.Sleep(_sleepMilliSecondsWhenWaitComplete);
            }
            // 如果是异步模式，则需要通知队列拥有者任务已完成
            if (_isAsyn)
            {
                ThreadPool.QueueUserWorkItem(FireTaskExecuted);
            }
        }

        /// <summary>
        /// 异步触发TaskExecuted事件
        /// </summary>
        /// <param name="state"></param>
        private void FireTaskExecuted(object state)
        {
            // 通知队列拥有者任务已完成
            OnTaskExecuted();
        }

        /// <summary>
        /// 子任务处理数据
        /// </summary>
        /// <param name="data">子任务所需处理数据</param>
        private void ExecuteDataTask(object data)
        {
            bool success = true;
            try
            {
                List<T> executeData = data as List<T>;
                // 通知队列拥有者处理数据
                OnExecuteData(executeData);
            }
            catch
            {
                success = false;
            }
            // 累计子任务运行结束的数量
            if (success)
            {
                // 累计到成功数上
                lock (_countLock)
                {
                    _currentSuccessSubTaskCount++;
                }
            }
            else
            {
                // 累计到失败数上
                lock (_countLock)
                {
                    _currentFailSubTaskCount++;
                }
            }
        }
        #endregion
    }

    #region 示例
    //static void Main(string[] args)
    //{
    //    List<Person> list = new List<Person>() { new Person("AA"), new Person("BB"), new Person("CC") };
    //    TaskQueue<Person> task =new TaskQueue<Person>();
    //    task.ExecuteData += task_ExecuteData;
    //    task.TaskCompleted += task_TaskCompleted;
    //    task.QueueUserTaskDataAsync(list);
    //    Console.WriteLine("主线程Ok");
    //    Console.ReadKey();
    //}
    //static void task_ExecuteData( List<Person> executeData)
    //{
    //    try
    //    {
    //        Thread.Sleep(3000);
    //        executeData.ForEach(a => Console.WriteLine(a.Name));
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //    }
    //}
    //static void task_TaskCompleted()
    //{
    //    Console.WriteLine("执行完成");
    //}
    //public class Person
    //{
    //    public string Name { get; set; }
    //    public Person(string name)
    //    {
    //        this.Name = name;
    //    }
    //}
    #endregion
}

