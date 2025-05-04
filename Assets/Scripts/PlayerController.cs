using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Transform body;
    public float playerSpeed = 5f;

    private Vector2 moveInput;
    private bool facingRight = true;

    private float lastHorizontalInput = 0f;
    private float lastVerticalInput = 0f;

    void Update()
    {
        HandleHorizontalInput();
        HandleVerticalInput();

        moveInput = new Vector2(lastHorizontalInput, lastVerticalInput).normalized;

        if (lastHorizontalInput != 0)
            Flip(lastHorizontalInput);
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = moveInput * playerSpeed;
    }

    private void HandleHorizontalInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            lastHorizontalInput = -1;
        if (Input.GetKeyDown(KeyCode.D))
            lastHorizontalInput = 1;

        bool aHeld = Input.GetKey(KeyCode.A);
        bool dHeld = Input.GetKey(KeyCode.D);

        if (aHeld && !dHeld)
            lastHorizontalInput = -1;
        else if (dHeld && !aHeld)
            lastHorizontalInput = 1;
        else if (!aHeld && !dHeld)
            lastHorizontalInput = 0;
        // if both are held, keep lastHorizontalInput as-is (priority logic)
    }

    private void HandleVerticalInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
            lastVerticalInput = -1;
        if (Input.GetKeyDown(KeyCode.W))
            lastVerticalInput = 1;

        bool sHeld = Input.GetKey(KeyCode.S);
        bool wHeld = Input.GetKey(KeyCode.W);

        if (sHeld && !wHeld)
            lastVerticalInput = -1;
        else if (wHeld && !sHeld)
            lastVerticalInput = 1;
        else if (!sHeld && !wHeld)
            lastVerticalInput = 0;
        // if both are held, keep lastVerticalInput as-is
    }

    private void Flip(float directionX)
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
