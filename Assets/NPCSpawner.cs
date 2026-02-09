using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public float spawnInterval = 3.0f;
    private float timer;
	public Transform[] SpawnPoints;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNPC();
            timer = 0;
        }
        // NPC Spawn Position 
        // Timer spawning NPCs
        // How to spawn NPC
    }

    // Update is called once per frame
    void SpawnNPC()
    {
		int index = Random.Range(0, SpawnPoints.Length);
        Instantiate(npcPrefab, SpawnPoints[index].position, SpawnPoints[index].rotation);
    }
}
