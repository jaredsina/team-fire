using UnityEngine;
using UnityEngine.AI; // Required for NavMesh features

public class NPCMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    void Update()
    {
        // Continuously update the destination to the target's position
        if(Input.GetMouseButtonDown(1))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
