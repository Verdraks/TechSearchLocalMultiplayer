using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    public abstract class InputReaderFactory : ScriptableObject
    {
        public abstract GameObject CreateLocalMultiplayerInputReader(InputDevice device);
    }
}