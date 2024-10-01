namespace Model.Entities.States
{
    //wip
    public abstract class State
    {
        public abstract void OnApplied();
        public abstract void OnDisposed();
    }
}