using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private LayerMask tagTargets;
    [SerializeField] private float delayShoot;
    [SerializeField] private float radiusShoot;
    [SerializeField] private float forceShoot;

    private bool _canShoot = true;
    private RaycastHit2D[] _hitsCache = new RaycastHit2D[10];

    
    public void TryShoot(Vector2 positionShoot)
    {
        if (!_canShoot) return;
        Shoot(ref positionShoot);
    }

    private void Shoot(ref Vector2 positionShoot)
    {
        _canShoot = false;
        StartCoroutine(Utils.Delay(delayShoot, () => _canShoot = true));

        int threshold =Physics2D.CircleCastNonAlloc(positionShoot, radiusShoot, Vector2.zero, _hitsCache, Mathf.Infinity, tagTargets);
        if (threshold > 0)
        {
            for (int i = 0; i < threshold; i++)
            {
                _hitsCache[i].rigidbody.AddForce((_hitsCache[i].transform.position-(Vector3)positionShoot).normalized * forceShoot,ForceMode2D.Impulse);
            }
        }
    }
}