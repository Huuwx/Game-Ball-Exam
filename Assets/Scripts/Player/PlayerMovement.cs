using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;

    private InputAction move;
    private InputAction jump;

    private Rigidbody2D rb;

    public float moveSpeed = 150f;
    public float jumpForce = 15f;
    public float pushForce = 120f;
    public float rotaionSpeed = 180f;
    public Vector2 input;

    public bool isWalled = false;
    public bool isGrounded = false;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move = playerInput.Player.Move;
        move.Enable();

        jump = playerInput.Player.Jump;
        jump.Enable();

        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }

    void Update()
    {
        input = move.ReadValue<Vector2>();
        if (input.x != 0 && isGrounded)
        {
            ParicalSystemController.Instance.movementPartical.Play();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(input.x * moveSpeed * Time.deltaTime, rb.velocity.y);
        transform.Rotate(0, 0, rotaionSpeed * -input.x * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            ParicalSystemController.Instance.FallPartical.Play();
            SoundController.Instance.PlayOneShot(SoundController.Instance.land);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalled = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
            Rigidbody2D boxRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (boxRb != null)
            {
                if (isWalled)
                {
                    boxRb.velocity = new Vector2(-input.x * pushForce * Time.deltaTime, boxRb.velocity.y);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            isWalled = false;
        }
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        if (isGrounded)
        {
            SoundController.Instance.PlayOneShot(SoundController.Instance.jump);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
