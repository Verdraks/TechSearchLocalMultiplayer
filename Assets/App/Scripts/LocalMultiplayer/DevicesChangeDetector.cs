using UnityEngine;
using UnityEngine.InputSystem;

namespace BT.LocalMultiplayer
{
    public class DevicesChangeDetector : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] RSO_DevicesRegistered rsoDeviceRegistered;
        
        [Header("Output")]
        [SerializeField] private RSE_DeviceReconnected rseDeviceReconnected;
        [SerializeField] private RSE_DeviceDisconnected rseDeviceDisconnected;
        
        private void OnEnable() => InputSystem.onDeviceChange += DeviceChangeCheck;
        private void OnDisable() => InputSystem.onDeviceChange -= DeviceChangeCheck;

        private void DeviceChangeCheck(InputDevice device, InputDeviceChange deviceChange)
        {
            if (!DeviceExistInGame(device, out var index)) return;

            switch (deviceChange)
            {
                case InputDeviceChange.Reconnected:
                    rseDeviceReconnected.Call(rsoDeviceRegistered.Value[index]);
                    break;
                case InputDeviceChange.Disconnected:
                    rseDeviceDisconnected.Call(rsoDeviceRegistered.Value[index]);
                    break;
            }
        }

        private bool DeviceExistInGame(InputDevice device, out int index)
        {
            index = 0;
            for (var i = 0; i < rsoDeviceRegistered.Value.Count; i++)
            {
                index = i;
                if (rsoDeviceRegistered.Value[i].InputDevice == device) break;
                if (i == rsoDeviceRegistered.Value.Count -1) return false;
            }

            return true;
        }
    }
}
