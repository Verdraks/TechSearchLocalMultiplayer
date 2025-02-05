using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilAbility : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private float maxPencilInk;
    [SerializeField] private float pencilInkSpeedRefill;
    [SerializeField] private float pencilInkCost;
    
    [Header("References")]
    [SerializeField] private GameObject drawPrefab;
    
    private bool _isDrawing;

    private readonly List<Vector2> _points = new();
    private Shape _shape;
    
    private const float DistancePointThreshold = 0.1f;

    private float _pencilInk;

    private void Awake() => _pencilInk = maxPencilInk/2;

    public void Move(Vector2 targetPosition)
    {
        Debug.Log(message: targetPosition);
    }

    public void Draw()
    {
        _isDrawing = !_isDrawing;
        if (_isDrawing) StartCoroutine(DrawShape());
    }

    private void Update()
    {
        if (!_isDrawing)
        {
            RefillInk();
        }
    }

    private void RefillInk()
    {
        _pencilInk = Mathf.Min(_pencilInk + pencilInkSpeedRefill * Time.deltaTime, maxPencilInk);
    }


    private IEnumerator DrawShape()
    {
        _points.Clear();

        _shape = Instantiate(drawPrefab).GetComponent<Shape>();
        _shape.InitializeShape();
        
        while (_isDrawing && _pencilInk >= pencilInkCost)
        {
            Vector2 mousePosition = Macro2D.MousePosWorld;
            
            if (_points.Count == 0 || (_points[^1] - mousePosition).sqrMagnitude > DistancePointThreshold)
            {
                _points.Add(mousePosition);
                _shape.UpdateShape(_points.Count,mousePosition);
                _pencilInk -= pencilInkCost;
            }
            yield return null;
        }
        
        if (_points.Count < 2) Destroy(_shape.gameObject);
        else
        {
            _shape.ReleaseShape(_points.ToArray());
        }
    }
    
}