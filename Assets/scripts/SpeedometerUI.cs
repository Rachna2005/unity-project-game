using UnityEngine;
using TMPro;

public class SpeedometerUI : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    CarController car;

    void Update()
    {
        if (car == null)
        {
            car = FindObjectOfType<CarController>();
            return;
        }

        speedText.text = Mathf.Round(car.CurrentSpeedKmh) + " km/h";
    }
}
