using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BT.LocalMultiplayer
{
    public class InputReaderLifecycleManager : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LocalMultiplayerSettings localMultiplayerSettings;
        
        [Header("References")] 
        [SerializeField] private InputReaderFactory inputReaderFactory;
        [SerializeField] private SpawningStrategy spawningStrategy;
        [Space(10)]
        [SerializeField] private RSO_DevicesRegistered rsoDevicesRegistered;
        
        [Header("Input")]
        [SerializeField] private RSE_DeviceRegister rseDeviceRegister;
        
        [Header("Output")]
        [SerializeField] private RSE_DeviceDataCreated rseDeviceDataCreated;

        private void OnEnable() => rseDeviceRegister.action += TryAssignDeviceInputReader;
        private void OnDisable() => rseDeviceRegister.action -= TryAssignDeviceInputReader;

        private void TryAssignDeviceInputReader(InputDevice inputDevice)
        {
            if (rsoDevicesRegistered.Value.Count >= localMultiplayerSettings.maxPlayers) return;
            foreach (var deviceData in rsoDevicesRegistered.Value)
            {
                if (deviceData.InputDevice == inputDevice) return;
            }
            
            AssignDeviceInputReader(ref inputDevice);
        }
        
        private void AssignDeviceInputReader(ref InputDevice inputDevice)
        {
            var inputReaderInstantiated = inputReaderFactory.CreateLocalMultiplayerInputReader(inputDevice);
            if (!inputReaderInstantiated) return;
            
            
            inputReaderInstantiated.transform.position = spawningStrategy.GetSpawnPosition();
            
            var deviceData = new DeviceData
            {
                InputDevice = inputDevice,
                MonitoredInputReader = inputReaderInstantiated.GetComponent<IInputReader>()
            };

            //Sorry is dirty
            deviceData.MonitoredInputReader.AssignDevice(inputDevice is Keyboard
                ? new[] { inputDevice, Mouse.current }
                : new[] { inputDevice });

            rsoDevicesRegistered.Value.Add(deviceData);
            rseDeviceDataCreated.Call(deviceData);
        }
        
    }
}
