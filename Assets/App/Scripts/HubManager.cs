using System;
using System.Threading.Tasks;
using BT.LocalMultiplayer;
using UnityEngine;
public class HubManager : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private string levelStartName = "Level_0";
    [SerializeField] private string nextLevelName = "Level_1";
    
    [Header("References")]
    [SerializeField] private RSO_DevicesRegistered rsoDeviceDataRegistered;
    
    [Header("Input")]
    [SerializeField] private RSE_DeviceDataCreated rseDeviceDataCreated;
    
    [Header("Output")]
    [SerializeField] private RSE_LoadSceneAdditive rseLoadSceneAdditive;

    private void Start()
    {
        rseLoadSceneAdditive.Call(levelStartName);
    }

    private void OnEnable() => rseDeviceDataCreated.action += CheckEnoughPlayerLoadLevel;
    private void OnDisable() => rseDeviceDataCreated.action -= CheckEnoughPlayerLoadLevel;


    private async void CheckEnoughPlayerLoadLevel(DeviceData deviceData)
    {
        await Task.Delay(100);
        if (rsoDeviceDataRegistered.ActivePlayerCount >= PartyManager.PlayerCountToStart)
        {
            rseLoadSceneAdditive.Call(nextLevelName);
            enabled = false;
        }
    }
    
}