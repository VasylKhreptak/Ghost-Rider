using System;
using System.Collections.Generic;
using UnityEngine;

public class ObservableList<T> : List<T>
{
    public event Action<T> onAdd;
    public event Action<T> onRemove;
    public event Action onChanged;
    public event Action onClear;

    public new void Add(T item)
    {
        onAdd?.Invoke(item);
        onChanged?.Invoke();

        Debug.Log("Add");

        base.Add(item);
    }

    public new void Clear()
    {
        onClear?.Invoke();
        onChanged?.Invoke();

        Debug.Log("Clear");

        base.Clear();
    }

    public new bool Remove(T item)
    {
        onRemove?.Invoke(item);
        onChanged?.Invoke();

        Debug.Log("Remove");

        return base.Remove(item);
    }
}
