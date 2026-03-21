using UnityEngine;
using UnityEngine.UI; // Add this for UI components
using UnityEngine.InputSystem;

public class VendingMachine : MonoBehaviour
{
    public GameObject player;
    public float interactionDistance = 4f;
    public GameObject vendingItem; 
    public float spinspeed = 7;

    
    private GameObject gameManager;
    private GoldScript goldScript; 
    private bool playerInRange = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        goldScript = gameManager.GetComponent<GoldScript>();

    }

    void Update()
    {
        Vector3 machinePos = gameObject.transform.position;
        Vector3 playerPos = player.transform.position;
        float distance = Vector3.Distance(machinePos, playerPos);
        // Debug.Log(distance);
        
        // Check if player is in range
        playerInRange = distance <= interactionDistance;
        
        // Check for interaction input
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            
            PurchaseItem(1);          
        }
        

    }
    
    void PurchaseItem(int itemIndex)
{
    if (goldScript.GetGoldAmount() >= 10)
    {
        Debug.Log("Purchased item: " + itemIndex);
        goldScript.ChangeGold(-10);

        // Instantiate the purchased item near the player
        if (vendingItem != null)
        {
            Vector3 spawnPos = player.transform.position + player.transform.right * 1.5f;
            Instantiate(vendingItem, spawnPos, player.transform.rotation);
            Renderer rend = vendingItem.GetComponent<Renderer>();
            rend.material.EnableKeyword("_EMISSION");
            rend.material.SetColor("_EmissionColor", Color.cyan * 5f);
            transform.Rotate(Vector3.up,spinspeed*Time.deltaTime,Space.World);


        }

    }
}
}