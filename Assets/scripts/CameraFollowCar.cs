using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4f, -8f);
    public float followSmooth = 6f;
    public float rotateSmooth = 8f;

    void LateUpdate()
    {
        // 🔴 Auto-find car if not assigned
        if (!target)
        {
            CarController car = FindObjectOfType<CarController>();
            if (car)
                target = car.transform;
            else
                return;
        }

        // Position behind car (local space)
        Vector3 desiredPosition = target.TransformPoint(offset);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSmooth * Time.deltaTime
        );

        // Rotate to face same direction as car
        Quaternion desiredRotation = Quaternion.LookRotation(
            target.forward,
            Vector3.up
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotateSmooth * Time.deltaTime
        );
    }
}
