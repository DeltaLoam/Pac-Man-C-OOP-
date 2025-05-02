using UnityEngine;

public class Dot : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        Debug.Log("PacMan touched a dot");
        GameManager.Instance.DotCollected(); // Notify GameManager
        Destroy(gameObject);                 // Remove dot from scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return; // Only PacMan can collect
        Collect();
    }
}
