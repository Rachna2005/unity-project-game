// using UnityEngine;

<<<<<<< HEAD
// public class CarSelection : MonoBehaviour
// {
//     public GameObject[] cars;   
//     private int currentIndex = 0;

//     void Start()
//     {
//         ShowCar(currentIndex);
//     }

//     void ShowCar(int index)
//     {
//         for (int i = 0; i < cars.Length; i++)
//         {
//             cars[i].SetActive(i == index);
//         }
//     }
=======
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
        // Rotate only the current car
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
>>>>>>> ebfc72f661a7b989703e5f97d896b74b42c6988c

//      public void RightArrow()
//     {
//         currentIndex++;

//         if (currentIndex >= cars.Length)
//             currentIndex = 0;

<<<<<<< HEAD
//         ShowCar();
//     }
=======
        ShowCar(currentIndex);
    }
>>>>>>> ebfc72f661a7b989703e5f97d896b74b42c6988c

//     public void LeftArrow()
//     {
//         currentIndex--;

//         if (currentIndex < 0)
//             currentIndex = cars.Length - 1;

<<<<<<< HEAD
//         ShowCar();
//     }
// }
=======
        ShowCar(currentIndex);
    }
}
>>>>>>> ebfc72f661a7b989703e5f97d896b74b42c6988c
