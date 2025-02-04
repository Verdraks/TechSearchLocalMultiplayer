using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    [CreateAssetMenu(fileName = "RSO_DevicesRegistered", menuName = "RSO/LocalMultiplayer/RSO_DevicesRegistered")]
    public class RSO_DevicesRegistered : BT.ScriptablesObject.RuntimeScriptableObject<List<DeviceData>>
    {
        public int ActivePlayerCount
        {
            get
            {
                const int count = 0;
                return Value?.Count(deviceData => deviceData.MonitoredInputReader?.GetControlledGameObject() != null) ?? count;
            }
        }
    }

    public struct DeviceData
    {
        public InputDevice InputDevice;
        public int DeviceOrderMonitor;
        public IInputReader MonitoredInputReader;
    }
}
