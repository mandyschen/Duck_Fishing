using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal"); // Left/Right Arrow or A/D
        movement.y = Input.GetAxis("Vertical");   // Up/Down Arrow or W/S
    }

    void FixedUpdate()
    {
        // No movement because player stays centered
    }
}
