using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PencilAbility : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private LayerMask layerMaskDraw;
    [SerializeField] private float maxPencilInk;
    [SerializeField] private float pencilInkSpeedRefill;
    [SerializeField] private float pencilInkCost;
    
    [Header("References")]
    [SerializeField] private GameObject drawPrefab;
    
    private bool _isDrawing;
    private bool _isInDrawingArea;
    private float _pencilInk;
    private Vector2 _drawPosition;

    private readonly List<Vector2> _points = new();
    private readonly RaycastHit2D[] _hitsAreaDraw = new RaycastHit2D[1];
    private Shape _shape;
    
    private const float DistancePointThreshold = 0.1f;


    private void Awake() => _pencilInk = maxPencilInk/2;

    public void PencilMove(Vector2 position)
    {
        _drawPosition = position;
    }

    public void Draw()
    {
        _isDrawing = !_isDrawing;
        if (_isDrawing)
        {
            CheckPencilInDrawingArea();
            StartCoroutine(DrawShape());
        }
    }

    private void CheckPencilInDrawingArea()
    {
        _isInDrawingArea = Physics2D.RaycastNonAlloc(_drawPosition, Vector2.zero,_hitsAreaDraw ,Mathf.Infinity,layerMaskDraw) > 0;
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
        
        while (_isDrawing && _pencilInk >= pencilInkCost && _isInDrawingArea)
        {
            
            if (_points.Count == 0 || (_points[^1] - _drawPosition).sqrMagnitude > DistancePointThreshold)
            {
                _points.Add(_drawPosition);
                _shape.UpdateShape(_points.Count,_drawPosition);
                _pencilInk -= pencilInkCost;
            }
            yield return null;
            CheckPencilInDrawingArea();
        }
        
        if (_points.Count < 2) Destroy(_shape.gameObject);
        else
        {
            _shape.ReleaseShape(_points.ToArray());
        }
    }
    
}