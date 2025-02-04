using UnityEngine;

namespace BT.LocalMultiplayer
{
    public abstract class SplitScreenFactory : ScriptableObject
    {
        public abstract Camera CreateSplitScreenCamera();
    }
}