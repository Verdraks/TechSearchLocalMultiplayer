using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

public class InputReaderPlayerCreator : MonoBehaviour, IInputReader, InputActionPlayerCreator.IControllerActions
{
    [Header("Output")] 
    public UnityEvent onInputDraw;
    public UnityEvent<Vector2> onInputShoot;
    
    private InputActionPlayerCreator _inputActionPlayer;

    public void EnableInputReader()
    {
        _inputActionPlayer ??= new InputActionPlayerCreator();
        _inputActionPlayer.Controller.SetCallbacks(this);
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(new InputDevice[] {Mouse.current });
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

    void InputActionPlayerCreator.IControllerActions.OnShoot(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                onInputShoot.Invoke(Macro2D.MousePosWorld);
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
}
