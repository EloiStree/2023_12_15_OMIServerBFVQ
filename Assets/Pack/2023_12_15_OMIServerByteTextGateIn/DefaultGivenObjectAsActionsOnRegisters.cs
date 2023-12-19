using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultGivenObjectAsActionsOnRegisters : MonoBehaviour
{
    public CharUTFRegistersMono m_charRegister;
    public NamedRegisterBFVQMono m_registersBFVQ;

    public void ParseToActionObject(OMIServerPrimitiveType primitiveType, object givenObject)
    {
      if (givenObject is CharUTFToNamedIndexed)
        {
            ParseToAction(primitiveType, (CharUTFToNamedIndexed)givenObject);
        }

    }

        

    public void ParseToActionObject(object givenObject)
    {





         if (givenObject is I_CharUTFToNameDefaultBool)
        {
            ParseToAction((I_CharUTFToNameDefaultBool)givenObject);
        }
        else if (givenObject is I_CharUTFToNameDefaultFloat)
        {
            ParseToAction((I_CharUTFToNameDefaultFloat)givenObject);
        }
        else if (givenObject is I_CharUTFToNameDefaultVector3)
        {
            ParseToAction((I_CharUTFToNameDefaultVector3)givenObject);
        }
        else if (givenObject is I_CharUTFToNameDefaultQuaternion)
        {
            ParseToAction((I_CharUTFToNameDefaultQuaternion)givenObject);
        }


        else if (givenObject is I_CharUTFToValueFloat)
        {
            ParseToAction((I_CharUTFToValueFloat)givenObject);
        }
        else if (givenObject is I_CharUTFToValueVector3)
        {
            ParseToAction((I_CharUTFToValueVector3)givenObject);
        }
        else if (givenObject is I_CharUTFToValueQuaternion)
        {
            ParseToAction((I_CharUTFToValueQuaternion)givenObject);
        }
        else if (givenObject is I_CharUTFToValueBoolean)
        {
            ParseToAction((I_CharUTFToValueBoolean)givenObject);
        }


        else if (givenObject is NamedBooleanValue)
        {
            ParseToAction((NamedBooleanValue)givenObject);
        }
        else if (givenObject is NamedFloatValue)
        {
            ParseToAction((NamedFloatValue)givenObject);
        }
        else if (givenObject is NamedQuaternionValue)
        {

            ParseToAction((NamedQuaternionValue)givenObject);
        }
        else if (givenObject is NamedVector3Value)
        {
            ParseToAction((NamedVector3Value)givenObject);
        }


        else if (givenObject is SetNamedBooleanValue)
        {
            ParseToAction((SetNamedBooleanValue)givenObject);
        }
        else if (givenObject is SetNamedFloatValue)
        {
            ParseToAction((SetNamedFloatValue)givenObject);
        }
        else if (givenObject is SetNamedVector3Value)
        {
            ParseToAction((SetNamedVector3Value)givenObject);
        }
        else if (givenObject is SetNamedQuaternionValue)
        {
            ParseToAction((SetNamedQuaternionValue)givenObject);
        }


        else if (givenObject is SetIfNotExistingNamedBooleanValue)
        {
            ParseToAction((SetIfNotExistingNamedBooleanValue)givenObject);
        }
        else if (givenObject is SetIfNotExistingNamedFloatValue)
        {
            ParseToAction((SetIfNotExistingNamedFloatValue)givenObject);
        }
        else if (givenObject is SetIfNotExistingNamedVector3Value)
        {
            ParseToAction((SetIfNotExistingNamedVector3Value)givenObject);
        }
        else if (givenObject is SetIfNotExistingNamedQuaternionValue)
        {
            ParseToAction((SetIfNotExistingNamedQuaternionValue)givenObject);
        }

        else if (givenObject is CharUTFToNamedIndexedBool)
        {
            ParseToAction((CharUTFToNamedIndexedBool)givenObject);
        }
        else if (givenObject is CharUTFToNamedIndexedFloat)
        {
            ParseToAction((CharUTFToNamedIndexedFloat)givenObject);
        }
        else if (givenObject is CharUTFToNamedIndexedVector3)
        {
            ParseToAction((CharUTFToNamedIndexedVector3)givenObject);
        }
        else if (givenObject is CharUTFToNamedIndexedQuaternion)
        {
            ParseToAction((CharUTFToNamedIndexedQuaternion)givenObject);
        }



        else if (givenObject is CharUTFToBoolArray)
        {
            ParseToAction((CharUTFToBoolArray)givenObject);
        }
        else if (givenObject is CharUTFToFloatArray)
        {
            ParseToAction((CharUTFToFloatArray)givenObject);
        }
        else if (givenObject is CharUTFToVector3Array)
        {
            ParseToAction((CharUTFToVector3Array)givenObject);
        }
        else if (givenObject is CharUTFToQuaternionArray)
        {
            ParseToAction((CharUTFToQuaternionArray)givenObject);
        }
        else if (givenObject is NamedBooleansChangeRequest)
        {
            ParseToAction((NamedBooleansChangeRequest)givenObject);
        }



        else
        {

            Debug.Log("Not take in charge:" + givenObject);
        }
    }


    private void ParseToAction(NamedBooleansChangeRequest givenObject)
    {
        foreach (var item in givenObject.m_namedBooleanToSetTrue)
            m_registersBFVQ.R.SetOrAdd(item.Trim().ToLower(), true);
        foreach (var item in givenObject.m_namedBooleanToSetFalse)
            m_registersBFVQ.R.SetOrAdd(item.Trim().ToLower(), false);

    }

    private void ParseToAction(OMIServerPrimitiveType primitiveType, CharUTFToNamedIndexed givenObject)
    {
        
            for (int i = 0; i < givenObject.m_stringNameArray.Length; i++)
                m_registersBFVQ.R.SetIfNotExisting(primitiveType, givenObject.m_stringNameArray[i]);
        
    }

    private void ParseToAction(CharUTFToQuaternionArray givenObject)
    {
        m_charRegister.R.Get(givenObject.m_charAsIndex, OMIServerPrimitiveType.Quaternion,
               out bool found, out CharUTFToNamedIndexed index);
        if (found)
        {
            for (int i = 0; i < index.m_stringNameArray.Length && i < givenObject.m_stringNameValueArray.Length; i++)
            {
                m_registersBFVQ.R.SetOrAdd(index.m_stringNameArray[i], givenObject.m_stringNameValueArray[i]);
            }
        }
    }
    private void ParseToAction(CharUTFToVector3Array givenObject)
    {
        m_charRegister.R.Get(givenObject.m_charAsIndex, OMIServerPrimitiveType.Vector3,
               out bool found, out CharUTFToNamedIndexed index);
        if (found)
        {
            for (int i = 0; i < index.m_stringNameArray.Length && i < givenObject.m_stringNameValueArray.Length; i++)
            {
                m_registersBFVQ.R.SetOrAdd(index.m_stringNameArray[i], givenObject.m_stringNameValueArray[i]);
            }
        }
    }

    private void ParseToAction(CharUTFToFloatArray givenObject)
    {
        m_charRegister.R.Get(givenObject.m_charAsIndex, OMIServerPrimitiveType.Float,
               out bool found, out CharUTFToNamedIndexed index);
        if (found)
        {
            for (int i = 0; i < index.m_stringNameArray.Length && i < givenObject.m_stringNameValueArray.Length; i++)
            {
                m_registersBFVQ.R.SetOrAdd(index.m_stringNameArray[i], givenObject.m_stringNameValueArray[i]);
            }
        }
    }

    private void ParseToAction(CharUTFToBoolArray givenObject)
    {
        m_charRegister.R.Get(givenObject.m_charAsIndex, OMIServerPrimitiveType.Boolean,
               out bool found, out CharUTFToNamedIndexed index);
        if (found)
        {
            for (int i = 0; i < index.m_stringNameArray.Length && i < givenObject.m_stringNameValueArray.Length; i++)
            {
                m_registersBFVQ.R.SetOrAdd(index.m_stringNameArray[i], givenObject.m_stringNameValueArray[i]);
            }
        }
    }


    private void ParseToAction(CharUTFToNamedIndexedQuaternion givenObject)
    {
        m_charRegister.R.SetOrAdd(OMIServerPrimitiveType.Quaternion, givenObject.GetWithoutDefaultValue());
        for (int i = 0; i < givenObject.m_stringNameArray.Length; i++)
            m_registersBFVQ.R.SetIfNotExisting(givenObject.m_stringNameArray[i], givenObject.m_defaultValue);

    }

    private void ParseToAction(CharUTFToNamedIndexedVector3 givenObject)
    {
        m_charRegister.R.SetOrAdd(OMIServerPrimitiveType.Vector3, givenObject.GetWithoutDefaultValue());
        for (int i = 0; i < givenObject.m_stringNameArray.Length; i++)
            m_registersBFVQ.R.SetIfNotExisting(givenObject.m_stringNameArray[i], givenObject.m_defaultValue);
    }

    private void ParseToAction(CharUTFToNamedIndexedFloat givenObject)
    {
        m_charRegister.R.SetOrAdd(OMIServerPrimitiveType.Float, givenObject.GetWithoutDefaultValue());
        for (int i = 0; i < givenObject.m_stringNameArray.Length; i++)
            m_registersBFVQ.R.SetIfNotExisting(givenObject.m_stringNameArray[i], givenObject.m_defaultValue);
    }

    private void ParseToAction(CharUTFToNamedIndexedBool givenObject)
    {
        m_charRegister.R.SetOrAdd(OMIServerPrimitiveType.Boolean, givenObject.GetWithoutDefaultValue());
        for (int i = 0; i < givenObject.m_stringNameArray.Length; i++)
            m_registersBFVQ.R.SetIfNotExisting(givenObject.m_stringNameArray[i], givenObject.m_defaultValue);
    }



    private void ParseToAction(I_CharUTFToValueBoolean givenObject)
    {
        
        givenObject.GetChatUniqueId(out char c);
        m_charRegister.R.Get(c, OMIServerPrimitiveType.Boolean, out bool found, out CharUTFToNamedIndexed utfList);

        if (found)
        {
            int i = 0;
            foreach (var item in givenObject.GetValueAsArray())
            {
                if (i < utfList.GetLenght())
                    m_registersBFVQ.R.SetOrAdd(utfList.GetFromIndex(i), item);
                i++;
            }
        }
        
    }

    private void ParseToAction(I_CharUTFToValueQuaternion givenObject)
    {
        givenObject.GetChatUniqueId(out char c);
        m_charRegister.R.Get(c, OMIServerPrimitiveType.Quaternion, out bool found, out CharUTFToNamedIndexed utfList);
        if (found)
        {
            int i = 0;
            foreach (var item in givenObject.GetValueAsArray())
            {
                if (i < utfList.GetLenght())
                    m_registersBFVQ.R.SetOrAdd(utfList.GetFromIndex(i), item);
                i++;
            }
        }
    }

    private void ParseToAction(I_CharUTFToValueVector3 givenObject)
    {
        givenObject.GetChatUniqueId(out char c);
        m_charRegister.R.Get(c, OMIServerPrimitiveType.Vector3, out bool found, out CharUTFToNamedIndexed utfList);
        if (found)
        {
            int i = 0;
            foreach (var item in givenObject.GetValueAsArray())
            {
                if(i< utfList.GetLenght())
                    m_registersBFVQ.R.SetOrAdd(utfList.GetFromIndex(i), item);
                i++;
            }
        }
    }

    private void ParseToAction(I_CharUTFToValueFloat givenObject)
    {
        givenObject.GetChatUniqueId(out char c);
        m_charRegister.R.Get(c, OMIServerPrimitiveType.Float, out bool found, out CharUTFToNamedIndexed utfList);
        if (found)
        {
            int i = 0;
            foreach (var item in givenObject.GetValueAsArray())
            {
                if (i < utfList.GetLenght())
                    m_registersBFVQ.R.SetOrAdd(utfList.GetFromIndex(i), item);
                i++;
            }
        }

    }

    private void ParseToAction(I_CharUTFToNameDefaultFloat givenObject)
    {
        m_charRegister.R.SetOrAdd(givenObject);
        foreach (var item in givenObject.GetValueAsArray())
        {
            m_registersBFVQ.R.SetIfNotExisting(item, givenObject.GetDefaultValue());
        }
    }
    private void ParseToAction(I_CharUTFToNameDefaultBool givenObject)
    {
        m_charRegister.R.SetOrAdd(givenObject);
        foreach (var item in givenObject.GetValueAsArray())
        {
            m_registersBFVQ.R.SetIfNotExisting(item, givenObject.GetDefaultValue());
        }

    }
    private void ParseToAction(I_CharUTFToNameDefaultVector3 givenObject)
    {
        m_charRegister.R.SetOrAdd(givenObject);
        foreach (var item in givenObject.GetValueAsArray())
        {
            m_registersBFVQ.R.SetIfNotExisting(item, givenObject.GetDefaultValue());
        }
    }
    private void ParseToAction(I_CharUTFToNameDefaultQuaternion givenObject)
    {
        m_charRegister.R.SetOrAdd(givenObject);
        foreach (var item in givenObject.GetValueAsArray())
        {
            m_registersBFVQ.R.SetIfNotExisting(item, givenObject.GetDefaultValue());
        }
    }

    private void ParseToAction(SetNamedQuaternionValue value)
    {
        m_registersBFVQ.R.Q.SetOrAddInRegister(value.m_data.GetName(), value.m_data.GetValue());

    }

    private void ParseToAction(SetNamedVector3Value value)
    {
        m_registersBFVQ.R.V.SetOrAddInRegister(value.m_data.GetName(), value.m_data.GetValue());

    }

    private void ParseToAction(SetNamedFloatValue value)
    {
        m_registersBFVQ.R.F.SetOrAddInRegister(value.m_data.GetName(), value.m_data.GetValue());

    }
    private void ParseToAction(SetNamedBooleanValue value)
    {
        m_registersBFVQ.R.B.SetOrAddInRegister(value.m_data.GetName(), value.m_data.GetValue());
    }



    private void ParseToAction(SetIfNotExistingNamedQuaternionValue value)
    {
        m_registersBFVQ.R.Q.SetIfNotExisting(value.m_data.GetName(), value.m_data.GetValue());

    }

    private void ParseToAction(SetIfNotExistingNamedVector3Value value)
    {
        m_registersBFVQ.R.V.SetIfNotExisting(value.m_data.GetName(), value.m_data.GetValue());

    }

    private void ParseToAction(SetIfNotExistingNamedFloatValue value)
    {
        m_registersBFVQ.R.F.SetIfNotExisting(value.m_data.GetName(), value.m_data.GetValue());

    }

    private void ParseToAction(SetIfNotExistingNamedBooleanValue value)
    {
        m_registersBFVQ.R.B.SetIfNotExisting(value.m_data.GetName(), value.m_data.GetValue());

    }

    private void ParseToAction(NamedVector3Value value)
    {
        m_registersBFVQ.R.V.SetOrAddInRegister(value.GetName(), value.GetValue());
    }

    private void ParseToAction(NamedQuaternionValue value)
    {
        m_registersBFVQ.R.Q.SetOrAddInRegister(value.GetName(), value.GetValue());
    }

    private void ParseToAction(NamedFloatValue value)
    {
        m_registersBFVQ.R.F.SetOrAddInRegister(value.GetName(), value.GetValue());
    }

    private void ParseToAction(NamedBooleanValue value)
    {
        m_registersBFVQ.R.B.SetOrAddInRegister(value.GetName(), value.GetValue());
    }
}
