using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputReaderPlayerDestructor : MonoBehaviour, IInputReader,InputActionPlayerDestructor.IControllerActions
{
    
    [Header("Output")]
    public UnityEvent onInputShoot;
    
    private InputActionPlayerDestructor _inputActionPlayer;

    void IInputReader.EnableInputReader()
    {
        _inputActionPlayer ??= new InputActionPlayerDestructor();
        _inputActionPlayer.Controller.SetCallbacks(this);
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(new InputDevice[] {Keyboard.current });
        _inputActionPlayer.Enable();
    }

    void IInputReader.DisableInputReader()
    {
        _inputActionPlayer.Controller.SetCallbacks(null);
        _inputActionPlayer.Disable();
    }

    GameObject IInputReader.GetControlledGameObject()
    {
        return gameObject;
    }

    void InputActionPlayerDestructor.IControllerActions.OnShoot(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                onInputShoot.Invoke();
                break;
        }
    }
}
