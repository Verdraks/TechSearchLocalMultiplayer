using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

public class InputReaderPlayerCreator : MonoBehaviour, IInputReader, InputActionPlayerCreator.IControllerActions
{
    [Header("Output")] 
    public UnityEvent onInputDraw;
    public UnityEvent<Vector2> onInputShoot;
    public UnityEvent<Vector2> onInputMove;
    
    private InputActionPlayerCreator _inputActionPlayer;
    private MouseWrapper mouseWrapper;

    public void EnableInputReader()
    {
        _inputActionPlayer.Enable();
        Cursor.visible = false;
    }

    public void DisableInputReader()
    {
        _inputActionPlayer.Disable();
        Cursor.visible = true;
    }

    public GameObject GetControlledGameObject()
    {
        return gameObject;
    }

    private void Update()
    {
        mouseWrapper.SetMousePosition(_inputActionPlayer.Controller.Move.ReadValue<Vector2>());
        onInputMove.Invoke(mouseWrapper.GetWorldMousePos());
    }

    void InputActionPlayerCreator.IControllerActions.OnShoot(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                onInputShoot.Invoke(mouseWrapper.GetWorldMousePos());
                break;
        }
    }

    void InputActionPlayerCreator.IControllerActions.OnDraw(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                onInputDraw.Invoke();
                break;
            case InputActionPhase.Canceled:
                onInputDraw.Invoke();
                break;
        }
    }

    void InputActionPlayerCreator.IControllerActions.OnMove(InputAction.CallbackContext context)
    {
    }

    void IInputReader.AssignDevice(InputDevice[] inputDevices)
    {
        _inputActionPlayer = new InputActionPlayerCreator();
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(inputDevices);
        _inputActionPlayer.Controller.SetCallbacks(this);
        
        switch (inputDevices[0])
        {
            case Gamepad:
                mouseWrapper = new MouseWrapperGamepad();
                break;
            default:
                mouseWrapper = new MouseWrapperDevice();
                break;
        }
    }
}