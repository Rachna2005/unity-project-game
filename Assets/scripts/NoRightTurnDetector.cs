using UnityEngine;

public class NoRightTurnDetector : MonoBehaviour
{
    bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        CarController car = other.GetComponentInParent<CarController>();
        if (car == null) return;

        triggered = true;

        Debug.Log("NO RIGHT TURN VIOLATION");

        car.LoseLife("NO RIGHT TURN!");
    }
}
