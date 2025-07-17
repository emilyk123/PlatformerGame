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
    [SerializeField] GameObject spawnLocation;
    Vector2 respawnLocation = new Vector2(-8, -0.25f);
    bool hasKey = false;
    int coinCount = 0;
    int health = 3;

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
        if (health <= 0)
        {
            Debug.Log("Died");
            transform.position = spawnLocation.transform.position;
            respawnLocation = spawnLocation.transform.position;
            health = 3;
        }
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
            health--;
            Debug.Log("Health: " + health);
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

        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinCount++;
            Debug.Log("Coin Count: " + coinCount);
        }
        
        if (collision.gameObject.tag == "GroundEnemy")
        {
            Debug.Log("Hit Ground Enemy");
            health--;
            Debug.Log("Health: " + health);
            transform.position = respawnLocation;
        }

        if (collision.gameObject.tag == "FlyingEnemy")
        {
            Debug.Log("Hit Flying Enemy");
            health--;
            Debug.Log("Health: " + health);
            transform.position = respawnLocation;
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
