using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] cars;
    private int currentIndex = 0;
    public float rotationSpeed = 50f;

    void Start()
    {
        ShowCar(currentIndex);
    }

    void Update()
    {
        if (cars.Length > 0 && cars[currentIndex] != null)
        {
            cars[currentIndex].transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void ShowCar(int index)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == index);
        }
    }

    public void RightArrow()
    {
        currentIndex = (currentIndex + 1) % cars.Length;
        ShowCar(currentIndex);
    }

    public void LeftArrow()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = cars.Length - 1;
        ShowCar(currentIndex);
    }

    // 🔴 THIS IS THE KEY PART
    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectedCar", currentIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameScene");
    }
}
