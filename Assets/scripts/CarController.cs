using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Driving Settings")]
    public float maxSpeedKmh = 60f;       // Forward max speed
    public float acceleration = 2.5f;     // Throttle build-up speed
    public float brakeStrength = 6f;       // Natural braking
    public float turnSpeed = 80f;          // Steering power
    public float steeringSmooth = 6f;      // Steering smoothness

    [Header("Game Over Settings")]
    public float fallYLimit = -5f;         // Fall below this = Game Over

    [Header("Runtime Info")]
    public float CurrentSpeedKmh;          // For UI & traffic rules

    Rigidbody rb;

    float throttleInput = 0f;              // -1 (reverse) → 1 (forward)
    float currentSpeed = 0f;               // m/s
    float currentSteer = 0f;
    bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        // ================= INPUT =================
        float steerInput = 0f;

        if (Input.GetKey(KeyCode.A)) steerInput = -1f;
        if (Input.GetKey(KeyCode.D)) steerInput = 1f;

        // ================= THROTTLE (FORWARD + REVERSE) =================
        if (Input.GetKey(KeyCode.W))
        {
            throttleInput += acceleration * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            throttleInput -= acceleration * Time.fixedDeltaTime;
        }
        else
        {
            throttleInput = Mathf.MoveTowards(
                throttleInput,
                0f,
                brakeStrength * Time.fixedDeltaTime
            );
        }

        throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);

        // ================= SPEED =================
        float maxSpeedMs = maxSpeedKmh / 3.6f;
        float reverseMaxMs = maxSpeedMs * 0.4f; // Reverse slower

        float targetSpeed =
            throttleInput >= 0
            ? throttleInput * maxSpeedMs
            : throttleInput * reverseMaxMs;

        currentSpeed = Mathf.Lerp(
            currentSpeed,
            targetSpeed,
            5f * Time.fixedDeltaTime
        );

        Vector3 forwardVelocity = transform.forward * currentSpeed;
        rb.velocity = new Vector3(
            forwardVelocity.x,
            rb.velocity.y,
            forwardVelocity.z
        );

        // ================= STEERING =================
        currentSteer = Mathf.Lerp(
            currentSteer,
            steerInput,
            steeringSmooth * Time.fixedDeltaTime
        );

        float speedFactor = Mathf.Clamp01(Mathf.Abs(currentSpeed) / maxSpeedMs);
        speedFactor = Mathf.Lerp(0.3f, 1f, speedFactor);

        if (speedFactor > 0.05f)
        {
            rb.MoveRotation(
                rb.rotation *
                Quaternion.Euler(
                    0f,
                    currentSteer * turnSpeed * speedFactor * Time.fixedDeltaTime,
                    0f
                )
            );
        }

        // ================= GRIP (ANTI-SLIDE) =================
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x *= 0.5f; // Strong grip
        rb.velocity = transform.TransformDirection(localVelocity);

        // ================= REAL SPEED (km/h) =================
        CurrentSpeedKmh = rb.velocity.magnitude * 3.6f;

        // ================= FALL CHECK =================
        if (transform.position.y < fallYLimit)
        {
            TriggerGameOver("You fell off the road!");
        }
    }

    // ================= CRASH CHECK =================
    void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Crash"))
        {
            TriggerGameOver("You crashed into an obstacle!");
        }
    }

    // ================= GAME OVER =================
    void TriggerGameOver(string reason)
    {
 isGameOver = true;

    Debug.Log("GAME OVER: " + reason);

    rb.velocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    enabled = false;

    FindObjectOfType<GameOverManager>().ShowGameOver();

    }
}
