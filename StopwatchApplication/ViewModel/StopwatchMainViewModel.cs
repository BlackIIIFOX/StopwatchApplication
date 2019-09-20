using StopwatchApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopwatchApplication.ViewModel
{
    /// <summary>
    /// ViewModel основного окна приложения.
    /// </summary>
    class StopwatchMainViewModel : ViewModelBase
    {
        public StopwatchMainViewModel()
        {
            StopwatchModel stopwatch = new StopwatchModel();
            stopwatch.Start();
        }
    }
}
