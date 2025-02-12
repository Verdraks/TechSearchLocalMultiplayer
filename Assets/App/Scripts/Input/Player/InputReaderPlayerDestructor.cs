using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputReaderPlayerDestructor : MonoBehaviour, IInputReader,InputActionPlayerDestructor.IControllerActions
{
    
    [Header("Output")]
    public UnityEvent<Vector2> onInputMove;
    public UnityEvent onInputJumpStarted;
    public UnityEvent onInputJumpPerformed;
    public UnityEvent onInputJumpCanceled;
    
    private InputActionPlayerDestructor _inputActionPlayer;

    void IInputReader.EnableInputReader()
    {
        _inputActionPlayer.Controller.SetCallbacks(this);
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

    void InputActionPlayerDestructor.IControllerActions.OnMove(InputAction.CallbackContext context)
    {
        onInputMove?.Invoke(context.ReadValue<Vector2>());
    }

    void InputActionPlayerDestructor.IControllerActions.OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                onInputJumpStarted?.Invoke();
                break;
            case InputActionPhase.Performed:
                onInputJumpPerformed?.Invoke();
                break;
            case InputActionPhase.Canceled:
                onInputJumpCanceled?.Invoke();
                break;
        }
    }

    void IInputReader.AssignDevice(InputDevice[] inputDevices)
    {
        _inputActionPlayer = new InputActionPlayerDestructor();
        _inputActionPlayer.devices = new ReadOnlyArray<InputDevice>(inputDevices);
    }
}
