using UnityEngine;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_SplitScreenFactoryDefault", menuName = "SSO/LocalMultiplayer/SplitScreenFactory/SplitScreenFactoryDefault")]
    public class SplitScreenFactoryDefault : SplitScreenFactory
    {
        public GameObject splitScreenCameraPrefab;
        
        public override Camera CreateSplitScreenCamera()
        {
            return Instantiate(splitScreenCameraPrefab).GetComponent<Camera>();
        }
    }
}