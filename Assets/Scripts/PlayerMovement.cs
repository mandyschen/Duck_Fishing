using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    public FishGameManager gameManager;
    public SpriteRenderer background;

    private Vector2 backgroundMinBounds;
    private Vector2 backgroundMaxBounds;

    public SpriteRenderer playerSprite;
    public Sprite[] availableSprites;
    public int[] pricesList;
    private int currSpriteIndex = 0;

    public int GetPrice(int index)
    {
        if (index >= 0 && index < availableSprites.Length)
        {
            return pricesList[index];
        }
        else
        {
            Debug.LogError("Invalid index!");
        }
        return 0;
    }

    public void ChangeSprite(int index)
    {
        if (index >= 0 && index < availableSprites.Length)
        {
            playerSprite.sprite = availableSprites[index];
            playerSprite.sortingOrder = 1;
            currSpriteIndex = index;
        }
        else
        {
            Debug.LogError("Invalid sprite index!");
        }
    }

    public void ZeroPrice(int index)
    {
        pricesList[index] = 0;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<FishGameManager>();
        }

        // Calculate the bounds of the background sprite in world space
        if (background != null)
        {
            Vector3 minBounds = background.bounds.min;
            Vector3 maxBounds = background.bounds.max;
            backgroundMinBounds = new Vector2(minBounds.x, minBounds.y);
            backgroundMaxBounds = new Vector2(maxBounds.x, maxBounds.y);
        }
        else
        {
            Debug.LogWarning("Background SpriteRenderer is not assigned!");
        }
    }

    void Update()
    {
        // Get input direction from arrow keys or WASD
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Calculate the target position
        Vector3 targetPosition = rb.position + (Vector2)(movementInput.normalized * moveSpeed * Time.fixedDeltaTime);

        // Clamp the position within the background bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, backgroundMinBounds.x, backgroundMaxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, backgroundMinBounds.y, backgroundMaxBounds.y);

        // Update the player's position
        rb.MovePosition(targetPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            gameManager.SetNearFish(true, other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            gameManager.SetNearFish(false, null);
        }
    }
}
