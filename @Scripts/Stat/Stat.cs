using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    [SerializeField] private float _baseValue;

    public Action<float> ValueChangeEvent;
    public List<float> modifiers;

    public float GetValue()
    {
        float finalValue = _baseValue;
        for (int i = 0; i < modifiers.Count; ++i)
        {
            finalValue += modifiers[i];
        }

        return finalValue;
    }

    public void AddModifier(float value)
    {
        if (value != 0)
        {
            modifiers.Add(value);
            ValueChangeEvent?.Invoke(GetValue());
        }
    }

    public void RemoveModifier(float value)
    {
        if (value != 0)
        {
            modifiers.Remove(value);
            ValueChangeEvent?.Invoke(GetValue());
        }
    }
    public void SetDefaultValue(float value)
    {
        _baseValue = value;
    }
}