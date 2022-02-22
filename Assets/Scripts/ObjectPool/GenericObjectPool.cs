
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{    
    [SerializeField]
    private T prefab;

    [SerializeField]
    private int _count;

    private Queue<T> pooledObjects = new Queue<T>();    
    
    public static GenericObjectPool<T> Instance { get; private set; }

    private void Awake()
    {                
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;

        AddObjects(_count);

        DontDestroyOnLoad(this);
    }
        
    public T Get()
    {        
        var obj = pooledObjects.Dequeue();;
        obj.gameObject.SetActive(true);

        return obj;
    }
    
    public void Release(T objectToSet)
    {
        objectToSet.gameObject.SetActive(false);
        pooledObjects.Enqueue(objectToSet);
    }
    
    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            AddPoolReference(newObject);
            Release(newObject);
        }
    }
    
    public abstract void AddPoolReference(T objectToAddReference);    
}