using System;
using System.Collections.Generic;

namespace Common.StateHandler
{
    /// <summary>
    /// Class with implemented <see cref="IStateHandler{T}"/>, used for managing a state typeof:<see cref="T"/>, and also provides event if the state changed
    /// </summary>
    public class StateHandler<T> : IStateHandler<T>
    {
        public T state;
        public event Action StateChanged;

        /// <summary>
        /// Method to change the managed state
        /// </summary>
        /// <param name="newState">The new value for the state</param>
        public void ChangeState(T newState)
        {
            if (!EqualityComparer<T>.Default.Equals(state, newState))
            {
                state = newState;

                if (StateChanged != null)
                {
                    StateChanged();
                }
            }
        }

        public T State
        {
            get
            {
                return state;
            }
        }
    }
}
