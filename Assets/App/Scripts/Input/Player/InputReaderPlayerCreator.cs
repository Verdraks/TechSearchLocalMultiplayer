using JetBrains.Annotations;
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
    public UnityEvent<Vector2> onInputMove;
    
    private InputActionPlayerCreator _inputActionPlayer;

    public void EnableInputReader()
    {
        _inputActionPlayer.Controller.SetCallbacks(this);
        _inputActionPlayer.Enable();
        Cursor.visible = false;
    }

    public void DisableInputReader()
    {
        _inputActionPlayer.Controller.SetCallbacks(null);
        _inputActionPlayer.Disable();
        Cursor.visible = true;
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

    void InputActionPlayerCreator.IControllerActions.OnMove(InputAction.CallbackContext context)
    {
        if (context.phase is InputActionPhase.Canceled) return;

        if (context.control.device is Keyboard or Mouse)
            onInputMove.Invoke(Macro2D.MousePosWorld);
        else
        {
            Debug.Log("fesfesf");
        }
    }

    void IInputReader.AssignDevice(InputDevice[] inputDevices)
    {
        _inputActionPlayer = new InputActionPlayerCreator();
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(inputDevices);
    }
}
