using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPoint : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private bool spawnPointTrigger;
    
    [FormerlySerializedAs("spawnPoint")]
    [Header("References")]
    [SerializeField] private RSO_Position position;
    
    private void Awake()
    {
        if (spawnPointTrigger) return;
        position.Value = transform.position;
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        position.Value = transform.position;
        enabled = false;
    }
    
}