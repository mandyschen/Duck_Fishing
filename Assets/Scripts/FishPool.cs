using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    public List<FishData> commonFish;
    public List<FishData> rareFish;
    public List<FishData> legendaryFish;

    public FishData GetRandomFish(int rarityLevel)
    {
        List<FishData> selectedPool = commonFish;

        if (rarityLevel > 5)
            selectedPool = rareFish;
        else if (rarityLevel > 10)
            selectedPool = legendaryFish;

        if (selectedPool.Count == 0) return null;

        return selectedPool[Random.Range(0, selectedPool.Count)];
    }
}
