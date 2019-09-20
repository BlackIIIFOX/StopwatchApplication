namespace HandControl.Services
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Имплементация интерфейса <see cref="ICommand"/> для осуществляения биндинга команд из View.
    /// </summary>
    internal class RelayCommand : ICommand
    {
        #region Fields
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        #endregion

        #region Constructor
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }
        #endregion

        #region Properties
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._execute(parameter ?? "<N/A>");
        }
        #endregion
    }
}
