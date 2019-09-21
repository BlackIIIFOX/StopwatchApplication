using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Timers;

namespace StopwatchApplication.Model
{
    /// <summary>
    /// Класс, реализующий функции секундомера. Позволяет:
    /// 1) Запускать время.
    /// 2) Останавливать время (пауза).
    /// 3) Продолжать остановленное время.
    /// 4) Сбросить время.
    /// 5) Запускать следующий круг.
    /// 6) Просматривать время круга и общее время.
    /// </summary>
    public class StopwatchModel : BaseModel, IDisposable
    {
        #region Fields
        /// <summary>
        /// Интервал таймера для отсчета секундомера.
        /// </summary>
        private readonly int timerInterval = 10;

        /// <summary>
        /// Коллекция, содержащая время преодоления каждого круга.
        /// </summary>
        private readonly ObservableCollection<Lap> timeOfEachLapField = new ObservableCollection<Lap>();

        /// <summary>
        /// Instantiate a SafeHandle instance.
        /// </summary>
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Таймер для подсчет секунд.
        /// </summary>
        private readonly Timer countingTimer = new Timer();

        /// <summary>
        /// Общее время секундомера с начала запуска.
        /// </summary>
        private TimeSpan totalStopwatchTimeField = new TimeSpan(); 

        /// <summary>
        /// Flag: Has Dispose already been called?
        /// </summary>
        private bool disposed = false;

        private DateTime lastTimeCallback;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StopwatchModel" /> class.
        /// </su1mmary>
        public StopwatchModel()
        {
            this.countingTimer.Interval = timerInterval;

            this.countingTimer.Elapsed += new ElapsedEventHandler((S, E) =>
            {
                var elapsed = DateTime.Now - this.lastTimeCallback;
                this.lastTimeCallback = DateTime.Now;
                this.TotalStopwatchTime += elapsed;
                //this.TotalStopwatchTime += new TimeSpan(0, 0, 0, 0, timerInterval);
            });

            TimeOfEachLap = new ReadOnlyObservableCollection<Lap>(timeOfEachLapField);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets общее время секундомера с начала запуска.
        /// </summary>
        public TimeSpan TotalStopwatchTime
        {
            get
            {
                lock (this)
                {
                    return totalStopwatchTimeField;
                }
            }

            private set
            {
                lock (this)
                {
                    this.totalStopwatchTimeField = value;
                }
            }
        }

        /// <summary>
        /// Get кол-во кругов секундомера, которые были преодолены.
        /// </summary>
        public int CountLaps { get; private set; }

        /// <summary>
        /// Время кругов.
        /// </summary>
        public ReadOnlyObservableCollection<Lap> TimeOfEachLap { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Запуск секундомера.
        /// </summary>
        public void Start()
        {
            this.countingTimer.Start();
            this.lastTimeCallback = DateTime.Now;
        }

        /// <summary>
        /// Остановка секундомера.
        /// </summary>
        public void Stop()
        {
            this.countingTimer.Stop();
        }

        /// <summary>
        /// Сброс секундомера. Сбрасывает круги и обнуляет время секудомера.
        /// </summary>
        public void Reset()
        {
            this.TotalStopwatchTime = TimeSpan.Zero;
            this.timeOfEachLapField.Clear();
            this.CountLaps = 0;
        }

        /// <summary>
        /// Выполняет начало нового круга. Время пройденого круга добавляется в список отметок секундомера.
        /// </summary>
        public void StartNewLap()
        {
            TimeSpan lapTime = this.timeOfEachLapField.Count == 0 ?
                this.TotalStopwatchTime :
                this.TotalStopwatchTime - this.timeOfEachLapField[this.timeOfEachLapField.Count - 1].TotalTime;

            this.timeOfEachLapField.Add(new Lap(++this.CountLaps, lapTime, this.TotalStopwatchTime));
        }

        /// <summary>
        /// Имплементация интерфейса <see cref="IDisposable"/>.
        /// </summary>
        public void Dispose()
        {
            //// Dispose of unmanaged resources.
            Dispose(true);
            //// Suppress finalization.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождение ресурсов объекта.
        /// </summary>
        /// <param name="disposing">Состояние освобождения объекта. Если уже начато освобождение, то повторное не начнется.</param>
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                this.handle.Dispose();
                //// Free any other managed objects here.
                this.countingTimer.Dispose();
            }

            this.disposed = true;
        }
        #endregion

        #region Classes

        /// <summary>
        /// Класс круга.
        /// Экземпляры класса являются Immutable объектами.
        /// </summary>
        public class Lap : BaseModel
        {
            #region Fields
            #endregion

            #region Constructors
            public Lap(int lapNumber, TimeSpan lapTime, TimeSpan totalTime)
            {
                this.LapNumber = lapNumber;
                this.LapTime = lapTime;
                this.TotalTime = totalTime;
            }
            #endregion

            #region Properties
            /// <summary>
            /// Gets номер круга.
            /// </summary>
            public int LapNumber { get; private set; }
            
            /// <summary>
            /// Gets время круга.
            /// </summary>
            public TimeSpan LapTime { get; private set; }
            
            /// <summary>
            /// Gets общее время секундомера, на котором был завершен круг.
            /// </summary>
            public TimeSpan TotalTime { get; private set; }
            #endregion
        }
        #endregion

    }
}
