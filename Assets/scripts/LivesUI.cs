using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public CarController car;

    void Update()
    {
        if (car != null)
        {
            livesText.text = "Lives: " + car.lives;
        }
    }
}
