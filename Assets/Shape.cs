using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shape : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private PolygonCollider2D polygonCollider;

    private const float Lifetime = 3f;
    private const float MassMultiplier = 1.5f;
    private const float MassBase = 1f;
    
    private void Awake()
    {
        rb.isKinematic = true;
    }

    public void InitializeShape()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    public void UpdateShape(int pointCount, Vector2 newPointPosition)
    {
        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPointPosition);
    }

    public void ReleaseShape(Vector2[] points)
    {
        polygonCollider.pathCount = 1;
        polygonCollider.SetPath(0, points);
        rb.isKinematic = false;
        lineRenderer.useWorldSpace = false;
        
        rb.mass = points.Length * MassMultiplier + MassBase;
        
        StartCoroutine(Utils.Delay(Lifetime,()=>Destroy(gameObject)));
    }
    
}


