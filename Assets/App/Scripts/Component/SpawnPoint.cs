using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private bool spawnPointTrigger;
    
    [Header("References")]
    [SerializeField] private RSO_SpawnPoint spawnPoint;
    
    private void Awake()
    {
        if (spawnPointTrigger) return;
        spawnPoint.Value = transform.position;
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        spawnPoint.Value = transform.position;
        enabled = false;
    }
    
}