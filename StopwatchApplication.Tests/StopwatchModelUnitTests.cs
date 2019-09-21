using System;
using System.Threading;
using System.Windows;
using StopwatchApplication.Model;
using Xunit;

namespace StopwatchApplication.Tests
{
    public class StopwatchModelUnitTests
    {
        /// <summary>
        /// Выполняет сравнения милисекунд с точностью в 100 мс.
        /// </summary>
        /// <param name="timeMs1">Какое время сравнивают.</param>
        /// <param name="timeMs2">С каким временем сравнивают.</param>
        /// <returns>Если времена равны с точностью в 100мс, то возвращается true, иначе false.</returns>
        private bool MillisCompare(double timeMs1, double timeMs2)
        {
            return Math.Abs(timeMs1 - timeMs2) < 100;
        }

        /// <summary>
        /// Тест соответвия секундомера реальному времени.
        /// Тест создает секундомер после чего ожидает 23650 милисекунд реального времени.
        /// По истечению обозначенного времени выполняется остановка секундомера.
        /// После остановки его время должно совпадать с исходным.
        /// </summary>
        [Fact]
        public void StartStopwatchTest_CreateStopwathAndWaitTime_StopwathTimeAndWaitTimeShouldBeEquals()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();
            int timeWaitMs = 23650;

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMs);
            stopwatch.Stop();

            // Assert
            Assert.True(MillisCompare(stopwatch.TotalStopwatchTime.TotalMilliseconds, timeWaitMs));
        }

        /// <summary>
        /// Тест сброса секундомера.
        /// Тест создает секундомер и запускает его, после чего останавливает и выполняет сброс.
        /// После сброса время секундомера и кол-во кругов должны быть равны 0.
        /// </summary>
        [Fact]
        public void ResetTest_CreateStopwathAfterStartAndStop_TimeFromStartAndCountLapsShouldBeZero()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();

            // Act
            stopwatch.Start();
            Thread.Sleep(100);
            stopwatch.Stop();
            stopwatch.Reset();

            // Assert
            Assert.Equal(stopwatch.TotalStopwatchTime, TimeSpan.Zero);
            Assert.Equal(0, stopwatch.CountLaps);
        }

        /// <summary>
        /// Тест соответсвтия времени круга реальному времени.
        /// Создает секундомер, запускает его, ожидает заданное время и начинает новый круг. После чего останавливает секундомер.
        /// Время предыдущего круга секундомера должно совпадать с реальным времени, которое прошло со старта секундомера.
        /// </summary>
        [Fact]
        public void StartNewLapTest_CreateStopwatchStartAndStartNewLap_TimerOldLapShouldBeSameAsRealTime()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();
            int timeWaitMs = 2000;

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMs);
            stopwatch.StartNewLap();
            Thread.Sleep(timeWaitMs);
            stopwatch.Stop();

            // Assert
            Assert.True(MillisCompare(stopwatch.TimeOfEachLap[stopwatch.TimeOfEachLap.Count - 1].LapTime.TotalMilliseconds, timeWaitMs));
        }

        /// <summary>
        /// Тест соответсвия кол-ва кругов реальному кол-ву вызова нового круга.
        /// Создает секундомер и запускает 5 кругов.
        /// Количество кругов секундомера должно быть равно 5.
        /// </summary>
        [Fact]
        public void CorrespondenceLapsToRealAmountTest_CreateStopwatchStartAndStartFiveNewLap_CountLapsSouldBeRealAmount()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();
            int realAmount = 5;

            // Act
            stopwatch.Start();
            for (int i = 0; i < realAmount; i++)
            {
                stopwatch.StartNewLap();
            }
            stopwatch.Stop();

            // Assert
            Assert.Equal(stopwatch.CountLaps, realAmount);
        }

        /// <summary>
        /// Тест паузы секундномера.
        /// Запускает секундомер, ожидает какое то время и останавливает его. После чего ожидает еще какое то время.
        /// После ожидания время остановки должно соответствовать времени секундомера, иначе таймер не корректно выполняет остановку.
        /// </summary>
        [Fact]
        public void StopwatchPauseTest_CreateStopwatchAfterStartAndStopWithDelay_AfterPauseTimeShouldNotChanged()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();
            int timeWaitMs = 2000;
            double concreteTimeStop = 0;

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMs);
            stopwatch.Stop();
            concreteTimeStop = stopwatch.TotalStopwatchTime.TotalMilliseconds;
            Thread.Sleep(timeWaitMs);

            // Assert
            Assert.True(MillisCompare(concreteTimeStop, stopwatch.TotalStopwatchTime.TotalMilliseconds));
        }

        /// <summary>
        /// Тест возобнавления секундомера.
        /// Запускает секундомер, после чего тест два раза подряд ожидает какое то время и останавливает секундомер.
        /// После остановки время секундомера должно равнятся сумме времен ожиданий, иначе возобновление работает некорректно.
        /// </summary>
        [Fact]
        public void StopwatchResumeTest_CreateStopwatchAfterrStartAndStopWithDelayOfTwoTimes_AfterStopTimeShouldBeEqualSummDelay()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();
            int timeWaitMsFirst = 1200, timeWaitMsSecond = 600;

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMsFirst);
            stopwatch.Stop();
            Thread.Sleep(timeWaitMsSecond);
            stopwatch.Start();
            Thread.Sleep(timeWaitMsSecond);
            stopwatch.Stop();

            // Assert
            Assert.True(this.MillisCompare(timeWaitMsFirst + timeWaitMsSecond, stopwatch.TotalStopwatchTime.TotalMilliseconds));
        }
    }
}
