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
    public class StopwatchModel : IDisposable
    {
        #region Fields
        /// <summary>
        /// Коллекция, содержащая время преодоления каждого круга.
        /// </summary>
        private readonly ObservableCollection<TimeSpan> timeOfEachLapField = new ObservableCollection<TimeSpan>();

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
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="StopwatchModel" /> class.
        /// </su1mmary>
        public StopwatchModel()
        {
            this.countingTimer.Interval = 100;

            this.countingTimer.Elapsed += new ElapsedEventHandler((S, E) =>
            {
                this.TotalStopwatchTime += new TimeSpan(0, 0, 0, 0, 100);
            });

            TimeOfEachLap = new ReadOnlyObservableCollection<TimeSpan>(timeOfEachLapField);
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
                return totalStopwatchTimeField;
            }

            private set
            {
                this.totalStopwatchTimeField = value;
            }
        }

        /// <summary>
        /// Get кол-во кругов секундомера, которые были преодолены.
        /// </summary>
        public int CountLaps { get; private set; }

        /// <summary>
        /// Время кругов.
        /// </summary>
        public ReadOnlyObservableCollection<TimeSpan> TimeOfEachLap { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Запуск секундомера.
        /// </summary>
        public void Start()
        {
            this.countingTimer.Start();
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
        }

        /// <summary>
        /// Выполняет начало нового круга. Время пройденого круга добавляется в список отметок секундомера.
        /// </summary>
        public void StartNewLap()
        {
            this.timeOfEachLapField.Add(this.TotalStopwatchTime);
            this.CountLaps++;
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

    }
}
