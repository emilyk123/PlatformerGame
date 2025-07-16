using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    [SerializeField] float movement = 5f;
    private int direction = -1;

    void Update()
    {
        transform.position += new Vector3(movement * direction * Time.deltaTime, 0f, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBorder")
        {
            direction *= -1;
        }
    }

}
