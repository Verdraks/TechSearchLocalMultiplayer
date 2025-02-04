using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace BT.LocalMultiplayer
{
    public class LocalMultiplayerLifecycle : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private RSO_DevicesRegistered rsoDeviceRegistered;
        
        [Header("Input")]
        [SerializeField] private RSE_ClearDevicesRegistered rseClearDevicesRegistered;

        private void OnEnable() => rseClearDevicesRegistered.action += ClearDevicesRegistered;
        private void OnDisable() => rseClearDevicesRegistered.action -= ClearDevicesRegistered;
        
        private void Awake() => rsoDeviceRegistered.Value ??= new List<DeviceData>();
        private void OnApplicationQuit() => rsoDeviceRegistered.Value = null;

        private void ClearDevicesRegistered()
        {
            if (rsoDeviceRegistered.Value != null)
            {
                foreach (var deviceData in rsoDeviceRegistered.Value)
                {
                    var controlledGameObject = deviceData.MonitoredInputReader.GetControlledGameObject();
                    if (controlledGameObject) Destroy(controlledGameObject);
                }
                rsoDeviceRegistered.Value.Clear();
            }
            else rsoDeviceRegistered.Value = new List<DeviceData>();
        }
    
    }
}
