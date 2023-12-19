[System.Serializable]
public class NamedBooleanValue : A_GetSetNamedValue<bool>
{
    public NamedBooleanValue()
    {
    }
    public NamedBooleanValue(string name, bool value)
    {
        SetNameAndValue(name, value);
    }


}


