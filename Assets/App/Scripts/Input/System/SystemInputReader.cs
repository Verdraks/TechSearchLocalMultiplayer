using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemInputReader : MonoBehaviour, IInputReader, InputActionSystem.IControllerActions
{
    [Header("Output")]
    [SerializeField] private RSE_InputSelect rseInputSelect;
    
    private InputActionSystem _inputActionSystem;
    private IInputReader inputReaderImplementation;

    void IInputReader.EnableInputReader()
    {
        _inputActionSystem.Controller.SetCallbacks(this);
        _inputActionSystem.Enable();
    }

    void IInputReader.DisableInputReader()
    {
        _inputActionSystem.Controller.SetCallbacks(null);
        _inputActionSystem.Disable();
    }

    void IInputReader.AssignDevice(InputDevice[] inputDevices)
    {
        _inputActionSystem = new InputActionSystem();
    }

    public GameObject GetControlledGameObject()
    {
        return gameObject;
    }

    void InputActionSystem.IControllerActions.OnSelect(InputAction.CallbackContext context)
    {
        rseInputSelect.Call(context.control.device);
    }

    private void Start()
    {
        ((IInputReader)this).EnableInputReader();
    }
}