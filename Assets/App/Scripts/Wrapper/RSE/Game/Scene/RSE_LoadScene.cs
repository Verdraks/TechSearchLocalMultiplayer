using UnityEngine;

/// <summary>
/// Runtime Scriptable Event to load a scene.
/// Param 1: Scene Name.
/// Param 2: Scene load in additive.
/// </summary>
[CreateAssetMenu(fileName = "RSE_LoadScene", menuName = "RSE/Scene/RSE_LoadScene")]
public class RSE_LoadScene : BT.ScriptablesObject.RuntimeScriptableEvent<string>{}