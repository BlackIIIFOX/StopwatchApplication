using HandControl.Services;
using StopwatchApplication.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static StopwatchApplication.Model.StopwatchModel;

namespace StopwatchApplication.ViewModel
{
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
        }
        #endregion

        #region Properties
        public StopwatchModel Stopwatch { get; private set; }

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
        #endregion

        #region Methods
        /// <summary>
        /// Запустить секундомер <see cref="Stopwatch"/>.
        /// </summary>
        private void StartStopwatch()
        {
            this.Stopwatch.Start();
        }

        /// <summary>
        /// Остановить секундомер <see cref="Stopwatch"/>.
        /// </summary>
        private void StopStopwatch()
        {
            this.Stopwatch.Stop();
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
