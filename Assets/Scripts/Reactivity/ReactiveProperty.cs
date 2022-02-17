using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveProperty<T> : IReactiveProperty<T>, IDisposable
{
    private T _value;
    private IList<IObserver<T>> _observers;

    public T Value 
    { 
        get => _value; 
        
        set
        {
            if (_value.Equals(value))                
            {
                _value = value;

                foreach (var observer in _observers)
                {
                    observer.OnNext(value);
                }
            }            
        }
    }

    public ReactiveProperty(T value)
    {
        _value = value;       
        _observers = new List<IObserver<T>>();
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);        
        return null;
    }

    public void Dispose()
    {                
        _observers.Clear();
    }
}