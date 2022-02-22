using UnityEngine;

public interface IObjectInPool<T> where T : Component
{
    GenericObjectPool<T> Pool { get; set; }
}
