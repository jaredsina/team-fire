using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float spawnInterval = 3.0f;
    private float timer;
    private float stopwatch;
    private bool spawndebounced = false;
	public Transform[] SpawnPoints;
    public GameObject[] foodModels;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    IEnumerator pauseGame(float time)
    {
        Debug.Log(time);
        if (time >= 15.0f)
        {
            spawndebounced = true;
            Debug.Log("pausing");
            yield return new WaitForSeconds(10.0f);
            spawndebounced = false;
            spawnInterval = 2.0f;
            stopwatch = 0;
        }   
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && !spawndebounced)
        {
            SpawnNPC();
            timer = 0;
        }
        // NPC Spawn Position 
        // Timer spawning NPCs
        // How to spawn NPC
        
        stopwatch += Time.deltaTime;
        StartCoroutine(pauseGame(stopwatch));

    }

    // Update is called once per frame
    void SpawnNPC()
    {
		int index = Random.Range(0, SpawnPoints.Length);
        int index2 = Random.Range(0, foodModels.Length);
        Instantiate(foodModels[index2], SpawnPoints[index].position, SpawnPoints[index].rotation);

    }
}
