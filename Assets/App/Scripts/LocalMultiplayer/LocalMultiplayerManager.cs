using UnityEngine;

namespace BT.LocalMultiplayer
{
    public class LocalMultiplayerManager : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private RSE_DeviceReconnected rseDeviceReconnected;
        [SerializeField] private RSE_DeviceReconnected rseDeviceDisconnected;

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
            
        }

        private void DeviceReconnected(DeviceData deviceData)
        {
            
        }
        
        
    }
}
