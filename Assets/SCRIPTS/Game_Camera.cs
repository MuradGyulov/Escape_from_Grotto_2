using UnityEngine;
using YG;

public class Game_Camera : MonoBehaviour
{
    [SerializeField] float followingSpeed;
    [SerializeField] Vector3 cameraOffset;

    private GameObject player;
    private Transform target;
    private Camera thisCamera;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target =  player.GetComponent<Transform>();

        thisCamera = GetComponent<Camera>();
        thisCamera.orthographicSize = YandexGame.savesData.cameraSize;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, followingSpeed);
        transform.position = smoothPosition;
    }
}
