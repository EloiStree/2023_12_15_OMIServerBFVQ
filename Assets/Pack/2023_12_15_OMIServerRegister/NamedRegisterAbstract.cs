using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NamedRegisterAbstract<T> where T :struct
{

    public NativeArray<T> m_values= new NativeArray<T>();
    public List<string> m_keys = new List<string>();
    public Dictionary<string, int> m_nameToIndex= new Dictionary<string, int>();
    public T m_notFoundValue;
    public T m_defaultValueIfNotExisting;

    public NativeArray<T> V { get { return m_values; } }
    public List<string> K { get { return m_keys; } }

    public void SetIfNotExisting(string namedVariable)
    {
        SetIfNotExisting(namedVariable, m_defaultValueIfNotExisting);
    }
    public void SetIfNotExisting(string name, T value)
    {
        name = name.ToLower(); 
        if (IsNameExist(name))
        {
            return;
        }
        SetOrAddInRegister(name, value);

    }
    public void SetOrAddNameInRegister(string name)
    {
        name = name.ToLower();
        if (IsNameExist(name))
        {
            return;
        }
        m_keys.Add(name);
        int index = m_keys.Count - 1;
        m_nameToIndex.Add(name, index);
        ValueMustBeEqualsToKeys();
    }

    public void SetOrAddInRegister(string name, T value)
    {
        name = name.ToLower();
        SetOrAddNameInRegister(name);
        m_values[ m_nameToIndex [name] ] = value;
    }


    public void GetInRegister(string name, out bool found, out T value)
    {
        name = name.ToLower();
        found = m_nameToIndex.ContainsKey(name);
        if (!found)
        {
            value = m_notFoundValue;
        }
        else
        {
            value = m_values[ m_nameToIndex[name]];
        }
    }

    private void ValueMustBeEqualsToKeys()
    {
        if (m_values.Length != m_keys.Count) {
            NativeArray<T> previous = m_values;
            m_values = new NativeArray<T>(m_keys.Count, Allocator.Persistent, NativeArrayOptions.ClearMemory);

            if (previous != null) { 
                for (int i = 0; i < previous.Length && i < m_values.Length; i++)
                { m_values[i] = previous[i]; }
                previous.Dispose();
            }
        }
    }

    private bool IsNameExist(string name)
    {
        return m_nameToIndex.ContainsKey(name);
    }
}
