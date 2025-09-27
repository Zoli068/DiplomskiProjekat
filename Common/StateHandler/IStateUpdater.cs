namespace Common.StateHandler
{
    /// <summary>
    /// Contains a method description that allows us to subscribe to an <see cref="IStateHandler{T}.StateChanged"/> event
    /// </summary>
    public interface IStateChangedHandler
    {
        /// <summary>
        /// The method description that we need to subscribe to an <see cref="IStateHandler{T}.StateChanged"/> event
        /// </summary>
        void StateChangedHandler();
    }
}
