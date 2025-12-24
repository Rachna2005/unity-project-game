using UnityEngine;

public class TrafficLightViolationDetector : MonoBehaviour
{
    [Header("Traffic Light Reference")]
    public Renderer trafficLightRenderer;

    [Header("Materials")]
    public Material redMaterial;
    public Material yellowMaterial;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        CarController car = other.GetComponentInParent<CarController>();

        if (car == null) return;

        Material currentMat = trafficLightRenderer.sharedMaterial;

        if (currentMat == redMaterial || currentMat == yellowMaterial)
        {
            Debug.Log("TRAFFIC VIOLATION: Ran red or yellow light");
            car.LoseLife("Ran red/yellow traffic light");
        }
        else
        {
            Debug.Log("Traffic light OK (green)");
        }
    }
}
