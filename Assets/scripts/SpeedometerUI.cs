using UnityEngine;
using TMPro;

public class SpeedometerUI : MonoBehaviour
{
    public CarController car;     // Reference to car
    public TextMeshProUGUI speedText;

    void Update()
    {
        if (car == null) return;

        // Round speed for display
        int speed = Mathf.RoundToInt(car.CurrentSpeedKmh);

        speedText.text = speed + " km/h";
    }
}
