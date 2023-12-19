using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ConvertTextToNamedArraysMono : MonoBehaviour
{

    public ObjectEvent m_onPushObjectEvent;

    [System.Serializable]
    public class ObjectEvent : UnityEvent<object> { }


    public StringEvent m_onDebugPush;
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
    public void Convert(string text) {
        text = text.Trim().ToLower();
        if (text == null)
            return;

        if (text.Length > 5 && text[0] == '~' && text[4] == ' ') {
            ParseAsUTF16(text);
        }
        else if (text.Length > 2 && text[0] == '~' && text[1] == ' ')
        {
            ParseAsUTF16OneByOne(text);
        }
        else if (text.IndexOf("🀲") >= 0 || text.IndexOf("🀸") >= 0) {
            TranslateInDominoBoolSet(ref text);
        }
    }

    private void ParseAsUTF16OneByOne(string text)
    {
        string[] tokens = text.Split(" ");
        if (tokens.Length > 2) {
            if (float.TryParse(tokens[1], out float value))
            {
                for (int i = 2; i < tokens.Length; i++)
                {
                    SetNamedFloatValue o = new SetNamedFloatValue();
                    o.m_data = new NamedFloatValue();
                    o.m_data.SetValue(  value);
                    o.m_data.SetName(tokens[i].Trim().ToLower());
                m_onPushObjectEvent.Invoke(o);
                }
            }
            else if (tokens[1].IndexOf(":") > -1)
            {
                if (IsVector3(tokens[1], out Vector3 v3))
                {
                    
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        SetNamedVector3Value o = new SetNamedVector3Value();
                        o.m_data = new NamedVector3Value();
                        o.m_data.SetValue(v3);
                o.m_data.SetName( tokens[i].Trim().ToLower());
                m_onPushObjectEvent.Invoke(o);
                    }
                }
                else if (IsQuaternion(tokens[1], out Quaternion q4))
                {
                    
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        SetNamedQuaternionValue o = new SetNamedQuaternionValue();
                        o.m_data = new NamedQuaternionValue();
                        o.m_data.SetValue(q4) ;
                        o.m_data.SetName(tokens[i].Trim().ToLower()) ;
                        m_onPushObjectEvent.Invoke(o);
                    }
                }

            }
            else {
                if (IsBooleanValue(tokens[1], out bool valueBool)) { 
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        SetNamedBooleanValue o = new SetNamedBooleanValue();
                        o.m_data = new NamedBooleanValue();
                        o.m_data.SetValue(valueBool)  ;
                        o.m_data.SetName(tokens[i].Trim().ToLower())  ;
                        m_onPushObjectEvent.Invoke(o);
                    }
                }

            }

            //Push("~ True ButtonDemo");
            //Push("~ 0.3 AxisDemo");
            //Push("~ -0.3:0.1:1 VectorDemo");
            //Push("~ -0.3:0.1:0:1 QuaternionDemo");

        }

    }

    string[] domino = new string[] { "🀸", "🀸" };
    private void TranslateInDominoBoolSet(ref string text)
    {

        NamedBooleansChangeRequest changeRequested = new NamedBooleansChangeRequest();
        string [] tokens = text.Split(domino,StringSplitOptions.RemoveEmptyEntries);
        if (text.IndexOf("🀸") > -1)
        {
            if(tokens.Length>=1)
                changeRequested.m_namedBooleanToSetTrue = tokens[0].Split(" ");
            if (tokens.Length >= 2)
                changeRequested.m_namedBooleanToSetFalse = tokens[1].Split(" ");
        }else
        {
            if (tokens.Length >= 1)
                changeRequested.m_namedBooleanToSetFalse = tokens[0].Split(" ");
            if (tokens.Length >= 2)
                changeRequested.m_namedBooleanToSetTrue = tokens[1].Split(" ");
        }
        m_onPushObjectEvent.Invoke(changeRequested);
        PushDebug( JsonUtility.ToJson(changeRequested));

    }

    private void PushDebug(string text)
    {
        m_debug = text;
        m_onDebugPush.Invoke(text);
    }

    private void ParseAsUTF16(string text)
    {
        if (!(text.Length > 5 && text[0] == '~' && text[4] == ' '))
            return;
        char CharUTF = text[1];
        char valueType = text[2];
        char isLabelOrValue = text[3];

        OMIServerPrimitiveType pType = OMIServerPrimitiveType.Boolean;
        OMIServerDataType dType = OMIServerDataType.Name;
        if (valueType == 'b') pType = OMIServerPrimitiveType.Boolean;
        else if (valueType == 'q') pType = OMIServerPrimitiveType.Quaternion;
        else if (valueType == 'f') pType = OMIServerPrimitiveType.Float;
        else if (valueType == 'v') pType = OMIServerPrimitiveType.Vector3;

        if (isLabelOrValue == 'n') {
            dType = OMIServerDataType.Name;
        }
        else if (isLabelOrValue == 'v')
        {
            dType = OMIServerDataType.Value;
        }

        string[] tokens = text.Split(" ");
        Parse(CharUTF, pType, dType, tokens);
        




    }

    private void Parse(char CharUTF,
        OMIServerPrimitiveType primitive,
        OMIServerDataType dataType,
        string[] tokens)
    {
        switch (primitive)
        {
            case OMIServerPrimitiveType.Boolean:
                ParseBool(CharUTF, dataType, tokens);
                break;
            case OMIServerPrimitiveType.Float:
                ParseFloat(CharUTF, dataType, tokens);
                break;
            case OMIServerPrimitiveType.Vector3:
                ParseVector3(CharUTF, dataType, tokens);
                break;
            case OMIServerPrimitiveType.Quaternion:
                ParseQuaternion(CharUTF, dataType, tokens);
                break;
            default:
                break;
        }
    }
    private void ParseBool(char CharUTF,
        OMIServerDataType dataType,
        string[] tokens)
    {
        if (tokens.Length < 2)
            return;

        if (dataType == OMIServerDataType.Name)
        {
            List<string> names = new List<string>();
            bool defaultValue = false;
            if (!IsBooleanValue(tokens[1], out defaultValue)) 
            {
                names.Add(tokens[1]);
            }
            for (int i = 2; i < tokens.Length; i++)
            {
                names.Add(tokens[i]);
            }
            CharUTFToNamedIndexedBool toPush = new CharUTFToNamedIndexedBool();
            toPush.m_defaultValue = defaultValue;
            toPush.m_charAsIndex = CharUTF;
            toPush.m_stringNameArray = names.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
        else {
            List<bool> values = new List<bool>();
            for (int i = 1; i < tokens.Length; i++)
            {
                if (IsBooleanValue(tokens[i], out bool value))
                {
                    values.Add(value);
                }
                else { 
                    values.Add(false);
                }
            }
            CharUTFToBoolArray toPush = new CharUTFToBoolArray();
            toPush.m_charAsIndex = CharUTF;
            toPush.m_stringNameValueArray = values.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
    }
    [TextArea(0,10)]
    public string m_debug="";

    private bool IsBooleanValue(string text, out bool booleanValue)
    {
        booleanValue = false;
        bool isTrue = text == "true" || text == "t" || text == "1" || text == "v" || text == "_";
        bool isFalse = text == "false" || text == "f" || text == "0" || text == "x" || text == "-";
        if (isTrue) booleanValue = true;
        if (isFalse) booleanValue = false;

        return isTrue || isFalse;            
    }
    private bool IsTrue(string text)
    {
        return text == "1" || text == "true" || text == "t" || text == "v";
    }

    private void ParseFloat(char CharUTF,
        OMIServerDataType dataType,
        string[] tokens)
    {
        if (tokens.Length < 2)
            return;

        if (dataType == OMIServerDataType.Name)
        {
            List<string> names = new List<string>();
            float defaultValue = 0;
            if (!float.TryParse(tokens[1], out defaultValue))
            {
                names.Add(tokens[1]);
            }
            for (int i = 2; i < tokens.Length; i++)
            {
                names.Add(tokens[i]);
            }
            CharUTFToNamedIndexedFloat toPush = new CharUTFToNamedIndexedFloat();
            toPush.m_defaultValue = defaultValue;
            toPush.m_charAsIndex = CharUTF;
            toPush.m_stringNameArray = names.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
        else
        {
            List<float> values = new List<float>();
            for (int i = 1; i < tokens.Length; i++)
            {
                if (float.TryParse(tokens[i], out float value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Add(0);
                }
            }
            CharUTFToFloatArray toPush = new CharUTFToFloatArray();
            toPush.m_charAsIndex =  CharUTF;
            toPush.m_stringNameValueArray = values.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
    }
    private void ParseQuaternion(char CharUTF,
        OMIServerDataType dataType,
        string[] tokens)
    {
        if (tokens.Length < 2)
            return;

        if (dataType == OMIServerDataType.Name)
        {
            List<string> names = new List<string>();
            Quaternion defaultValue = Quaternion.identity;
            if (!IsQuaternion(tokens[1], out defaultValue))
            {
                names.Add(tokens[1]);
            }
            for (int i = 2; i < tokens.Length; i++)
            {
                names.Add(tokens[i]);
            }
            CharUTFToNamedIndexedQuaternion toPush = new CharUTFToNamedIndexedQuaternion();
            toPush.m_defaultValue = defaultValue;
            toPush.m_charAsIndex =  CharUTF;
            toPush.m_stringNameArray = names.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
        else
        {
            List<Quaternion> values = new List<Quaternion>();
            for (int i = 1; i < tokens.Length; i++)
            {
                if (IsQuaternion(tokens[i], out Quaternion value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Add(Quaternion.identity);
                }
            }
            CharUTFToQuaternionArray toPush = new CharUTFToQuaternionArray();
            toPush.m_charAsIndex =  CharUTF;
            toPush.m_stringNameValueArray = values.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
    }

    
    private void ParseVector3(char CharUTF,
        OMIServerDataType dataType,
        string[] tokens)
    {
        if (tokens.Length < 2)
            return;

        if (dataType == OMIServerDataType.Name)
        {
            List<string> names = new List<string>();
            Vector3 defaultValue = Vector3.zero;
            if (!IsVector3(tokens[1], out defaultValue))
            {
                names.Add(tokens[1]);
            }
            for (int i = 2; i < tokens.Length; i++)
            {
                names.Add(tokens[i]);
            }
            CharUTFToNamedIndexedVector3 toPush = new CharUTFToNamedIndexedVector3();
            toPush.m_defaultValue = defaultValue;
            toPush.m_charAsIndex =  CharUTF;
            toPush.m_stringNameArray = names.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
        else
        {
            List<Vector3> values = new List<Vector3>();
            for (int i = 1; i < tokens.Length; i++)
            {
                if (IsVector3(tokens[i], out Vector3 value))
                {
                    values.Add(value);
                }
                else
                {
                    values.Add(Vector3.zero);
                }
            }
            CharUTFToVector3Array toPush = new CharUTFToVector3Array();
            toPush.m_charAsIndex =  CharUTF;
            toPush.m_stringNameValueArray = values.ToArray();
            m_onPushObjectEvent.Invoke(toPush);
            PushDebug(JsonUtility.ToJson(toPush));
        }
    }

    private bool IsVector3(string text, out Vector3 value)
    {
        value = Vector3.zero;
        string[] tokens = text
               .Split(':');
        if (tokens.Length == 3) { 
            float.TryParse(tokens[0], out value.x);
            float.TryParse(tokens[1], out value.y);
            float.TryParse(tokens[2], out value.z);
        }

        return tokens.Length == 3;

    }
    private bool IsQuaternion(string text, out Quaternion value)
    {
        value = Quaternion.identity;
        string[] tokens = text
               .Split(':');
        if (tokens.Length == 4)
        {
            float.TryParse(tokens[0], out value.x);
            float.TryParse(tokens[1], out value.y);
            float.TryParse(tokens[2], out value.z);
            float.TryParse(tokens[3], out value.w);
        }

        return tokens.Length == 4;

    }


}
