using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ParallaxEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParallaxLayer[] layers;
    [Space(10)]
    [SerializeField] private RSO_Position rsoPosition;
    
    private Vector3 oldPosition;

    private void Start()
    {
        oldPosition = rsoPosition.Value;
    }

    private void Update() => UpdateEffect();

    private void UpdateEffect()
    {
        if (Mathf.Approximately(oldPosition.x, rsoPosition.Value.x)) return;
        float delta = oldPosition.x - rsoPosition.Value.x;
        oldPosition = rsoPosition.Value;
        
        foreach (ParallaxLayer layer in layers)
        {
            Vector3 newPosition = layer.transform.localPosition;
            newPosition.x += delta * layer.parallaxFactor * Time.deltaTime;
            layer.transform.localPosition = newPosition;
        }
    }
    
}

[System.Serializable]
public struct ParallaxLayer
{
    public Transform transform;
    public float parallaxFactor;
}


