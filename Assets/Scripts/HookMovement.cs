using UnityEngine;

public class HookMovement : MonoBehaviour
{
    public FishGameManager gameManager;
    public float gravity = 300f;
    public float jumpForce = 500f;
    private RectTransform rectTransform;
    private float minY, maxY;
    private float verticalVelocity = 0f;

    private bool isTouching = false;
    private float scoreCooldown = 0.05f;
    private float nextScoreTime = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TargetBox"))
        {
            isTouching = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TargetBox"))
        {
            isTouching = false;
        }
    }

    void Update()
    {
        if (Time.time >= nextScoreTime)
        {
            nextScoreTime = Time.time + scoreCooldown;
            if (!isTouching)
            {
                gameManager.DecreaseScore();
            }
            else
            {
                gameManager.IncreaseScore();
            }
        }
        verticalVelocity -= gravity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
        }

        float newY = rectTransform.anchoredPosition.y + verticalVelocity * Time.deltaTime;

        if (newY >= maxY)
        {
            newY = maxY;
            verticalVelocity = -verticalVelocity / 2;
        }

        if (newY <= minY)
        {
            newY = minY;
            verticalVelocity = 0f;
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        RectTransform panelRect = transform.parent.GetComponent<RectTransform>();

        minY = -panelRect.rect.height / 2 + rectTransform.rect.height / 2;
        maxY = panelRect.rect.height / 2 - rectTransform.rect.height / 2;
    }
}
