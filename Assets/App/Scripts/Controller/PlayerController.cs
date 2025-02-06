using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float dashForce;
    [SerializeField] private float smoothMove = 0.1f;
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    private int directionDash;
    private Vector2 directionMove;

    private void FixedUpdate()
    {
        var targetVelocity = directionMove * moveSpeed;
        rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y);
    }
    
    public void Move(Vector2 direction)
    {
        directionDash = direction.x == 0 ? directionDash : (int)direction.normalized.x;
        directionMove = new Vector2(direction.normalized.x ,0);
    }

    public void Dash()
    {
        rb.AddForce(new Vector2(directionDash*dashForce, 0f), ForceMode2D.Impulse);
    }
}