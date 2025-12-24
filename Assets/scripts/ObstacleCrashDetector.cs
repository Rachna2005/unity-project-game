using UnityEngine;

public class ObstacleCrashDetector : MonoBehaviour
{
    public float crashForceThreshold = 6f;   // Adjust this
    public float pushForce = 4f;              // How far cone flies

    bool hasCrashed = false;

    void OnCollisionEnter(Collision collision)
    {
        if (hasCrashed) return;

        // Get the car
        CarController car = collision.gameObject.GetComponentInParent<CarController>();
        if (car == null) return;

        float impactForce = collision.relativeVelocity.magnitude;

        // Only trigger if impact is strong enough
        if (impactForce >= crashForceThreshold)
        {
            hasCrashed = true;

            Debug.Log("REAL CRASH detected! Force: " + impactForce);

            // Push the cone away (visual feedback)
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDir = collision.relativeVelocity.normalized;
                rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            }

            // Delay Game Over slightly so player sees the crash
            StartCoroutine(GameOverAfterDelay(car));
        }
    }

    System.Collections.IEnumerator GameOverAfterDelay(CarController car)
    {
        yield return new WaitForSeconds(0.5f);
        car.SendMessage("TriggerGameOver", "Crashed into obstacle!");
    }
}
