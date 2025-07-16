using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpAmount = 3f;
    bool isGrounded = true;
    Vector2 movement;
    float jump = 0f;
    Rigidbody2D rb;
    Vector2 respawnLocation = Vector2.zero;
    bool hasKey = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<float>();
    }

    void FixedUpdate()
    {
        if (isGrounded && jump != 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpAmount);
        }
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += movement.x * speed * Time.deltaTime;
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RespawnPoint")
        {
            respawnLocation = collision.transform.position;
        }

        if (collision.gameObject.tag == "Spike")
        {
            transform.position = respawnLocation;
        }

        if (collision.gameObject.tag == "Key")
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Door" && hasKey == true)
        {
            Debug.Log("Load next room");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
