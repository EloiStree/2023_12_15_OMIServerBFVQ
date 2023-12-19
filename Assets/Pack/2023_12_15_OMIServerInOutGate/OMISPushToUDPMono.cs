using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class OMISPushToUDPMono : MonoBehaviour
{
    public string m_ip="192.168.1.250";
    public int m_port=2571;
    public void PushTextUTF8(string text) {

        try
        {
            using (UdpClient udpClient = new UdpClient())
            {
                byte[] data = Encoding.UTF8.GetBytes(text);

                udpClient.Send(data, data.Length, m_ip, m_port);
            }
        }
        catch (Exception )
        {
        }
    }
    public void PushBytesAsRaw(byte[] bytes) {

        try
        {
            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.Send(bytes, bytes.Length, m_ip, m_port);
            }
        }
        catch (Exception )
        {
        }
    }
    
}
