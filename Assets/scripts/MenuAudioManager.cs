using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    static MenuAudioManager instance;

    void Awake()
    {
        // Prevent duplicates
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
