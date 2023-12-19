using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class NamedRegisterBFVQMono : MonoBehaviour {

    public NamedRegisterBFVQ m_registers = new NamedRegisterBFVQ();

    public NamedRegisterBFVQ R { get { return m_registers; } }
    public NamedRegisterBFVQ GetRegisters(){ return m_registers; } 
}

public class NamedRegisterBFVQ 
{
    NamedRegisterBool m_registerBool = new();
    NamedRegisterFloat m_registerFloat = new();
    NamedRegisterVector3 m_registerVector3 = new();
    NamedRegisterQuaternion m_registerQuaternion = new();

    public NamedRegisterBool B { get { return m_registerBool; } }
    public NamedRegisterFloat F { get { return m_registerFloat; } }
    public NamedRegisterVector3 V { get { return m_registerVector3; } }
    public NamedRegisterQuaternion Q { get { return m_registerQuaternion; } }

    public void SetOrAdd(string namedVariable, bool value) =>
        m_registerBool.SetOrAddInRegister(namedVariable, value);
    public void SetOrAdd(string namedVariable, float value) =>
        m_registerFloat.SetOrAddInRegister(namedVariable, value);
    public void SetOrAdd(string namedVariable, Vector3 value) =>
        m_registerVector3.SetOrAddInRegister(namedVariable, value);
    public void SetOrAdd(string namedVariable, Quaternion value) =>
        m_registerQuaternion.SetOrAddInRegister(namedVariable, value);

    public void Get(string namedVariable,out bool found, out bool value) =>
       m_registerBool.GetInRegister(namedVariable,out found,out value);
    public void Get(string namedVariable, out bool found, out float value) =>
        m_registerFloat.GetInRegister(namedVariable, out found, out value);

    public string[] GetAllKeys(OMIServerPrimitiveType type)
    {
        switch (type)
        {
            case OMIServerPrimitiveType.Boolean:return m_registerBool.m_keys.ToArray();
            case OMIServerPrimitiveType.Float: return m_registerFloat.m_keys.ToArray();
            case OMIServerPrimitiveType.Vector3: return m_registerVector3.m_keys.ToArray();
            case OMIServerPrimitiveType.Quaternion: return m_registerQuaternion.m_keys.ToArray();
            default:
                return new string[0];
                break;
        }
    }

    public NativeArray<bool> GetAllBooleanValue()
    { return m_registerBool.m_values; }
    public NativeArray<float> GetAllFloatValue()
    { return m_registerFloat.m_values; }
    public NativeArray<Vector3> GetAllVector3Value()
    { return m_registerVector3.m_values; }
    public NativeArray<Quaternion> GetAllQuaternionValue()
    { return m_registerQuaternion.m_values; }

    public void Get(string namedVariable, out bool found, out Vector3 value) =>
        m_registerVector3.GetInRegister(namedVariable, out found, out value);
    public void Get(string namedVariable, out bool found, out Quaternion value) =>
        m_registerQuaternion.GetInRegister(namedVariable, out found, out value);

    public void SetIfNotExisting(string namedVariable, Vector3 value)
        => m_registerVector3.SetIfNotExisting(namedVariable, value);
    public void SetIfNotExisting(string namedVariable, Quaternion value)
        => m_registerQuaternion.SetIfNotExisting(namedVariable, value);
    public void SetIfNotExisting(string namedVariable, bool value)
        => m_registerBool.SetIfNotExisting(namedVariable, value);
    public void SetIfNotExisting(string namedVariable, float value)
        => m_registerFloat.SetIfNotExisting(namedVariable, value);


    public void SetIfNotExisting(OMIServerPrimitiveType type, string namedVariable)
    {
        if (type == OMIServerPrimitiveType.Boolean)
            m_registerBool.SetIfNotExisting(namedVariable);
        if (type == OMIServerPrimitiveType.Float)
            m_registerFloat.SetIfNotExisting(namedVariable);
        if (type == OMIServerPrimitiveType.Vector3)
            m_registerVector3.SetIfNotExisting(namedVariable);
        if (type == OMIServerPrimitiveType.Quaternion)
            m_registerQuaternion.SetIfNotExisting(namedVariable);
    }
  

}
