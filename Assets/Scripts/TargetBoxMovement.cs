using UnityEngine;

public class TargetBoxMovement : MonoBehaviour
{
    public float speed = 200f;
    private RectTransform rectTransform;
    private float direction = 1f;
    private float minY, maxY;

    private float randomChangeTime = 2f;
    private float randomChangeTimer = 0f;
    private float randomChangeChance = 0.5f; 

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        RectTransform panelRect = transform.parent.GetComponent<RectTransform>();

        minY = -panelRect.rect.height / 2 + rectTransform.rect.height / 2;
        maxY = panelRect.rect.height / 2 - rectTransform.rect.height / 2;
    }

    void Update()
    {
        Debug.Log("Speed: " + speed);
        rectTransform.anchoredPosition += new Vector2(0, direction * speed * Time.deltaTime);

        randomChangeTimer += Time.deltaTime;

        if (randomChangeTimer >= randomChangeTime)
        {
            if (Random.Range(0f, 1f) < randomChangeChance)
            {
                direction *= -1f;
            }

            randomChangeTimer = 0f;
        }

        if (rectTransform.anchoredPosition.y >= maxY || rectTransform.anchoredPosition.y <= minY)
        {
            direction *= -1f;
        }
    }
}
