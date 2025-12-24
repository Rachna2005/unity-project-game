using UnityEngine;
using TMPro;
using System.Collections;

public class UIMessageManager : MonoBehaviour
{
    public static UIMessageManager Instance;

    public TextMeshProUGUI warningText;
    public float displayTime = 2f;

    void Awake()
    {
        Instance = this;
        warningText.gameObject.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(message));
    }

    IEnumerator ShowRoutine(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        warningText.gameObject.SetActive(false);
    }
    public void ShowStopCountdown(float timeLeft)
{
    warningText.gameObject.SetActive(true);
    warningText.text = "STOP: " + timeLeft.ToString("0.0") + "s";
}

public void ShowGo()
{
    warningText.text = "GO";
}
}
