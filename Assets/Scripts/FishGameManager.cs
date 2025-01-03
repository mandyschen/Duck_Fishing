using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FishGameManager : MonoBehaviour
{
    public GameObject gamePanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI directionText;
    public int targetScore = 2000;
    public int initialScore = 1000;

    public FishManager fishManager;
    public GameManager gameManager;
    public TargetBoxMovement targetBoxMovement;
    public float commonSpeed = 200f;
    public float rareSpeed = 500f;
    public float legendarySpeed = 1000f;

    private int score = 0;
    private bool gameStarted = false;
    private Fish targetFish;
    private bool nearFish = false;

    public Image[] fishUIImage;
    public Image emptySlot;
    public Canvas uiCanvas;

    public void SellFish()
    {
        foreach (Image im in fishUIImage)
        {
            im.sprite = emptySlot.sprite;
        }
    }

    bool MoveFishToUI(int index)
    {
        if (targetFish != null && fishUIImage != null)
        {
            SpriteRenderer fishSpriteRenderer = targetFish.GetComponent<SpriteRenderer>();
            
            if (index < fishUIImage.Length)
            {
                fishUIImage[index].sprite = fishSpriteRenderer.sprite;
                return true;
            }
            else
            {
                directionText.text = "You have too many fish! Sell first.";
                return false;
            }
        }
        return false;
    }

    void Update()
    {
        if (nearFish && Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            StartGame();
            if (targetFish != null)
            {
                // Destroy(targetFish.transform.root.gameObject);
                // MoveFishToUI();
                fishManager.SpawnFish();
                fishManager.DestroyFish();
            }
        }

        if (gameStarted)
        {
            scoreText.text = "Score: " + score;

            if (score >= targetScore)
            {
                EndGame(true);
            }
            else if (score <= 0)
            {
                EndGame(false);
            }
        }
    }

    public void SetNearFish(bool isNear, Fish fish)
    {
        nearFish = isNear;
        targetFish = fish;
    }

    void StartGame()
    {
        gameStarted = true;
        gamePanel.SetActive(true);
        score = initialScore;
        if(targetFish.fishData.difficulty == 1)
        {
            targetBoxMovement.speed = commonSpeed;
        }
        else if(targetFish.fishData.difficulty == 2)
        {
            targetBoxMovement.speed = rareSpeed;
        }
        else{
            targetBoxMovement.speed = legendarySpeed;
        }
    }

    void EndGame(bool success)
    {
        gameStarted = false;
        gamePanel.SetActive(false);

        if (success)
        {
            int index = gameManager.GetCollectedFish();
            if(MoveFishToUI(index))
            {
                gameManager.IncrementFish(targetFish);
            }
            directionText.text = "Game won!";

        }
        else
        {
            directionText.text = "Game lost!";
            
        }
        Destroy(targetFish.transform.root.gameObject);
    }

    public void IncreaseScore()
    {
        if (gameStarted)
        {
            score++;
        }
    }

    public void DecreaseScore()
    {
        if (gameStarted && score > 0)
        {
            score--;
        }
    }
}
