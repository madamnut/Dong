using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform ball;       // Ball ������Ʈ ����
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
            direction = 1; // ���� �̵�
        else if (transform.position.y > ball.position.y + 0.1f && canMoveDown)
            direction = -1; // �Ʒ��� �̵�
        else
            direction = 0;

        Vector2 newPosition = rb.position + Vector2.up * direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    // �� �浹 ó��
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

    // ������ �������� �� �ٽ� �̵� ����
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
