using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gamePanel;
    public TextMeshProUGUI scoreText;
    public int targetScore = 10;
    private int score = 0;
    private bool gameStarted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted)
        {
            StartGame();
        }

        if (gameStarted)
        {
            scoreText.text = "Score: " + score;

            if (score >= targetScore)
            {
                EndGame();
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;
        gamePanel.SetActive(true); 
        score = 0;
        Debug.Log("Game Started");
    }

    void EndGame()
    {
        gameStarted = false;
        gamePanel.SetActive(false);
        Debug.Log("Game Ended");
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
