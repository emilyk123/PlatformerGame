using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector2 movement;
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        Debug.Log(movement);
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += movement.x * speed * Time.deltaTime;
        transform.position = position;
    }
}
