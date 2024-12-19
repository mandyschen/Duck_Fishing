using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI collectedFishText;
    public TextMeshProUGUI coinsText;
    public GameObject shopPanel;
    public PlayerMovement playerMovement;

    private int collectedFish = 0;
    private int coins = 0;
    private int fishPrice = 10;

    public void ChangeSprite(int index)
    {
        int cost = playerMovement.GetPrice(index);
        if (coins >= cost)
        {
            playerMovement.ChangeSprite(index);
            coins -= cost;
            coinsText.text = "Coins: " + coins;
            playerMovement.ZeroPrice(index);
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
    }

    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }

    public void IncrementFish()
    {
        collectedFish++;
        collectedFishText.text = "Fish: " + collectedFish;
    }

    public void SellFish()
    {
        coins += collectedFish * fishPrice;
        coinsText.text = "Coins: " + coins;
        collectedFish = 0;
        collectedFishText.text = "Fish: " + collectedFish;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
