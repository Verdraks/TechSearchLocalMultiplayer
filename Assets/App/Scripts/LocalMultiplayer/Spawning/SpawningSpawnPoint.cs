using UnityEngine;
using UnityEngine.Serialization;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_SpawningSpawnPoint", menuName = "SSO/LocalMultiplayer/SpawningStrategy/SpawningSpawnPoint")]
    public class SpawningSpawnPoint : SpawningStrategy
    {
        [FormerlySerializedAs("spawnPoint")] public RSO_Position position;
        
        public override Vector3 GetSpawnPosition()
        {
            return position.Value;
        }
    }
}