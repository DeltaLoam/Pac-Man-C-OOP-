using UnityEngine;

public class BigDot : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("PacMan touched a dot");
        GameManager.Instance.PowerDotCollected(); // Trigger hunt state
        Destroy(gameObject);                      // Remove big dot
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return; // Only PacMan collects
        Collect();
    }
}
