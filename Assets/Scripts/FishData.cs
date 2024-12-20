using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewFish", menuName = "Fish/Create New Fish")]
public class FishData : ScriptableObject
{
    public string fishName;
    public Sprite fishSprite;
    public int price;
    public int difficulty;
    public int rarity;        // Lower values are rarer
}