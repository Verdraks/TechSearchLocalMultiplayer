using UnityEngine;
public class PlayerControllerPassive : MonoBehaviour
{
    public void Move(Vector2 targetPosition)
    {
        Debug.Log(message: targetPosition);
    }

    public void Shoot()
    {
        Debug.Log(message: "Shoot");
    }
}