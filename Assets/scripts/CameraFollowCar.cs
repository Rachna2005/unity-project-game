using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public Transform target;              // The car
    public Vector3 offset = new Vector3(0f, 4f, -8f);
    public float followSmooth = 6f;
    public float rotateSmooth = 8f;

    void LateUpdate()
    {
        if (!target) return;

        // Calculate position BEHIND the car (local space)
        Vector3 desiredPosition = target.TransformPoint(offset);

        // Smooth position follow
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            followSmooth * Time.deltaTime
        );

        // Smooth rotation to look in same direction as car
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
