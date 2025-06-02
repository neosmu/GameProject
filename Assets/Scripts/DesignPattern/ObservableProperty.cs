using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObservableProperty<T>
{
    [SerializeField] private T value;
    public T Value
    {
        get => value;
        set
        {
            if (Equals(this.value, value)) return;
            this.value = value;
            Notify();
        }
    }
    private UnityEvent<T> onValueChanged = new();

    public ObservableProperty(T value = default)
    {
        this.value = value;
    }

    public void Subscribe(UnityAction<T> action)
    {
        onValueChanged.AddListener(action);
    }

    public void Unsubscribe(UnityAction<T> action)
    {
        onValueChanged.RemoveListener(action);
    }

    public void UnsubscribeAll()
    {
        onValueChanged.RemoveAllListeners();
    }

    private void Notify()
    {
        onValueChanged?.Invoke(Value);
    }
}