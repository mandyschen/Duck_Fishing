using UnityEngine;
using System.Collections.Generic;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numberOfFish = 3;
    public float spawnRangeX = 7.5f;
    public float spawnRangeY = 4.5f;
    public float minDistanceBetweenFish = 1.5f;
    public Camera mainCamera;

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
        float camX = mainCamera.transform.position.x;
        float camY = mainCamera.transform.position.y;

        float randomX = Random.Range(camX - spawnRangeX, camX + spawnRangeX);
        float randomY = Random.Range(camY - spawnRangeY, camY + spawnRangeY);

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
