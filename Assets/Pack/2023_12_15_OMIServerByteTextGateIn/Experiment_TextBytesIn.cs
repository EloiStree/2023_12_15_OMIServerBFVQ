using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Experiment_TextBytesIn : MonoBehaviour
{

    public int m_lastIndexReceived;
    public string m_lastIndexString;

    public void TranslateByteToClass(byte [] givenBytes) {
        if (givenBytes.Length > 5)
        {
            ToCharArray(out byte id, out string c, givenBytes[0]
                , givenBytes[1], givenBytes[2], givenBytes[3], givenBytes[4]);
            if (givenBytes[0] == 5) ConvertToArrayValueBoolean(ref givenBytes);
            if (givenBytes[0] == 6) ConvertToArrayValueFloat(ref givenBytes);
            if (givenBytes[0] == 7) ConvertToArrayValueVector(ref givenBytes);
            if (givenBytes[0] == 8) ConvertToArrayValueQuaternion(ref givenBytes);
        }

    }


    public static void  ToCharArray(out byte fctIndex, out string charUTF8, byte b0, byte b1, byte b2, byte b3, byte b4)
    {
        fctIndex = b0;
        charUTF8= Encoding.UTF8.GetString(new byte[] { b1, b2, b3, b4 });
    }

    private void ConvertToArrayValueBoolean(ref byte[] givenBytes)
    {
        //Bytes 1           = What follow
        //Bytes 2,3,4,5     = Char as UTF16
    }
    private void ConvertToArrayValueFloat(ref byte[] givenBytes)
    {
        //Bytes 1           = What follow
        //Bytes 2,3,4,5     = Char as UTF16
    }
    private void ConvertToArrayValueVector(ref byte[] givenBytes)
    {
        throw new NotImplementedException();
    }
    private void ConvertToArrayValueQuaternion(ref byte[] givenBytes)
    {
        throw new NotImplementedException();
    }

    private void GetFloatFromBytes(ref byte[] givenBytes, int startIndex4Bytes, out float result)
    {
        byte[] floatBytes = { givenBytes[startIndex4Bytes], givenBytes[startIndex4Bytes + 1], givenBytes[startIndex4Bytes + 2], givenBytes[startIndex4Bytes + 3] };
        result = BitConverter.ToSingle(floatBytes, 0);
    }
    private void GetUTF16CharFromBytes(ref byte[] givenBytes, int startIndex4Bytes, out string result)
    {
        byte[] floatBytes = { 
            givenBytes[startIndex4Bytes],
            givenBytes[startIndex4Bytes + 1],
            givenBytes[startIndex4Bytes + 2],
            givenBytes[startIndex4Bytes + 3],
            givenBytes[startIndex4Bytes + 4],
            givenBytes[startIndex4Bytes + 5],
            givenBytes[startIndex4Bytes + 6],
            givenBytes[startIndex4Bytes + 7],
            givenBytes[startIndex4Bytes + 8],
            givenBytes[startIndex4Bytes + 9],
            givenBytes[startIndex4Bytes + 10],
            givenBytes[startIndex4Bytes + 11],
            givenBytes[startIndex4Bytes + 12],
            givenBytes[startIndex4Bytes + 13],
            givenBytes[startIndex4Bytes + 14],
            givenBytes[startIndex4Bytes + 15] };
        result = Encoding.Unicode.GetString(floatBytes);
    }


    public void SubstractBytesToUTF8(ref byte[] originalBytes, int toSubstractCount, out string utf8) {
        SubstractBytes(ref originalBytes, toSubstractCount, out byte[] utf8Bytes);
        utf8 = Encoding.UTF8.GetString(utf8Bytes);
    }
    

    public void SubstractBytes(ref byte[] originalBytes, int toSubstractCount, out byte [] subtractedBytes ) {
        if (originalBytes.Length <= toSubstractCount)
        {
            subtractedBytes = originalBytes;
            return;
        }
        else {
            subtractedBytes = new byte[originalBytes.Length - toSubstractCount];
            Array.Copy(originalBytes, toSubstractCount, subtractedBytes, 0, subtractedBytes.Length);
        }
    }

}
