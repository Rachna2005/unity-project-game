using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject winPanel;

    void Start()
    {
        Time.timeScale = 1f;

        if (gameOverCanvas) gameOverCanvas.SetActive(false);
        if (winPanel) winPanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        Debug.Log("GAME OVER");

        Time.timeScale = 0f; // freeze game

        if (gameOverCanvas)
            gameOverCanvas.SetActive(true);
    }

    public void ShowWin()
    {
        Debug.Log("YOU WIN");

        Time.timeScale = 0f; // freeze game

        if (winPanel)
            winPanel.SetActive(true);
    }
}
