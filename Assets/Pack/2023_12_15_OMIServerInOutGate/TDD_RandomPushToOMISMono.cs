using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TDD_RandomPushToOMISMono : MonoBehaviour, I_GetPrimitiveArrayToPush
{
    public string m_charUniqueid="ù";
    public TDD_RandomPushOMISMono m_source;

    public float m_timeBetweenRandomizing=1;


    private void Start()
    {
        InvokeRepeating("InvokeRandomize", m_timeBetweenRandomizing, m_timeBetweenRandomizing);
    }
    public void InvokeRandomize() {
        m_source.RandomizeAll();
    }
    public void GetArray(out bool[] values, out string[] namedId)
    {
        values = m_source.m_boolValues.Select(k=>k.m_value).ToArray(); 
        namedId = m_source.m_boolValues.Select(k => k.m_name).ToArray();
    }

    public void GetArray(out float[] values, out string[] namedId)
    {
        values = m_source.m_floatValues.Select(k => k.m_value).ToArray();
        namedId = m_source.m_floatValues.Select(k => k.m_name).ToArray();
    }

    public void GetArray(out Vector3[] values, out string[] namedId)
    {
        values = m_source.m_vector3Values.Select(k => k.m_value).ToArray();
        namedId = m_source.m_vector3Values.Select(k => k.m_name).ToArray();
    }

    public void GetArray(out Quaternion[] values, out string[] namedId)
    {
        values = m_source.m_quaternionValues.Select(k => k.m_value).ToArray();
        namedId = m_source.m_quaternionValues.Select(k => k.m_name).ToArray();
    }

    public string GetUniqueCharId()
    {
        return m_charUniqueid;
    }
}
