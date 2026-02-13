using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 16f;

    private Rigidbody2D body;
    private PlayerInput playerInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        // Read the WASD composite we made
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        
        // Apply horizontal speed, keep gravity's vertical speed
        body.linearVelocity = new Vector2(input.x * speed, body.linearVelocity.y);
    }

    private void Update()
    {
        // Jump when Space is pressed
        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            // Only jump if we aren't already falling/jumping (Ground Check)
            if (Mathf.Abs(body.linearVelocity.y) < 0.01f)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            }
        }
    }
}