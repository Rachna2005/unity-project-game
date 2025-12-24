using UnityEngine;

public class RulesButtonHandler : MonoBehaviour
{
    public GameObject rulesPanel;

    public void ShowRules()
    {
        if (rulesPanel != null)
            rulesPanel.SetActive(true);
    }

    public void HideRules()
    {
        if (rulesPanel != null)
            rulesPanel.SetActive(false);
    }
}
