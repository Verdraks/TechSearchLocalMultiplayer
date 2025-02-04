using UnityEngine;

public interface IInputReader
{
    void EnableInputReader();
    void DisableInputReader();
    GameObject GetControlledGameObject();
}
