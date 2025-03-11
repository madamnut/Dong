using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform ball;       // Ball 오브젝트 참조
    public float speed = 5f;

    private Rigidbody2D rb;
    private float direction;

    private bool canMoveUp = true;
    private bool canMoveDown = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float direction = 0;

        if (transform.position.y < ball.position.y - 0.1f && canMoveUp)
            direction = 1; // 위로 이동
        else if (transform.position.y > ball.position.y + 0.1f && canMoveDown)
            direction = -1; // 아래로 이동
        else
            direction = 0;

        Vector2 newPosition = rb.position + Vector2.up * direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    // 벽 충돌 처리
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

    // 벽에서 떨어졌을 때 다시 이동 가능
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (collision.transform.position.y > transform.position.y)
                canMoveUp = true;

            if (collision.transform.position.y < transform.position.y)
                canMoveDown = true;
        }
    }
}
