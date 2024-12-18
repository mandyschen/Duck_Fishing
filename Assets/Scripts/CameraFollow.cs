using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        Vector3 newCameraPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        transform.position = newCameraPosition;
    }
}
