using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerInputReaderMouse : MonoBehaviour, IInputReader, InputActionPlayer.IControllerActions
{
    [Header("Output")] 
    public UnityEvent<Vector2> onInputMove;
    public UnityEvent onInputShoot;
    
    private InputActionPlayer _inputActionPlayer;
    private Vector3 Position
    {
        get
        {
            var ray = Camera.main!.ScreenPointToRay(Mouse.current.position.ReadValue());
            return Physics.Raycast(ray, out RaycastHit hit) ? hit.point : Vector3.zero;
        }
    }

    public void EnableInputReader()
    {
        _inputActionPlayer ??= new InputActionPlayer();
        _inputActionPlayer.Controller.SetCallbacks(this);
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(new InputDevice[] {Mouse.current });
        _inputActionPlayer.Enable();
    }

    public void DisableInputReader()
    {
        _inputActionPlayer.Controller.SetCallbacks(null);
        _inputActionPlayer.Disable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                onInputMove.Invoke(Position);
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
