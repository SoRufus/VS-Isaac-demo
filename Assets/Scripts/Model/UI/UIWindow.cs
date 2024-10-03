using System;
using UnityEngine;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;

namespace Model.UI
{
    public abstract class UIWindow<T>: MonoBehaviour, IDisposable
    {
        protected T Data;
        public event Action OnHidden;

        public void Setup(T data)
        {
            Data = data;
        }

        public virtual void Dispose()
        {
            OnHidden?.Invoke();
        }
    }
}