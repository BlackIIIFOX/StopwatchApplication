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
        /// ��������� ��������� ���������� � ��������� � 100 ��.
        /// </summary>
        /// <param name="timeMs1">����� ����� ����������.</param>
        /// <param name="timeMs2">� ����� �������� ����������.</param>
        /// <returns>���� ������� ����� � ��������� � 100��, �� ������������ true, ����� false.</returns>
        private bool MillisCompare(double timeMs1, double timeMs2)
        {
            return Math.Abs(timeMs1 - timeMs2) < 100;
        }

        /// <summary>
        /// ���� ���������� ����������� ��������� �������.
        /// ���� ������� ���������� ����� ���� ������� 23650 ���������� ��������� �������.
        /// �� ��������� ������������� ������� ����������� ��������� �����������.
        /// ����� ��������� ��� ����� ������ ��������� � ��������.
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
        /// ���� ������ �����������.
        /// ���� ������� ���������� � ��������� ���, ����� ���� ������������� � ��������� �����.
        /// ����� ������ ����� ����������� � ���-�� ������ ������ ���� ����� 0.
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
        /// ���� ������������ ������� ����� ��������� �������.
        /// ������� ����������, ��������� ���, ������� �������� ����� � �������� ����� ����. ����� ���� ������������� ����������.
        /// ����� ����������� ����� ����������� ������ ��������� � �������� �������, ������� ������ �� ������ �����������.
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
        /// ���� ����������� ���-�� ������ ��������� ���-�� ������ ������ �����.
        /// ������� ���������� � ��������� 5 ������.
        /// ���������� ������ ����������� ������ ���� ����� 5.
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
        /// ���� ����� ������������.
        /// ��������� ����������, ������� ����� �� ����� � ������������� ���. ����� ���� ������� ��� ����� �� �����.
        /// ����� �������� ����� ��������� ������ ��������������� ������� �����������, ����� ������ �� ��������� ��������� ���������.
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
        /// ���� ������������� �����������.
        /// ��������� ����������, ����� ���� ���� ��� ���� ������ ������� ����� �� ����� � ������������� ����������.
        /// ����� ��������� ����� ����������� ������ �������� ����� ������ ��������, ����� ������������� �������� �����������.
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
