using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class OMISPushFromInterfaceMono : MonoBehaviour
{
    public GameObject m_source;

    public TextEvent m_pushLabel;
    public ByteEvent m_pushBytes;


    [System.Serializable]
    public class TextEvent : UnityEvent<string> { }
    [System.Serializable]
    public class ByteEvent : UnityEvent<byte[]> { }

    public UnityEvent m_onStartPush;

    public int m_textBoolCharCount = 0;
    public int m_textFloatCharCount = 0;
    public int m_textVectorCharCount = 0;
    public int m_textQuaternoinCharCount = 0;
    public int m_boolByteCount = 0;
    public int m_floatByteCount = 0;
    public int m_vectorByteCount = 0;
    public int m_quaternoinByteCount = 0;



    public bool m_useStartToPush=true;
    public bool m_useUpdateToPush = true;

    private void Start()
    {
        if (!m_useStartToPush)
            return;
        if (m_source == null)
            return;
        I_GetPrimitiveArrayToPush i = m_source.GetComponent<I_GetPrimitiveArrayToPush>();
        PushNames(i);
        m_onStartPush.Invoke();
    }

    [ContextMenu("Push names")]
    public void PushNames()
    {
        if (m_source == null)
            return;
        I_GetPrimitiveArrayToPush i = m_source.GetComponent<I_GetPrimitiveArrayToPush>();
        PushNames(i);
    }
    [ContextMenu("Push Values")]
    public void PushValues()
    {
        if (m_source == null)
            return;
        I_GetPrimitiveArrayToPush i = m_source.GetComponent<I_GetPrimitiveArrayToPush>();
        PushValues(i);
    }

    void Update()
    {
        if (!m_useUpdateToPush)
            return;
        if (m_source == null)
            return;
        I_GetPrimitiveArrayToPush i = m_source.GetComponent<I_GetPrimitiveArrayToPush>();
        PushValues(i);

    }

    private string m_bnt;
    private string m_fnt;
    private string m_vnt;
    private string m_qnt;
    private byte[] m_bnb;
    private byte[] m_fnb;
    private byte[] m_vnb;
    private byte[] m_qnb;



    private  void PushNames(I_GetPrimitiveArrayToPush i)
    {
        if (i != null)
        {

            string c = i.GetUniqueCharId();
            i.GetArray(out bool[]       b         , out string[] bn);
            i.GetArray(out float[]      f        , out string[] fn);
            i.GetArray(out Vector3[]    v      , out string[] vn);
            i.GetArray(out Quaternion[] q   , out string[] qn);


            if (bn != null && bn.Length > 0)
            {
                m_bnt = "~" + c + "BN " + string.Join(" ", bn);
                m_textBoolCharCount = m_bnt.Length;
                PushName(m_bnt);
            }
            if (fn != null && fn.Length > 0)
            {
                m_fnt = "~" + c + "FN " + string.Join(" ", fn);
                m_textFloatCharCount = m_fnt.Length;
                PushName(m_fnt);
            }
            if (vn != null && vn.Length > 0)
            {
                m_vnt = "~" + c + "VN " + string.Join(" ", vn);
                m_textVectorCharCount = m_vnt.Length;
                PushName(m_vnt);
            }
            if (qn != null && qn.Length > 0)
            {
                m_qnt = "~" + c + "QN " + string.Join(" ", qn);
                m_textQuaternoinCharCount = m_qnt.Length;
                PushName(m_qnt);
            }

        }
    }

    private  void PushValues(I_GetPrimitiveArrayToPush i)
    {
        if (i != null)
        {
            char c= i.GetUniqueCharId()[0];
            i.GetArray(out bool[] b, out string[] bn);
            i.GetArray(out float[] f, out string[] fn);
            i.GetArray(out Vector3[] v, out string[] vn);
            i.GetArray(out Quaternion[] q, out string[] qn);

            if (bn != null && bn.Length > 0)
            {
                m_bnb =Append(4,c, OMISBytesUtility.ConvertBoolArrayToByteArray(b));
                m_boolByteCount         = m_bnb.Length;
                PushBytes(m_bnb);
            }
            if (fn != null && fn.Length > 0)
            {
                m_fnb =Append(5,c, OMISBytesUtility.ConvertFloatToBytes(f));
                m_floatByteCount        = m_fnb.Length;
                PushBytes(m_fnb);
            }
            if (vn != null && vn.Length > 0)
            {
                m_vnb =Append(6,c, OMISBytesUtility.ConvertVectorToBytes(v));
                m_vectorByteCount       = m_vnb.Length;
                PushBytes(m_vnb);
            }
            if (qn != null && qn.Length > 0)
            {
                m_qnb =Append(7, c, OMISBytesUtility.ConvertQuaternionToBytes(q));
                m_quaternoinByteCount   = m_qnb.Length;
                PushBytes(m_qnb);
            }
        }
    }


    public void PushName(string text)
    {
        m_pushLabel.Invoke(text);
    }
    public void PushBytes(byte[] bytes)
    {
        m_pushBytes.Invoke(bytes);
    }


    public static byte[] Append(byte idOfBytes, char givenCharId, byte[] bytes) {

        byte[] byteArray = OMISBytesUtility.ConvertCharToByteArray(givenCharId);
        byte[] result = new byte[bytes.Length + 5];
        result[0] = idOfBytes;
        for (int i = 0; i < byteArray.Length; i++)
        {
            result[1 + i] = byteArray[i];

        }
        for (int i = 0; i < bytes.Length; i++)
        {
            result[5 + i] = bytes[i];
        }
        return result;

    }


}
