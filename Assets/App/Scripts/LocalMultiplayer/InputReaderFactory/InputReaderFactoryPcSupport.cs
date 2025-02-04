using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "SSO_InputReaderFactoryPcSupport", menuName = "SSO/LocalMultiplayer/InputReaderFactory/InputReaderFactoryPcSupport")]
    public class InputReaderFactoryPcSupport : InputReaderFactory
    {
        public GameObject prefabKeyboard;
        public GameObject prefabMouse;

        public override GameObject CreateLocalMultiplayerInputReader(InputDevice device)
        {
            switch (device)
            {
                case Keyboard:
                    return Instantiate(prefabKeyboard);
                case Mouse:
                    return Instantiate(prefabMouse);
                default:
                    return null;
            }
        }
    }
}