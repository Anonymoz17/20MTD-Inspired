using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb2d;
    public Transform body; // Reference to the Body GameObject
    private Vector2 moveInput;
    private bool facingRight = true;

    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(xInput, yInput).normalized;

        // Flip based on horizontal input
        if (xInput != 0)
            Flip(xInput);
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = moveInput * speed;
    }

    void Flip(float directionX)
    {
        bool shouldFaceRight = directionX > 0;
        if (shouldFaceRight != facingRight)
        {
            facingRight = shouldFaceRight;

            Vector3 scale = body.localScale;
            scale.x *= -1;
            body.localScale = scale;
        }
    }
}
