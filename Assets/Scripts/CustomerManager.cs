using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Prefab and Spawn")]
    public GameObject customerPrefab;
    public Transform spawnPoint; // Point A
    
    [Header("Customer Spots")]
    public List<CustomerSpot> customerSpots = new List<CustomerSpot>(); // Points B, C, D, E
    
    [Header("Spawn Settings")]
    public float spawnInterval = 3f;
    private float spawnTimer = 0f;
    
    void Update()
    {
        // Auto-spawn customers for testing
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnCustomer();
            spawnTimer = 0f;
        }
    }
    
    public void SpawnCustomer()
    {
        CustomerSpot availableSpot = FindAvailableSpot();
        
        if (availableSpot == null)
        {
            Debug.Log("No available spots for customer!");
            return;
        }
        
        // Spawn customer at point A
        GameObject customerObj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        Customer customer = customerObj.GetComponent<Customer>();
        
        // Send customer to available spot
        customer.SetDestination(availableSpot);
    }
    
    private CustomerSpot FindAvailableSpot()
    {
        foreach (CustomerSpot spot in customerSpots)
        {
            if (!spot.IsOccupied)
            {
                return spot;
            }
        }
        return null;
    }
}