using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "RSE_DeviceReconnected", menuName = "RSE/LocalMultiplayer/RSE_DeviceReconnected")]
    public class RSE_DeviceReconnected : BT.ScriptablesObject.RuntimeScriptableEvent<DeviceData>{}
}
