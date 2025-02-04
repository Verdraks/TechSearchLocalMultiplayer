using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "RSE_DeviceRegister", menuName = "RSE/LocalMultiplayer/RSE_DeviceRegister")]
    public class RSE_DeviceRegister : BT.ScriptablesObject.RuntimeScriptableEvent<InputDevice>{}
}