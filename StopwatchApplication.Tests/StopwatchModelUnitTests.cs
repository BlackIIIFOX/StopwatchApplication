using System;
using System.Threading;
using System.Windows;
using StopwatchApplication.Model;
using Xunit;

namespace StopwatchApplication.Tests
{
    public class StopwatchModelUnitTests
    {
        private readonly int increment = 50;

        /// <summary>
        /// ����������� ����� �������� �� <see cref="increment"/>.
        /// ������������� ����������, �.�. ���������� �� �������� ��������������.
        /// </summary>
        /// <param name="delay">������� ����� ��������.</param>
        /// <returns>����� �������� � �����������.</returns>
        private int IncrementDelay(int delay)
        {
            return delay + increment;
        }

        /// <summary>
        /// ��������� ����� �������� �� <see cref="increment"/>.
        /// �������� ��������� ��� ���������� ������������������� ������� �������� � �������� �������.
        /// </summary>
        /// <param name="delay">������������������ �����.</param>
        /// <returns>������� �����.</returns>
        private int DecrementDelay(int delay)
        {
            return delay - increment;
        }

        /// <summary>
        /// ���� ���������� ����������� ��������� �������.
        /// ���� ������� ���������� ����� ���� ������� ��������� ���-�� ���������� ��������� ������� �� 10000 �� 20000.
        /// �� ��������� ������������� ������� ����������� ��������� �����������.
        /// ����� ��������� ��� ����� ������ ��������� � ��������.
        /// </summary>
        [Fact]
        public void StartStopwatchTest_CreateStopwathAndWaitTime_StopwathTimeAndWaitTimeShouldBeEquals()
        {
            // Arranges
            StopwatchModel stopwatch = new StopwatchModel();
            int timeWaitMs = IncrementDelay(5400);

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMs);
            stopwatch.Stop();

            // Assert
            Assert.Equal(stopwatch.TotalStopwatchTime.TotalMilliseconds, DecrementDelay(timeWaitMs));
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
            int timeWaitMs = this.IncrementDelay(2000);
            
            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMs);
            stopwatch.StartNewLap();
            Thread.Sleep(timeWaitMs);
            stopwatch.Stop();

            // Assert
            Assert.Equal(stopwatch.TimeOfEachLap[stopwatch.TimeOfEachLap.Count - 1].TotalMilliseconds, this.DecrementDelay(timeWaitMs));
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
            int timeWaitMs = this.IncrementDelay(2000);
            double concreteTimeStop = 0;

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMs);
            stopwatch.Stop();
            concreteTimeStop = stopwatch.TotalStopwatchTime.TotalMilliseconds;
            Thread.Sleep(timeWaitMs);
            
            // Assert
            Assert.True(concreteTimeStop.Equals(stopwatch.TotalStopwatchTime.TotalMilliseconds));
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
            int timeWaitMsFirst = this.IncrementDelay(1200), timeWaitMsSecond = this.IncrementDelay(600);

            // Act
            stopwatch.Start();
            Thread.Sleep(timeWaitMsFirst);
            stopwatch.Stop();
            Thread.Sleep(timeWaitMsSecond);
            stopwatch.Start();
            Thread.Sleep(timeWaitMsSecond);
            stopwatch.Stop();

            // Assert
            Assert.Equal(this.DecrementDelay(timeWaitMsFirst) + this.DecrementDelay(timeWaitMsSecond), stopwatch.TotalStopwatchTime.TotalMilliseconds);
        }
    }
}
