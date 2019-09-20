using System.Windows;
using StopwatchApplication.ViewModel;

namespace StopwatchApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class StopwatchMainView : Window
    {
        public StopwatchMainView()
        {
            this.DataContext = new StopwatchMainViewModel();
            InitializeComponent();
        }
    }
}
