using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // 추가: 승리/패배 텍스트 참조
    public GameObject winText;
    public GameObject loseText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LaunchBall();

        // 시작 시 텍스트 숨김
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

    // Goal 충돌 시 승패 판정 추가
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            spriteRenderer.color = Color.red;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            // 골대 위치로 승패 판단 (좌측이면 패배, 우측이면 승리)
            if (collision.transform.position.x < 0)
                loseText.SetActive(true); // 왼쪽 벽이면 패배
            else
                winText.SetActive(true);  // 오른쪽 벽이면 승리
        }
    }
}
