using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerKeyboardPrefab;
    [SerializeField] private GameObject playerMousePrefab;
    [Space(10)]
    [SerializeField] private RSO_DevicesRegistered rsoDevicesRegistered;
    
    [Header("Input")]
    [SerializeField] private RSE_InputSelect rseInputSelect;

    private void Awake() => rsoDevicesRegistered.Value = new List<DeviceData>();
    private void OnDestroy() => rsoDevicesRegistered.Value = null;

    private void OnEnable() => rseInputSelect.action += Spawn;
    private void OnDisable() => rseInputSelect.action -= Spawn;
    
    private void Spawn(InputDevice device)
    {
        foreach (var deviceData in rsoDevicesRegistered.Value)
        {
            if (deviceData.InputDevice == device) return;
        }

        GameObject authoredGameObject;
        
        switch (device)
        {
            case Keyboard:
                authoredGameObject = Instantiate(playerKeyboardPrefab);
                break;
            case Mouse:
                authoredGameObject = Instantiate(playerMousePrefab);
                break;
            default:
                Debug.LogWarning("Input device format not supported");
                return;
        }
        
        rsoDevicesRegistered.Value.Add(new DeviceData
        {
            InputDevice = device,
            AuthoredGameObject = authoredGameObject
        });
    }
    
}