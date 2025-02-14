using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private RSE_ClearDevicesRegistered rseClearDevicesRegistered;
        
        [Header("Output")]
        [SerializeField] private RSE_DeviceDataCreated rseDeviceDataCreated;

        private void Awake() => rsoDevicesRegistered.Value = new List<DeviceData>();
        private void OnApplicationQuit() => rsoDevicesRegistered.Value = null;
        
        private void OnEnable()
        {
            rseDeviceRegister.action += TryAssignDeviceInputReader;
            rseClearDevicesRegistered.action += ClearDevicesRegistered;
        }

        private void OnDisable()
        {
            rseDeviceRegister.action -= TryAssignDeviceInputReader;
            rseClearDevicesRegistered.action -= ClearDevicesRegistered;
        }

        private void TryAssignDeviceInputReader(InputDevice inputDevice)
        {
            if (rsoDevicesRegistered.Value.Count >= localMultiplayerSettings.maxPlayers) return;
            foreach (var deviceData in rsoDevicesRegistered.Value)
            {
                if (deviceData.InputDevice[0] == inputDevice) return;
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
                InputDevice = inputDevice is Keyboard
                ? new[] { inputDevice, Mouse.current }
                : new[] { inputDevice },
                MonitoredInputReader = inputReaderInstantiated.GetComponent<IInputReader>()
            };
            
            deviceData.MonitoredInputReader.AssignDevice(deviceData.InputDevice);

            rsoDevicesRegistered.Value.Add(deviceData);
            rseDeviceDataCreated.Call(deviceData);
        }
        
        private void ClearDevicesRegistered()
        {
            if (rsoDevicesRegistered.Value != null)
            {
                foreach (var deviceData in rsoDevicesRegistered.Value)
                {
                    var controlledGameObject = deviceData.MonitoredInputReader.GetControlledGameObject();
                    if (controlledGameObject) Destroy(controlledGameObject);
                }
                rsoDevicesRegistered.Value.Clear();
            }
            else rsoDevicesRegistered.Value = new List<DeviceData>();
        }
        
    }
}
