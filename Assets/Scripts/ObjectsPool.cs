using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T> : IEnumerable<T> where T : MonoBehaviour
{
    private LinkedList<T> pool;

    public ObjectsPool(IEnumerable<T> collection = null)
    {
        pool = new LinkedList<T>();

        if (collection != null)
        {
            foreach (T el in collection)
            {
                pool.AddFirst(el);
            }
        }
    }

    public T Pop()
    {
        T last;

        if (pool.Last != null)
        {
            last = pool.Last.Value;
            pool.RemoveLast();
            return last;
        }
        else
        {
            return null;
        }
    }

    public void Push(T obj)
    {
        pool.AddFirst(obj);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return pool.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
