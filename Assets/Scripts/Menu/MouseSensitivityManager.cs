using UnityEngine;
using Cinemachine;

public class MouseSensitivityManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cvcFreeLook;

    public void SetSensitivity(float sensitivityValue)
    {
        cvcFreeLook.m_YAxis.m_MaxSpeed = sensitivityValue / 25;
        cvcFreeLook.m_XAxis.m_MaxSpeed = sensitivityValue * 6;
    }
}
