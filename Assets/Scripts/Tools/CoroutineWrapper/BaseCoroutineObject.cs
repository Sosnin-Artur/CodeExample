using System;
using System.Collections;
using UnityEngine;

public abstract class BaseCoroutineObject : IDisposable
{
    public Coroutine Coroutine { get; protected set; }
    public MonoBehaviour Owner { get; private set; }    
    public Func<IEnumerator> Routine { get; private set; }

    public BaseCoroutineObject(
        MonoBehaviour owner,         
        Func<IEnumerator> routine)
    {
        Owner = owner;        
        Routine = routine;
    }

    public abstract void Start();    

    public bool IsProcessing => Coroutine != null;

    public abstract void Stop();

    public void Dispose()
    {
        Stop();
    }
}