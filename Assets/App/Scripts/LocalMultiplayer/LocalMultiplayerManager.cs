using UnityEngine;

namespace BT.LocalMultiplayer
{
    public class LocalMultiplayerManager : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private RSE_DeviceReconnected rseDeviceReconnected;
        [SerializeField] private RSE_DeviceDisconnected rseDeviceDisconnected;
        
        private void OnEnable()
        {
            rseDeviceReconnected.action += DeviceReconnected;
            rseDeviceDisconnected.action += DeviceDisconnected;
        }

        private void OnDisable()
        {
            rseDeviceReconnected.action -= DeviceReconnected;
            rseDeviceDisconnected.action -= DeviceDisconnected;
        }


        private void DeviceDisconnected(DeviceData deviceData)
        {
            deviceData.MonitoredInputReader.GetControlledGameObject().SetActive(false);
            deviceData.MonitoredInputReader.DisableInputReader();
        }

        private void DeviceReconnected(DeviceData deviceData)
        {
            deviceData.MonitoredInputReader.AssignDevice(deviceData.InputDevice);
            deviceData.MonitoredInputReader.EnableInputReader();
            deviceData.MonitoredInputReader.GetControlledGameObject().SetActive(true);
        }
    }
}
