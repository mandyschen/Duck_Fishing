using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Auto-assign GameManager if not set in Inspector
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    void Update()
    {
        // Input direction from arrow keys or WASD
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = movementInput.normalized * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Player: TOUCH FISH");
            gameManager.SetNearFish(true, other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Player: Left Fish");
            gameManager.SetNearFish(false, null);
        }
    }
}
