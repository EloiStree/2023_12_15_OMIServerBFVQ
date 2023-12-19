using UnityEngine;

[System.Serializable]
public class NamedFloatValue : A_GetSetNamedValue<float>
{

    public NamedFloatValue()
    {
    }
    public NamedFloatValue(string name, float value)
    {
        SetNameAndValue(name, value);
    }
}


public interface I_GetSetNamedValue<T> : I_GetNamedValue<T>, I_SetNamedValue<T>
{ }

public interface I_GetNamedValue<T>
{

    public void GetName(out string name);
    public string GetName();
    public void GetValue(out T value);
    public T GetValue();

}
public interface I_SetNamedValue<T>
{

    public void SetNameAndValue( string name, T value);
    public void SetName( string name);
    public void SetValue( T value);

}

public interface I_GetSetNamedFloatValue : I_GetSetNamedValue<float> { }
public interface I_GetNamedFloatValue : I_GetNamedValue<float> { }

public interface I_GetSetNamedBoolValue : I_GetSetNamedValue<bool> { }
public interface I_GetNamedBoolValue : I_GetNamedValue<bool> { }

public interface I_GetSetNamedQuaternionValue : I_GetSetNamedValue<Quaternion> { }
public interface I_GetNamedQuaternionValue : I_GetNamedValue<Quaternion> { }

public interface I_GetSetNamedVector3Value : I_GetSetNamedValue<Vector3> { }
public interface I_GetNamedVector3Value : I_GetNamedValue<Vector3> { }


public abstract class A_GetNamedValueParams<T>
{
    [SerializeField] protected string m_name;
    [SerializeField] protected T m_value;
    public A_GetNamedValueParams(string name, T value)
    {
        m_name = name;
        m_value = value;
    }

    public A_GetNamedValueParams()
    {
    }
}



public abstract class A_GetNamedValue<T> : A_GetNamedValueParams<T>, I_GetNamedValue<T>
{

    public void GetName(out string name)
    {
        name = m_name;
    }

    public string GetName()
    {
        return m_name;
    }

    public void GetValue(out T value)
    {
        value = m_value;
    }

    public T GetValue()
    {
        return m_value;
    }
}
public abstract class A_GetSetNamedValue<T> : A_GetNamedValueParams<T>, I_GetSetNamedValue<T>
{
    public void GetName(out string name)
    {
        name = m_name;
    }

    public string GetName()
    {
        return m_name;
    }

    public void GetValue(out T value)
    {
        value = m_value;
    }

    public T GetValue()
    {
        return m_value;
    }

    public void SetName(string name)
    {
        m_name = name;
    }

    public void SetNameAndValue(string name, T value)
    {
        SetName(name);
        SetValue(value);
    }

    public void SetValue(T value)
    {
        m_value = value;
    }
}
