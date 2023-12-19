using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDD_PushTextArrayNamedIndexIn : MonoBehaviour
{

    public StringEvent m_onPush;
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }

    //¥ £ ® µ ¶
    public float m_timeBetween = 3;
    public IEnumerator Start()
    {


        Push("~åBN 0 A B X Y AL AR AD AU");
        yield return new WaitForSeconds(m_timeBetween);
        Push("~åFN 1 JLH JLV JRH JRV");
        yield return new WaitForSeconds(m_timeBetween);
        Push("~åVN 0:0:0 HandLeftPos handRightPos");
        yield return new WaitForSeconds(m_timeBetween);
        Push("~åQN 0:0:0:0 HandLeftPos handRightPos");

        yield return new WaitForSeconds(m_timeBetween);
        Push("~åBV 0 1 1 1 0 1 0 T F True False ");
        yield return new WaitForSeconds(m_timeBetween);
        Push("~åFV -0.1 0.23 0.8 0.5 ");
        yield return new WaitForSeconds(m_timeBetween);
        Push("~åVV 0.52:0.45:0.99 6:2:5 ");
        yield return new WaitForSeconds(m_timeBetween);
        Push("~åQV 0.52:0.45:0.99:0.45 0:0:0:1 ");
        yield return new WaitForSeconds(m_timeBetween);

        Push("X Y 🀲 B A");
        yield return new WaitForSeconds(m_timeBetween);
        Push("X Y 🀸 B A");

        yield return new WaitForSeconds(m_timeBetween);

        Push("~ True ButtonDemo");
        Push("~ 0.3 AxisDemo");
        Push("~ -0.3:0.1:1 VectorDemo");
        Push("~ -0.3:0.1:0:1 QuaternionDemo");
        yield return new WaitForSeconds(m_timeBetween);
    }
    private void Push(float time, string text)
    {
        m_onPush.Invoke(text);
    }
    private void Push(string text)
    {
        m_onPush.Invoke(text);
    }
}
