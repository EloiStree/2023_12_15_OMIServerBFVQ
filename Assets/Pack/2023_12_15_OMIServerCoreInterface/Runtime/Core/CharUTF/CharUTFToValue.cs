using UnityEngine;

[System.Serializable]
public class CharUTFToValue<T> : I_CharUTFToValue<T>, I_SetCharUTFToValue<T> where T : struct
{
    public char m_charAsIndex;
    public T[] m_stringNameValueArray;


    public void GetChatUniqueId(out char charAsString)
    {
        charAsString = m_charAsIndex;
    }

    public char GetChatUniqueId()
    {
        return m_charAsIndex;
    }

    public void GetValueAsArray(out T[] values)
    {
        values = m_stringNameValueArray;
    }

    public T[] GetValueAsArray()
    {
        return m_stringNameValueArray;
    }

    public void SetCharAndArray(char charAsString, T[] array)
    {
        m_charAsIndex = charAsString;
        m_stringNameValueArray = array;
    }
}


public interface I_CharUTFToValue<T> {

    public void GetChatUniqueId(out char charAsString);
    public char GetChatUniqueId();
    public void GetValueAsArray(out T[] values );
    public T[] GetValueAsArray();
}

public interface I_SetCharUTFToValue<T> {
    public void SetCharAndArray(char charAsString, T[] array);
}

public interface I_CharUTFToValueBoolean : I_CharUTFToValue<bool> { }
public interface I_CharUTFToValueFloat : I_CharUTFToValue<float> { }
public interface I_CharUTFToValueVector3 : I_CharUTFToValue<Vector3> { }
public interface I_CharUTFToValueQuaternion : I_CharUTFToValue<Quaternion> { }
public interface I_CharUTFToName : I_CharUTFToValue<string> { }
public interface I_CharUTFToNameDefault <G>: I_CharUTFToName
{
    public void GetDefaultValue(out G value);
    public G GetDefaultValue();
}
public interface I_CharUTFToNameDefaultFloat : I_CharUTFToNameDefault<float> { }
public interface I_CharUTFToNameDefaultBool : I_CharUTFToNameDefault<bool> { }
public interface I_CharUTFToNameDefaultQuaternion : I_CharUTFToNameDefault<Quaternion> { }
public interface I_CharUTFToNameDefaultVector3 : I_CharUTFToNameDefault<Vector3> { }