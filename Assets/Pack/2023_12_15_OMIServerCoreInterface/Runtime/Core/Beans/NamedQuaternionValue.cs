using UnityEngine;

[System.Serializable]
public class NamedQuaternionValue : A_GetSetNamedValue<Quaternion>
{
    public NamedQuaternionValue()
    {
    }
    public NamedQuaternionValue(string name, Quaternion value)
    {
        SetNameAndValue(name, value);
    }
}
