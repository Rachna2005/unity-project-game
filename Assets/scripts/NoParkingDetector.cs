using UnityEngine;

public class NoParkingDetector : MonoBehaviour
{
    public float allowedStopTime = 2f;      // seconds allowed to stop
    public float stopSpeedThreshold = 0.2f; // km/h

    CarController car;
    float stopTimer = 0f;
    bool violationTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        car = other.GetComponentInParent<CarController>();
        if (car == null) return;

        stopTimer = 0f;
        violationTriggered = false;

        Debug.Log("Entered NO PARKING zone");
    }

    void Update()
    {
        if (car == null || violationTriggered) return;

        // Check if car is stopped
        if (car.CurrentSpeedKmh <= stopSpeedThreshold)
        {
            stopTimer += Time.deltaTime;

            if (stopTimer >= allowedStopTime)
            {
                violationTriggered = true;

                Debug.Log("NO PARKING VIOLATION");

                car.LoseLife("NO PARKING!");
            }
        }
        else
        {
            // Car is moving → reset timer
            stopTimer = 0f;
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
