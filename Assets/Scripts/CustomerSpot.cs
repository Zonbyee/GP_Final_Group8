using UnityEngine;

public class CustomerSpot : MonoBehaviour
{
    private bool isOccupied = false;
    public Customer currentCustomer;
    
    public bool IsOccupied => isOccupied;
    
    public void OccupySpot(Customer customer)
    {
        isOccupied = true;
        currentCustomer = customer;
    }
    
    public void ReleaseSpot()
    {
        isOccupied = false;
        currentCustomer = null;
    }
    
    // Visual feedback in editor
    void OnDrawGizmos()
    {
        Gizmos.color = isOccupied ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}