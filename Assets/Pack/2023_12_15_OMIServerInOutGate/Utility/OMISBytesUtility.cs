using System;
using System.Text;
using UnityEngine;

public class OMISBytesUtility {



    #region BYTE TO PRIMITIVE
    public static byte[] ConvertCharToByteArray(char character)
    {
        Encoding encoding = Encoding.Unicode;
        byte[] byteArray = encoding.GetBytes(new char[] { character });

        return byteArray;
    }

    public static byte[] ConvertQuaternionToBytes(Quaternion[] quaternionArray)
    {
        if (quaternionArray == null)
            quaternionArray = new Quaternion[0];
        int quaternionSizeInBytes = sizeof(float) * 4;
        byte[] result = new byte[quaternionArray.Length * quaternionSizeInBytes];

        for (int i = 0; i < quaternionArray.Length; i++)
        {
            byte[] xBytes = BitConverter.GetBytes(quaternionArray[i].x);
            byte[] yBytes = BitConverter.GetBytes(quaternionArray[i].y);
            byte[] zBytes = BitConverter.GetBytes(quaternionArray[i].z);
            byte[] wBytes = BitConverter.GetBytes(quaternionArray[i].w);

            Buffer.BlockCopy(xBytes, 0, result, i * quaternionSizeInBytes, xBytes.Length);
            Buffer.BlockCopy(yBytes, 0, result, i * quaternionSizeInBytes + sizeof(float), yBytes.Length);
            Buffer.BlockCopy(zBytes, 0, result, i * quaternionSizeInBytes + sizeof(float) * 2, zBytes.Length);
            Buffer.BlockCopy(wBytes, 0, result, i * quaternionSizeInBytes + sizeof(float) * 3, wBytes.Length);
        }

        return result;
    }

    public static byte[] ConvertVectorToBytes(Vector3[] vectorArray)
    {
        if (vectorArray == null)
            vectorArray = new Vector3[0];

        int vectorSizeInBytes = sizeof(float) * 3;
        byte[] result = new byte[vectorArray.Length * vectorSizeInBytes];

        for (int i = 0; i < vectorArray.Length; i++)
        {
            byte[] xBytes = BitConverter.GetBytes(vectorArray[i].x);
            byte[] yBytes = BitConverter.GetBytes(vectorArray[i].y);
            byte[] zBytes = BitConverter.GetBytes(vectorArray[i].z);

            Buffer.BlockCopy(xBytes, 0, result, i * vectorSizeInBytes, xBytes.Length);
            Buffer.BlockCopy(yBytes, 0, result, i * vectorSizeInBytes + sizeof(float), yBytes.Length);
            Buffer.BlockCopy(zBytes, 0, result, i * vectorSizeInBytes + sizeof(float) * 2, zBytes.Length);
        }

        return result;

    }

    public static byte[] ConvertFloatToBytes(float[] floatArray)
    {
        if (floatArray == null)
            floatArray = new float[0];
        int floatSizeInBytes = sizeof(float);
        byte[] result = new byte[floatArray.Length * floatSizeInBytes];

        for (int i = 0; i < floatArray.Length; i++)
        {
            byte[] floatBytes = BitConverter.GetBytes(floatArray[i]);
            Buffer.BlockCopy(floatBytes, 0, result, i * floatSizeInBytes, floatSizeInBytes);
        }

        return result;
    }
    public static  byte[] ConvertBoolArrayToByteArray(bool[] boolArray)

    {

        if (boolArray == null)
            boolArray = new bool[0];

        int numBytes = boolArray.Length / 8;
        if (boolArray.Length % 8 != 0)
            numBytes++;

        byte[] byteArray = new byte[numBytes];
        int byteIndex = 0;
        int bitIndex = 0;

        for (int i = 0; i < boolArray.Length; i++)
        {
            if (boolArray[i])
                byteArray[byteIndex] |= (byte)(1 << (7 - bitIndex));

            bitIndex++;

            if (bitIndex == 8)
            {
                bitIndex = 0;
                byteIndex++;
            }
        }

        return byteArray;
    }

    #endregion

    #region BYTE TO PRIMITIVE
    public static char ConvertByteArrayToChar(byte[] byteArray)
    {
        Encoding encoding = Encoding.UTF8;
        char character = encoding.GetChars(byteArray)[0];
        return character;
    }

    public static Quaternion[] ConvertBytesToQuaternionArray(byte[] byteArray)
    {
        int quaternionSizeInBytes = sizeof(float) * 4;
        int numQuaternions = byteArray.Length / quaternionSizeInBytes;

        Quaternion[] quaternionArray = new Quaternion[numQuaternions];

        for (int i = 0; i < numQuaternions; i++)
        {
            float x = BitConverter.ToSingle(byteArray, i * quaternionSizeInBytes);
            float y = BitConverter.ToSingle(byteArray, i * quaternionSizeInBytes + sizeof(float));
            float z = BitConverter.ToSingle(byteArray, i * quaternionSizeInBytes + sizeof(float) * 2);
            float w = BitConverter.ToSingle(byteArray, i * quaternionSizeInBytes + sizeof(float) * 3);

            quaternionArray[i] = new Quaternion(x, y, z, w);
        }

        return quaternionArray;
    }

    public static Vector3[] ConvertBytesToVectorArray(byte[] byteArray)
    {
        int vectorSizeInBytes = sizeof(float) * 3;
        int numVectors = byteArray.Length / vectorSizeInBytes;

        Vector3[] vectorArray = new Vector3[numVectors];

        for (int i = 0; i < numVectors; i++)
        {
            float x = BitConverter.ToSingle(byteArray, i * vectorSizeInBytes);
            float y = BitConverter.ToSingle(byteArray, i * vectorSizeInBytes + sizeof(float));
            float z = BitConverter.ToSingle(byteArray, i * vectorSizeInBytes + sizeof(float) * 2);

            vectorArray[i] = new Vector3(x, y, z);
        }

        return vectorArray;
    }

    public static float[] ConvertBytesToFloatArray(byte[] byteArray)
    {
        int floatSizeInBytes = sizeof(float);
        int numFloats = byteArray.Length / floatSizeInBytes;

        float[] floatArray = new float[numFloats];

        for (int i = 0; i < numFloats; i++)
        {
            floatArray[i] = BitConverter.ToSingle(byteArray, i * floatSizeInBytes);
        }

        return floatArray;
    }

    public static  bool[] ConvertByteArrayToBoolArray(byte[] byteArray)
    {
        int numBools = byteArray.Length * 8;
        bool[] boolArray = new bool[numBools];

        int byteIndex = 0;
        int bitIndex = 0;

        for (int i = 0; i < numBools; i++)
        {
            boolArray[i] = (byteArray[byteIndex] & (1 << (7 - bitIndex))) != 0;

            bitIndex++;

            if (bitIndex == 8)
            {
                bitIndex = 0;
                byteIndex++;
            }
        }

        return boolArray;
    }

    #endregion

    public static float[] GetFloatInByte( int index, ref byte[] bytes)
    {
        int floatsToRead = (bytes.Length - index) / sizeof(float);
        float[] result = new float[floatsToRead];

        for (int i = 0; i < floatsToRead; i++)
        {
            result[i] = BitConverter.ToSingle(bytes, index);
            index += sizeof(float);
        }

        return result;
    }

    public static Vector3[] ConvertToVector3(float[] floatArray)
    {
        if (floatArray.Length % 3 != 0)
        {
            return new Vector3[0];
            //throw new ArgumentException("The length of the float array must be a multiple of 3.");
        }

        int vectorCount = floatArray.Length / 3;
        Vector3[] vectorArray = new Vector3[vectorCount];

        for (int i = 0; i < vectorCount; i++)
        {
            int startIndex = i * 3;
            vectorArray[i] = new Vector3(floatArray[startIndex], floatArray[startIndex + 1], floatArray[startIndex + 2]);
        }

        return vectorArray;
    }

    public static Quaternion[] ConvertToQuaternion(float[] floatArray)
    {
        if (floatArray.Length % 4 != 0)
        {
            return new Quaternion[0];
            //throw new ArgumentException("The length of the float array must be a multiple of 4.");
        }

        int quaternionCount = floatArray.Length / 4;
        Quaternion[] quaternionArray = new Quaternion[quaternionCount];

        for (int i = 0; i < quaternionCount; i++)
        {
            int startIndex = i * 4;
            quaternionArray[i] = new Quaternion(floatArray[startIndex], floatArray[startIndex + 1], floatArray[startIndex + 2], floatArray[startIndex + 3]);
        }

        return quaternionArray;
    }

    public static bool[] ConvertToBoolArray(ref byte[] byteArray, int skipBytes)
    {
        int boolArrayLength = (byteArray.Length - skipBytes) * 8; // Each byte has 8 bits
        bool[] boolArray = new bool[boolArrayLength];

        for (int i = skipBytes; i < byteArray.Length; i++)
        {
            byte currentByte = byteArray[i];

            for (int j = 0; j < 8; j++)
            {
                int boolIndex = (i - skipBytes) * 8 + j;
                boolArray[boolIndex] = ((currentByte >> (7 - j)) & 1) == 1;
            }
        }

        return boolArray;
    }

}