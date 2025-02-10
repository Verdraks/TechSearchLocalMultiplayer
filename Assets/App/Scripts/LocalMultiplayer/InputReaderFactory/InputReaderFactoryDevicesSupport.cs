using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_InputReaderFactoryDevicesSupport", menuName = "SSO/LocalMultiplayer/InputReaderFactory/InputReaderFactoryDevicesSupport")]
    public class InputReaderFactoryDevicesSupport : InputReaderFactory
    {
        public GameObject prefabMouse;
        public GameObject prefabOtherDevice;
        
        public override GameObject CreateLocalMultiplayerInputReader(InputDevice device)
        {
            switch (device)
            {
                case Mouse:
                    return Instantiate(prefabMouse);
                case Keyboard:
                    return Instantiate(prefabOtherDevice);
                case Gamepad:
                    return Instantiate(prefabOtherDevice);
                default:
                    return null;
            }
        }
    }
}