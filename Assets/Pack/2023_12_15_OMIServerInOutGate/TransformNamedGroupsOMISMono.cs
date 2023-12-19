using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformNamedGroupsOMISMono : MonoBehaviour
{
     

    public List<TransformGroup> m_group = new List<TransformGroup>();

    [System.Serializable]
    public class TransformGroup
    {
        public string m_subPath;
        public TransformTarget[] m_targets;
        public TransformGroup [] m_subGroup;
    }
    [System.Serializable]
    public class TransformTarget
    {
        public string m_name;
        public Transform m_target;
    }

    [System.Serializable]
    public class TransformNamedToTarget {
        public string m_named;
        public Transform m_target;
        public string GetFullNamed() { return m_named; }

        public Vector3 GetWorldPosition() { return m_target.position; }
        public Quaternion GetWorldRotation() { return m_target.rotation; }
        public Vector3 GetWorldPositionFromRoot(Transform root) {
            GetWorldToLocal_DirectionalPoint(m_target.position, m_target.rotation, root, out Vector3 loc, out Quaternion rot);
            return loc;
        }
        public Quaternion GetWorldRotationFromRoot(Transform root) {

            GetWorldToLocal_DirectionalPoint(m_target.position, m_target.rotation, root, out Vector3 loc, out Quaternion rot);
            return rot;
        }
    }



    public static void GetWorldToLocal_DirectionalPoint(in Vector3 worldPosition, in Quaternion worldRotation, in Transform rootReference, out Vector3 localPosition, out Quaternion localRotation)
    {
        Vector3 p = rootReference.position;
        Quaternion r = rootReference.rotation;
        GetWorldToLocal_DirectionalPoint(in worldPosition, in worldRotation, in p, in r, out localPosition, out localRotation);
    }

    public static void GetWorldToLocal_DirectionalPoint(in Vector3 worldPosition, in Quaternion worldRotation, in Vector3 positionReference, in Quaternion rotationReference, out Vector3 localPosition, out Quaternion localRotation)
    {
        localRotation = Quaternion.Inverse(rotationReference) * worldRotation;
        localPosition = Quaternion.Inverse(rotationReference) * (worldPosition - positionReference);
    }
    public List<TransformNamedToTarget> m_allAsArray = new List<TransformNamedToTarget>();

    [ContextMenu("Refresh Array")]
    public void RefreshFullArray()
    {
        m_allAsArray.Clear(); // Clear the existing list before refreshing

        // Iterate through each TransformGroup and its TransformTargets
        foreach (var group in m_group)
        {
            if (group.m_targets != null)
            {
                foreach (var target in group.m_targets)
                {
                    // Combine m_subPath and m_name to form m_named
                    string named = string.IsNullOrEmpty(group.m_subPath) ? target.m_name : $"{group.m_subPath}{target.m_name}";

                    m_allAsArray.Add(new TransformNamedToTarget
                    {
                        m_named = named,
                        m_target = target.m_target
                    });
                }
            }

            // Recursively add targets from subgroups
            if (group.m_subGroup != null)
            {
                foreach (var subGroup in group.m_subGroup)
                {
                    AddTargetsFromGroup(subGroup, group.m_subPath);
                }
            }
        }
    }

    // Helper method to recursively add targets from subgroups
    private void AddTargetsFromGroup(TransformGroup group, string parentSubPath)
    {
        if (group.m_targets != null)
        {
            foreach (var target in group.m_targets)
            {
                // Combine parentSubPath, m_subPath, and m_name to form m_named
                string named = string.IsNullOrEmpty(parentSubPath) ? target.m_name : $"{parentSubPath}{group.m_subPath}{target.m_name}";

                m_allAsArray.Add(new TransformNamedToTarget
                {
                    m_named = named,
                    m_target = target.m_target
                });
            }
        }

        // Recursively add targets from subgroups
        if (group.m_subGroup != null)
        {
            foreach (var subGroup in group.m_subGroup)
            {
                AddTargetsFromGroup(subGroup, string.IsNullOrEmpty(parentSubPath) ? group.m_subPath : $"{parentSubPath}{group.m_subPath}");
            }
        }
    }
}
