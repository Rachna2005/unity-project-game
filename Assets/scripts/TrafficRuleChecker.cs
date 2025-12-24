using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficRuleChecker : MonoBehaviour
{
    public TrafficLightState trafficLight;
    public GameObject ruleBreakText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trafficLight.isRed)
            {
                ruleBreakText.SetActive(true);
                Invoke("HideText", 3f);
            }
        }
    }

    void HideText()
    {
        ruleBreakText.SetActive(false);
    }
}