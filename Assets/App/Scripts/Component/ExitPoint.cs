using System;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string nextScene;
    
    [Header("Output")]
    [SerializeField] private RSE_LoadSceneAdditive rseLoadScene;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDestructor"))
        {
            if (String.IsNullOrEmpty(nextScene)) return;
            rseLoadScene.Call(nextScene);
            other.gameObject.SetActive(false);
        }
    }
}
