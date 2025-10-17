using System;

public interface IReactiveProperty<T> : IObservable<T>
{
    public T Value { get; set; }
}