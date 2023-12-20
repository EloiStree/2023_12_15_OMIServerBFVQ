using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OMISV3QCubeDebugMono : MonoBehaviour
{
    public Transform m_root;
    public GameObject m_prefab;


    public Dictionary<string, Transform> m_keyToCube = new Dictionary<string, Transform>();

    public void Push(string name, Vector3 localPosition)
    {
        CreateifNotExisting(name);
        m_keyToCube[name].localPosition =localPosition;

    }
    public void Push(string name, Quaternion localRotation)
    {
        CreateifNotExisting(name);
        m_keyToCube[name].localRotation = localRotation;
    }

    private void CreateifNotExisting(string name)
    {
        if (!m_keyToCube.ContainsKey(name))
        {
            GameObject gamo = GameObject.Instantiate(m_prefab);
            gamo.transform.parent = m_root;
            gamo.name = "C:"+name;
            m_keyToCube.Add(name, gamo.transform);
        }
    }

}
