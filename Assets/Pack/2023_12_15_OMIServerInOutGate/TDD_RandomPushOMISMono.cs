using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_RandomPushOMISMono : MonoBehaviour
{


    public RandomBool[] m_boolValues = new RandomBool[5];
    public RandomFloat[] m_floatValues = new RandomFloat[5];
    public RandomVector3[] m_vector3Values = new RandomVector3[5];
    public RandomQuaternion[] m_quaternionValues = new RandomQuaternion[5];

    public float m_vector3Distance = 5;
    public void Reset()
    {
        RandomizeAll();
        SetName();
    }

    [ContextMenu("Set Names to default")]
    private void SetName()
    {
        int i = 0;
        foreach (var item in m_boolValues)
        {
            item.m_name = "B" + i;
            i++;
        }
        i = 0;
        foreach (var item in m_floatValues)
        {
            item.m_name = "F" + i;
            i++;
        }
        i = 0;
        foreach (var item in m_vector3Values)
        {
            item.m_name = "V" + i;
            i++;
        }
        i = 0;
        foreach (var item in m_quaternionValues)
        {
            item.m_name = "Q" + i;
            i++;
        }
    }

    [ContextMenu("Randomize All")]
    public void RandomizeAll() {

        foreach (var item in m_boolValues)
        {
            item.Randomize();
        }
        foreach (var item in m_floatValues)
        {
            item.Randomize();
        }
        foreach (var item in m_vector3Values)
        {
            item.Randomize(m_vector3Distance);
        }
        foreach (var item in m_quaternionValues)
        {
            item.Randomize();
        }
    }

    [System.Serializable]
    public class RandomFloat
    {
        public string m_name;
        public float m_value;
        public void Randomize() { m_value = Random.value; }
    }
    [System.Serializable]
    public class RandomBool
    {
        public string m_name;
        public bool m_value;

        public void Randomize() { m_value = Random.value < 0.5f; }
    }
    [System.Serializable]
    public class RandomVector3
    {
        public string m_name;
        public Vector3 m_value;
        public void Randomize() { m_value = new Vector3(Random.value, Random.value, Random.value); }
        public void Randomize(float distance) { m_value = new Vector3(Random.value* distance, Random.value * distance, Random.value * distance); }
    }
    [System.Serializable]
    public class RandomQuaternion
    {
        public string m_name;
        public Quaternion m_value;
        public void Randomize() { m_value = new Quaternion(Random.value, Random.value, Random.value, Random.value); }
    }


}
