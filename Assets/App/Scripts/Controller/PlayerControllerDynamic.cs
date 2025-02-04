using UnityEngine;
public class PlayerControllerDynamic : MonoBehaviour
{
    public void Move(Vector2 direction)
    {
        Debug.Log(message: direction);
    }

    public void Shoot()
    {
        Debug.Log(message: "Shoot");
    }
}