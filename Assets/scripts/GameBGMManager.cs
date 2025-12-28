using UnityEngine;

public class GameBGMManager : MonoBehaviour
{
    static GameBGMManager instance;

    void Awake()
    {
        // Prevent duplicate music if scene reloads
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
