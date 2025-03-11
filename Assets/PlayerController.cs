using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private float direction;

    private bool canMoveUp = true;
    private bool canMoveDown = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Vertical");

        if (direction > 0 && !canMoveUp)
            direction = 0;

        if (direction < 0 && !canMoveDown)
            direction = 0;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + Vector2.up * direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (collision.transform.position.y > transform.position.y)
                canMoveUp = false;

            if (collision.transform.position.y < transform.position.y)
                canMoveDown = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("충돌 감지: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (collision.transform.position.y > transform.position.y)
                canMoveUp = true;

            if (collision.transform.position.y < transform.position.y)
                canMoveDown = true;
        }
    }
}
