using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private CustomerSpot targetSpot;
    private bool hasArrived = false;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    public void SetDestination(CustomerSpot spot)
    {
        targetSpot = spot;
        agent.SetDestination(spot.transform.position);
        spot.OccupySpot(this);
    }
    
    void Update()
    {
        // Only check if not already arrived
        if (!hasArrived && targetSpot != null && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                hasArrived = true; // Set flag before calling
                OnReachedSpot();
            }
        }
    }
    
    private void OnReachedSpot()
    {
        // You can add logic here for what happens when customer reaches the spot
        Debug.Log("Customer reached spot: " + targetSpot.name);
    }
    
    void OnDestroy()
    {
        if (targetSpot != null)
        {
            targetSpot.ReleaseSpot();
        }
    }
}