using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class carcontroll : MonoBehaviour
{
    public float acceleration = 50f;   // speed up
    public float maxSpeed = 60f;        // top speed
    public float turnSpeed = 100f;
    public float brakeStrength = 30f;   // space brake
    public float drag = 5f;             // slow down naturally

    private float currentSpeed = 0f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");     // Up / Down
        float turnInput = Input.GetAxis("Horizontal");   // Left / Right

        // Accelerate
        if (moveInput != 0)
        {
            currentSpeed += moveInput * acceleration * Time.deltaTime;
        }
        else
        {
            // Natural slow down
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, drag * Time.deltaTime);
        }

        // Brake (SPACE)
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, brakeStrength * Time.deltaTime);
        }

        // Limit speed
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Move car
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Turn only when moving
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
        }
    }
}
