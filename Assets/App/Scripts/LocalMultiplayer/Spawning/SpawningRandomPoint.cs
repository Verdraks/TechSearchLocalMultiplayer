using UnityEngine;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_SpawningRandomPoint", menuName = "SSO/LocalMultiplayer/SpawningStrategy/SpawningRandomPoint")]
    public class SpawningRandomPoint : SpawningStrategy
    {
        public Vector3[] points;

        public override Vector3 GetSpawnPosition()
        {
            return points[Random.Range(0, points.Length)];
        }
    }
}