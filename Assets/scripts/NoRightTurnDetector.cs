using UnityEngine;

public class NoRightTurnDetector : MonoBehaviour
{
    public float rightTurnAngleThreshold = 25f;

    CarController car;
    float entryYaw;
    bool violationTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        car = other.GetComponentInParent<CarController>();
        if (car == null) return;

        entryYaw = car.transform.eulerAngles.y;
        violationTriggered = false;

        Debug.Log("Entered NO RIGHT TURN zone");
    }

    void Update()
    {
        if (car == null || violationTriggered) return;

        float currentYaw = car.transform.eulerAngles.y;
        float deltaYaw = Mathf.DeltaAngle(entryYaw, currentYaw);

        // Positive deltaYaw = right turn
        if (deltaYaw > rightTurnAngleThreshold)
        {
            violationTriggered = true;

            // 🔴 SHOW WARNING + ❤️ LOSE LIFE
            car.LoseLife("NO RIGHT TURN!");

            Debug.Log("NO RIGHT TURN VIOLATION");
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
