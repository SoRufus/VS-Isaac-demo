namespace Model.Entities.States
{
    public abstract class State
    {
        public abstract void OnApplied();
        public abstract void OnDisposed();
    }
}