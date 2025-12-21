using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // pause the game
    }
}
