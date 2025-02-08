using BT.LocalMultiplayer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RSO_DevicesRegistered rsoDeviceRegistered;
    
    [Header("Input")]
    [SerializeField] private RSE_InputSelect rseInputSelect;
    
    [Header("Output")]
    [SerializeField] private RSE_DeviceRegister rseDeviceRegister;
    
    private bool _partyStarted;
    
    public const int PlayerCountToStart = 2;

    private void OnEnable() => rseInputSelect.action += TryAddNewPlayer;
    private void OnDisable() => rseInputSelect.action -= TryAddNewPlayer;

    private void TryAddNewPlayer(InputDevice inputDevice)
    {
        if (_partyStarted) return;
        rseDeviceRegister.Call(inputDevice);
        if (rsoDeviceRegistered.ActivePlayerCount >= PlayerCountToStart)
        {
            _partyStarted = true;
            rseInputSelect.action -= TryAddNewPlayer;
            foreach (var deviceData in rsoDeviceRegistered.Value)
            {
                deviceData.MonitoredInputReader.EnableInputReader();
            }
        }
    }
    
}