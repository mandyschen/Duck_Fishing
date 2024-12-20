using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    public FishGameManager gameManager;
    public SpriteRenderer background;
    public GameObject fishGamePanel;

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

        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;
        backgroundMinBounds = new Vector2(minBounds.x, minBounds.y);
        backgroundMaxBounds = new Vector2(maxBounds.x, maxBounds.y);
    }

    void Update()
    {
        if (fishGamePanel == null || !fishGamePanel.activeSelf)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector3 targetPosition = rb.position + (Vector2)(movementInput.normalized * moveSpeed * Time.fixedDeltaTime);

            targetPosition.x = Mathf.Clamp(targetPosition.x, backgroundMinBounds.x, backgroundMaxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, backgroundMinBounds.y, backgroundMaxBounds.y);

            rb.MovePosition(targetPosition);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Enter fish.");
            Fish fishComponent = other.GetComponent<Fish>();
            if (fishComponent != null)
            {
                gameManager.SetNearFish(true, fishComponent);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Exit fish.");
            gameManager.SetNearFish(false, null);
        }
    }
}
