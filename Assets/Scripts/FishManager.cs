using UnityEngine;
using System.Collections.Generic;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numberOfFish = 3;
    public float spawnRangeX = 7.5f;
    public float spawnRangeY = 4.5f;
    public float minDistanceBetweenFish = 1.5f;
    public SpriteRenderer background;

    private List<GameObject> activeFish = new List<GameObject>();

    private void Start()
    {
        SpawnInitialFish();
    }

    void SpawnInitialFish()
    {
        for (int i = 0; i < numberOfFish; i++)
        {
            SpawnFish();
        }
    }

    public void SpawnFish()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        while (!IsValidPosition(spawnPosition))
        {
            spawnPosition = GetRandomSpawnPosition();
        }

        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
        newFish.SetActive(true);

        activeFish.Add(newFish);
    }

    public void DestroyFish()
    {
        activeFish.RemoveAll(fish => fish == null);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Bounds bounds = background.bounds;

        Vector3 fishSize = Vector3.zero;
        if (fishPrefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer fishRenderer))
        {
            fishSize = fishRenderer.bounds.size;
        }

        float spawnMinX = bounds.min.x + fishSize.x / 2;
        float spawnMaxX = bounds.max.x - fishSize.x / 2;

        float spawnMinY = bounds.min.y + fishSize.y / 2;
        float spawnMaxY = bounds.max.y - fishSize.y / 2;

        float randomX = Random.Range(spawnMinX, spawnMaxX);
        float randomY = Random.Range(spawnMinY, spawnMaxY);

        return new Vector3(randomX, randomY, 0f);
    }

    bool IsValidPosition(Vector3 position)
    {
        foreach (GameObject fish in activeFish)
        {
            if (fish != null && Vector3.Distance(position, fish.transform.position) < minDistanceBetweenFish)
            {
                return false;
            }
        }
        return true;
    }
}