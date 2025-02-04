using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "RSO_DevicesRegistered", menuName = "RSO/LocalMultiplayer/RSO_DevicesRegistered")]
public class RSO_DevicesRegistered : BT.ScriptablesObject.RuntimeScriptableObject<List<DeviceData>>{}

public struct DeviceData
{
    public InputDevice InputDevice;
    public int DeviceOrderAuthoring;
    public GameObject AuthoredGameObject;
}