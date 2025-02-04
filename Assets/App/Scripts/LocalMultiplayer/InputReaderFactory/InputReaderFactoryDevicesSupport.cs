using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_InputReaderFactoryDevicesSupport", menuName = "SSO/LocalMultiplayer/InputReaderFactory/InputReaderFactoryDevicesSupport")]
    public class InputReaderFactoryDevicesSupport : InputReaderFactory
    {
        public GameObject prefabKeyboard;
        public GameObject prefabGamepad;
        
        public override GameObject CreateLocalMultiplayerInputReader(InputDevice device)
        {
            switch (device)
            {
                case Keyboard:
                    return Instantiate(prefabKeyboard);
                case Mouse:
                    return Instantiate(prefabKeyboard);
                case Gamepad:
                    return Instantiate(prefabGamepad);
                default:
                    return null;
            }
        }
    }
}