namespace TrualityEngine.Core
{
    /// <summary>
    /// If object implements this interface, object can be returned by yield in <see cref="Coroutine"/>
    /// </summary>
    public interface ICoroutineable
    {
        /// <summary>
        /// Returns true if coroutine have finished
        /// </summary>
        /// <returns></returns>
        bool GetIsDone();

    }
}
