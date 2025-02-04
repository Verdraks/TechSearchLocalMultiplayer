using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

public class PlayerInputReaderKeyboard : MonoBehaviour, IInputReader,InputActionPlayer.IControllerActions
{
    
    [Header("Output")] 
    public UnityEvent<Vector2> onInputMove;
    public UnityEvent onInputShoot;
    
    private InputActionPlayer _inputActionPlayer;
    private Vector2 Direction => _inputActionPlayer.Controller.Move.ReadValue<Vector2>();

    public void EnableInputReader()
    {
        _inputActionPlayer ??= new InputActionPlayer();
        _inputActionPlayer.Controller.SetCallbacks(this);
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(new InputDevice[] {Keyboard.current });
        _inputActionPlayer.Enable();
    }

    public void DisableInputReader()
    {
        _inputActionPlayer.Controller.SetCallbacks(null);
        _inputActionPlayer.Disable();
    }

    public GameObject GetControlledGameObject()
    {
        return gameObject;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                onInputMove.Invoke(Direction);
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                onInputShoot.Invoke();
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }
}
