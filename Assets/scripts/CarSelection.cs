// using UnityEngine;

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

//      public void RightArrow()
//     {
//         currentIndex++;

//         if (currentIndex >= cars.Length)
//             currentIndex = 0;

//         ShowCar();
//     }

//     public void LeftArrow()
//     {
//         currentIndex--;

//         if (currentIndex < 0)
//             currentIndex = cars.Length - 1;

//         ShowCar();
//     }
// }
