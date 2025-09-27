using System;

namespace Common.StateHandler
{
    /// <summary>
    /// Describes all the neccessary things for implementing a <typeparamref name="T"/> type state, and to trigger an event if <see cref="ChangeState(T)"/> happens
    /// </summary>
    /// <typeparam name="T">The type of the state</typeparam>
    public interface IStateHandler<T>
    {
        /// <summary>
        /// Event object with <see cref="Action"/> delegate, this will be triggered when <see cref="ChangeState(T)"/> got executed
        /// </summary>
        event Action StateChanged;

        /// <summary>
        /// Method to change the managed state
        /// </summary>
        /// <param name="newState">The new value for the state</param>
        void ChangeState(T newState);

        /// <summary>
        /// The managed state
        /// </summary>
        T State { get; }
    }
}
