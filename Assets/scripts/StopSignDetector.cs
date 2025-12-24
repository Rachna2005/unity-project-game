using UnityEngine;

public class StopSignDetector : MonoBehaviour
{
    public float requiredStopTime = 1f;
    public float stopSpeedThreshold = 0.2f;

    float stopTimer;
    bool hasStopped;
    CarController car;

    void OnTriggerEnter(Collider other)
    {
        car = other.GetComponentInParent<CarController>();
        if (car == null) return;

        stopTimer = 0f;
        hasStopped = false;

        UIMessageManager.Instance.ShowStopCountdown(requiredStopTime);
    }

    void Update()
    {
        if (car == null) return;

        if (car.CurrentSpeedKmh <= stopSpeedThreshold)
        {
            stopTimer += Time.deltaTime;
            float remaining = Mathf.Max(0, requiredStopTime - stopTimer);

            UIMessageManager.Instance.ShowStopCountdown(remaining);

            if (stopTimer >= requiredStopTime)
            {
                hasStopped = true;
                UIMessageManager.Instance.ShowGo();
            }
        }
        else
        {
            stopTimer = 0f;
            UIMessageManager.Instance.ShowStopCountdown(requiredStopTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (car == null) return;

        if (!hasStopped)
        {
            car.LoseLife("STOP sign violation!");
        }

        UIMessageManager.Instance.warningText.gameObject.SetActive(false);
        car = null;
    }
}
