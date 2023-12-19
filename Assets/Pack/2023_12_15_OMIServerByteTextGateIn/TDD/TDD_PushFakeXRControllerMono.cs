using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDD_PushFakeXRControllerMono : MonoBehaviour
{
    public JoystickXR m_leftHand;
    public JoystickXR m_rightHand;

    [System.Serializable]
    public class JoystickXR
    {
        public bool m_primary;
        public bool m_secondary;
        public bool m_joystick;
        [Range(-1, 1)]
        public float m_trigger;
        [Range(-1, 1)]
        public float m_grap;

        [Range(-1, 1)]
        public float m_joystickAxisHorizontal;

        [Range(-1, 1)]
        public float m_joystickAxisVertical;
        public Transform m_fakeJoystickPositionLocal;

        public bool m_isHandTracked;
        public bool m_isControllerTracked;
    }

    public bool m_useUpdateRefresh=true;
    void Update()
    {
        if(m_useUpdateRefresh)  
            RefreshRate();

    }
    public OMIServerObjectEvent m_onPush;
    private void RefreshRate()
    {
        Push(m_leftHand, "Left_");
        Push(m_rightHand, "Right_");
    }

    private void Push(JoystickXR hand, string start)
    {
        PushValue(hand.m_primary, start + "primatry");
        PushValue(hand.m_secondary, start + "secondary");
        PushValue(hand.m_joystick, start + "jooystick");
        PushValue(hand.m_isHandTracked, start + "istrackedhand");
        PushValue(hand.m_isControllerTracked, start + "istrackedcontroller");
        PushValue(hand.m_grap, start + "grap");
        PushValue(hand.m_trigger, start + "trigger");
        PushValue(hand.m_joystickAxisHorizontal, start + "horizontal");
        PushValue(hand.m_joystickAxisVertical, start + "vertical");
        PushValue(hand.m_fakeJoystickPositionLocal.localPosition, start + "position");
        PushValue(hand.m_fakeJoystickPositionLocal.localRotation, start + "position");
    }

    private void PushValue(bool value, string name)
    {
        m_onPush.Invoke(new NamedBooleanValue(name, value));
    }
    private void PushValue(float value, string name)
    {
        m_onPush.Invoke(new NamedFloatValue(name, value));
    }
    private void PushValue(Quaternion value, string name)
    {
        m_onPush.Invoke(new NamedQuaternionValue(name, value));
    }
    private void PushValue(Vector3 value, string name)
    {
        m_onPush.Invoke(new NamedVector3Value(name, value));
    }
}
