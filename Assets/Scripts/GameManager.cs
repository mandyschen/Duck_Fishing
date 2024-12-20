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
    public FishGameManager fishGameManager;

    private int collectedFish = 0;
    private List<Fish> collectedFishList = new List<Fish>();
    private int coins = 0;
    // private int fishPrice = 10;

    public int GetCollectedFish()
    {
        return collectedFish;
    }

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

    public void IncrementFish(Fish targetFish)
    {
        collectedFishList.Add(targetFish);
        collectedFish++;
        collectedFishText.text = "Fish: " + collectedFish;
    }

    public void SellFish()
    {
        // coins += collectedFish * fishPrice;
        foreach (Fish targetFish in collectedFishList)
        {
            coins += targetFish.GetPrice();
        }
        collectedFishList.Clear();
        coinsText.text = "Coins: " + coins;
        collectedFish = 0;
        collectedFishText.text = "Fish: " + collectedFish;
        fishGameManager.SellFish();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
