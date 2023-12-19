using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharUTFToNamedIndexed: I_CharUTFToValueName
{
    public string m_charAsIndex="";
    public string[] m_stringNameArray= new string[0];

    public CharUTFToNamedIndexed()
    {
    }

    public CharUTFToNamedIndexed(string charAsIndex, params string[] stringNameArray)
    {
        m_charAsIndex = charAsIndex;
        m_stringNameArray = stringNameArray;
    }

    public void GetChatUniqueId(out string charAsString)
    {
        charAsString = m_charAsIndex;
    }

    public string GetChatUniqueId()
    {
        return m_charAsIndex;
    }

    public void GetValueAsArray(out string[] values)
    {
        values = m_stringNameArray;
    }

    public string[] GetValueAsArray()
    {
        return m_stringNameArray;
    }
}
[System.Serializable]
public class CharUTFToNamedIndexedWithDefault<T>
{
    public string m_charAsIndex="";
    public string[] m_stringNameArray= new string[0];
    public T m_defaultValue;

    public CharUTFToNamedIndexed GetWithoutDefaultValue()
    {
        return new CharUTFToNamedIndexed(m_charAsIndex, m_stringNameArray);
    }
}

[System.Serializable]
public class CharUTFToNamedIndexedEvent : UnityEvent<CharUTFToNamedIndexed> { }


[System.Serializable]
public class CharUTFToNamedIndexedBool        : CharUTFToNamedIndexedWithDefault<bool> { }

[System.Serializable]
public class CharUTFToNamedIndexedFloat       : CharUTFToNamedIndexedWithDefault<float> { }

[System.Serializable]
public class CharUTFToNamedIndexedQuaternion : CharUTFToNamedIndexedWithDefault<Quaternion>
{
   
}

[System.Serializable]
public class CharUTFToNamedIndexedVector3     : CharUTFToNamedIndexedWithDefault<Vector3> { }


[System.Serializable]
public class CharUTFToNamedIndexedBoolEvent : UnityEvent<CharUTFToNamedIndexedBool> { }

[System.Serializable]
public class CharUTFToNamedIndexedFloatEvent : UnityEvent<CharUTFToNamedIndexedFloat> { }

[System.Serializable]
public class CharUTFToNamedIndexedQuaternionEvent : UnityEvent<CharUTFToNamedIndexed> { }

[System.Serializable]
public class CharUTFToNamedIndexedVector3Event : UnityEvent <CharUTFToNamedIndexedVector3> { }