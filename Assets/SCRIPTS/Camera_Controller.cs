using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followingSpeed;
    [SerializeField] Vector3 cameraOffset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, followingSpeed);
        transform.position = smoothPosition;
    }
}
