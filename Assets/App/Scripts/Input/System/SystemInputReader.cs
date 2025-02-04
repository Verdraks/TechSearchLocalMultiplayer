using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemInputReader : MonoBehaviour, IInputReader, InputActionSystem.IControllerActions
{
    [Header("Output")]
    [SerializeField] private RSE_InputSelect rseInputSelect;
    
    private InputActionSystem _inputActionSystem;

    void IInputReader.EnableInputReader()
    {
        _inputActionSystem ??= new InputActionSystem();
        _inputActionSystem.Controller.SetCallbacks(this);
        _inputActionSystem.Enable();
    }

    void IInputReader.DisableInputReader()
    {
        _inputActionSystem.Controller.SetCallbacks(null);
        _inputActionSystem.Disable();
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