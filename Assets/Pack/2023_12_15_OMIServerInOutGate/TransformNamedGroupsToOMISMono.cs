using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformNamedGroupsToOMISMono : MonoBehaviour, I_GetPrimitiveArrayToPush
{
    public string m_charUniqueid="$";
    public TransformNamedGroupsOMISMono m_source;
    public bool m_pushPositions=true;
    public bool m_pushRotations=false;
    public Transform m_useRoot;

    public void GetArray(out bool[] values, out string[] namedId)
    {values = null; namedId = null;}

    public void GetArray(out float[] values, out string[] namedId)
    {values = null; namedId = null;}

    public void GetArray(out Vector3[] values, out string[] namedId)
    {
        if (!m_pushPositions)
        {
            values = null; namedId = null;
        }
        else { 
            values = m_source.m_allAsArray.Select(k => m_useRoot==null?k.GetWorldPosition():k.GetWorldPositionFromRoot(m_useRoot)).ToArray();
            namedId = m_source.m_allAsArray.Select(k => k.m_named).ToArray();
        }
    }

    public void GetArray(out Quaternion[] values, out string[] namedId)
    {
        if (!m_pushRotations)
        {
            values = null; namedId = null;
        }
        else
        {
            values = m_source.m_allAsArray.Select(k => m_useRoot == null ? k.GetWorldRotation() : k.GetWorldRotationFromRoot(m_useRoot)).ToArray();
            namedId = m_source.m_allAsArray.Select(k => k.m_named).ToArray();
        }
    }

    public string GetUniqueCharId()
    {
        return m_charUniqueid;
    }
}
