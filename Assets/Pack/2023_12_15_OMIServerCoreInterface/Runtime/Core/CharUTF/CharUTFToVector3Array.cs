using UnityEngine;

[System.Serializable]
public class CharUTFToVector3Array : CharUTFToValue<Vector3>, I_CharUTFToValueVector3
{
    public CharUTFToVector3Array(char c, Vector3[] array)
    {
        SetCharAndArray(c, array);
    }
    public CharUTFToVector3Array()
    {
        SetCharAndArray(' ', new Vector3[0]);
    }
}
