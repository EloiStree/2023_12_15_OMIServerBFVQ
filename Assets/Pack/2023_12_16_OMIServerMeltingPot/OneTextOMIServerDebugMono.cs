using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class OneTextOMIServerDebugMono : MonoBehaviour
{

    public StringEvent m_textDebug;
    public CharUTFRegistersMono m_charRegister;
    public NamedRegisterBFVQMono m_registersBFVQ;

    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }


    [ContextMenu("Push and Refresh")]
    public void RefreshAndPush()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("# Char UTF16");
        sb.AppendLine(" ");
        sb.AppendLine("## Boolean");
        DisplayDico(sb, OMIServerPrimitiveType.Boolean);
        sb.AppendLine(" ");
        sb.AppendLine("## Float");
        DisplayDico(sb, OMIServerPrimitiveType.Float);
        sb.AppendLine(" ");
        sb.AppendLine("## Vector3");
        DisplayDico(sb, OMIServerPrimitiveType.Vector3);
        sb.AppendLine(" ");
        sb.AppendLine("## Quaternion");
        DisplayDico(sb, OMIServerPrimitiveType.Quaternion);
        sb.AppendLine(" ");


        sb.AppendLine("# Value");
        sb.AppendLine(" ");
        sb.AppendLine("## Boolean");
        DisplayDicoValue(sb, OMIServerPrimitiveType.Boolean);
        sb.AppendLine(" ");
        sb.AppendLine("## Float");
        DisplayDicoValue(sb, OMIServerPrimitiveType.Float);
        sb.AppendLine(" ");
        sb.AppendLine("## Vector3");
        DisplayDicoValue(sb, OMIServerPrimitiveType.Vector3);
        sb.AppendLine(" ");
        sb.AppendLine("## Quaternion");
        DisplayDicoValue(sb, OMIServerPrimitiveType.Quaternion);
        sb.AppendLine(" ");



        m_textDebug.Invoke(sb.ToString());
    }

    private void DisplayDico(StringBuilder sb, OMIServerPrimitiveType type)
    {
        sb.AppendLine("Key: " + string.Join(", ", m_charRegister.R.GetAllCharKeys(type)));
        foreach (var item in m_charRegister.R.GetAll(type))
        {
            sb.AppendLine("  -" + JsonUtility.ToJson(item, false));
        }
    }
    private void DisplayDicoValue(StringBuilder sb, OMIServerPrimitiveType type)
    {
        sb.AppendLine("Key: " + string.Join(", ", m_registersBFVQ.R.GetAllKeys(type)));
        
        switch (type)
        {
            case OMIServerPrimitiveType.Boolean:sb.AppendLine(string.Join(", ",
                m_registersBFVQ.R.GetAllBooleanValue().Select(k=>k.ToString()) ));
                break;
            case OMIServerPrimitiveType.Float:
                sb.AppendLine(string.Join(", ",m_registersBFVQ.R.GetAllFloatValue().Select(k => k.ToString())));
                break;
            case OMIServerPrimitiveType.Vector3:
                sb.AppendLine(string.Join(", ",m_registersBFVQ.R.GetAllVector3Value().Select(k => k.ToString())));
                break;
            case OMIServerPrimitiveType.Quaternion:
                sb.AppendLine(string.Join(", ",m_registersBFVQ.R.GetAllQuaternionValue().Select(k => k.ToString())));
                break;
            default:
                break;
        }
       
    }

    [ContextMenu("Push and Refresh")]
    public void SaveAsFile(string text)
    {
        m_path = Application.dataPath + "/" + m_fileSubPath;

        File.WriteAllText(m_path, text);
    }
    public string m_fileSubPath = "OMIServerDebug.txt";
    public string m_path="";
}
