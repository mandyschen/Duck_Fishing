using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float backgroundMoveSpeed = 2f;
    
    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = Vector3.zero; // Player stays at the center
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * backgroundMoveSpeed * Time.deltaTime;
        transform.position -= movement;
    }
}
