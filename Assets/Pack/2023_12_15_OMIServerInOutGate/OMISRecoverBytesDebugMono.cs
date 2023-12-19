using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class OMISRecoverBytesDebugMono : MonoBehaviour
{

    public OMIServerObjectEvent m_onConvertedBytesObject;
    public ReceivedByte m_received;
    public ReceivedByteBoolean m_bBytes;
    public ReceivedByteFloat m_fBytes;
    public ReceivedByteVector3 m_vBytes;
    public ReceivedByteQuaternion m_qBytes;


    [System.Serializable]
    public class ReceivedByteBoolean : ReceivedByteGeneric<bool> { }
    [System.Serializable]
    public class ReceivedByteFloat : ReceivedByteGeneric<float> { }
    [System.Serializable]
    public class ReceivedByteVector3 : ReceivedByteGeneric<Vector3> { }
    [System.Serializable]
    public class ReceivedByteQuaternion : ReceivedByteGeneric<Quaternion> { }
    [System.Serializable]
    public class ReceivedByte
    {
        public byte m_fctByte;
        public byte m_b1;
        public byte m_b2;
        public byte m_b3;
        public byte m_b4;
        public char m_charAsString;
        public int m_countBytes;
        public float [] m_allFloat;
    }
    public class ReceivedByteGeneric<T> : ReceivedByte
    {
        public int m_countElements;
        public T[] m_valueArray;
    }


    public void PushIn(byte[] bytes) {
        if (bytes == null)
            return;

        if (bytes.Length <= 5)
            return;
        
        if (bytes[0] == 4) Set(ref bytes, m_bBytes);
        else if (bytes[0] == 5) Set(ref bytes, m_fBytes);
        else if (bytes[0] == 6) Set(ref bytes, m_vBytes);
        else if (bytes[0] == 7) Set(ref bytes, m_qBytes);
        else Set(ref bytes, m_received);


    }

    private void Set(ref byte[] bytes, ReceivedByteFloat received)
    {
        Set(ref bytes, (ReceivedByte)received);
        received.m_valueArray = received.m_allFloat;

        m_onConvertedBytesObject.Invoke(new CharUTFToFloatArray(received.m_charAsString, received.m_valueArray));

    }

    private void Set(ref byte[] bytes, ReceivedByteVector3 received)
    {
        Set(ref bytes, (ReceivedByte)received);
        received.m_valueArray = OMISBytesUtility.
            ConvertToVector3(
            received.m_allFloat);
        m_onConvertedBytesObject.Invoke(new CharUTFToVector3Array(received.m_charAsString, received.m_valueArray));

    }

    private void Set(ref byte[] bytes, ReceivedByteQuaternion received)
    {
        Set(ref bytes, (ReceivedByte)received);
        received.m_valueArray = OMISBytesUtility.
            ConvertToQuaternion(
            received.m_allFloat);
        m_onConvertedBytesObject.Invoke(new CharUTFToQuaternionArray(received.m_charAsString, received.m_valueArray));

    }
    private void Set(ref byte[] bytes, ReceivedByteBoolean received)
    {
        Set(ref bytes, (ReceivedByte)received);
        received.m_valueArray = OMISBytesUtility.
            ConvertToBoolArray(
            ref bytes,5);
        m_onConvertedBytesObject.Invoke(new CharUTFToBoolArray(received.m_charAsString, received.m_valueArray));

    }

    private void Set(ref byte[] bytes, ReceivedByte received)
    {
        received.m_countBytes = bytes.Length;
        received.m_fctByte = bytes[0];
        received.m_b1 = bytes[1];
        received.m_b2 = bytes[2];
        received.m_b3 = bytes[3];
        received.m_b4 = bytes[4];
        received.m_charAsString = ConvertBytesToStringChar(
        bytes[1], bytes[2], bytes[3], bytes[4]);
        received.m_allFloat = OMISBytesUtility.GetFloatInByte(5, ref bytes);

    }

    public char ConvertBytesToStringChar(byte b0, byte b1, byte b2, byte b3)
    {
        byte[] utf8Bytes = { b0, b1, b2, b3 };
        return ConvertToChar(utf8Bytes);


    }
    static char ConvertToChar(byte[] byteArray)
    {
      

        int intValue = BitConverter.ToInt32(byteArray, 0);
        char resultChar = (char)intValue;

        return resultChar;
    }


}
