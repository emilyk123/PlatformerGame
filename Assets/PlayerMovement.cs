using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector2 movement;
    float jump = 0f;
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump = context.ReadValue<float>();
        Debug.Log(jump);
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += movement.x * speed * Time.deltaTime;
        transform.position = position;
    }
}
