using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Driving Settings")]
    public float maxSpeedKmh = 60f;
    public float acceleration = 2.5f;
    public float brakeStrength = 6f;
    public float turnSpeed = 80f;
    public float steeringSmooth = 6f;

    [Header("Game Over Settings")]
    public float fallYLimit = -5f;

    [Header("Life System")]
    public int lives = 3;

    [Header("Runtime Info")]
    public float CurrentSpeedKmh;

    [Header("Audio")]
    public AudioSource engineAudio;
    public AudioSource crashAudio;

    Rigidbody rb;

    float throttleInput;
    float currentSpeed;
    float currentSteer;

    bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);

        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        if (engineAudio)
        {
            engineAudio.loop = true;
            engineAudio.playOnAwake = false;
        }

        if (crashAudio)
            crashAudio.playOnAwake = false;
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        HandleMovement();
        HandleEngineSound();

        CurrentSpeedKmh = rb.velocity.magnitude * 3.6f;

        if (transform.position.y < fallYLimit)
            TriggerGameOver("You fell off the road!");
    }

 void HandleMovement()
{
    float steerInput = 0f;
    if (Input.GetKey(KeyCode.A)) steerInput = -1f;
    if (Input.GetKey(KeyCode.D)) steerInput = 1f;

    if (Input.GetKey(KeyCode.W))
        throttleInput += acceleration * Time.fixedDeltaTime;
    else if (Input.GetKey(KeyCode.S))
        throttleInput -= brakeStrength * Time.fixedDeltaTime;
    else
        throttleInput = Mathf.MoveTowards(throttleInput, 0f, brakeStrength * Time.fixedDeltaTime);

    throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);

    float maxSpeedMs = maxSpeedKmh / 3.6f;
    float targetSpeed = throttleInput * maxSpeedMs;

    currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 5f * Time.fixedDeltaTime);

    // ✅ DIRECT VELOCITY CONTROL (THIS IS WHY IT MOVES PROPERLY)
    Vector3 velocity = transform.forward * currentSpeed;
    rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

    currentSteer = Mathf.Lerp(currentSteer, steerInput, steeringSmooth * Time.fixedDeltaTime);

    float speedFactor = Mathf.Clamp01(Mathf.Abs(currentSpeed) / maxSpeedMs);
    speedFactor = Mathf.Lerp(0.3f, 1f, speedFactor);

    if (speedFactor > 0.05f)
    {
        rb.MoveRotation(
            rb.rotation *
            Quaternion.Euler(0f, currentSteer * turnSpeed * speedFactor * Time.fixedDeltaTime, 0f)
        );
    }
}

    void HandleEngineSound()
    {
        if (!engineAudio) return;

        float speed = rb.velocity.magnitude;

        if (speed > 0.5f)
        {
            if (!engineAudio.isPlaying)
                engineAudio.Play();

            engineAudio.pitch = Mathf.Lerp(0.9f, 1.6f, speed / (maxSpeedKmh / 3.6f));
        }
        else if (engineAudio.isPlaying)
        {
            engineAudio.Stop();
        }
    }

    public void LoseLife(string reason)
    {
        if (isGameOver) return;

        lives--;

        UIMessageManager.Instance?.ShowMessage(
            reason + "\nLives left: " + lives
        );

        if (lives <= 0)
            TriggerGameOver("Too many violations!");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Crash"))
            TriggerGameOver("You crashed!");
    }

    void TriggerGameOver(string reason)
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("GAME OVER: " + reason);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (engineAudio && engineAudio.isPlaying)
            engineAudio.Stop();

        if (crashAudio)
            crashAudio.Play();

        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1.2f);

        Time.timeScale = 0f;

        FindObjectOfType<GameOverManager>()?.ShowGameOver();
    }
}
