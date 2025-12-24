using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSignsHandler : MonoBehaviour
{
    public GameObject trafficSignsPanel;

    public void ShowTrafficSigns()
    {
        trafficSignsPanel.SetActive(true);
    }

    public void HideTrafficSigns()
    {
        trafficSignsPanel.SetActive(false);
    }
}
