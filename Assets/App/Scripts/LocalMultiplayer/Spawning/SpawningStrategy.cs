using UnityEngine;

namespace BT.LocalMultiplayer
{
    public abstract class SpawningStrategy : ScriptableObject
    {
        public abstract Vector3 GetSpawnPosition();
    }
}
