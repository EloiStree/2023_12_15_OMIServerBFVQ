using UnityEngine;

[System.Serializable]
public class NamedVector3Value : A_GetSetNamedValue<Vector3>
{
    public NamedVector3Value()
    {
    }
    public NamedVector3Value(string name, Vector3 value)
    {
        SetNameAndValue(name, value);
    }
}
