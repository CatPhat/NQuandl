using System;
using System.Threading.Tasks;

namespace NQuandl.Api.Quandl
{
    public interface IRateGate
    {
        /// <summary>
        ///     Number of occurrences allowed per unit of time.
        /// </summary>
        int Occurrences { get; }

        /// <summary>
        ///     The length of the time unit, in milliseconds.
        /// </summary>
        int TimeUnitMilliseconds { get; }

        /// <summary>
        ///     Releases unmanaged resources held by an instance of this class.
        /// </summary>
        void Dispose();

        /// <summary>
        ///     Blocks the current thread until allowed to proceed or until the
        ///     specified timeout elapses.
        /// </summary>
        /// <param name="millisecondsTimeout">Number of milliseconds to wait, or -1 to wait indefinitely.</param>
        /// <returns>true if the thread is allowed to proceed, or false if timed out</returns>
        bool WaitToProceed(int millisecondsTimeout);

        /// <summary>
        ///     Blocks the current thread until allowed to proceed or until the
        ///     specified timeout elapses.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>true if the thread is allowed to proceed, or false if timed out</returns>
        bool WaitToProceed(TimeSpan timeout);

        /// <summary>
        ///     Blocks the current thread indefinitely until allowed to proceed.
        /// </summary>
        void WaitToProceed();

        Task WaitToProceedAsync();
    }
}