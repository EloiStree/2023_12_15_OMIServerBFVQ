using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedRegisterBFVQToCubeDebugMono : MonoBehaviour
{
    public NamedRegisterBFVQMono m_register;
    public OMISV3QCubeDebugMono m_cubeToMove;


    void Update()
    {
        foreach (var key in m_register.R.V.K)
        {
            m_register.R.V.GetInRegister(key, out bool found, out Vector3 v);
            if (found)
                m_cubeToMove.Push(key, v);
        }
        foreach (var key in m_register.R.Q.K)
        {
            m_register.R.Q.GetInRegister(key, out bool found, out Quaternion v);
            if (found)
                m_cubeToMove.Push(key, v);
        }

    }
}
