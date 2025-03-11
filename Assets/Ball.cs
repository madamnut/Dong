using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // �߰�: �¸�/�й� �ؽ�Ʈ ����
    public GameObject winText;
    public GameObject loseText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LaunchBall();

        // ���� �� �ؽ�Ʈ ����
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    void LaunchBall()
    {
        Vector2[] directions =
        {
            new Vector2(1, 0.8f),
            new Vector2(1, -0.8f),
            new Vector2(-1, 0.8f),
            new Vector2(-1, -0.8f)
        };

        Vector2 initialDir = directions[Random.Range(0, directions.Length)].normalized;
        rb.velocity = initialDir * speed;
    }

    // Goal �浹 �� ���� ���� �߰�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            spriteRenderer.color = Color.red;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // ��� ��ġ�� ���� �Ǵ� (�����̸� �й�, �����̸� �¸�)
            if (collision.transform.position.x < 0)
                loseText.SetActive(true); // ���� ���̸� �й�
            else
                winText.SetActive(true);  // ������ ���̸� �¸�
        }
    }
}
