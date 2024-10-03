using System;
using UnityEngine;

namespace Model.UI
{
    public abstract class UIWindow<T>: MonoBehaviour, IDisposable
    {
        protected T Data;
        public event Action OnHidden;

        public void Setup(T data)
        {
            Data = data;
            OnShow();
        }

        public virtual void OnShow()
        {
            
        }
        
        public virtual void Dispose()
        {
            OnHidden?.Invoke();
            Destroy(gameObject);
        }
    }
}