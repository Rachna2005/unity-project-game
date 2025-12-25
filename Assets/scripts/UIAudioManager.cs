using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }
}
