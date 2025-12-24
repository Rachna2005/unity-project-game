using UnityEngine;

public class GoalZoneDetector : MonoBehaviour
{
    private bool winTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (winTriggered) return;

        // Get the car controller from the car (or its children)
        CarController car = other.GetComponentInParent<CarController>();
        if (car == null) return;

        winTriggered = true;

        Debug.Log("GOAL REACHED");

        // Stop car movement safely
        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // freeze physics
        }

        // Stop engine sound
        AudioSource engineAudio = car.GetComponent<AudioSource>();
        if (engineAudio != null && engineAudio.isPlaying)
        {
            engineAudio.Stop();
        }

        // Disable car control script
        car.enabled = false;

        // Show win UI and freeze game
        GameOverManager gm = FindObjectOfType<GameOverManager>();
        if (gm != null)
        {
            gm.ShowWin();
        }
        else
        {
            Debug.LogError("GameOverManager NOT found");
        }
    }
}
