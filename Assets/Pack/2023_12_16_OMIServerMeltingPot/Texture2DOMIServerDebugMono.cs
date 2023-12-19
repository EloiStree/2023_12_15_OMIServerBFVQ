using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Texture2DOMIServerDebugMono : MonoBehaviour
{
    public CharUTFRegistersMono m_charRegister;
    public NamedRegisterBFVQMono m_registersBFVQ;

    public Texture2DEvent m_onBooleanTexture;
    public Texture2DEvent m_onFloatTexture;
    public Texture2DEvent m_onVector3Texture;
    public Texture2DEvent m_onQuaternionTexture;

    [System.Serializable]
    public class Texture2DEvent : UnityEvent<Texture2D> { }

    public Texture2D m_booleanTexture;
    public Texture2D m_floatTexture;
    public Texture2D m_vector3Texture;
    public Texture2D m_quaternionTexture;

    public bool m_autoRefresh;
    public float m_timeBetween = 0.5f;
    private void Start()
    {
        if (m_autoRefresh) 
            InvokeRepeating("Refresh", m_timeBetween, m_timeBetween);
    }

    public bool mipmap;
    public bool linear;

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        if (m_booleanTexture==null || m_registersBFVQ.R.B.V.Length != m_booleanTexture.width)
        {
            m_booleanTexture = new Texture2D(m_registersBFVQ.R.B.V.Length, 1,TextureFormat.ARGB32, mipmap, linear);
            m_booleanTexture.filterMode = FilterMode.Point;
            m_onBooleanTexture.Invoke(m_booleanTexture);
        }
        if (m_floatTexture == null || m_registersBFVQ.R.F.V.Length != m_floatTexture.width)
        {
            m_floatTexture = new Texture2D(m_registersBFVQ.R.F.V.Length, 1, TextureFormat.ARGB32, mipmap, linear);
            m_floatTexture.filterMode = FilterMode.Point;
            m_onFloatTexture.Invoke(m_floatTexture);
        }
        if (m_vector3Texture == null || m_registersBFVQ.R.V.V.Length != m_vector3Texture.width)
        {
            m_vector3Texture = new Texture2D(m_registersBFVQ.R.V.V.Length, 4, TextureFormat.ARGB32, mipmap, linear);
            m_vector3Texture.filterMode = FilterMode.Point;
            m_onVector3Texture.Invoke(m_vector3Texture);
        }
        if (m_quaternionTexture == null || m_registersBFVQ.R.Q.V.Length != m_quaternionTexture.width)
        {
            m_quaternionTexture = new Texture2D(m_registersBFVQ.R.Q.V.Length,5, TextureFormat.ARGB32, mipmap, linear);
            m_quaternionTexture.filterMode = FilterMode.Point;
            m_onQuaternionTexture.Invoke(m_quaternionTexture);
        }
        NativeArray<bool> vb = m_registersBFVQ.R.B.V;
        NativeArray<float> vf = m_registersBFVQ.R.F.V;
        NativeArray<Vector3> vv = m_registersBFVQ.R.V.V;
        NativeArray<Quaternion> vq = m_registersBFVQ.R.Q.V;
        for (int i = 0; i < vb.Length; i++) { 
            m_booleanTexture.SetPixel(i, 0, vb[i] ? Color.green : Color.red);
        }
        for (int i = 0; i < vf.Length; i++) { 
            m_floatTexture.SetPixel(i, 0, (vf[i] > 0f ? Color.green : Color.red) * Mathf.Abs (vf[i]));
        }
        for (int i = 0; i < vv.Length; i++)
        {
            float max = vv[i].x;
            if (vv[i].y > max) max = vv[i].y;
            if (vv[i].z > max) max = vv[i].z;
            m_vector3Texture.SetPixel(i, 0, (vv[i].x > 0f ? Color.green : Color.red) * (Mathf.Abs(vv[i].x)/ max));
            m_vector3Texture.SetPixel(i, 1, (vv[i].y > 0f ? Color.green : Color.red) * (Mathf.Abs(vv[i].y)/ max));
            m_vector3Texture.SetPixel(i, 2, (vv[i].z > 0f ? Color.green : Color.red) * (Mathf.Abs(vv[i].z)/ max));
            m_vector3Texture.SetPixel(i, 3, new Color((vv[i].x / max), (vv[i].y / max), (Mathf.Abs(vv[i].z) / max)));
        }
        for (int i = 0; i < vq.Length; i++) {
            m_quaternionTexture.SetPixel(i, 0,(vq[i].x > 0f ? Color.green:Color.red) * (Mathf.Abs(vq[i].x )));
            m_quaternionTexture.SetPixel(i, 1, (vq[i].y > 0f ? Color.green : Color.red) * (Mathf.Abs(vq[i].y) ));
            m_quaternionTexture.SetPixel(i, 2, (vq[i].z > 0f ? Color.green : Color.red) * (Mathf.Abs(vq[i].z) ));
            m_quaternionTexture.SetPixel(i, 3, (vq[i].w > 0f ? Color.green : Color.red) * (Mathf.Abs(vq[i].w )));
            m_quaternionTexture.SetPixel(i, 4, new Color((vq[i].x ), (vq[i].y ), (vq[i].z ), (Mathf.Abs(vq[i].w) )));
        }

        m_booleanTexture.Apply();
        m_floatTexture.Apply();
        m_vector3Texture.Apply();
        m_quaternionTexture.Apply();
    }

}
