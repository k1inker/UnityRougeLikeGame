using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(StatsControl))]
public class BaseMovement : MonoBehaviour
{
    private StatsControl _movementSpeed;
    private Rigidbody2D _rb;

    internal bool facingRight = true;

    void Start()
    {
        _movementSpeed = GetComponent<StatsControl>();
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction)
    {
        _rb.velocity = direction * _movementSpeed.velocity;
    }
    public void Turn(float directionX)
    {
        if (directionX > 0 && !facingRight)
            Flip();
        else if (directionX < 0 && facingRight)
            Flip();
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
}
