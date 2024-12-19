using UnityEngine;
using TMPro;

public class FishGameManager : MonoBehaviour
{
    public GameObject gamePanel;
    public TextMeshProUGUI scoreText;
    public int targetScore = 2000;
    public int initialScore = 1000;

    public FishManager fishManager;
    public GameManager gameManager;

    private int score = 0;
    private bool gameStarted = false;
    private GameObject targetFish;
    private bool nearFish = false;

    void Update()
    {
        if (nearFish && Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            StartGame();
            if (targetFish != null)
            {
                Destroy(targetFish);
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

    public void SetNearFish(bool isNear, GameObject fish)
    {
        nearFish = isNear;
        targetFish = fish;
    }

    void StartGame()
    {
        gameStarted = true;
        gamePanel.SetActive(true);
        score = initialScore;
    }

    void EndGame(bool success)
    {
        gameStarted = false;
        gamePanel.SetActive(false);

        if (success)
        {
            Debug.Log("Game Won!");
            gameManager.IncrementFish();
        }
        else
        {
            Debug.Log("Game Lost!");
        }
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
