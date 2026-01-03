using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;

    public float spawnTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpawnRandomFood();
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomFood();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnRandomFood()
    {
        GameObject selectedFood = prefabs[Random.Range(0, prefabs.Count)];

        Vector3 spawnPosition = new Vector3(Random.Range(11.735f, 15.785f), 60.999f, -86.268f);

        Instantiate(selectedFood, spawnPosition, Quaternion.identity);
    }
}
