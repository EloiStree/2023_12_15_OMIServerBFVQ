using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUTFRegistersMono : MonoBehaviour
{
    public CharUTFRegistersBFVQ m_register= new CharUTFRegistersBFVQ();

    public CharUTFRegistersBFVQ R { get { return m_register; } }

}

public class CharUTFRegistersBFVQ 
{

    public Dictionary<string, CharUTFToNamedIndexed> m_charToBooleanArray = new Dictionary<string, CharUTFToNamedIndexed>();
    public Dictionary<string, CharUTFToNamedIndexed> m_charToFloatArray = new Dictionary<string, CharUTFToNamedIndexed>();
    public Dictionary<string, CharUTFToNamedIndexed> m_charToVectorArray = new Dictionary<string, CharUTFToNamedIndexed>();
    public Dictionary<string, CharUTFToNamedIndexed> m_charToQuaternionArray = new Dictionary<string, CharUTFToNamedIndexed>();


    public IEnumerable<CharUTFToNamedIndexed> GetAll(OMIServerPrimitiveType primitiveType)
    {
        switch (primitiveType)
        {
            case OMIServerPrimitiveType.Boolean: return m_charToBooleanArray.Values;
            case OMIServerPrimitiveType.Float: return m_charToFloatArray.Values;
            case OMIServerPrimitiveType.Vector3: return m_charToVectorArray.Values;
            case OMIServerPrimitiveType.Quaternion: return m_charToQuaternionArray.Values;
            default:
                return null;
        }
    }
    public IEnumerable<string> GetAllCharKeys(OMIServerPrimitiveType primitiveType)
    {
        switch (primitiveType)
        {
            case OMIServerPrimitiveType.Boolean: return m_charToBooleanArray.Keys;
            case OMIServerPrimitiveType.Float: return m_charToFloatArray.Keys;
            case OMIServerPrimitiveType.Vector3: return m_charToVectorArray.Keys;
            case OMIServerPrimitiveType.Quaternion: return m_charToQuaternionArray.Keys;
            default:
                return null;
        }
    }

    public void SetOrAdd(OMIServerPrimitiveType primitiveType, CharUTFToNamedIndexed namedIndex) {
        if (primitiveType == OMIServerPrimitiveType.Boolean)
            SetOrAdd(m_charToBooleanArray, namedIndex);
        if (primitiveType == OMIServerPrimitiveType.Float)
            SetOrAdd(m_charToFloatArray, namedIndex);
        if (primitiveType == OMIServerPrimitiveType.Vector3)
            SetOrAdd(m_charToVectorArray, namedIndex);
        if (primitiveType == OMIServerPrimitiveType.Quaternion)
            SetOrAdd(m_charToQuaternionArray, namedIndex);
    }

    private void SetOrAdd(Dictionary<string, CharUTFToNamedIndexed> dictionnary, CharUTFToNamedIndexed namedIndex)
    {
        if (!dictionnary.ContainsKey(namedIndex.m_charAsIndex))
        { dictionnary.Add(namedIndex.m_charAsIndex, namedIndex); }
        else { dictionnary[namedIndex.m_charAsIndex] = namedIndex; }
    }
    private void Get(string lookingFor, Dictionary<string, CharUTFToNamedIndexed> dictionnary,out bool found, out CharUTFToNamedIndexed namedIndex)
    {
        found = dictionnary.ContainsKey(lookingFor);
        if (found)
            namedIndex = dictionnary[lookingFor];
        else namedIndex = null;
        
    }

    public void Get(string lookingFor,  OMIServerPrimitiveType primitiveType,out bool found, out CharUTFToNamedIndexed namedIndex)
    {
        if (primitiveType == OMIServerPrimitiveType.Boolean) {

            Get(lookingFor, m_charToBooleanArray,out  found, out namedIndex);
            return;
        }
        else if (primitiveType == OMIServerPrimitiveType.Float)
        {
            Get(lookingFor, m_charToFloatArray, out found, out namedIndex);
            return;
        }
        else if (primitiveType == OMIServerPrimitiveType.Vector3)
        {

            Get(lookingFor, m_charToVectorArray, out found, out namedIndex);
            return;
        }
        else if (primitiveType == OMIServerPrimitiveType.Quaternion)
        {
            Get(lookingFor, m_charToQuaternionArray, out found, out namedIndex);
            return;
        }
        found = false;
        namedIndex = null;
    }
}
