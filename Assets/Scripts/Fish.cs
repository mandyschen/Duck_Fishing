using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishData fishData;

    public void Setup(FishData data)
    {
        fishData = data;
        GetComponent<SpriteRenderer>().sprite = data.fishSprite;
    }

    public int GetPrice()
    {
        return fishData.price;
    }

    public string GetName()
    {
        return fishData.name;
    }

    public int GetRarity()
    {
        return fishData.rarity;
    }
}
