using UnityEngine;

public class SlowDownZoneDetector : MonoBehaviour
{
    public float speedLimitKmh = 30f;     // Max allowed speed
    public float allowedOverTime = 1f;    // Seconds allowed above limit

    CarController car;
    float overSpeedTimer = 0f;
    bool violationTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        car = other.GetComponentInParent<CarController>();
        if (car == null) return;

        overSpeedTimer = 0f;
        violationTriggered = false;

        // Show warning immediately
        UIMessageManager.Instance.ShowMessage("SLOW DOWN!\nSpeed limit: " + speedLimitKmh + " km/h");
    }

    void Update()
    {
        if (car == null || violationTriggered) return;

        if (car.CurrentSpeedKmh > speedLimitKmh)
        {
            overSpeedTimer += Time.deltaTime;

            if (overSpeedTimer >= allowedOverTime)
            {
                violationTriggered = true;
                car.LoseLife("Did not slow down!");
            }
        }
        else
        {
            // Player slowed down correctly
            overSpeedTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        CarController exitingCar = other.GetComponentInParent<CarController>();
        if (exitingCar == car)
        {
            car = null;
        }
    }
}
