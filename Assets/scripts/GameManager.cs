using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int selectedCarIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // IMPORTANT
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
