using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowTarget : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform transformFollower;
    [SerializeField] private float smoothTimeFollow = 0.1f;

    private Vector3 _velocity = Vector3.zero;
    
    public void Follow(Vector2 targetPosition)
    {
        transformFollower.position = Vector3.SmoothDamp(transformFollower.position,targetPosition, ref _velocity, smoothTimeFollow);
    }
    
}
