using HandControl.Services;
using StopwatchApplication.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static StopwatchApplication.Model.StopwatchModel;

namespace StopwatchApplication.ViewModel
{
    /// <summary>
    /// Состояния работы окна <see cref="StopwatchMainView"/>.
    /// </summary>
    public enum StopwatchViewModes { Launch, Run, Wait, DisplayLap }

    /// <summary>
    /// ViewModel основного окна приложения.
    /// </summary>
    class StopwatchMainViewModel : ViewModelBase
    {
        #region Fields
        private readonly StopwatchModel stopwatchField = new StopwatchModel();
        #endregion

        #region Constructors
        public StopwatchMainViewModel()
        {
            this.Stopwatch = this.stopwatchField;
            this.StopwatchViewMode = StopwatchViewModes.Wait;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets текущий режим работы окна секундомера. Влияет на отображаемые элементы.
        /// </summary>
        public StopwatchViewModes StopwatchViewMode { get; private set; }

        /// <summary>
        /// Gets секундомер.
        /// </summary>
        public StopwatchModel Stopwatch { get; private set; }

        /// <summary>
        /// Gets круги секундомера.
        /// </summary>
        public ReadOnlyObservableCollection<Lap> Laps
        {
            get
            {
                return stopwatchField.TimeOfEachLap;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Gets команду старта секундомера.
        /// </summary>
        public ICommand StartStopwatchCommand
        {
            get
            {
                return new RelayCommand((object obj) => this.StartStopwatch());
            }
        }

        /// <summary>
        /// Gets команду остановки секундомера.
        /// </summary>
        public ICommand StopStopwatchCommand
        {
            get
            {
                return new RelayCommand((object obj) => this.StopStopwatch());
            }
        }

        /// <summary>
        /// Gets команду сброса секундомера.
        /// </summary>
        public ICommand ResetStopwatchCommand
        {
            get
            {
                return new RelayCommand((object obj) => this.ResetStopwatch());
            }
        }

        /// <summary>
        /// Gets команду начала нового круга.
        /// </summary>
        public ICommand StartNewLapStopwatchCommand
        {
            get
            {
                return new RelayCommand((object obj) => this.StartNewLapStopwatch());
            }
        }

        /// <summary>
        /// Gets команду закрытия приложения.
        /// </summary>
        public ICommand CloseAppCommand
        {
            get
            {
                return new RelayCommand((object obj) => this.CloseApp());
            }
        }

        /// <summary>
        /// Закрыть приложение.
        /// </summary>
        private void CloseApp()
        {
            this.Stopwatch.Dispose();
            Environment.Exit(1);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Запустить секундомер <see cref="Stopwatch"/>.
        /// </summary>
        private void StartStopwatch()
        {
            this.Stopwatch.Start();
            this.StopwatchViewMode = StopwatchViewModes.Run;
        }

        /// <summary>
        /// Остановить секундомер <see cref="Stopwatch"/>.
        /// </summary>
        private void StopStopwatch()
        {
            this.Stopwatch.Stop();
            this.StopwatchViewMode = StopwatchViewModes.Wait;
        }

        /// <summary>
        /// Сбросить секундомер <see cref="Stopwatch"/>.
        /// </summary>
        private void ResetStopwatch()
        {
            this.Stopwatch.Reset();
        }

        /// <summary>
        /// Начать новый круга на секундомере <see cref="Stopwatch"/>.
        /// </summary>
        private void StartNewLapStopwatch()
        {
            this.Stopwatch.StartNewLap();
        }
        #endregion
    }
}
