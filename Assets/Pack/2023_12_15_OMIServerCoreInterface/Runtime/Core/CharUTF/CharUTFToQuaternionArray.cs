using UnityEngine;

[System.Serializable]
public class CharUTFToQuaternionArray : CharUTFToValue<Quaternion>, I_CharUTFToValueQuaternion
{
    public CharUTFToQuaternionArray(char c, Quaternion[] array)
    {
        SetCharAndArray(c, array);
    }
    public CharUTFToQuaternionArray()
    {
        SetCharAndArray(' ', new Quaternion[0]);
    }
}
