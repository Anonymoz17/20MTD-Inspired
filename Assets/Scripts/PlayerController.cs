using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform body;
    public float playerSpeed = 5f;

    private Vector3 moveInput;
    private bool facingRight = true;

    void Update()
    {
        HandleInput();
        HandleFacing();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * playerSpeed;
    }

    void LateUpdate()
    {
        if (Camera.main != null && body != null)
        {
            Vector3 camDir = Camera.main.transform.forward;
            body.forward = camDir;
        }
    }

    private void HandleInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void HandleFacing()
    {
        if (moveInput.x > 0 && !facingRight) Flip();
        if (moveInput.x < 0 && facingRight) Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = body.localScale;
        scale.x *= -1;
        body.localScale = scale;
    }
}
