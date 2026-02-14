using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 machinePos = gameObject.transform.position;
        Vector3 playerPos = player.transform.position;
        float distance = Vector3.Distance(machinePos,playerPos);
        Debug.Log(distance);
    }
}
