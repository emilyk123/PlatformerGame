using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] float movement = 2f;
    [SerializeField] float flyingAmplitude = 2f;
    [SerializeField] float flyingWavelength = 0.5f;
    private int direction = -1;

    void Update()
    {
        transform.position += new Vector3(movement * direction * Time.deltaTime, Mathf.Sin(flyingWavelength * Time.time) * flyingAmplitude, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBorder")
        {
            direction *= -1;
        }
    }
}
