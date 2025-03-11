using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 7f;
    [HideInInspector] public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // 추가: 튕길 때 재생할 효과음
    public AudioClip bounceSound;
    public AudioClip PlayerScoredSound;
    public AudioClip AiScoredSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // AudioSource 컴포넌트 가져오기 또는 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        LaunchBall();
    }

    public void LaunchBall()
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
        rb.isKinematic = false;
        spriteRenderer.color = Color.white;
    }

    public void StopBall()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Goal 태그가 아닌 충돌 시 튕기는 효과음 재생
        if (!collision.gameObject.CompareTag("Goal"))
        {
            if (bounceSound != null)
            {
                audioSource.PlayOneShot(bounceSound);
            }
        }

        // Goal 태그와 충돌한 경우, GameManager를 통해 점수 처리
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (collision.transform.position.x < 0)
            {
                audioSource.PlayOneShot(AiScoredSound);
                GameManager.instance.ScorePoint(false); // 왼쪽 골대: 상대 점수
            }
            else
            {
                audioSource.PlayOneShot(PlayerScoredSound);
                GameManager.instance.ScorePoint(true);  // 오른쪽 골대: 플레이어 점수
            }
        }
    }
}
