using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        int index = PlayerPrefs.GetInt("SelectedCar", 0);

        if (index < 0 || index >= carPrefabs.Length)
            index = 0;

        Instantiate(
            carPrefabs[index],
            spawnPoint.position,
            spawnPoint.rotation
        );
        Debug.Log("Spawning car index: " + index);
    }
    
}
