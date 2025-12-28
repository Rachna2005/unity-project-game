using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    CarController car;

    void Update()
    {
        if (car == null)
        {
            car = FindObjectOfType<CarController>();
            return;
        }

        livesText.text = "Lives: " + car.lives;
    }
}
