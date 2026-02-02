using UnityEngine;
using UnityEngine.AI; // Required for NavMesh features

public class NPCMovement : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    void Update()
    {
        agent.SetDestination(player.position);
        // Continuously update the destination to the target's position
        // 
        //         if(Input.GetMouseButtonDown(1))
        //         {
        //             Ray movePosition = Camera.main.ScreenPointToRay(new Vector3(100,100,100));
        //             if (Physics.Raycast(movePosition, out var hitInfo))
        //             {
        //                 agent.SetDestination(hitInfo.point);
        //             }
        //         }
        // 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Hit: ");
        }
    }
}


    


