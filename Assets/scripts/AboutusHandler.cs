using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AboutUsHandler : MonoBehaviour
{
    public GameObject aboutUsPanel;

    public void ShowAboutUs()
    {
        if (aboutUsPanel != null)
            aboutUsPanel.SetActive(true);
    }

    public void HideAboutUs()
    {
        if (aboutUsPanel != null)
            aboutUsPanel.SetActive(false);
    }
}
