using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputReader
{
    void EnableInputReader();
    void DisableInputReader();
    
    void AssignDevice(InputDevice[] device = null);
    
    GameObject GetControlledGameObject();
}