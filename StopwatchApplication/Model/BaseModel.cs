using System.ComponentModel;

namespace StopwatchApplication.Model
{
    /// <summary>
    /// BaseModel - базовая имплементация компонента Model паттерна MVVM.
    /// Класс содержит в себе имплементацию интерфейса INotifyPropertyChanged для оповещения об изменениях.
    /// Все Model должны насследоваться от этого класса.
    /// \brief базовая имплементация компонента Model паттерна MVVM.
    /// \version 1.0
    /// \date Апрель 2019 года
    /// \authors Paul Rozhkin(blackiiifox@gmail.com)
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
#pragma warning disable
        /// <summary>
        /// Имплементация интерфейса INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
