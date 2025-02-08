using UnityEngine;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_SpawningSpawnPoint", menuName = "SSO/LocalMultiplayer/SpawningStrategy/SpawningSpawnPoint")]
    public class SpawningSpawnPoint : SpawningStrategy
    {
        public RSO_SpawnPoint spawnPoint;
        
        public override Vector3 GetSpawnPosition()
        {
            return spawnPoint.Value;
        }
    }
}