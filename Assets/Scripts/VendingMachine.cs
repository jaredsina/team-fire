using UnityEngine;
using UnityEngine.UI; // Add this for UI components
using UnityEngine.InputSystem;

public class VendingMachine : MonoBehaviour
{
    public GameObject player;
    public float interactionDistance = 3f;
    
    
    private GameObject gameManager;
    private GoldScript goldscript; 
    private bool playerInRange = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        goldscript = gameManager.GetComponent<GoldScript>();

    }

    void Update()
    {
        Vector3 machinePos = gameObject.transform.position;
        Vector3 playerPos = player.transform.position;
        float distance = Vector3.Distance(machinePos, playerPos);
        
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
        if (goldscript.GetGoldAmount() >= 10){

            Debug.Log("Purchased item: " + itemIndex);
            goldscript.ChangeGold(-10);
        }
    }
}