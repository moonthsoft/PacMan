using System.Collections.Generic;
using UnityEngine;

namespace Moonthsoft.Core.FSM
{
    public interface IState<T> where T : class
    {
        public virtual void Update() { }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public abstract void Init(T entity);
    }
}